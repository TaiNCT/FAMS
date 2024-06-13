// @ts-nocheck
/* eslint-disable @typescript-eslint/no-explicit-any */
import * as React from "react";
import { useParams, useSearchParams } from "react-router-dom";
import Table from "@mui/material/Table";
import TableContainer from "@mui/material/TableContainer";
import TableFooter from "@mui/material/TableFooter";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import { styled } from "@mui/material/styles";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import TableHead from "@mui/material/TableHead";
import Checkbox from "@mui/material/Checkbox";
import Style from "./style.module.scss";
import RowPerPage from "../RowPerPage";
import { Pagination, TableBody } from "@mui/material";
import ColumnsPopup from "../ColumnsPopup";
import SettingsIcon from "@mui/icons-material/Settings";
import ActionDetailButton from "../ActionDetailButton";
import CircularIndeterminate from "../Loading";
import { useItems } from "../../../../config/StudentInformanagement/hooks";
import { Response, StudentInclassResponse } from "model/StudentLamNS";
import dayjs from "dayjs";
import sortIcon from "../../../../assets/LogoStManagement/sort.png";
import { Empty } from "antd";
import GlobalLoading from "../../../global/GlobalLoading";

//Table components styles
const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    color: theme.palette.common.white,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 16,
    height: 24,
  },
  [`&.${tableCellClasses.root}`]: {
    borderBottom: "1px solid black",
  },
}));

const StyledTableRow = styled(TableRow)(({ theme }) => ({}));

//Define Columns of table
interface Column {
  id:
    | "FullName"
    | "Dob"
    | "Gender"
    | "Phone"
    | "Email"
    | "University"
    | "Major"
    | "GraduateTime"
    | "GPA"
    | "Address"
    | "RECer"
    | "Status";
  label: string;
  align?: "center";
  color?: "#fff";
  bgColor?: "#2D3748";
  format?: (value: number) => string;
}

const columns: readonly Column[] = [
  {
    id: "FullName",
    label: "Full Name",
    bgColor: "#2D3748",
    align: "center",
    color: "#fff",
  },
  {
    id: "Dob",
    label: "Birthday",
    bgColor: "#2D3748",
    align: "center",
    color: "#fff",
  },
  {
    id: "Gender",
    label: "Gender",
    align: "center",
    bgColor: "#2D3748",
    color: "#fff",
  },
  {
    id: "Phone",
    label: "Phone",
    align: "center",
    bgColor: "#2D3748",
    color: "#fff",
  },
  {
    id: "Email",
    label: "Email",
    align: "center",
    bgColor: "#2D3748",
    color: "#fff",
  },
  {
    id: "University",
    label: "University",
    align: "center",
    bgColor: "#2D3748",
    color: "#fff",
  },
  {
    id: "Major",
    label: "Major",
    align: "center",
    bgColor: "#2D3748",
    color: "#fff",
  },
  {
    id: "GraduateTime",
    label: "GraduateTime",
    align: "center",
    bgColor: "#2D3748",
    color: "#fff",
  },
  {
    id: "GPA",
    label: "GPA",
    align: "center",
    bgColor: "#2D3748",
    color: "#fff",
  },
  {
    id: "Address",
    label: "Address",
    align: "center",
    bgColor: "#2D3748",
    color: "#fff",
  },
  {
    id: "RECer",
    label: "RECer",
    align: "center",
    bgColor: "#2D3748",
    color: "#fff",
  },
  {
    id: "Status",
    label: "Status",
    align: "center",
    bgColor: "#2D3748",
    color: "#fff",
  },
];

