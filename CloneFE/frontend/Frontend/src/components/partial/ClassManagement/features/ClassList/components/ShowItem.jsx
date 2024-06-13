import * as React from "react";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import showStyle from "../../../assert/css/ShowItem.module.scss";

function ShowItem({ perPage, setPerPage }) {
	function handleChange(event) {
		setPerPage(event.target.value);
	}

	return (
		<div style={{ width: "46%" }}>
			<div
				style={{
					display: "flex",
					alignItems: "center",
					gap: "10px",
					justifyContent: "end",
					paddingBottom: "3px",
				}}
			>
				<div>Rows per page</div>
				<div className={showStyle.show}>
					<FormControl sx={{ minWidth: 120, border: "none" }}>
						<Select labelId="demo-simple-select-standard-label" id="demo-simple-select-standard" value={perPage} onChange={handleChange} label="perPage" style={{ border: "none", width: "55%" }}>
							<MenuItem value={5}>5</MenuItem>
							<MenuItem value={10}>10</MenuItem>
						</Select>
					</FormControl>
				</div>
			</div>
		</div>
	);
}

export default ShowItem;
