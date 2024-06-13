import * as React from "react";
import Button from "@mui/material/Button";
import { styled } from "@mui/material/styles";
import Dialog from "@mui/material/Dialog";
import DialogTitle from "@mui/material/DialogTitle";
import DialogContent from "@mui/material/DialogContent";
import DialogActions from "@mui/material/DialogActions";
import IconButton from "@mui/material/IconButton";
import CloseIcon from "@mui/icons-material/Close";
import style from "./style.module.scss";
import Stack from "@mui/material/Stack";
import MailBody from "./MailBody";
import { LoadingButton } from "@mui/lab";
import { context } from ".";
import axios from "../../../../axiosAuth";
import { color } from "framer-motion";

const BootstrapDialog = styled(Dialog)(({ theme }) => ({
	"& .MuiDialogContent-root": {
		padding: theme.spacing(3),
	},
	"& .MuiDialogActions-root": {
		padding: theme.spacing(2),
	},
}));

export default function DetailedPopup({ open, setOpen, template, from, to, lastmsg, firstmsg, subject, scores }) {
	const [isLoading, setIsLoading] = React.useState(false);
	const parentContext = React.useContext(context);
	// Grab the "send" function to send it later

	return (
		<React.Fragment>
			<BootstrapDialog className={style.overwrite_detailed_dialog} onClose={() => {}} open={open === 1}>
				<DialogTitle sx={{ m: 0, p: 2 }} id="customized-dialog-title" className={style.header}>
					Email preview
				</DialogTitle>
				<IconButton
					aria-label="close"
					onClick={() => setOpen(null)}
					sx={{
						position: "absolute",
						right: 8,
						top: 8,
						color: (theme) => theme.palette.grey[500],
					}}
				>
					<CloseIcon />
				</IconButton>
				<DialogContent className={style.cardholder}>
					<div>
						<h1>Template Name</h1>
						<label>{template ? template.name : "Empty"}</label>
					</div>
					<div>
						<h1>From</h1>
						<label>{from}</label>
					</div>
					<div>
						<h1>To</h1>
						<label style={{ fontStyle: "italic" }}>{to}</label>
					</div>
					<div>
						<h1>Subject</h1>
						<label>{subject}</label>
					</div>
					<div>
						<h1 className={style.email_header}>Body</h1>
						{/* Complicated body with HTML here */}
						<MailBody first_message={firstmsg} scores={scores} last_message={lastmsg} />
					</div>
				</DialogContent>
				<DialogActions
					style={
						scores
							? {
									boxShadow: "0px 0px 5px #0000005e",
							  }
							: {}
					}
					className={style.scaledown}
				>
					<Stack style={{ width: "100%" }} direction="row" justifyContent="center" alignItems="center">
						<Button style={{ color: "black", border: "1px solid black" }} variant="outlined" onClick={() => setOpen(0)}>
							Back
						</Button>
						<LoadingButton
							loading={isLoading}
							style={isLoading ? {} : { backgroundColor: "#2D3748" }}
							variant="contained"
							onClick={() => {
								setIsLoading(true);
								parentContext.send(() => setIsLoading(false));
							}}
						>
							Send
						</LoadingButton>
					</Stack>
				</DialogActions>
			</BootstrapDialog>
		</React.Fragment>
	);
}
