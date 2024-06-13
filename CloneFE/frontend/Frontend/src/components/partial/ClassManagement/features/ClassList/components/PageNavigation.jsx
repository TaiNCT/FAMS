import * as React from "react";
import Pagination from "@mui/material/Pagination";
import Stack from "@mui/material/Stack";
import style from "../../../assert/css/Pagination.module.scss";
export default function PageNavigation({
  page,
  setPage,
  totalItems,
  ItemsPerPage,
}) {
  const handleChange = (event, value) => {
    setPage(value);
  };
  const totalPages = Math.ceil(totalItems / ItemsPerPage);

  return (
    <div className={style.paginate}>
      <Stack
        spacing={2}
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "end",
        }}
      >
        <Pagination
          count={totalPages}
          page={page}
          onChange={handleChange}
          color="primary"
        />
      </Stack>
    </div>
  );
}
