// @ts-nocheck
import * as React from "react";
import { useEffect, useState } from "react";
import Paper from "@mui/material/Paper";
import {
  TableContainer,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  TablePagination,
} from "@mui/material";
import SortIcon from "@mui/icons-material/Sort";
import style from "./style.module.scss";
import { RowTable } from "./RowTable";
import axios from "axios";
import { Outlet, useOutlet, useParams } from "react-router-dom";
import FunctionButton from "./FuntctionButton";
import { Text } from "@chakra-ui/react";
import GlobalLoading from "../../../global/GlobalLoading.jsx";

const backend_api: string = `${import.meta.env.VITE_API_HOST}:${
  import.meta.env.VITE_API_PORT
}`;

function ViewTraineeScore() {
  // check if an outlet exists
  const outlet = useOutlet();
  if (outlet) return <Outlet />;

  // Set states
  const { classId } = useParams();
  const [page, setPage] = React.useState(0);
  const [count, setCount] = React.useState(0);
  const [isLoading, setIsLoading] = useState(true);
  const [data, setdata] = React.useState([]);
  const [sortConfig, setSortConfig] = useState<string | null>(null);
  const [rowsPerPage, setRowsPerPage] = useState(30); // Set default rows per page to 30
  const [isBlurred, setIsBlurred] = useState(false);

  const handleChangePage = (
    event: React.MouseEvent<HTMLButtonElement> | null,
    newPage: number
  ) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0); // Reset to the first page when changing rows per page
  };

  // Creating a sorted array
  const sorted_data = React.useMemo(() => {
    if (sortConfig === null) return data;

    // Put all null values in the beginning of the list
    const all_nulls = data.filter((e) => e[sortConfig] === null);
    const all_values = data.filter((e) => e[sortConfig] !== null);

    return all_nulls.concat(
      all_values.sort((a, b) =>
        a[sortConfig].toString().localeCompare(b[sortConfig].toString())
      )
    );
  }, [data, sortConfig]);

  // Do sorting
  const sortOnClick = (option: string) =>
    setSortConfig(sortConfig ? null : option);

  // Fetch data from the backend API
  useEffect(() => {
    setIsLoading(true);
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
    };
    axios
      .get(`${backend_api}/api/Score/${classId}`, { headers: config.headers })
      .then((resp) => {

        for (const v of resp.data.data) {
          // Average 1
          v.ave1 = (
            (v.html + v.css + v.quiz3 + v.quiz4 + v.quiz5 + v.quiz6) /
            6
          ).toFixed(1);

          // Average score 2
          v.ave2 = ((v.practice1 + v.practice2 + v.practice3) / 3).toFixed(1);

          // Final module
          v.finalmod = (v.pracfinal + v.quizfinal) / 2;

          // Level module
          v.level1 = v.level1 === 0 ? "N/A" : v.level1;

          // Practice final
          v.pracfinal2 =
            v.pracfinal && v.quizfinal
              ? (v.pracfinal + v.quizfinal) / 2
              : "N/A";

          // Status 1
          v.status1 = v.gpa1 >= 60;

          // status 2
          v.status2 = v.gpa2 >= 60;

          // Level Module 2
          v.level2 = v.level2 === 0 ? "N/A" : v.level2;
        }

        setdata(resp.data.data);
        setCount(resp.data.count);
        setIsLoading(false);
      })
      .catch((e) => {
        setIsLoading(false);
      });
  }, []);

  return (
    <div className={`${style.general} ${isBlurred ? style.blur : ""}`}>
      <div>
        <GlobalLoading isLoading={isLoading} />
        <Paper className={style.main}>
          <Text
            as="h3"
            mb={8}
            pl={40}
            py={14}
            letterSpacing={6}
            bg="#2d3748"
            color="#fff"
            fontSize="2rem"
            width="100%"
          >
            Score List
          </Text>{" "}
          <div className={style.importButton}>
            <FunctionButton
              onUpdateData={() => {}}
              onBlurToggle={setIsBlurred}
            />
          </div>
          <TableContainer sx={{ minHeight: 800 }} className={style.body}>
            <Table stickyHeader aria-label="sticky table">
              <TableHead>
                <TableRow stickyHeader aria-label="sticky table">
                  <TableCell
                    align="center"
                    sx={{ position: "sticky", left: "0", zIndex: "10" }}
                  >
                    Full Name{" "}
                    <SortIcon onClick={() => sortOnClick("fullName")} />
                  </TableCell>
                  <TableCell
                    align="center"
                    sx={{ position: "sticky", left: "210px", zIndex: "10" }}
                  >
                    Account{" "}
                    <SortIcon onClick={() => sortOnClick("faaccount")} />
                  </TableCell>
                  <TableCell align="center" colSpan={7}>
                    Quiz
                  </TableCell>
                  <TableCell align="center" colSpan={4}>
                    ASM
                  </TableCell>
                  <TableCell>
                    Quiz Final
                    <SortIcon onClick={() => sortOnClick("quizfinal")} />
                  </TableCell>
                  <TableCell>
                    Practice Final
                    <SortIcon onClick={() => sortOnClick("pracfinal")} />
                  </TableCell>
                  <TableCell>
                    Final Module
                    <SortIcon onClick={() => sortOnClick("finalmod")} />
                  </TableCell>
                  <TableCell>
                    GPA Module
                    <SortIcon onClick={() => sortOnClick("gpa1")} />
                  </TableCell>
                  <TableCell>
                    Level Module
                    <SortIcon onClick={() => sortOnClick("level1")} />
                  </TableCell>
                  <TableCell>
                    Status
                    <SortIcon onClick={() => sortOnClick("status1")} />
                  </TableCell>
                  <TableCell>
                    MOCK
                    <SortIcon onClick={() => sortOnClick("mock")} />
                  </TableCell>
                  <TableCell>
                    Final Module
                    <SortIcon onClick={() => sortOnClick("pracfinal2")} />
                  </TableCell>
                  <TableCell>
                    GPA Module
                    <SortIcon onClick={() => sortOnClick("gpa2")} />
                  </TableCell>
                  <TableCell>
                    Level Module
                    <SortIcon onClick={() => sortOnClick("level2")} />{" "}
                  </TableCell>
                  <TableCell>
                    Status
                    <SortIcon onClick={() => sortOnClick("status2")} />{" "}
                  </TableCell>
                  <TableCell></TableCell>
                </TableRow>
                <TableRow>
                  <TableCell
                    colSpan={2}
                    sx={{
                      position: "sticky",
                      zIndex: "10",
                      background: "black",
                      left: "0",
                    }}
                  ></TableCell>
                  <TableCell>
                    HTML
                    <SortIcon onClick={() => sortOnClick("html")} />
                  </TableCell>
                  <TableCell>
                    CSS
                    <SortIcon onClick={() => sortOnClick("css")} />
                  </TableCell>
                  <TableCell>
                    Quiz 3<SortIcon onClick={() => sortOnClick("quiz3")} />
                  </TableCell>
                  <TableCell>
                    Quiz 4<SortIcon onClick={() => sortOnClick("quiz4")} />
                  </TableCell>
                  <TableCell>
                    Quiz 5<SortIcon onClick={() => sortOnClick("quiz5")} />
                  </TableCell>
                  <TableCell>
                    Quiz 6<SortIcon onClick={() => sortOnClick("quiz6")} />
                  </TableCell>
                  <TableCell>
                    Ave
                    <SortIcon onClick={() => sortOnClick("ave1")} />{" "}
                  </TableCell>
                  <TableCell>
                    Practice 1
                    <SortIcon onClick={() => sortOnClick("practice1")} />
                  </TableCell>
                  <TableCell>
                    Practice 2
                    <SortIcon onClick={() => sortOnClick("practice2")} />
                  </TableCell>
                  <TableCell>
                    Practice 3
                    <SortIcon onClick={() => sortOnClick("practice3")} />
                  </TableCell>
                  <TableCell>
                    Ave
                    <SortIcon onClick={() => sortOnClick("ave2")} />{" "}
                  </TableCell>
                  <TableCell colSpan={7}></TableCell>
                  <TableCell colSpan={6}></TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {sorted_data
                  .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                  .map((e, index) => {
                    return <RowTable data={e} key={index}></RowTable>;
                  })}
              </TableBody>
            </Table>
          </TableContainer>
          <TablePagination
            component={"div"}
            rowsPerPageOptions={[10, 25, 30]} // Include 30 as an option
            className={style.pagination}
            count={count}
            rowsPerPage={rowsPerPage} // Use the state for rows per page
            page={page}
            onPageChange={handleChangePage}
            onRowsPerPageChange={handleChangeRowsPerPage} // Handle rows per page change
          />
        </Paper>{" "}
      </div>
    </div>
  );
}

export { ViewTraineeScore };
