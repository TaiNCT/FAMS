// @ts-nocheck
import style from "./style.module.scss";
import StudentListItemList from "./student-list-item-list";
import { useEffect, useState } from "react";
import React from "react";
import { Popover } from "@mui/material";
import ColumnPopUp from "./ColumnPopUp/column-pop-up";
import FilterPopUp from "./FilterPopUp/filter-pop-up";
import { Link, useSearchParams } from "react-router-dom";
import CustomizedMenus from "../ViewSystemPage/ActionButton/index";
import { studentApi } from "../../config/axios";
import noDataImg from "../../assets/LogoStManagement/noData.png";
import GlobalLoading from "@/components/global/GlobalLoading";

const SystemViewPage: React.FC = () => {
  const [selectedPage, setSelectedPage] = useState<number>(1);
  const [search, setSearch] = useSearchParams();

  // SORT FUNCTION
  function handleSort(value: string) {
    if (search.get("sortBy") === value) {
      if (search.get("sortOrder") === "desc") {
        search.delete("sortBy");
        search.delete("sortOrder");
      } else {
        search.set(
          "sortOrder",
          search.get("sortOrder") === "asc" ? "desc" : "asc"
        );
      }
    } else {
      search.set("sortBy", value);
      search.set("sortOrder", "asc");
    }
    setSearch(`?${search.toString()}`, { replace: true });
  }

  // GET STUDENT LIST
  const [StudentList, setStudentList] = useState<any>();
  const [loading, setLoading] = useState<boolean>(true);
  const fetchStudentData = async () => {
    try {
      const response = await studentApi.get(
        `/GetAllStudents?${search.toString()}`
      );
      setStudentList(response.data.result);
      setLoading(false);
    } catch (error) {
      console.error("Error fetching student data:", error);
    }
  };
  useEffect(() => {
    fetchStudentData();
  }, [search]);

  // CHECK BOXES SETTINGS FOR STUDENT LIST
  const [selectedStudentIds, setSelectedStudentIds] = useState<string[]>([]);
  const handleCheckboxChange = (studentId: string, checked: boolean) => {
    setSelectedStudentIds((prevSelectedStudentIds) =>
      checked
        ? [...prevSelectedStudentIds, studentId]
        : prevSelectedStudentIds.filter((id) => id !== studentId)
    );
  };

  const fetchIdData = async () => {
    try {
      const response = await studentApi.get(`/GetAllId?${search.toString()}`);
      const newIds = response.data.result.filter(
        (id) => !selectedStudentIds.includes(id)
      );
      setSelectedStudentIds((prevIds) => [...prevIds, ...newIds]);
    } catch (error) {
      console.error("Error fetching student data:", error);
    }
  };

  const handleSelectAll = async (checked: boolean) => {
    if (checked) {
      await fetchIdData();
    } else {
      try {
        const response = await studentApi.get(`/GetAllId?${search.toString()}`);
        const fetchedIds = response.data.result;
        const uniqueSelectedIds = selectedStudentIds.filter(
          (id) => !fetchedIds.includes(id)
        );
        setSelectedStudentIds(uniqueSelectedIds);
      } catch (error) {
        console.error("Error fetching student data:", error);
      }
    }
  };

  // EXPORT EXCEL METHOD
  function HandleClickExport() {
    const keyword = search.get("keyword");
    const dob = search.get("dob");
    const gender = search.get("gender");
    const status = search.get("status");
    const sortBy = search.get("sortBy");
    const sortOrder = search.get("sortOrder");
    studentApi
      .get("ExportStudentSystemList", {
        params: {
          keyword: keyword,
          dob: dob,
          gender: gender,
          status: status,
          sortBy: sortBy,
          sortOrder: sortOrder,
        },
        responseType: "blob",
      })
      .then((res) => {
        const blob = new Blob([res.data], {
          type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        });
        const url = window.URL.createObjectURL(blob);
        const outputFilename = `${Date.now()}.xlsx`;
        const a = document.createElement("a");
        a.style.display = "none";
        a.href = url;
        a.setAttribute("download", outputFilename);
        document.body.appendChild(a);
        a.click();
        alert("your file has downloaded!");
      })
      .catch(function (e) {
      });
  }

  // SEARCH SETTINGS
  const searchKeyword = search.get("keyword") ?? "";
  const [searchInput, setSearchInput] = useState(searchKeyword);
  function handleSearch(text: string) {
    selectPage(1);
    search.set("keyword", text);
    setSearch(`?${search.toString()}`, { replace: true });
    setSearchInput(text);
  }
  // PAGINATION SETTINGS
  const selectPage = (value: number) => {
    setSelectedPage(value);
    search.set("pageNumber", String(value));
    setSearch(`?${search.toString()}`, { replace: true });
  };
  function handleRowsPerPageChange(value: number) {
    search.set("pageSize", String(value));
    setSearch(`?${search.toString()}`, { replace: true });
    search.set("pageNumber", String(1));
    setSearch(`?${search.toString()}`, { replace: true });
    selectPage(1);
  }
  // PAGE NUMBER DISPLAY SETTINGS
  let totalPageNumber = StudentList != null ? StudentList.totalCount : 0;
  const totalPage =
    search.get("pageSize") == null
      ? Math.ceil(totalPageNumber / 5)
      : Math.ceil(totalPageNumber / search.get("pageSize"));
  const firstPage = () => {
    selectPage(1);
  };
  const nextPage = () => {
    if (selectedPage != totalPage) {
      selectPage(selectedPage + 1);
    }
  };
  const previousPage = () => {
    if (selectedPage != 1) {
      selectPage(selectedPage - 1);
    }
  };
  const lastPage = () => {
    selectPage(totalPage);
  };
  const renderPageNumbers = () => {
    const pageNumbers = [];
    const renderDots = <span key="dots">.&nbsp;&nbsp;.&nbsp;&nbsp;.</span>;
    const renderLastPage = (
      <a key={totalPage} onClick={() => selectPage(totalPage)}>
        {totalPage}
      </a>
    );
    const renderFirstPage = (
      <a key={1} onClick={() => selectPage(1)}>
        {1}
      </a>
    );

    if (selectedPage === 1 && totalPage > 5) {
      for (let i = selectedPage; i <= selectedPage + 2; i++) {
        const isSelected = i === selectedPage;
        pageNumbers.push(
          <a
            key={i}
            className={isSelected ? style.selected : ""}
            onClick={() => selectPage(i)}
          >
            {i}
          </a>
        );
      }
      pageNumbers.push(renderDots);
      pageNumbers.push(renderLastPage);
    } else if (
      selectedPage > 1 &&
      selectedPage <= Math.ceil(totalPage / 2) &&
      totalPage > 5
    ) {
      for (let i = selectedPage - 1; i <= selectedPage + 1; i++) {
        const isSelected = i === selectedPage;
        pageNumbers.push(
          <a
            key={i}
            className={isSelected ? style.selected : ""}
            onClick={() => selectPage(i)}
          >
            {i}
          </a>
        );
      }
      pageNumbers.push(renderDots);
      pageNumbers.push(renderLastPage);
    } else if (
      selectedPage > Math.ceil(totalPage / 2) &&
      selectedPage != totalPage &&
      totalPage > 5
    ) {
      pageNumbers.push(renderFirstPage);
      pageNumbers.push(renderDots);
      for (let i = selectedPage - 1; i <= selectedPage + 1; i++) {
        const isSelected = i === selectedPage;
        pageNumbers.push(
          <a
            key={i}
            className={isSelected ? style.selected : ""}
            onClick={() => selectPage(i)}
          >
            {i}
          </a>
        );
      }
    } else if (selectedPage == totalPage && totalPage > 5) {
      pageNumbers.push(renderFirstPage);
      pageNumbers.push(renderDots);
      for (let i = totalPage - 2; i <= totalPage; i++) {
        const isSelected = i === selectedPage;
        pageNumbers.push(
          <a
            key={i}
            className={isSelected ? style.selected : ""}
            onClick={() => selectPage(i)}
          >
            {i}
          </a>
        );
      }
    } else if (totalPage <= 5) {
      for (let i = 1; i <= totalPage; i++) {
        const isSelected = i === selectedPage;
        pageNumbers.push(
          <a
            key={i}
            className={isSelected ? style.selected : ""}
            onClick={() => selectPage(i)}
          >
            {i}
          </a>
        );
      }
    }
    return pageNumbers;
  };

  // COLUMN POP-UP SETTINGS
  const [anchorEl, setAnchorEl] = React.useState<HTMLButtonElement | null>(
    null
  );
  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };
  const open = Boolean(anchorEl);
  const id = open ? "simple-popover" : undefined;

  // FILTER POP UP SETTINGS
  const [filterPopUp, setFilterPopUp] =
    React.useState<HTMLButtonElement | null>(null);
  const handleFilterClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setFilterPopUp(event.currentTarget);
  };
  const handleFilterClose = () => {
    setFilterPopUp(null);
  };
  const opennn = Boolean(filterPopUp);
  const iddd = opennn ? "simple-popover" : undefined;

  // COLUMN DISPLAY SETTINGS
  const [displayedColumns, setDisplayedColumns] = useState<string[]>([
    "Full name",
    "Date of birth",
    "Gender",
    "RECer",
    "Status",
  ]);

  const handleApplyColumns = (selectedColumns: string[]) => {
    setDisplayedColumns(selectedColumns);
  };

  const handleResetColumns = () => {
    setDisplayedColumns([
      "Full name",
      "Date of birth",
      "Gender",
      "RECer",
      "Status",
    ]);
  };

  if (loading) {
    return <GlobalLoading isLoading={loading} />;
  }

  return (
    <>
      <div className={style.main}>
        <div id={style.main_view}>
          <header>Student List</header>
          <div className={style.search_bar}>
            <div className={style.search_bar_left}>
              <input
                value={searchInput}
                onChange={(e) => handleSearch(e.target.value)}
                placeholder="Search by..."
                type="text"
              />
              <button aria-describedby={iddd} onClick={handleFilterClick}>
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="24"
                  height="24"
                  viewBox="0 0 24 24"
                  fill="none"
                >
                  <g clip-path="url(#clip0_20_1031)">
                    <path
                      d="M10 18H14V16H10V18ZM3 6V8H21V6H3ZM6 13H18V11H6V13Z"
                      fill="#DFDEDE"
                    />
                  </g>
                </svg>
                Filter
              </button>
              <Popover
                id={iddd}
                open={opennn}
                anchorEl={filterPopUp}
                onClose={handleFilterClose}
                anchorOrigin={{
                  vertical: "bottom",
                  horizontal: "left",
                }}
              >
                <FilterPopUp />
              </Popover>
            </div>
            <div className={style.search_bar_right}>
              <button onClick={HandleClickExport}>
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="24"
                  height="24"
                  viewBox="0 0 24 24"
                  fill="none"
                >
                  <g clip-path="url(#clip0_20_328)">
                    <path
                      d="M19 9H15V3H9V9H5L12 16L19 9ZM11 11V5H13V11H14.17L12 13.17L9.83 11H11ZM5 18H19V20H5V18Z"
                      fill="#DFDEDE"
                    />
                  </g>
                </svg>
                Export
              </button>
              <Link to="/add-student">
                <button>
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    width="24"
                    height="24"
                    viewBox="0 0 24 24"
                    fill="none"
                  >
                    <g clip-path="url(#clip0_20_1645)">
                      <path
                        d="M13 7H11V11H7V13H11V17H13V13H17V11H13V7ZM12 2C6.49 2 2 6.49 2 12C2 17.51 6.49 22 12 22C17.51 22 22 17.51 22 12C22 6.49 17.51 2 12 2ZM12 20C7.59 20 4 16.41 4 12C4 7.59 7.59 4 12 4C16.41 4 20 7.59 20 12C20 16.41 16.41 20 12 20Z"
                        fill="#DFDEDE"
                      />
                    </g>
                  </svg>
                  Add new
                </button>
              </Link>
              <CustomizedMenus selectedStudentIds={selectedStudentIds} />
            </div>
          </div>
          <div className={style.student_list}>
            {StudentList == null ? (
              <img src={noDataImg} />
            ) : (
              <table>
                <thead>
                  <tr>
                    <th>
                      <input
                        type="checkbox"
                        onChange={(e) => handleSelectAll(e.target.checked)}
                        checked={selectedStudentIds.length == totalPageNumber}
                      />
                    </th>
                    {displayedColumns.map((column) => (
                      <>
                        <th key={column}>
                          <button
                            onClick={() =>
                              handleSort(
                                column.replace(/\s+/g, "").toLowerCase()
                              )
                            }
                          >
                            <svg
                              xmlns="http://www.w3.org/2000/svg"
                              width="18"
                              height="18"
                              viewBox="0 0 18 18"
                              fill="none"
                            >
                              <g clip-path="url(#clip0_3214_19363)">
                                <path
                                  d="M5.70833 13.5H7.54167C7.79375 13.5 8 13.1625 8 12.75C8 12.3375 7.79375 12 7.54167 12H5.70833C5.45625 12 5.25 12.3375 5.25 12.75C5.25 13.1625 5.45625 13.5 5.70833 13.5ZM5.25 5.25C5.25 5.6625 5.45625 6 5.70833 6H13.0417C13.2937 6 13.5 5.6625 13.5 5.25C13.5 4.8375 13.2937 4.5 13.0417 4.5H5.70833C5.45625 4.5 5.25 4.8375 5.25 5.25ZM5.70833 9.75H10.2917C10.5438 9.75 10.75 9.4125 10.75 9C10.75 8.5875 10.5438 8.25 10.2917 8.25H5.70833C5.45625 8.25 5.25 8.5875 5.25 9C5.25 9.4125 5.45625 9.75 5.70833 9.75Z"
                                  fill="#DFDEDE"
                                />
                              </g>
                            </svg>
                          </button>
                          <span>{column}</span>
                        </th>
                      </>
                    ))}
                    <th>
                      <button aria-describedby={id} onClick={handleClick}>
                        <svg
                          xmlns="http://www.w3.org/2000/svg"
                          width="24"
                          height="24"
                          viewBox="0 0 24 24"
                          fill="none"
                        >
                          <g clip-path="url(#clip0_3479_6375)">
                            <path
                              d="M19.43 12.98C19.47 12.66 19.5 12.34 19.5 12C19.5 11.66 19.47 11.34 19.43 11.02L21.54 9.37C21.73 9.22 21.78 8.95 21.66 8.73L19.66 5.27C19.57 5.11 19.4 5.02 19.22 5.02C19.16 5.02 19.1 5.03 19.05 5.05L16.56 6.05C16.04 5.65 15.48 5.32 14.87 5.07L14.49 2.42C14.46 2.18 14.25 2 14 2H9.99999C9.74999 2 9.53999 2.18 9.50999 2.42L9.12999 5.07C8.51999 5.32 7.95999 5.66 7.43999 6.05L4.94999 5.05C4.88999 5.03 4.82999 5.02 4.76999 5.02C4.59999 5.02 4.42999 5.11 4.33999 5.27L2.33999 8.73C2.20999 8.95 2.26999 9.22 2.45999 9.37L4.56999 11.02C4.52999 11.34 4.49999 11.67 4.49999 12C4.49999 12.33 4.52999 12.66 4.56999 12.98L2.45999 14.63C2.26999 14.78 2.21999 15.05 2.33999 15.27L4.33999 18.73C4.42999 18.89 4.59999 18.98 4.77999 18.98C4.83999 18.98 4.89999 18.97 4.94999 18.95L7.43999 17.95C7.95999 18.35 8.51999 18.68 9.12999 18.93L9.50999 21.58C9.53999 21.82 9.74999 22 9.99999 22H14C14.25 22 14.46 21.82 14.49 21.58L14.87 18.93C15.48 18.68 16.04 18.34 16.56 17.95L19.05 18.95C19.11 18.97 19.17 18.98 19.23 18.98C19.4 18.98 19.57 18.89 19.66 18.73L21.66 15.27C21.78 15.05 21.73 14.78 21.54 14.63L19.43 12.98ZM17.45 11.27C17.49 11.58 17.5 11.79 17.5 12C17.5 12.21 17.48 12.43 17.45 12.73L17.31 13.86L18.2 14.56L19.28 15.4L18.58 16.61L17.31 16.1L16.27 15.68L15.37 16.36C14.94 16.68 14.53 16.92 14.12 17.09L13.06 17.52L12.9 18.65L12.7 20H11.3L11.11 18.65L10.95 17.52L9.88999 17.09C9.45999 16.91 9.05999 16.68 8.65999 16.38L7.74999 15.68L6.68999 16.11L5.41999 16.62L4.71999 15.41L5.79999 14.57L6.68999 13.87L6.54999 12.74C6.51999 12.43 6.49999 12.2 6.49999 12C6.49999 11.8 6.51999 11.57 6.54999 11.27L6.68999 10.14L5.79999 9.44L4.71999 8.6L5.41999 7.39L6.68999 7.9L7.72999 8.32L8.62999 7.64C9.05999 7.32 9.46999 7.08 9.87999 6.91L10.94 6.48L11.1 5.35L11.3 4H12.69L12.88 5.35L13.04 6.48L14.1 6.91C14.53 7.09 14.93 7.32 15.33 7.62L16.24 8.32L17.3 7.89L18.57 7.38L19.27 8.59L18.2 9.44L17.31 10.14L17.45 11.27ZM12 8C9.78999 8 7.99999 9.79 7.99999 12C7.99999 14.21 9.78999 16 12 16C14.21 16 16 14.21 16 12C16 9.79 14.21 8 12 8ZM12 14C10.9 14 9.99999 13.1 9.99999 12C9.99999 10.9 10.9 10 12 10C13.1 10 14 10.9 14 12C14 13.1 13.1 14 12 14Z"
                              fill="#EDF2F7"
                            />
                          </g>
                        </svg>
                      </button>
                      <Popover
                        id={id}
                        open={open}
                        anchorEl={anchorEl}
                        onClose={handleClose}
                        anchorOrigin={{
                          vertical: "bottom",
                          horizontal: "left",
                        }}
                      >
                        <ColumnPopUp
                          onApply={handleApplyColumns}
                          onReset={handleResetColumns}
                        />
                      </Popover>
                    </th>
                  </tr>
                </thead>
                <tbody>
                  <StudentListItemList
                    uri={`/GetAllStudents?${search.toString()}`}
                    displayedColumns={displayedColumns}
                    onCheckboxChange={handleCheckboxChange}
                    selectedIds={selectedStudentIds}
                  />
                </tbody>
              </table>
            )}
          </div>

          <div className={style.page_number_container}>
            <div style={{ width: 100 }}></div>
            <div className={style.page_number}>
              <button onClick={firstPage}>
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="24"
                  height="24"
                  viewBox="0 0 24 24"
                  fill="none"
                >
                  <g
                    clip-path="url(#clip0_20_1187)"
                    transform="rotate(180, 12, 12)"
                  >
                    <path
                      d="M5.59 7.41L10.18 12L5.59 16.59L7 18L13 12L7 6L5.59 7.41ZM16 6H18V18H16V6Z"
                      fill="#2D3748"
                    />
                  </g>
                </svg>
              </button>
              <button onClick={previousPage}>
                <svg
                  xmlns="http://www.w3.org   /2000/svg"
                  width="24"
                  height="24"
                  viewBox="0 0 24 24"
                  fill="none"
                >
                  <g clip-path="url(#clip0_20_1165)">
                    <path
                      d="M16 7.16212L14.9323 6L9 12.5L14.9383 19L16 17.8379L11.1234 12.5L16 7.16212Z"
                      fill="#2D3748"
                    />
                  </g>
                </svg>
              </button>
              {renderPageNumbers()}
              <button onClick={nextPage}>
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="24"
                  height="24"
                  viewBox="0 0 24 24"
                  fill="none"
                >
                  <g clip-path="url(#clip0_20_1184)">
                    <path
                      d="M9 16.8149L10.0617 17.8766L16 11.9383L10.0617 6L9 7.0617L13.8766 11.9383L9 16.8149Z"
                      fill="#2D3748"
                    />
                  </g>
                </svg>
              </button>
              <button onClick={lastPage}>
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="24"
                  height="24"
                  viewBox="0 0 24 24"
                  fill="none"
                >
                  <g clip-path="url(#clip0_20_1187)">
                    <path
                      d="M5.59 7.41L10.18 12L5.59 16.59L7 18L13 12L7 6L5.59 7.41ZM16 6H18V18H16V6Z"
                      fill="#2D3748"
                    />
                  </g>
                </svg>
              </button>
            </div>
            <div className={style.rows_per_page}>
              <span>Rows per page</span>
              &nbsp;&nbsp;
              <select
                onChange={(e) =>
                  handleRowsPerPageChange(parseInt(e.target.value))
                }
              >
                <option value="5">5</option>
                <option value="10">10</option>
                <option value="15">15</option>
                <option value="20">20</option>
              </select>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default SystemViewPage;
