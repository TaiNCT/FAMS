import React from "react";
import SearchIcon from "@mui/icons-material/Search";
import style from "../../assert/css/Input.module.scss";
import TextField from "@mui/material/TextField";
export default function Search_Input({ onChange, onSubmit }) {
	return (
		<div>
			<div className={style.formContainer}>
				<TextField
					className={style.formControl}
					placeholder=" Search By..."
					onChange={onChange}
					onKeyDown={onSubmit}
					InputProps={{
						startAdornment: <SearchIcon fontSize="small" />,
						style: {
							color: "black", //màu cho placeholder
						},
					}}
				/>
			</div>
		</div>
	);
}
