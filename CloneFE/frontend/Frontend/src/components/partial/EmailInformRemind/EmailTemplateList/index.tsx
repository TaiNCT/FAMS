import { Box, Tab, Tabs, Typography } from "@mui/material";
import React from "react";
import style from "./style.module.scss";
// import AllCategories from "./components/AllCategoriesTable";
import AllCategories from "./table/AllCategoriesTable";
// import RemindCategory from "../Categories/RemindCategories/RemindCategoryTable";
// import NoticeCategory from "../Categories/NoticeCategories/NoticeCategoryTable";
// import OtherCategory from "../Categories/OtherCategories/OtherCategoryTable";

export default function EmailTemplateList() {
	const [value, setValue] = React.useState(0);
	const handleChange = (event: React.SyntheticEvent, newValue: number) => {
		setValue(newValue);
	};

	return (
		<div className={style.main}>
			<div className={style.partial_view}>
				<Box sx={{ padding: "10px", bgcolor: "#2D3748" }}>
					<Typography variant="h5" color={"white"} gutterBottom>
						Email Template
					</Typography>
				</Box>
				<Box
					sx={{
						borderBottom: 1,
						borderColor: "divider",
						borderBottomColor: "black",
					}}
				>
					<Tabs sx={{ pt: 2 }} value={value} onChange={handleChange}>
						<Tab label="All Categories" value={0}></Tab>
						<Tab label="Reserve" value={2}></Tab>
						<Tab label="Notice" value={4}></Tab>
						<Tab label="Remind" value={3}></Tab>
						<Tab label="Other" value={5}></Tab>
					</Tabs>
				</Box>
				<div>
					<AllCategories />
				</div>
				{/* <div>{value === 0 && <AllCategories />}</div> */}
				{/* <div>{value === 2 && <ReserveCategory />}</div>
				<div>{value === 3 && <RemindCategory />}</div>
				<div>{value === 4 && <NoticeCategory />}</div>
				<div>{value === 5 && <OtherCategory />}</div> */}
			</div>
		</div>
	);
}
