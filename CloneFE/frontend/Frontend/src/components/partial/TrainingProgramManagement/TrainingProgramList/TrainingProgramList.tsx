import { Box } from "@chakra-ui/react";
import React, { useState, useEffect, useCallback, useRef } from "react";
import * as TrainingProgramService from "./services/TrainingProgramService";
import Table from "./components/Table";
import Header from "./components/Header";
import NavigationDot from "../components/NavigationDot/NavigationDot";
import style from "./style.module.scss";
import { TrainingProgram } from "./models/TrainingProgram.model";
import { TrainingProgramDataContext } from "../contexts/DataContext";
import { getOptions } from "./utils/functionHelper";
import { Empty } from "antd";

const TrainingProgramList: React.FC = () => {
  // Context properties
  const [data, setData] = useState<TrainingProgram[]>([]);
  const [tags, setTags] = useState<string[]>([]);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [rowsPerPage, setRowsPerPage] = useState<number>(5);
  const [rowsPerPageOption, setRowsPerPageOption] = useState<number[]>([5]);
  const [totalPages, setTotalPages] = useState<number>(1);
  const [sort, setSort] = useState<string>("");
  const [rotation, setRotation] = useState<number>(0);
  const [selectedColumn, setSelectedColumn] = useState<string>("");
  const [oldSelectedColumn, setOldSelectedColumn] = useState<string>("");
  const [searchKeyword, setSearchKeyword] = useState<string>("");
  const [filterValues, setFilterValues] = useState<
    Record<string, string | string[]>
  >({});
  const filterClearRef = useRef<any>();

  const fetchData = useCallback(async () => {
    try {
      const FilterhasValues = Object.values(filterValues).some((value) => {
        if (Array.isArray(value)) {
          return value.length > 0;
        } else {
          return value !== "";
        }
      });
      let result;
      if (sort && !searchKeyword && !FilterhasValues) {
        result = await TrainingProgramService.sorting(
          sort,
          currentPage,
          rowsPerPage
        );
      } else if (searchKeyword) {
        result = await TrainingProgramService.searching(
          searchKeyword,
          currentPage,
          rowsPerPage,
          sort
        );
      } else if (FilterhasValues) {
        result = await TrainingProgramService.filtering(
          filterValues,
          currentPage,
          rowsPerPage,
          sort
        );
      } else if (!sort || !searchKeyword || !FilterhasValues) {
        result = await TrainingProgramService.paginationList(
          currentPage,
          rowsPerPage
        );
      }
      if (result && result.list != null) {
        setData(result.list);
        setTotalPages(result.totalPage);
        setRowsPerPageOption(getOptions(result.totalRecord));
      } else {
        setData([]);
        setTotalPages(1);
      }
    } catch (error) {
      console.error("Lỗi khi lấy dữ liệu:", error);
    }
  }, [sort, currentPage, rowsPerPage, searchKeyword, filterValues]);

  useEffect(() => {
    fetchData();
  }, [fetchData]);

  const handlePageChange = useCallback(
    (page: number) => {
      setCurrentPage(page);
    },
    [setCurrentPage]
  );

  const handleRowsPerPageChange = useCallback(
    (newRowsPerPage: number) => {
      setCurrentPage(1);
      setRowsPerPage(newRowsPerPage);
    },
    [setCurrentPage, setRowsPerPage]
  );

  const handleSort = useCallback(
    (sortField: string) => {
      const newSort = sort === `-${sortField}` ? sortField : `-${sortField}`;
      setSort(newSort);
    },
    [sort, setSort]
  );

  const handleResetTable = useCallback(
    (filter = false) => {
      setCurrentPage(1);
      setSort("");
      setSelectedColumn("");
      setOldSelectedColumn("");
      setRotation(0);
      if (filter) {
        setFilterValues({});
      }
    },
    [filterValues, setFilterValues]
  );

  const handleFilter = useCallback(
    (values: Record<string, string | string[]>) => {
      setSearchKeyword("");
      setFilterValues(values);
      handleResetTable();
    },
    [filterValues, setFilterValues]
  );

  const handleSearch = useCallback(
    (value: string) => {
      setFilterValues({});
      setSearchKeyword(value);
      handleResetTable();
      if (filterClearRef.current) {
        filterClearRef.current.handleClearFilter();
      }
    },
    [tags, setTags]
  );

  return (
    <main className={style.main}>
      <TrainingProgramDataContext.Provider
        value={{
          setData,
          setCurrentPage,
          setTotalPages,
          setRowsPerPageOption,
          currentPage,
          rowsPerPage,
        }}
      >
        <Box pt={0.5} mb={5}>
          <Header
            searchKeyword={searchKeyword}
            tags={tags}
            setTags={setTags}
            onFilter={handleFilter}
            handleSearch={handleSearch}
            handleResetTable={handleResetTable}
            filterClearRef={filterClearRef}
            filterValues={filterValues}
            sort={sort}
          />
        </Box>
        <Box>
          {data && data.length > 0 ? (
            <Box>
              <Table
                data={data}
                handleSort={handleSort}
                rotation={rotation}
                setRotation={setRotation}
                setSelectedColumn={setSelectedColumn}
                selectedColumn={selectedColumn}
                setOldSelectedColumn={setOldSelectedColumn}
                oldSelectedColumn={oldSelectedColumn}
              />
            </Box>
          ) : (
            <div>
              <Empty image={Empty.PRESENTED_IMAGE_SIMPLE} />
            </div>
          )}
        </Box>
        <NavigationDot
          totalPages={totalPages}
          currentPage={currentPage}
          onPageChange={handlePageChange}
          rowsPerPageOptions={rowsPerPageOption}
          onRowsPerPageChange={handleRowsPerPageChange}
        />
      </TrainingProgramDataContext.Provider>
    </main>
  );
};

export default TrainingProgramList;
