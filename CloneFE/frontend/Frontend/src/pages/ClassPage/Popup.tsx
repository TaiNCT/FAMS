import style from "./style.module.scss";
import MailOutlineIcon from "@mui/icons-material/MailOutline";
import SendIcon from "@mui/icons-material/Send";
import WifiCalling3Icon from "@mui/icons-material/WifiCalling3";
import { useContext } from "react";
import { editClasscontext } from "./EditClassPage";

export default function Popup({ email, phone, popup }) {
	// Importing context here
	const context = useContext(editClasscontext);

	const nopropagate = (e) => {
		e.preventDefault();
		e.stopPropagation();
	};

	return (
		<>
			{popup && (
				<div
					className={style.popup}
					onClick={() => {
						context.setpopup(false);
					}}
				>
					<div onClick={nopropagate}>
						<div>
							<WifiCalling3Icon></WifiCalling3Icon>
							<label>{phone}</label>
						</div>
						<div>
							<MailOutlineIcon></MailOutlineIcon>
							<label>{email}</label>
						</div>
						<div
							onClick={() => {
								context.setpopup(false);
								context.setopen(true);
							}}
						>
							<SendIcon></SendIcon>
							<label>Send Email</label>
						</div>
					</div>
				</div>
			)}
		</>
	);
}
