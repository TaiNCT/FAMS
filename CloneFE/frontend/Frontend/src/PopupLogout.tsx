import * as React from "react";
import Box from "@mui/material/Box";
import Modal from "@mui/material/Modal";
import Button from "@mui/material/Button";
import { useNavigate } from "react-router-dom";

const style = {
	position: "absolute" as "absolute",
	top: "50%",
	left: "50%",
	transform: "translate(-50%, -50%)",
	width: 400,
	bgcolor: "background.paper",
	border: "2px solid #000",
	boxShadow: 24,
	pt: 2,
	px: 4,
	pb: 3,
};

export default function PopupLogout({ open, setOpen }) {
	const navigate = useNavigate();
	const [re, setRe] = React.useState(false);
	const handleClose = () => {
		setOpen(false);

		// Delete token here
		localStorage.clear();
		navigate("/login");
	};

	return (
		<Modal open={open} onClose={handleClose} aria-labelledby="parent-modal-title" aria-describedby="parent-modal-description">
			<Box sx={{ ...style, width: 400 }}>
				<h1
					style={{
						fontWeight: "bold",
						fontSize: "1.4em",
						marginBottom: "0.6em",
					}}
					id="parent-modal-title"
				>
					Session expired
				</h1>
				<p id="parent-modal-description">Duis mollis, est non commodo luctus, nisi erat porttitor ligula.</p>
				<React.Fragment>
					<Button
						style={{
							marginTop: "1em",
						}}
						onClick={handleClose}
						variant="contained"
					>
						Ok
					</Button>
				</React.Fragment>
			</Box>
		</Modal>
	);
}
