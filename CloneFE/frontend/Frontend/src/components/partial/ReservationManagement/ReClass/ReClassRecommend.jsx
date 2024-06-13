import React from "react";
import { useState, useEffect } from "react";
import HighlightOffIcon from "@mui/icons-material/HighlightOff";
import styles from "./ReClassRecommendStyle.module.scss";
import { GetNextModuleList } from "../API/listApi";
import { useNavigate } from "react-router-dom";
import { message } from "antd";
import { GetReClassPossibilies } from "../API/listApi";
const token = localStorage.getItem("token");
const headers = {
	Authorization: `Bearer ${token}`,
};

// @ts-ignore
const backend_api = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;

function ReClassRecommend({ classInfo, isOpen, onClose, reservedClassId, onPopUpSuccessfully, setReClassList }) {
	if (!isOpen) {
		return null;
	} else {
		const navigate = useNavigate();

		const handleCancelClick = () => {
			navigate("/reservation-management");
		};

		const [nextModuleList, setNextModuleList] = useState([]);
		useEffect(() => {
			const fetchDataApi = async () => {
				const getNextModuleList = await GetNextModuleList(reservedClassId, classInfo.classId);
				setNextModuleList(getNextModuleList);
			};
			fetchDataApi();
		}, []);

		const handleBackToClass = () => {
			const url = `${backend_api}/ReClass/BackToClass/${reservedClassId}/${classInfo.classId}`;

			fetch(url, {
				method: "POST",
				headers: {
					"content-type": "application/json; charset=UTF-8",
					Authorization: `Bearer ${localStorage.getItem("token")}`,
				},
			}).then((res) => {
				if (res.ok) {
					const fetchDataApi = async () => {
						const getReClassPosibilities = await GetReClassPossibilies(reservedClassId);
						setReClassList(getReClassPosibilities);
					};
					fetchDataApi();

					onClose();
					const message = "ReClassSuccessful";
					navigate(`/reclass/${reservedClassId}/${message}`);
				}
			});
		};

		return (
			<div className={styles.container}>
				<div className={styles.headerContent}>
					<p className={styles.headerTitle}>Reserving details</p>
					<HighlightOffIcon className={styles.cancelButton} onClick={onClose} />
				</div>
				<div>
					<p className={styles.partTitle}>Class information</p>

					<div className={styles.classItem}>
						<p className={styles.smallPartTitle}>Class Name</p>
						<input type="text" value={classInfo?.className} />
					</div>

					<div className={styles.classItem}>
						<p className={styles.smallPartTitle}>Class Code</p>
						<input type="text" value={classInfo?.classCode} />
					</div>

					<div className={styles.classItem}>
						<p className={styles.smallPartTitle}>Class Module</p>
						<input type="text" value={classInfo?.moduleName} />
					</div>

					<div className={styles.classItem}>
						<p className={styles.smallPartTitle}>Next module</p>

						{nextModuleList &&
							nextModuleList.map((i, index) => {
								return (
									<label>
										<input type="radio" name="class" key={index} /> {i.moduleName} ({i.className}) - {i.startDate}
									</label>
								);
							})}
					</div>

					<div className={styles.btn}>
						<button onClick={handleCancelClick} className={styles.btnCancel}>
							Cancel
						</button>
						<button onClick={handleBackToClass} className={styles.btnBackToClass}>
							Back to class
						</button>
					</div>
				</div>
			</div>
		);
	}
}

export default ReClassRecommend;
