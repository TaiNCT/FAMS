import { useEffect, useState } from "react";
import * as React from "react";
import TableList from "./TableList";
import Search from "./Search";
import Navbar from "../../../layouts/Navbar";
import Sidebar from "../../../layouts/Sidebar";
import style from "../../../../pages/DashboardPage/style.module.scss";
import Footer from "../../../layouts/Footer";
import { Pagination } from "antd";
import { ToastContainer, toast, Bounce } from "react-toastify";
import { useParams, useNavigate, useLocation } from "react-router-dom";

// Comment this out later... or get rid of it if you want
const token = localStorage.getItem("token");
const headers = {
	Authorization: `Bearer ${token}`,
};

const baseUrl = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;

function ViewListStudent() {
	const [listStudent, setListStudent] = useState([]);
	const [currentPage, setCurrentPage] = useState(1);
	const [totalItem, setTotalItem] = useState(0);
	const [rowsPerPage, setRowsPerPage] = useState(5);
	const [isSearching, setIsSearching] = useState(false);
	const [valueSearch, setValueSearch] = useState("");
	const [conditionSearch, setConditionSearch] = useState([]);
	const [dataListStudentForExport, setDataListStudentForExport] = useState([])
	const [isAdvancedSearch, setIsAdvancedSearch] = useState(false);

	const onChangePage = (page) => {
		setCurrentPage(page);
	};

	useEffect(() => {
		if (!isSearching && !isAdvancedSearch) {
			const getList = () => {
				return fetch(`${baseUrl}/ReservedStudent/GetAllFromElastic/${rowsPerPage}/${currentPage}`, { headers: headers }).then((data) => data.json());
			};
			getList().then((items) => {
				setListStudent(items.studentReservedList);
				setTotalItem(items.itemCount);
			});


			const getListExport = () => {
				return fetch(`${baseUrl}/ReservedStudent/GetAllFromElasticForExport`, { headers: headers }).then((data) => data.json());
			};
			getListExport().then((items) => {
				setDataListStudentForExport(items);
			});

		} else {
			if (valueSearch != "" && !isAdvancedSearch) {
				const getList = () => {
					return fetch(`${baseUrl}/ReservedStudent/SearchStudentsByElasticSearch/${valueSearch}/${rowsPerPage}/${currentPage}`, { headers: headers }).then((data) => data.json());
				};
				getList().then((items) => {
					setListStudent(items.studentReservedList);
					setTotalItem(items.itemCount);
				});


				const getListExport = () => {
					return fetch(`${baseUrl}/ReservedStudent/SearchStudentsByElasticSearchForExport/${valueSearch}`, { headers: headers }).then((data) => data.json());
				};
				getListExport().then((items) => {
					setDataListStudentForExport(items);
				});
			} else {
				if (!isAdvancedSearch) {
					const getList = () => {
						return fetch(`${baseUrl}/ReservedStudent/GetAllFromElastic/${rowsPerPage}/${currentPage}`, { headers: headers }).then((data) => data.json());
					};
					getList().then((items) => {
						setListStudent(items.studentReservedList);
						setTotalItem(items.itemCount);
					});

					const getListExport = () => {
						return fetch(`${baseUrl}/ReservedStudent/GetAllFromElasticForExport`, { headers: headers }).then((data) => data.json());
					};
					getListExport().then((items) => {
						setDataListStudentForExport(items);
					});
				}

			}
		}
	}, [currentPage, isSearching, totalItem]);

	// useEffect(() => {
	// 	if (conditionSearch.length > 0) {
	// 		const queryString = conditionSearch.map((keyword) => `keywords=${encodeURIComponent(keyword)}`).join("&");

	// 		const getList = () => {
	// 			return fetch(`${baseUrl}/ReservedStudent/AdvancedSearch/${rowsPerPage}/${currentPage}?${queryString}`, { headers: headers }).then((data) => data.json());
	// 		};
	// 		getList().then((items) => {
	// 			setListStudent(items.studentReservedList);
	// 			setTotalItem(items.itemCount);
	// 		});
	// 	}
	// }, [conditionSearch]);

	const { message } = useParams();
	const navigate = useNavigate();

	useEffect(() => {
		if (message === "Remove successful") {
			toast.success("Remove successful");
			navigate(`/reservation-management`);
		} else if (message === "Drop out successful") {
			toast.success("Drop out successful");
			navigate(`/reservation-management`);
		} else if (message === "Something wrong") {
			toast.error("Something wrong");
			navigate(`/reservation-management`);
		}
	}, [message]);

	return (
		<div style={{ height: "max-content", marginTop: "2%" }} id={style.reservation_view}>

			<Search
				dataListStudent={listStudent}
				setDataListStudent={setListStudent}
				rowsPerPage={rowsPerPage}
				currentPage={currentPage}
				setCurrentPage={setCurrentPage}
				totalItem={totalItem}
				setTotalItem={setTotalItem}
				isSearching={isSearching}
				setIsSearching={setIsSearching}
				setValueSearch={setValueSearch}
				conditionSearch={conditionSearch}
				setConditionSearch={setConditionSearch}
				dataListStudentForExport={dataListStudentForExport}
				setDataListStudentForExport={setDataListStudentForExport}
				setIsAdvancedSearch={setIsAdvancedSearch} />

			<div className="mt-5">
				<TableList dataListStudent={listStudent} setDataListStudent={setListStudent} setTotalItem={setTotalItem} dataListStudentForExport={dataListStudentForExport} setDataListStudentForExport={setDataListStudentForExport} />
				<Pagination current={currentPage} onChange={onChangePage} total={totalItem} showTotal={(total, range) => `${range[0]}-${range[1]} of ${total} students`} defaultPageSize={rowsPerPage} showSizeChanger={false} style={{ marginTop: "1%", marginLeft: "37%" }} />
			</div>
		</div>
	);
}

export default ViewListStudent;
