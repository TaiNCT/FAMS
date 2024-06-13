import React, { useState, useEffect } from "react";
import { Button, Row, Form, InputGroup } from "react-bootstrap";
import Navbar from "../../../layouts/Navbar";
import Sidebar from "../../../layouts/Sidebar";
import style from "../../../../pages/DashboardPage/style.module.scss";
import styles from "./style.module.scss";
import Footer from "../../../layouts/Footer";
import { useNavigate } from "react-router-dom";
import { FormGroup, FormControlLabel, Switch } from "@mui/material";
import HighlightOffIcon from "@mui/icons-material/HighlightOff";
import { debounce } from "lodash";
import MenuItem from "@mui/material/MenuItem";
import Select from "@mui/material/Select";
import { ToastContainer, toast } from "react-toastify";

const token = localStorage.getItem("token");
const headers = {
	Authorization: `Bearer ${token}`,
};

const baseUrl = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;

function AddNewStudentReserve(onClose) {
	const navigate = useNavigate();
	const handleNavigateClick = () => {
		navigate("/reservation-management");
	};
	const [studentData, setStudentData] = useState(null);
	const [inputStudent, setInputStudent] = useState("");
	const [fromDate, setFromDate] = useState("");
	const [toDate, setToDate] = useState("");
	const [reason, setReason] = useState("");
	const [classId, setClassId] = useState("");
	const [classCode, setClassCode] = useState("");
	const [classNameList, setClassNameList] = useState([]);
	const [currentModule, setCurrentModule] = useState("");
	const [stuId, setStuId] = useState("");
	const [classListBySearchStuEmail, setClassListBySearchStuEmail] = useState([]);
	const [selectedClassNameValue, setselectedClassNameValue] = useState("");
	const [minStartDate, setMinStartDate] = useState(new Date().toISOString().split("T")[0]);
	const [checkStudent, setCheckStudent] = useState(false);

	const tomorrow = new Date();
	tomorrow.setDate(tomorrow.getDate() + 1); // Lấy ngày mai
	const minEndDate = tomorrow.toISOString().split("T")[0];

	const [validated, setValidated] = useState(false);

	const [checkboxes, setCheckboxes] = useState({
		checkbox1: false,
		checkbox2: false,
		checkbox3: false,
		checkbox4: false,
		checkbox5: false,
		checkbox6: false,
	});
	const handleCheckboxChange = (name) => {
		setCheckboxes((prevState) => ({
			...prevState,
			[name]: !prevState[name],
		}));
	};
	useEffect(() => {
		// Check if all checkboxes are true
		const allChecked = Object.values(checkboxes).every((checkbox) => checkbox);
		setIsSubmitDisabled(!allChecked);
	}, [checkboxes]);

	const handleSearchStudentEmail = debounce((e) => {
		let term = e.target.value;
		if (term != "") {
			const getList = () => {
				return fetch(`${baseUrl}/ReservedStudent/SearchStudent?studentIdORemail=${e.target.value}`, { headers: headers }).then((response) => {
					if (!response.ok) {
						// Handle the "Not Found" scenario or any other error
						setStuId("");
						setCheckStudent(true);
						setClassListBySearchStuEmail([]);
						setClassId("");
						setClassCode("");
						setCurrentModule("");
						throw new Error(`Request failed with status: ${response.status}`);
					}
					setCheckStudent(false);
					return response.json();
				});
			};
			getList().then((items) => {
				setClassListBySearchStuEmail(items);
			});
		} else {
			setCheckStudent(false);
		}
	}, 200);

	const handleSelectClassName = (e) => {
		const newValue = e.target.value;
		setselectedClassNameValue(newValue);
		const selectedClassObject = classListBySearchStuEmail.find((item) => item.classId === newValue);
		if (selectedClassObject) {
			setClassId(selectedClassObject.classId);
			setClassCode(selectedClassObject.classCode);
			setCurrentModule(selectedClassObject.moduleName);
			setStuId(classListBySearchStuEmail[0].studentId);
		}
	};
	const [isSubmitDisabled, setIsSubmitDisabled] = useState(true);

	const [error, setError] = useState("");
	let handleSubmit = async (event) => {
		const form = event.currentTarget;
		if (form.checkValidity() === false) {
			event.preventDefault();
			event.stopPropagation();
		}

		setValidated(true);
		event.preventDefault();

		// Check if fromDate is greater than toDate
		if (fromDate && toDate && new Date(fromDate) >= new Date(toDate)) {
			setError("Start date must be earlier than End date");
			return;
		}

		const fromDateObj = new Date(fromDate);
		const toDateObj = new Date(toDate);

		const sixMonthsInMillis = 6 * 30 * 24 * 60 * 60 * 1000;
		if (toDateObj.getTime() - fromDateObj.getTime() >= sixMonthsInMillis) {
			setError("The period from StartDate to EndDate cannot exceed 6 months");
			return;
		}
		await fetch(`${baseUrl}/ReservedStudent/InsertStudent?studentId=${stuId}&classId=${classId}&reason=${reason}&startDate=${fromDate}&endDate=${toDate}`, {
			method: "POST",
			headers: {
				"Content-Type": "application/json",
				Authorization: `Bearer ${localStorage.getItem("token")}`,
			},
			body: JSON.stringify({
				studentId: stuId,
				classId: classId,
				reason: reason,
				startDate: fromDate,
				endDate: toDate,
			}),
		})
			.then((res) => {
				if (res.ok) {
					// return new Promise((resolve, reject) => {
					//     resolve(res.json());
					setStuId("");
					setClassId("");
					setClassCode("");
					setCurrentModule("");
					setReason("");
					setFromDate("");
					setToDate("");
					setCheckboxes({
						checkbox1: false,
						checkbox2: false,
						checkbox3: false,
						checkbox4: false,
						checkbox5: false,
						checkbox6: false,
					});
					setInputStudent("");
					setClassListBySearchStuEmail([]);
					toast.success("Add student successful");
					// });
				} else {
					setStuId("");
					setClassId("");
					setClassCode("");
					setCurrentModule("");
					setReason("");
					setFromDate("");
					setToDate("");
					setCheckboxes({
						checkbox1: false,
						checkbox2: false,
						checkbox3: false,
						checkbox4: false,
						checkbox5: false,
						checkbox6: false,
					});
					setInputStudent("");
					setClassListBySearchStuEmail([]);
					toast.error("Something wrong");
				}
			})
			.catch((error) => {
				toast.error("Something wrong");
			});
	};

	return (
		<div style={{ display: "flex", justifyContent: "center", marginTop: "2em", marginBottom: "2em" }}>
			<div style={{ backgroundColor: "white", borderRadius: "10px", boxShadow: "0px 0px 10px rgba(0, 0, 0, 0.1)", width: "60%" }}>
				{/* Header */}
				<div className={styles.header}>
					<div className={styles.headerContent}>
						<p className={styles.headerTitle}>Add Reserving</p>
						<HighlightOffIcon onClick={handleNavigateClick} className={styles.cancelButton} />
					</div>
				</div>
				<Form onSubmit={handleSubmit} className="mt-5" style={{ padding: "20px" }}>
					<Form.Group className="mb-2">
						<Row>
							<Form.Label className={styles.form_label}>Select Student</Form.Label>
						</Row>
						<Row>
							<Form.Control
								required
								className={styles.form_control}
								id="studentInput"
								placeholder="Enter email"
								value={inputStudent}
								onChange={(e) => {
									handleSearchStudentEmail(e), setInputStudent(e.target.value);
								}} // Add onChange event handler
							/>
						</Row>
						{checkStudent && inputStudent.length !== 0 ? (
							<Row>
								<p className={styles.className_message}>Student not found</p>
							</Row>
						) : null}
					</Form.Group>
					<Form.Group className="mb-5">
						<Row>
							<Form.Label className={styles.form_label}>Class name</Form.Label>
						</Row>
						{/* <Row>
                            <Form.Control className={styles.form_control} value={className} id="className" disabled />

                        </Row> */}
						<Row>
							<Select required className={styles.selectBox} value={selectedClassNameValue} onChange={handleSelectClassName} labelId="demo-simple-select-label" id="demo-simple-select" disabled={checkStudent && inputStudent.length !== 0}>
								{classListBySearchStuEmail &&
									classListBySearchStuEmail.map((item) => (
										<MenuItem key={item.classId} value={item.classId}>
											{item.className}
										</MenuItem>
									))}
							</Select>
						</Row>
					</Form.Group>
					<Form.Group className="mb-5">
						<Row>
							<Form.Label className={styles.form_label}>Class code</Form.Label>
						</Row>
						<Row>
							<Form.Control className={styles.form_control} value={classCode} id="classCode" disabled />
						</Row>
					</Form.Group>
					<Form.Group className="mb-5">
						<Row>
							<Form.Label className={styles.form_label}>Current modules</Form.Label>
						</Row>
						<Row>
							<Form.Control className={styles.form_control} value={currentModule} id="currentModule" disabled />
						</Row>
					</Form.Group>
					<Form.Group className="mb-5">
						<Row>
							<Form.Label className={styles.form_label}>Reserving reason</Form.Label>
						</Row>
						<Row>
							<Form.Control className={styles.form_control} value={reason} onChange={(e) => setReason(e.target.value)} placeholder="Enter reason" required />
						</Row>
					</Form.Group>
					<Form.Group className="mb-5">
						<Row>
							<Form.Label className={styles.form_label}>Reserving period</Form.Label>
						</Row>
						<Row style={{ display: "flex", justifyContent: "space-between" }}>
							<Form.Control type="date" className="me-2" placeholder="From" value={fromDate} min={minStartDate} onChange={(e) => setFromDate(e.target.value)} required />
							<Form.Control type="date" placeholder="To" value={toDate} min={minEndDate} onChange={(e) => setToDate(e.target.value)} required />
						</Row>
					</Form.Group>
					{/* Line */}
					<hr className="my-4" style={{ borderTop: "2px solid black" }} />

					{/* End of line */}

					<Form.Group className="my-3">
						<Form.Label className={styles.form_label}>Reserving Condition</Form.Label>
					</Form.Group>
					<Form.Group style={{ display: "flex" }} className="my-3">
						<Form.Check type="checkbox" checked={checkboxes.checkbox1} onChange={() => handleCheckboxChange("checkbox1")} />
						<Form.Check.Label onClick={() => handleCheckboxChange("checkbox1")} className="ms-3">
							Complete tuition payment
						</Form.Check.Label>
					</Form.Group>
					<Form.Group style={{ display: "flex" }} className="my-3">
						<Form.Check type="checkbox" checked={checkboxes.checkbox2} onChange={() => handleCheckboxChange("checkbox2")} />
						<Form.Check.Label onClick={() => handleCheckboxChange("checkbox2")} className="ms-3">
							Ensure the course has not progressed beyond 50%
						</Form.Check.Label>
					</Form.Group>
					<Form.Group style={{ display: "flex" }} className="my-3">
						<Form.Check type="checkbox" checked={checkboxes.checkbox3} onChange={() => handleCheckboxChange("checkbox3")} />
						<Form.Check.Label onClick={() => handleCheckboxChange("checkbox3")} className="ms-3">
							Determine retention fee payment
						</Form.Check.Label>
					</Form.Group>
					<Form.Group style={{ display: "flex" }} className="my-3">
						<Form.Check type="checkbox" checked={checkboxes.checkbox4} onChange={() => handleCheckboxChange("checkbox4")} />
						<Form.Check.Label onClick={() => handleCheckboxChange("checkbox4")} className="ms-3">
							Perfom one-time retention check
						</Form.Check.Label>
					</Form.Group>
					<Form.Group style={{ display: "flex" }} className="my-3">
						<Form.Check type="checkbox" checked={checkboxes.checkbox5} onChange={() => handleCheckboxChange("checkbox5")} />
						<Form.Check.Label onClick={() => handleCheckboxChange("checkbox5")} className="ms-3">
							Identify the concluding module
						</Form.Check.Label>
					</Form.Group>
					<FormGroup className="my-3">
						<FormControlLabel control={<Switch />} label="Active Reserving" id="activeReservingSwitch" checked={checkboxes.checkbox6} onChange={() => handleCheckboxChange("checkbox6")} />
					</FormGroup>

					{error && <p style={{ color: "red" }}>{error}</p>}
					<Form.Group style={{ display: "flex", justifyContent: "end", marginTop: "5px" }}>
						<Button
							style={{
								color: "red",
								fontWeight: "bold",
								textDecoration: "underline",
								marginRight: "10px",
							}}
							onClick={handleNavigateClick}
						>
							Cancel
						</Button>
						<Button
							type="submit"
							disabled={isSubmitDisabled}
							style={{
								marginLeft: "10px",
								padding: "5px 15px",
								backgroundColor: isSubmitDisabled ? "#ccc" : "#2D3748",
								color: "white",
								border: "none",
								borderRadius: "5px",
								cursor: "pointer",
								transition: "background-color 0.3s ease",
							}}
						>
							Create
						</Button>
					</Form.Group>
				</Form>
			</div>
		</div>
	);
}

export default AddNewStudentReserve;
