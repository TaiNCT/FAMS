import { useState } from "react";
import * as React from "react";
import TextField from "@mui/material/TextField";
import CloseIcon from "@mui/icons-material/Close";
import { debounce } from "lodash";
import style from "./SearchInput.module.scss";
import FilterListIcon from "@mui/icons-material/FilterList";
import { Link } from "react-router-dom";
import { useEffect } from "react";
import DeleteOutlineIcon from "@mui/icons-material/DeleteOutline";
import DownloadIcon from '@mui/icons-material/Download';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import { toast } from "react-toastify";

const backend_api = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;

const token = localStorage.getItem("token");
const headers = {
	Authorization: `Bearer ${token}`,
};
const baseUrl = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;

function Search({ dataListStudent, setDataListStudent, rowsPerPage, currentPage, setCurrentPage, setTotalItem, isSearching, setIsSearching, setValueSearch, totalItem, conditionSearch, setConditionSearch, dataListStudentForExport,setDataListStudentForExport, setIsAdvancedSearch }) {
	const [condition, setCondition] = useState([]);
	const [valueCondition, setValueCondition] = useState("");

	// useEffect(() => {
	// 	if (condition.length == 0) {
	// 		const getList = () => {
	// 			return fetch(`${baseUrl}/ReservedStudent/GetAllFromElastic/${rowsPerPage}/${currentPage}`, { headers: headers }).then((data) => data.json());
	// 		};
	// 		getList().then((items) => {
	// 			setDataListStudent(items.studentReservedList);
	// 			setTotalItem(items.itemCount);
	// 		});
	// 	}
	// }, [condition]);

	/*auto search khi input*/
	const handleSearch = debounce((e) => {
		let term = e.target.value;
		setValueSearch(term);
		setCurrentPage(1);
		if (term != "") {
			setIsSearching(true);
		} else {
			setIsSearching(false);
		}
		// if (term != "") {
		// 	setIsSearching(true);
		// 	const getList = () => {
		// 		return fetch(`${baseUrl}/ReservedStudent/SearchStudentsByElasticSearch/${e.target.value}/${rowsPerPage}/${currentPage}`, { headers: headers }).then((data) => data.json());
		// 	};
		// 	getList().then((items) => {
		// 		setDataListStudent(items.studentReservedList);
		// 		setTotalItem(items.itemCount);
		// 	});
		// } else {
		// 	setIsSearching(false);
		// 	const getList = () => {
		// 		return fetch(`${baseUrl}/ReservedStudent/GetAllFromElastic/${rowsPerPage}/${currentPage}`, { headers: headers }).then((data) => data.json());
		// 	};
		// 	getList().then((items) => {
		// 		setDataListStudent(items.studentReservedList);
		// 		setTotalItem(items.itemCount);
		// 	});
		// }
	}, 800);

	const handleChange = (e) => {
		setValueCondition(e.target.value);
	};

	const handleInputCondition = (e) => {
		e.preventDefault();
		let term = document.searchForm.searchBox.value;
		if (term != "") {
			if (condition.length <= 3) {
				setCondition([...condition, valueCondition]);
			}
			setValueCondition("");
			setIsSearching(false);
		}
	};

	const removeCondition = (i) => {
		if (condition.length != 0) {
			const array = condition.filter((c) => c != i);
			setCondition(array);
		} else {
		}
	};

	const removeAllCondition = () => {
		if (condition.length != 0) {
			setCondition([]);
			const getList = () => {
				return fetch(`${baseUrl}/ReservedStudent/GetAllFromElastic/${rowsPerPage}/${currentPage}`, { headers: headers }).then((data) => data.json());
			};
			getList().then((items) => {
				setDataListStudent(items.studentReservedList);
				setTotalItem(items.itemCount);
				setIsAdvancedSearch(false)
			});

			const getListExport = () => {
				return fetch(`${baseUrl}/ReservedStudent/GetAllFromElasticForExport`, { headers: headers }).then((data) => data.json());
			};
			getListExport().then((items) => {
				setDataListStudentForExport(items);
			});
		}
	};

	/*auto search khi dùng advanced search*/
	const searchByConditions = (e) => {
		if (condition.length > 0) {
			setCurrentPage(1);
			setConditionSearch(condition);
			const queryString = condition.map((keyword) => `keywords=${encodeURIComponent(keyword)}`).join("&");

			const getList = () => {
				return fetch(`${baseUrl}/ReservedStudent/AdvancedSearch/${rowsPerPage}/${currentPage}?${queryString}`, { headers: headers }).then((data) => data.json());
			};
			getList().then((items) => {
				setDataListStudent(items.studentReservedList);
				setTotalItem(items.itemCount);
				setIsAdvancedSearch(true)
			});

			const getListExport = () => {
				return fetch(`${baseUrl}/ReservedStudent/AdvancedSearchForExport?${queryString}`, { headers: headers }).then((data) => data.json());
			};
			getListExport().then((items) => {
				setDataListStudentForExport(items);
			});
		}
	};

	async function handleExport(e, dataListStudentForExport) {

		if(dataListStudentForExport == null){
			return;
		}

		const url = `${baseUrl}/ReservedStudent/ExportReservedStudent`;
		const request = {
			method: "POST",
			headers: {
			  "Content-Type": "application/json",
			  "Authorization": `Bearer ${localStorage.getItem("token")}`
			},
			body: JSON.stringify(dataListStudentForExport),
		  };
	  
		await fetch(url, request)
			.then(response => {
				if (!response.ok) {
					throw new Error('Network response was not ok');
				}
				return response.blob(); // Extract the response body as a Blob
			})
			.then(blob => {
				// Create a URL for the Blob object
				const url = window.URL.createObjectURL(blob);

				// Create a temporary anchor element
				const a = document.createElement('a');
				a.href = url;
				a.download = 'reservedList.xlsx'; // Specify the filename

				// Append the anchor element to the document body
				document.body.appendChild(a);

				// Programmatically trigger a click event to start the download
				a.click();

				// Clean up: Remove the anchor element and revoke the URL
				document.body.removeChild(a);
				window.URL.revokeObjectURL(url);
				toast.success("Export successfully");
			})
			.catch(error => {
				console.error('Error exporting reserved students:', error);
				toast.error("Export unsuccessfully");
			});
	}

	return (
		<>
			<div className={style.searchItems} style={{ display: "flex" }}>
				<form name="searchForm" onSubmit={(e) => handleInputCondition(e)}>
					<TextField
						name="searchBox"
						id="outlined-basic"
						placeholder="Search by..."
						variant="outlined"
						value={valueCondition}
						onChange={(e) => {
							handleSearch(e);
							handleChange(e);
						}}
						InputProps={{
							style: { fontStyle: "italic" },
						}}
					/>
				</form>
				<button className={style.advancedSearchBtn} onClick={e => searchByConditions(e)}>
					<FilterListIcon style={{ marginRight: 10, verticalAlign: "middle" }} />
					Advanced Search
				</button>
				<button
					onClick={e => handleExport(e, dataListStudentForExport)}
					style={{
						width: 100,
						height: 36,
						backgroundColor: "#D07038",
						color: "white",
						fontSize: 14,
						borderRadius: 8,
						border: "none",
						textTransform: "math-auto",
						fontWeight: "600",
						alignItemsL: "right",
						marginLeft: "42%"
					}}
				>
					<DownloadIcon /> Export
				</button>

				<button
					style={{
						width: 100,
						height: 36,
						backgroundColor: "#2D3748",
						color: "white",
						fontSize: 14,
						borderRadius: 8,
						border: "none",
						textTransform: "math-auto",
						fontWeight: "600",
						alignItemsL: "right"
					}}
				>
					<Link to="/addnewreserve"><AddCircleOutlineIcon style={{marginRight: '0.3em'}} /> Add New</Link>
				</button>
			</div>
			<div style={{ display: "flex" }}>
				{condition &&
					condition.length > 0 &&
					condition.map((i) => (
						<div
							style={{
								background: "grey",
								width: "max-content",
								height: 30,
								fontSize: 20,
								margin: 10,
								paddingRight: 20,
								paddingLeft: 20,
								borderRadius: 10,
								textAlign: "center",
								display: "flex",
								alignItems: "center",
								gap: "5px",
							}}
						>
							<div>{i}</div>
							<div>
								<CloseIcon onClick={() => removeCondition(i)} style={{ fontSize: 12 }} />
							</div>
						</div>
					))}
				{condition.length > 0 ? <DeleteOutlineIcon onClick={removeAllCondition} style={{ fontSize: "2em", marginTop: "0.25em", color: "red" }} /> : null}
			</div>
		</>
	);
}

export default Search;
