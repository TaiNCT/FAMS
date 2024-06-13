import { Button, InputAdornment, TextField } from "@mui/material";
import React, { useContext, useState } from "react";
import { FaSearch } from "react-icons/fa";
import { LuPlusCircle } from "react-icons/lu";
import { MdFilterList } from "react-icons/md";
import FirstTabPanel from "./FirstTabPanel";
import CreateFormPopUp from "../../CreateEmailTemplatePopup";
import FormFilter from "../../FilterEmailTemplate";
import { EmailTable, EmailTemplate, TableParams } from "./EmailConfigTable";
import style from "../style.module.scss";
import { createContext } from "react";
import axios from "../../../../../axiosAuth";

// eslint-disable-next-line react-refresh/only-export-components
export const context = createContext(null);


// @ts-ignore
const backend_api: string = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;

export default function AllCategories() {
	// const [change, setChange] = useState(0);
	// const [value] = React.useState(0);
	// const [, setEmailTemplates] = useState<EmailTemplate[]>([]);
	// const [totalPage, setTotalPage] = useState<number>(0);
	// const [updatedData, setUpdatedData] = useState<EmailTemplate[]>();
	// const handleDataUpdate = (data: EmailTemplate[]) => {
	// 	setEmailTemplates(data);
	// 	setUpdatedData(data);
	// };

	// const [tableParams, setTableParams] = useState<TableParams>({
	// 	pagination: {
	// 		current: 1,
	// 		pageSize: 10,
	// 	},
	// });
	// const [searchParams, setSearchParams] = useState<Record<string, unknown>>({});

	// const [isModalVisible3, setIsModalVisible3] = useState(false);
	// const toggleModalFilter = () => {
	// 	setIsModalVisible3((wasModalIsVisible) => !wasModalIsVisible);
	// };

	// const [isModalVisible6, setIsModalVisible6] = useState(false);
	// const toggleModalCreate = () => {
	// 	setIsModalVisible6((wasModalIsVisible) => !wasModalIsVisible);
	// };

	// const handleSearch = async (searchTerm: string) => {
	// 	setSearchParams((prevSearchParams) => ({
	// 		...prevSearchParams,
	// 		name: searchTerm,
	// 		description: searchTerm,
	// 	}));
	// };

	// let timeout: NodeJS.Timeout;

	// const handleChangeSearch = (event: React.ChangeEvent<HTMLInputElement>) => {
	// 	clearTimeout(timeout);

	// 	timeout = setTimeout(() => {
	// 		handleSearch(event.target.value);
	// 	});
	// };

	return (
		<div className={style.buttonContainer}>
			<div className={style.buttonSearch}>
				<TextField
					size="small"
					placeholder="Search by..."
					onChange={() => {}}
					id="outlined-start-adornment"
					InputProps={{
						startAdornment: (
							<InputAdornment position="start">
								<FaSearch color="#2D3748" />
							</InputAdornment>
						),
					}}
				/>
			</div>
			<div className={style.buttonFilter}>
				<Button
					onClick={() => {}}
					sx={{
						bgcolor: "#2D3748",
					}}
					variant="contained"
					startIcon={<MdFilterList color="white" />}
				>
					Filter
				</Button>
				{/* {isModalVisible3 && (
							<div className={style.popup}>
								<FormFilter
									pages={{
										totalPage: totalPage,
										setTotalPage: setTotalPage,
									}}
									onClose={toggleModalFilter}
									setUpdatedData={setUpdatedData}
									tableParams={tableParams}
									setSearchParams={setSearchParams}
									searchParams={searchParams}
								/>
							</div>
						)} */}
			</div>
			<div className={style.buttonCreate}>
				<Button
					onClick={() => {}}
					sx={{
						bgcolor: "#2D3748",
					}}
					variant="contained"
					startIcon={<LuPlusCircle color="white" />}
				>
					Add new
				</Button>
			</div>
		</div>
	);
}
