import { useEffect, useState } from "react";
import * as React from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import OptionBoard from "./OptionList";
import { createContext } from "react";
import SendEmailPopupForm from "../../EmailInformRemind/SendEmailPopupForm";
import { reserveContext } from "../../../../pages/ReservedListPage";
import SortIcon from '@mui/icons-material/Sort';
import a from "../../../../../src/assets/LogoStManagement/logo.png"
import { InboxOutlined } from '@ant-design/icons';
import tableStyle from "./TableListStyle.module.scss"

// Create a context
export const context = createContext(null);
const baseUrl = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;
const token = localStorage.getItem("token");
const headers = {
	Authorization: `Bearer ${token}`,
};
function TableList({ dataListStudent, setDataListStudent, setopen, setTotalItem, dataListStudentForExport, setDataListStudentForExport }) {
	const context_ = React.useContext(reserveContext);
	const [row, setRow] = useState({});
	const [open_, setOpen] = useState(null);
	const [selectedStudentId, setSelectedStudentId] = useState(null);
	const [reservedClassId, setReservedClassId] = useState(null);
	const [classAvailable, setClassAvailable] = useState(null);
	const handleRemoveStudent = (reservedStudentIdToRemove) => {

		// Filter out the student with the provided studentIdToRemove
		const updatedDataListStudent = dataListStudent.filter((student) => student.reservedClassId !== reservedStudentIdToRemove);
		setDataListStudent(updatedDataListStudent);
		const updatedDataListStudentForExport = dataListStudentForExport.filter((student) => student.reservedClassId !== reservedStudentIdToRemove);
		setDataListStudentForExport(updatedDataListStudentForExport)
		// const getList = () => {
		// 	return fetch(`${baseUrl}/ReservedStudent/GetAllFromElastic/${rowsPerPage}/${currentPage}`, { headers: headers }).then((data) => data.json());
		// };
		// getList().then((items) => {
		// 	setDataListStudent(items.studentReservedList);
		// 	setTotalItem(items.itemCount);
		// });
	};
	const [email, setEmail] = useState(null);
	const [studentid, setStudentId] = useState(null);
	const [formatSort, setFormatSort] = useState("asc");

	const handleOptionClick = (row, studentId, reserveClassId, classEndDate) => {
		// Setting information here
		setEmail(row.email);
		setStudentId(row.studentId);

		// Setting other factors
		setRow(row);
		setSelectedStudentId(studentId);
		setReservedClassId(reserveClassId);
		var parts = classEndDate.split("/");
		var day = parseInt(parts[0], 10);
		var month = parseInt(parts[1], 10) - 1; // Subtract 1 from the month since months in JavaScript are zero-based
		var year = parseInt(parts[2], 10);
		var today = new Date();
		var classEndDateConvert = new Date(year, month, day);
		if (classEndDateConvert < today) {
			setClassAvailable(false);
		} else {
			setClassAvailable(true);
		}
	};

	function parseDate(dateString) {
		const parts = dateString.split('/');
		// Assuming date format is dd/mm/yyyy
		const formattedDate = `${parts[2]}-${parts[1]}-${parts[0]}`;
		return new Date(formattedDate);
	}

	const handleSort = (e, field) => {
		switch (field) {
			case "FullName":
				if (formatSort == "asc") {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						return b.studentName.localeCompare(a.studentName);
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						return b.studentName.localeCompare(a.studentName);
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("desc")
				} else {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						return a.studentName.localeCompare(b.studentName);
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						return a.studentName.localeCompare(b.studentName);
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("asc")
				}
				break;
			case "StudentCode":
				if (formatSort == "asc") {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						return b.mutatableStudentId.localeCompare(a.mutatableStudentId);
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						return b.mutatableStudentId.localeCompare(a.mutatableStudentId);
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("desc")
				} else {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						return a.mutatableStudentId.localeCompare(b.mutatableStudentId);
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						return a.mutatableStudentId.localeCompare(b.mutatableStudentId);
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("asc")
				}
				break;
			case "Gender":
				if (formatSort == "asc") {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						return b.gender.localeCompare(a.gender);
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						return b.gender.localeCompare(a.gender);
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("desc")
				} else {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						return a.gender.localeCompare(b.gender);
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						return a.gender.localeCompare(b.gender);
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("asc")
				}
				break;
			case "Birthday":
				if (formatSort == "asc") {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						const dateA = parseDate(a.dob);
						const dateB = parseDate(b.dob);
						return dateA - dateB;
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						const dateA = parseDate(a.dob);
						const dateB = parseDate(b.dob);
						return dateA - dateB;
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("desc")
				} else {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						const dateA = parseDate(a.dob);
						const dateB = parseDate(b.dob);
						return dateB - dateA;
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						const dateA = parseDate(a.dob);
						const dateB = parseDate(b.dob);
						return dateB - dateA;
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("asc")
				}
				break;
			case "Hometown":
				if (formatSort == "asc") {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						return b.address.localeCompare(a.address);
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						return b.address.localeCompare(a.address);
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("desc")
				} else {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						return a.address.localeCompare(b.address);
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						return a.address.localeCompare(b.address);
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("asc")
				}
				break;
			case "ClassName":
				if (formatSort == "asc") {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						return b.className.localeCompare(a.className);
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						return b.className.localeCompare(a.className);
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("desc")
				} else {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						return a.className.localeCompare(b.className);
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						return a.className.localeCompare(b.className);
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("asc")
				}
				break;
			case "ReservedModule":
				if (formatSort == "asc") {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						return b.moduleName.localeCompare(a.moduleName);
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						return b.moduleName.localeCompare(a.moduleName);
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("desc")
				} else {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						return a.moduleName.localeCompare(b.moduleName);
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						return a.moduleName.localeCompare(b.moduleName);
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("asc")
				}
				break;
			case "Reason":
				if (formatSort == "asc") {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						return b.reason.localeCompare(a.reason);
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						return b.reason.localeCompare(a.reason);
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("desc")
				} else {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						return a.reason.localeCompare(b.reason);
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						return a.reason.localeCompare(b.reason);
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("asc")
				}
				break;
			case "StartDate":
				if (formatSort == "asc") {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						const dateA = parseDate(a.startDate);
						const dateB = parseDate(b.startDate);
						return dateA - dateB;
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						const dateA = parseDate(a.startDate);
						const dateB = parseDate(b.startDate);
						return dateA - dateB;
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("desc")
				} else {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						const dateA = parseDate(a.startDate);
						const dateB = parseDate(b.startDate);
						return dateB - dateA;
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						const dateA = parseDate(a.startDate);
						const dateB = parseDate(b.startDate);
						return dateB - dateA;
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("asc")
				}
				break;
			case "EndDate":
				if (formatSort == "asc") {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						const dateA = parseDate(a.endDate);
						const dateB = parseDate(b.endDate);
						return dateA - dateB;
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						const dateA = parseDate(a.endDate);
						const dateB = parseDate(b.endDate);
						return dateA - dateB;
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("desc")
				} else {
					const sortedDataListStudent = [...dataListStudent].sort((a, b) => {
						const dateA = parseDate(a.endDate);
						const dateB = parseDate(b.endDate);
						return dateB - dateA;
					});
					setDataListStudent(sortedDataListStudent);

					const sortedDataListStudentForExport = [...dataListStudentForExport].sort((a, b) => {
						const dateA = parseDate(a.endDate);
						const dateB = parseDate(b.endDate);
						return dateB - dateA;
					});
					setDataListStudentForExport(sortedDataListStudentForExport);

					setFormatSort("asc")
				}
				break;
		}


	}

	return (
		<context.Provider
			value={{
				closePopup: () => setSelectedStudentId(null),
				setEmailPopup: setOpen,
				setEmail: setEmail,
				setStudentId: setStudentId,
			}}
		>
			<div style={{ position: "relative", overflowX: "auto", overflowY: "auto", height: "120%", boxShadow: "1px 1px 1px 1px rgba(0, 0, 0, 0.3)", borderRadius: "10px", width: "100%" }}>
				<TableContainer style={{ minWidth: "100%", width: "max-content", overflowX: "hidden", marginBottom: "1%", width: "max-content", borderRadius: "10px", boxShadow: "1px 1px 1px 1px rgba(0, 0, 0, 0.3)" }} component={Paper}>
					<Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
						<TableHead style={{ backgroundColor: "#2D3748" }}>
							<TableRow>
								<TableCell onClick={e => handleSort(e, "FullName")} style={{ color: "#FFFFFF" }}>FullName <SortIcon /></TableCell>
								<TableCell onClick={e => handleSort(e, "StudentCode")} style={{ color: "#FFFFFF", width: "10%" }}>Student Code <SortIcon /></TableCell>
								<TableCell onClick={e => handleSort(e, "Gender")} style={{ color: "#FFFFFF" }}>Gender <SortIcon /></TableCell>
								<TableCell onClick={e => handleSort(e, "Birthday")} style={{ color: "#FFFFFF" }}>Birthday <SortIcon /></TableCell>
								<TableCell onClick={e => handleSort(e, "Hometown")} style={{ color: "#FFFFFF" }}>Hometown <SortIcon /></TableCell>
								<TableCell onClick={e => handleSort(e, "ClassName")} style={{ color: "#FFFFFF", width: "10%" }}>Class Name <SortIcon /></TableCell>
								<TableCell onClick={e => handleSort(e, "ReservedModule")} style={{ color: "#FFFFFF", width: "12%" }}>Reserved Module <SortIcon /></TableCell>
								<TableCell onClick={e => handleSort(e, "Reason")} style={{ color: "#FFFFFF" }}>Reason <SortIcon /></TableCell>
								<TableCell onClick={e => handleSort(e, "StartDate")} style={{ color: "#FFFFFF" }}>Start Date <SortIcon /></TableCell>
								<TableCell onClick={e => handleSort(e, "EndDate")} style={{ color: "#FFFFFF" }}>End Date <SortIcon /></TableCell>
								<TableCell style={{ color: "#FFFFFF" }}>Status <SortIcon /></TableCell>
								<TableCell style={{ color: "#FFFFFF" }}></TableCell>
							</TableRow>
						</TableHead>
						<TableBody>
							{dataListStudent.length === 0 ? (
								<TableRow>
									<TableCell style={{opacity: 0.5}} colSpan={9} align="center">
										<InboxOutlined className={tableStyle.noDataIcon} />
										<p>No data</p>
									</TableCell>
								</TableRow>
							) : (
								// Render additional rows dynamically based on dataListStudent
								dataListStudent &&
								dataListStudent.map((row) => (
									<TableRow key={row.reservedClassId} sx={{ "&:last-child td, &:last-child th": { border: 0 } }}>
										<TableCell style={{ fontWeight: "650", color: "#2D3748" }} component="th" scope="row">
											{row.studentName}
										</TableCell>
										<TableCell style={{ width: "10%" }}>{row.mutatableStudentId}</TableCell>
										<TableCell>{row.gender}</TableCell>
										<TableCell>{row.dob}</TableCell>
										<TableCell>{row.address}</TableCell>
										<TableCell style={{ width: "10%" }}>{row.className}</TableCell>
										<TableCell style={{ width: "12%" }}>{row.moduleName}</TableCell>
										<TableCell>{row.reason}</TableCell>
										<TableCell>{row.startDate}</TableCell>
										<TableCell>{row.endDate}</TableCell>
										<TableCell style={{ width: "8%" }} ><p style={{ color: "white", backgroundColor: "#FEA501", borderRadius: "20px", padding: "10% 20%", }}>Reserve</p></TableCell>
										<TableCell>
											<svg
												xmlns="http://www.w3.org/2000/svg"
												width="16"
												height="16"
												fill="currentColor"
												className="bi bi-three-dots"
												viewBox="0 0 16 16"
												onClick={() => {
													handleOptionClick(row, row.studentId, row.reservedClassId, row.classEndDate);
												}}
											>
												<path d="M3 9.5a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3m5 0a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3m5 0a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3" />
											</svg>
										</TableCell>
									</TableRow>
								))
							)}
						</TableBody>
					</Table>
				</TableContainer>
				{selectedStudentId && (
					<>
						<div
							style={{
								position: "fixed",
								top: 0,
								left: 0,
								width: "100%",
								height: "100%",
								backgroundColor: "rgba(0, 0, 0, 0.5)", // Semi-transparent black
								zIndex: 9999, // Ensure it's above everything else
							}}
							onClick={() => setSelectedStudentId(null)} // Close the OptionBoard when overlay is clicked
						/>
						<div
							style={{
								position: "fixed",
								top: "40%",
								left: "50%",
								transform: "translate(-10%, -10%)",
								zIndex: 10000, // Ensure it's above the overlay
								width: "50%",
							}}
						>
							<OptionBoard
								studentId={selectedStudentId}
								reservedClassId={reservedClassId}
								onClose={() => {
									setSelectedStudentId(null);
								}}
								onRemoveSuccess={(reservedStudentIdToRemove) => handleRemoveStudent(reservedStudentIdToRemove)} // Pass the callback function
								checkClassAvailable={classAvailable}
								setDataListStudent={setDataListStudent}
								setTotalItem={setTotalItem}
							/>
						</div>
					</>
				)}
			</div>
			<SendEmailPopupForm
				setOpen={setOpen}
				open={open_}
				title={"My title"} // Grab from API
				sendto={email}
				studentid={studentid}
			/>
		</context.Provider>
	);
}

export default TableList;