function CustomizedTables({
  pagination,
  setPagination,
  onCheckboxChange,
  selectedIds,
  onSelectedAllInPage,
  IsSelectAllInPage,
  disableCheckAll,
  onSelectAll,
  isSelectAll,
}: {
  pagination: any;
  setPagination: any;
  onCheckboxChange: (studentId: string, checked: boolean) => void;
  selectedIds: string[];
  onSelectedAllInPage: (listId: string[]) => void;
  IsSelectAllInPage: boolean;
  disableCheckAll: () => void;
  onSelectAll: () => void;
  isSelectAll: boolean;
}) {
  //Class id paramenter
  const { classId = "1" } = useParams();
  //Search Params hook
  const [search, setSearch] = useSearchParams();
  //Loading state
  const [loading, setLoading] = React.useState(false);

  //State for choose which table columns are display
  const [selectedColumns, setSelectedColumns] = React.useState({
    FullName: true, //
    Dob: true, //
    Gender: false, //
    Phone: true, //
    Email: true, //
    University: false,
    Major: false,
    GraduateTime: false,
    GPA: true, //
    Address: false,
    RECer: false, //
    Status: true, //
  });

  React.useEffect(() => {
    const selectedColumnsRaw = localStorage.getItem("selectedColumns");
    if (selectedColumnsRaw) {
      const items = JSON.parse(selectedColumnsRaw);
      items ? setSelectedColumns(items) : "";
    }
  }, [setSelectedColumns]);

  //Get List Student by custom UseItems Hook
  const studentsResponse = useItems(classId, pagination);

  //Store response
  const response = React.useMemo<Response<StudentInclassResponse>>(
    () =>
      studentsResponse.data! ?? {
        result: { students: [], totalCount: 0 },
        isSuccess: false,
        message: "",
      },
    [studentsResponse.data]
  );
  //Get loading
  React.useEffect(() => {
    if (studentsResponse.isLoading) {
      setLoading(true);
    } else {
      setLoading(false);
    }
  }, [
    studentsResponse.isLoading,
    studentsResponse.isSuccess,
    studentsResponse.data,
    studentsResponse.error,
  ]);

  const students = response.result?.students ?? [];

  const totalCount = response.result?.totalCount ?? 0;
  const NumberPages = Math.ceil(totalCount / pagination.pageSize);
  const emptyRows =
    students.length < pagination.pageSize
      ? pagination.pageSize - students.length
      : 0;

  const handleChangePage = (
    event: React.ChangeEvent<unknown>,
    value: number
  ) => {
    search.set("pageNumber", String(value));
    setSearch(`?${search.toString()}`, { replace: true });
    setPagination({
      ...pagination,
      pageNumber: value,
    });

    disableCheckAll();
  };

  function handleChangeRowsPerPage(value: number) {
    search.set("pageSize", String(value));
    setSearch(`?${search.toString()}`, { replace: true });
    search.set("pageNumber", String(1));
    setSearch(`?${search.toString()}`, { replace: true });
    setPagination({
      pageSize: value,
      pageNumber: 1,
    });

    disableCheckAll();
  }

  function handleSort(value: string) {
    value = value.replace(/\s/g, "");
    const sortedCondition = search.get("sortBy") ?? "";
    const sortOrder = search.get("sortOrder") ?? "";

    if (sortedCondition === value) {
      if (sortOrder === "asc") {
        search.set("sortOrder", "desc");
      } else if (sortOrder === "desc") {
        search.delete("sortBy");
        search.delete("sortOrder");
      }
    } else {
      search.set("sortBy", value);
      search.set("sortOrder", "asc");
    }

    setSearch(`?${search.toString()}`, { replace: true });
    disableCheckAll();
  }

  return (
    <>
      <div className={Style.selectNotiContainer}>
        <GlobalLoading isLoading={loading} />
        {isSelectAll ? (
          <p>
            All {totalCount} studens are selected.
            <button onClick={disableCheckAll}>
              Click here to clear all select
            </button>
          </p>
        ) : IsSelectAllInPage ? (
          <p>
            All {selectedIds.length} Students In This Page Is Selected.
            <button onClick={onSelectAll}>
              Click Here to select all {totalCount} student in this class
            </button>
          </p>
        ) : (
          ""
        )}
      </div>
      <div>
        <TableContainer
          sx={{
            boxShadow: "none",
            width: "100%",
            maxWidth: 1200,
            overflow: "auto",
            minHeight: "400px",
            minWidth: "100%",
          }}
          component={Paper}
        >
          <Table
            stickyHeader
            sx={{
              position: "relative",
              minHeight: "300px",
            }}
            aria-label="customized table"
          >
            <TableHead className={Style.th}>
              <TableRow sx={{ backgroundColor: "#2d3748" }}>
                <StyledTableCell
                  align="center"
                  style={{
                    backgroundColor: "#2d3748",
                    width: "60px",
                    maxWidth: "60px",
                    minWidth: "60px",
                    border: 0,
                    borderTopLeftRadius: 20,
                    position: "sticky",
                    left: "0",
                    zIndex: "10",
                  }}
                >
                  <div className={Style.checkBoxContainer}>
                    <Checkbox
                      iconStyle={{ fill: "white" }}
                      labelStyle={{ color: "white" }}
                      sx={{
                        color: "white",
                        "&.Mui-checked": {
                          color: "white",
                        },
                      }}
                      onChange={() =>
                        onSelectedAllInPage(
                          students.map(
                            (student) => student.studentInfoDTO.studentId
                          )
                        )
                      }
                      checked={IsSelectAllInPage || isSelectAll}
                    />
                  </div>
                </StyledTableCell>
                {columns.map(
                  (column) =>
                    selectedColumns[column.id] && (
                      <TableCell
                        key={column.id}
                        align={column.align}
                        sx={{
                          backgroundColor: column.bgColor,
                          color: column.color,
                          width: "155px",
                          minWidth: "155px",
                          maxWidth: "155px",
                          fontSize: "16px",
                          fontWeight: "500",
                          border: 0,
                          position: column.id == "FullName" ? "sticky" : "none",
                          zIndex: column.id == "FullName" ? "10" : "none",
                          left: column.id == "FullName" ? "60px" : "none",
                        }}
                      >
                        <div
                          style={{
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "center",
                          }}
                        >
                          {column.label}
                          <button onClick={() => handleSort(column.label)}>
                            <img
                              src={sortIcon}
                              style={{ width: "18px", height: "18px" }}
                              alt="sort icon"
                            />
                          </button>
                        </div>
                      </TableCell>
                    )
                )}
                <StyledTableCell
                  style={{
                    backgroundColor: "#2d3748",
                    width: "60px",
                    minWidth: "60px",
                    maxWidth: "60px",
                  }}
                  align="center"
                >
                  <ColumnsPopup
                    button={
                      <button
                        style={{ backgroundColor: "#2D3748", border: "none" }}
                      >
                        <SettingsIcon sx={{ color: "white" }} />
                      </button>
                    }
                    selectedColumns={selectedColumns}
                    setSelectedColumns={setSelectedColumns}
                  />
                </StyledTableCell>
              </TableRow>
            </TableHead>
            {students.length > 0 ? (
              <>
                <TableBody>
                  {students?.map((row: any) => (
                    <StyledTableRow key={row.name}>
                      <StyledTableCell
                        style={{
                          padding: "0 0 0 12px",
                          position: "sticky",
                          left: "0",
                          zIndex: "10",
                          backgroundColor: "white",
                        }}
                        className={Style.trButton}
                        align="center"
                      >
                        <Checkbox
                          onChange={(event) =>
                            onCheckboxChange(
                              row.studentInfoDTO.studentId,
                              event.target.checked
                            )
                          }
                          checked={selectedIds.includes(
                            row.studentInfoDTO.studentId
                          )}
                          sx={{
                            "&.Mui-checked": {
                              color: "#111e2e",
                            },
                          }}
                        />
                      </StyledTableCell>
                      {selectedColumns.FullName && (
                        <StyledTableCell
                          sx={{
                            fontWeight: "bold",
                            padding: 0,
                            fontSize: "16px",
                            position: "sticky",
                            left: "60px",
                            zIndex: "100",
                            background: "white",
                          }}
                          className={Style.tr}
                          align="center"
                        >
                          {row.studentInfoDTO.fullName}
                        </StyledTableCell>
                      )}
                      {selectedColumns.Dob && (
                        <StyledTableCell
                          sx={{ padding: 0, fontSize: "16px" }}
                          className={Style.tr}
                          align="center"
                        >
                          {dayjs(row.studentInfoDTO.dob).format("MM-DD-YYYY")}
                        </StyledTableCell>
                      )}
                      {selectedColumns.Gender && (
                        <StyledTableCell
                          sx={{ padding: 0, fontSize: "16px" }}
                          className={Style.tr}
                          align="center"
                        >
                          {row.studentInfoDTO.gender}
                        </StyledTableCell>
                      )}
                      {selectedColumns.Phone && (
                        <StyledTableCell
                          sx={{ padding: 0, fontSize: "16px" }}
                          className={Style.tr}
                          align="center"
                        >
                          {row.studentInfoDTO.phone}
                        </StyledTableCell>
                      )}
                      {selectedColumns.Email && (
                        <StyledTableCell
                          sx={{ padding: 0, fontSize: "16px" }}
                          className={Style.tr}
                          align="center"
                        >
                          {row.studentInfoDTO.email}
                        </StyledTableCell>
                      )}
                      {selectedColumns.University && (
                        <StyledTableCell
                          sx={{ padding: 0, fontSize: "16px" }}
                          className={Style.tr}
                          align="center"
                        >
                          {row.studentInfoDTO.university}
                        </StyledTableCell>
                      )}
                      {selectedColumns.Major && (
                        <StyledTableCell
                          sx={{ padding: 0, fontSize: "16px" }}
                          className={Style.tr}
                          align="center"
                        >
                          {row.majorDTO.name}
                        </StyledTableCell>
                      )}
                      {selectedColumns.GraduateTime && (
                        <StyledTableCell
                          sx={{ padding: 0, fontSize: "16px" }}
                          className={Style.tr}
                          align="center"
                        >
                          {dayjs(row.studentInfoDTO.GraduateTime).format(
                            "MM-DD-YYYY"
                          )}
                        </StyledTableCell>
                      )}
                      {selectedColumns.GPA && (
                        <StyledTableCell
                          sx={{ padding: 0, fontSize: "16px" }}
                          className={Style.tr}
                          align="center"
                        >
                          {row.studentInfoDTO.gpa}
                        </StyledTableCell>
                      )}
                      {selectedColumns.Address && (
                        <StyledTableCell
                          sx={{ padding: 0, fontSize: "16px" }}
                          className={Style.tr}
                          align="center"
                        >
                          {row.studentInfoDTO.address}
                        </StyledTableCell>
                      )}
                      {selectedColumns.RECer && (
                        <StyledTableCell
                          sx={{ padding: 0, fontSize: "16px" }}
                          className={Style.tr}
                          align="center"
                        >
                          {row.studentInfoDTO.recer}
                        </StyledTableCell>
                      )}
                      {selectedColumns.Status && (
                        <StyledTableCell
                          sx={{ padding: 0, fontSize: "16px" }}
                          className={Style.tr}
                          align="center"
                        >
                          <p
                            className={`${Style.status} ${
                              Style[row.studentClassDTOs[0].attendingStatus]
                            }`}
                          >
                            {row.studentClassDTOs[0].attendingStatus ==
                            "InClass"
                              ? "In Class"
                              : row.studentClassDTOs[0].attendingStatus ==
                                "DropOut"
                              ? "Drop Out"
                              : row.studentClassDTOs[0].attendingStatus}
                          </p>
                        </StyledTableCell>
                      )}

                      <StyledTableCell
                        className={Style.trButton}
                        align="center"
                      >
                        <ActionDetailButton student={row.studentInfoDTO} />
                      </StyledTableCell>
                    </StyledTableRow>
                  ))}
                  {emptyRows > 0 && (
                    <TableRow>
                      <TableCell
                        sx={{ maxWidth: "50px", border: "none" }}
                        colSpan={6}
                      />
                    </TableRow>
                  )}
                </TableBody>
              </>
            ) : (
              <StyledTableRow>
                <Empty
                  style={{
                    position: "absolute",
                    top: "50%",
                    left: "50%",
                    transform: "translateY(-50%) translateX(-50%)",
                  }}
                  image={Empty.PRESENTED_IMAGE_SIMPLE}
                />
              </StyledTableRow>
            )}
          </Table>
        </TableContainer>
        {NumberPages > 0 && !loading && (
          <TableFooter className={Style.tableFooter}>
            <div style={{ opacity: 0 }} className={Style.tableFooter__item}>
              1
            </div>
            <Pagination
              sx={{
                ul: {
                  "& .Mui-selected": {
                    backgroundColor: "#2d3748",
                    color: "#fff",
                  },
                  "& .css-yuzg60-MuiButtonBase-root-MuiPaginationItem-root": {
                    backgroundColor: "E2E8F0 !important",
                  },
                },
              }}
              onChange={handleChangePage}
              count={NumberPages}
              page={pagination.pageNumber}
              showLastButton
              className={Style.tableFooter__item}
            />
            <RowPerPage
              RowPerPage={pagination.pageSize}
              setRowsPerPage={handleChangeRowsPerPage}
            />
          </TableFooter>
        )}
      </div>
    </>
  );
}

export default CustomizedTables;
