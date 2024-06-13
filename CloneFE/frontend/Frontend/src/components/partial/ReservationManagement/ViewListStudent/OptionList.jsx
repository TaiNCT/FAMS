import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import RemoveFailed from "./RemoveFailed";
import RemoveSuccess from "./RemoveSuccess";
import { useContext } from "react";
import { reserveContext } from "../../../../pages/ReservedListPage";
import { context as Tablecontext  } from "./TableList";

const baseUrl = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;
const token = localStorage.getItem("token");
const headers = {
	Authorization: `Bearer ${token}`,
};


function OptionsBoard({ onClose, studentId, reservedClassId, onRemoveSuccess, onSelectOption, checkClassAvailable, setDataListStudent, setTotalItem }) {
	const context = useContext(reserveContext);
	const tablecontext = useContext(Tablecontext);
	const setopen = context.setopen;

	const navigate = useNavigate();
	const handleReClassClick = () => {
		navigate(`/reclass/${reservedClassId}/null`);
	};
	const [showConfirmationRemove, setShowConfirmationRemove] = useState(false);

	const [selectedStudentId, setSelectedStudentId] = useState(studentId);


	const [showConfirmationDropOut, setShowConfirmationDropOut] = useState(false);

	const [isRemoveSuccess, setIsRemoveSuccess] = useState(false); // Add state variable for remove success

	const optionStyle = {
		display: "flex",
		alignItems: "center",
		padding: "10px",
		cursor: "pointer",
		fontSize: "16px",
		position: "relative",
	};

	const optionStyleFalse = {
		display: "flex",
		alignItems: "center",
		padding: "10px",
		fontSize: "16px",
		position: "relative",
		opacity: "0.5",
	};

	const iconStyle = {
		marginRight: "12px",
		width: "24px",
		height: "24px",
	};
	const removeButton = {
		display: "flex",
		justifyContent: "right",
	};
	const handleRemoveClick = () => {
		setShowConfirmationRemove(true);
	};
	const handleDropOutClick = () => {
		setShowConfirmationDropOut(true);
	};

	const handleRemoveConfirmation = (confirmed) => {
		if (confirmed) {
			const url = `${baseUrl}/ReservedStudent/UpdateReserveStatus?studentId=${selectedStudentId}&reservedClassId=${reservedClassId}`;

			fetch(url, {
				method: "PUT",
				headers: {
					"content-type": "application/json; charset=UTF-8",
					Authorization: `Bearer ${localStorage.getItem("token")}`,
				},
			})
				.then((res) => {
					if (res.ok) {
						setIsRemoveSuccess(true); // Set remove success state
						setSelectedStudentId(null); // Clear the selected student id
						onRemoveSuccess(reservedClassId); // Update dataListStudent in parent component
						onClose();
						let message = "Remove successful";
						navigate(`/reservation-management/${message}`);
					} else {
						let message = "Something wrong";
						navigate(`/reservation-management/${message}`);
					}
				})
				.catch((rejected) => {
					<RemoveFailed />;
				});
		}
		setShowConfirmationRemove(false);
	};

	const handleDropOutConfirmation = (confirmed) => {
		if (confirmed) {
			const url = `${baseUrl}/ReservedStudent/UpdateDropOut?studentId=${selectedStudentId}&reservedClassId=${reservedClassId}`;

			fetch(url, {
				method: "PUT",
				headers: {
					"content-type": "application/json; charset=UTF-8",
					Authorization: `Bearer ${localStorage.getItem("token")}`,
				},
			})
				.then((res) => {
					if (res.ok) {
						setIsRemoveSuccess(true); // Set remove success state
						setSelectedStudentId(null); // Clear the selected student id
						onRemoveSuccess(reservedClassId); // Update dataListStudent in parent component
						let message = "Drop out successful";
						navigate(`/reservation-management/${message}`);
						onClose();
					} else {
						let message = "Something wrong";
						navigate(`/reservation-management/${message}`);
					}
				})
				.catch((rejected) => {
					<RemoveFailed />;
				});
		}
		setShowConfirmationDropOut(false);
	};

	return (
		<div
			style={{
				position: "absolute",
				backgroundColor: "white",
				border: "1px solid #ccc",
				// zIndex: 999,
				borderRadius: "5px",
			}}
		>
			<div {...(checkClassAvailable ? { style: optionStyleFalse } : { style: optionStyle, onClick: handleReClassClick })}>
				<div style={iconStyle}>
					<svg width="23" height="24" viewBox="0 0 23 24" fill="none" xmlns="http://www.w3.org/2000/svg">
						<g clip-path="url(#clip0_184_16177)">
							<path
								d="M17.25 23.4999H10.9729C9.93794 23.4999 8.9221 23.0687 8.20335 22.3212L1.20752 15.0283L3.19127 13.2745C3.78544 12.7474 4.65752 12.642 5.3571 13.0158L7.66669 14.2424V5.09035C7.66669 3.76785 8.74002 2.69452 10.0625 2.69452C10.2254 2.69452 10.3884 2.71369 10.5513 2.74244C10.6375 1.4966 11.6725 0.509521 12.9375 0.509521C13.7617 0.509521 14.4804 0.921605 14.9117 1.5541C15.1896 1.4391 15.4963 1.3816 15.8125 1.3816C17.135 1.3816 18.2084 2.45494 18.2084 3.77744V4.04577C18.3617 4.01702 18.5246 3.99785 18.6875 3.99785C20.01 3.99785 21.0834 5.07119 21.0834 6.39369V19.6666C21.0834 21.7845 19.3679 23.4999 17.25 23.4999ZM3.96752 15.1433L9.58335 20.9891C9.94752 21.3629 10.4459 21.5833 10.9634 21.5833H17.25C18.3042 21.5833 19.1667 20.7208 19.1667 19.6666V6.39369C19.1667 6.12535 18.9559 5.91452 18.6875 5.91452C18.4192 5.91452 18.2084 6.12535 18.2084 6.39369V11.9999H16.2917V3.77744C16.2917 3.5091 16.0809 3.29827 15.8125 3.29827C15.5442 3.29827 15.3334 3.5091 15.3334 3.77744V11.9999H13.4167V2.90535C13.4167 2.63702 13.2059 2.42619 12.9375 2.42619C12.6692 2.42619 12.4584 2.63702 12.4584 2.90535V11.9999H10.5417V5.09035C10.5417 4.82202 10.3309 4.61119 10.0625 4.61119C9.79419 4.61119 9.58335 4.8316 9.58335 5.09035V17.4241L4.45627 14.712L3.96752 15.1433Z"
								fill="#2D3748"
							/>
						</g>
						<defs>
							<clipPath id="clip0_184_16177">
								<rect width="23" height="23" fill="white" transform="translate(0 0.5)" />
							</clipPath>
						</defs>
					</svg>
				</div>
				Re-class
			</div>
			<div
				style={optionStyle}
				onClick={() => {
					tablecontext.setEmailPopup(0);
					tablecontext.closePopup();
				}}
			>
				<div style={iconStyle}>
					<svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
						<g clip-path="url(#clip0_184_16251)">
							<path d="M22 6C22 4.9 21.1 4 20 4H4C2.9 4 2 4.9 2 6V18C2 19.1 2.9 20 4 20H20C21.1 20 22 19.1 22 18V6ZM20 6L12 10.99L4 6H20ZM20 18H4V8L12 13L20 8V18Z" fill="#2D3748" />
						</g>
						<defs>
							<clipPath id="clip0_184_16251">
								<rect width="24" height="24" fill="white" />
							</clipPath>
						</defs>
					</svg>
				</div>
				Send Email
			</div>
			<div style={optionStyle} onClick={handleDropOutClick}>
				<div style={iconStyle}>
					<svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
						<g clip-path="url(#clip0_184_16152)">
							<path d="M12 2C6.48 2 2 6.48 2 12C2 17.52 6.48 22 12 22C17.52 22 22 17.52 22 12C22 6.48 17.52 2 12 2ZM12 20C7.58 20 4 16.42 4 12C4 7.58 7.58 4 12 4C16.42 4 20 7.58 20 12C20 16.42 16.42 20 12 20Z" fill="#2D3748" />
							<path d="M12 17C14.7614 17 17 14.7614 17 12C17 9.23858 14.7614 7 12 7C9.23858 7 7 9.23858 7 12C7 14.7614 9.23858 17 12 17Z" fill="#2D3748" />
						</g>
						<defs>
							<clipPath id="clip0_184_16152">
								<rect width="24" height="24" fill="white" />
							</clipPath>
						</defs>
					</svg>
				</div>
				Drop Out
			</div>
			{showConfirmationDropOut && (
				<div style={{ position: "absolute", top: "50%", left: "50%", transform: "translate(-50%, -50%)", backgroundColor: "white", padding: "20px", zIndex: "1000", borderRadius: "5px", border: "1px solid #ccc", width: "30em" }}>
					<div style={{ display: "flex", borderBottom: "1px solid #ccc" }}>
						<div style={{ marginRight: "8px" }}>
							<svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
								<g clip-path="url(#clip0_184_16128)">
									<path d="M12 5.99L19.53 19H4.47L12 5.99ZM12 2L1 21H23L12 2ZM13 16H11V18H13V16ZM13 10H11V14H13V10Z" fill="#E74A3B" />
								</g>
								<defs>
									<clipPath id="clip0_184_16128">
										<rect width="24" height="24" fill="white" />
									</clipPath>
								</defs>
							</svg>
						</div>

						<div
							style={{
								color: "#2D3748",
								fontWeight: "bold",
							}}
						>
							Drop Out
						</div>
					</div>
					<div>Do you really want to drop out this student?</div>
					<div style={removeButton}>
						<button
							style={{
								color: "red",
								fontWeight: "bold",
								textDecoration: "underline",
							}}
							onClick={() => handleDropOutConfirmation(false)}
						>
							Cancel
						</button>
						<button
							style={{
								marginLeft: "10px",
								padding: "5px 5px", // Add padding to the button for better clickability and appearance
								backgroundColor: "#2D3748", // Change the background color
								color: "white", // Change the text color
								border: "none", // Remove the border
								borderRadius: "5px", // Add border radius for rounded corners
								cursor: "pointer", // Change cursor to pointer on hover
								transition: "background-color 0.3s ease", // Add transition for smoother hover effect
								fontWeight: "bold",
							}}
							onClick={() => handleDropOutConfirmation(true)}
						>
							Drop Out
						</button>
					</div>
				</div>
			)}
			<div {...(checkClassAvailable ? { onClick: handleRemoveClick, style: optionStyle } : { style: optionStyleFalse })}>
				<div style={iconStyle}>
					<svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
						<g clip-path="url(#clip0_184_16099)">
							<path
								d="M12 2C6.47 2 2 6.47 2 12C2 17.53 6.47 22 12 22C17.53 22 22 17.53 22 12C22 6.47 17.53 2 12 2ZM12 20C7.59 20 4 16.41 4 12C4 7.59 7.59 4 12 4C16.41 4 20 7.59 20 12C20 16.41 16.41 20 12 20ZM15.59 7L12 10.59L8.41 7L7 8.41L10.59 12L7 15.59L8.41 17L12 13.41L15.59 17L17 15.59L13.41 12L17 8.41L15.59 7Z"
								fill="#2D3748"
							/>
						</g>
						<defs>
							<clipPath id="clip0_184_16099">
								<rect width="24" height="24" fill="white" />
							</clipPath>
						</defs>
					</svg>
				</div>
				Remove reserve
			</div>
			{showConfirmationRemove && (
				<div style={{ position: "absolute", top: "50%", left: "50%", transform: "translate(-50%, -50%)", backgroundColor: "white", padding: "20px", borderRadius: "5px", border: "1px solid #ccc", width: "30em" }}>
					<div style={{ display: "flex", borderBottom: "1px solid #ccc" }}>
						<div style={{ marginRight: "8px" }}>
							<svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
								<g clip-path="url(#clip0_184_16128)">
									<path d="M12 5.99L19.53 19H4.47L12 5.99ZM12 2L1 21H23L12 2ZM13 16H11V18H13V16ZM13 10H11V14H13V10Z" fill="#E74A3B" />
								</g>
								<defs>
									<clipPath id="clip0_184_16128">
										<rect width="24" height="24" fill="white" />
									</clipPath>
								</defs>
							</svg>
						</div>

						<div
							style={{
								color: "#2D3748",
								fontWeight: "bold",
							}}
						>
							Remove reserving
						</div>
					</div>
					<div>Do you really want to remove this student from reserving list?</div>
					<div style={removeButton}>
						<button
							style={{
								color: "red",
								fontWeight: "bold",
								textDecoration: "underline",
							}}
							onClick={() => handleRemoveConfirmation(false)}
						>
							Cancel
						</button>
						<button
							style={{
								marginLeft: "10px",
								padding: "5px 5px", // Add padding to the button for better clickability and appearance
								backgroundColor: "#2D3748", // Change the background color
								color: "white", // Change the text color
								border: "none", // Remove the border
								borderRadius: "5px", // Add border radius for rounded corners
								cursor: "pointer", // Change cursor to pointer on hover
								transition: "background-color 0.3s ease", // Add transition for smoother hover effect
								fontWeight: "bold",
							}}
							onClick={() => handleRemoveConfirmation(true)}
						>
							Delete
						</button>
						{isRemoveSuccess && <RemoveSuccess />}
					</div>
				</div>
			)}
		</div>
	);
}

export default OptionsBoard;
