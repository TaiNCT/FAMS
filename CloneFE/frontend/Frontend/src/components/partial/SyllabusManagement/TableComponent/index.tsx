// @ts-nocheck
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { SyllabusList } from "../types";
import { Chip, ThemeProvider, createTheme } from "@mui/material";
import SortIcon from "@mui/icons-material/Sort";
import styled from "@emotion/styled";
import {
  Box,
  PopoverContent,
  Button,
  PopoverTrigger,
  Popover,
} from "@chakra-ui/react";
import { MdOutlineMoreHoriz } from "react-icons/md";
import { useEffect, useState } from "react";
import RowsPerPage from "../layouts/RowPerPage";
import PaginationButtons from "../layouts/Paginate";
import ImportButton from "../layouts/ImportButton";
import ApiClient from "../ApiClient";
import { create } from "zustand";
import ActionMenu from "../layouts/ActionMenu";
import { useNavigate } from "react-router-dom";
import { Empty } from "antd";
import GlobalLoading from '../../../global/GlobalLoading'

const WhiteSortIcon = styled(SortIcon)({
  color: "#DFDEDE",
});

let apiClient = new ApiClient();

type State = {
  rows: SyllabusList[];
  setRows: (rows: SyllabusList[]) => void;
};

type Loading = {
  isLoading: boolean;
  setIsLoading: (isLoading: boolean) => void;
};

export const useSyllabusStore = create<State>((set) => ({
  rows: [],
  setRows: (rows) => set({ rows }),
}));

export const useLoading = create<Loading>((set) => ({
  isLoading: false,
  setIsLoading: (isLoading) => set({ isLoading }),
}));

const theme = createTheme({
  palette: {
    primary: {
      main: "#285D9A",
    },
    secondary: {
      main: "#DFDEDE",
    },
  },
  typography: {
    fontFamily: ["Inter"].join(","),
  },
  breakpoints: {
    values: {
      xs: 0,
      sm: 600,
      md: 900,
      lg: 1200,
      xl: 1536,
    },
  },
});

const columns: GridColDef[] = [
  {
    field: "topicName",
    headerName: "Syllabus",
    flex: 0.25,
    renderCell: (params) => {
      return <strong>{params.value}</strong>;
    },
  },
  { field: "topicCode", headerName: "Code", flex: 0.1 },
  {
    field: "createdDate",
    headerName: "Created On",
    flex: 0.1,
    renderCell(params) {
      const date = new Date(params.value);
      return `${date.toLocaleDateString()}`;
    },
  },
  { field: "createdBy", headerName: "Created By", flex: 0.2 },
  {
    field: "days",
    headerName: "Duration",
    flex: 0.1,
    renderCell(params) {
      return `${params.value} days`;
    },
  },
  {
    field: "attendeeNumber",
    headerName: "Attendee number",
    flex: 0.15,
    renderCell: (params) => {
      if (params.value === null)
        return (
          <Box
            as="span"
            key={params.value}
            justifyContent="center"
            bgColor="#285D9A"
            color={"white"}
            py={1}
            px={3}
            borderRadius="15px"
          >
            N/A
          </Box>
        );
      return `${params.value}`;
    },
  },
  {
    field: "status",
    headerName: "Status",
    flex: 0.1,
    renderCell: (params) => {
      if (params.value.toString().toLowerCase() === "active")
        return (
          <Chip
            style={{
              width: "76px",
              height: "27px",
              padding: "5px, 15px, 5px, 15px",
              background:"#2f913f",
              color:"#fff"
            }}
            label={"Active"}
            key={params.value}
          />
        );
      else if (params.value.toString().toLowerCase() === "inactive")
        return (
          <Chip
            style={{
              width: "76px",
              height: "27px",
              padding: "5px, 15px, 5px, 15px",
              background:"#2d3748",
              color:"#fff"
            }}
            label={"Inactive"}
            key={params.value}
          />
        );
      return (
        <Box
          as="span"
          key={params.value}
          justifyContent="center"
          bgColor="#285D9A"
          color={"white"}
          py={1}
          px={"1.5em"}
          width={"76px"}
          height={"27px"}
          borderRadius="15px"
        >
          draft
        </Box>
      );
    },
  },
  {
    field: "more_icon",
    headerName: "",
    flex: 0.05,
    sortable: false,
    renderCell: (params) => {
      const { isLoading, setIsLoading } = useLoading();

      const handleDelete = async (syllabusId: string) => {
        try {
          const response = await apiClient.deleteSyllabus(syllabusId);
          if (response) setIsLoading(true);
        } catch (error) {
          console.error(error);
        }
      };

      const handleDuplicate = async () => {
        const rowValue = params.row;
        const timestamp: number = new Date().getTime();


        const createTopicCode = `${rowValue.topicName
          .split(" ")
          .map((word) => word.charAt(0).toUpperCase())
          .join("")}_${timestamp}`;

        try {
          const response = await apiClient.duplicateSyllabus(
            rowValue.syllabusId,
            createTopicCode
          );
          if (response) setIsLoading(true);
        } catch (error) {
          console.error(error);
        }
      };
      return (
        <Popover>
          <PopoverTrigger>
            <Button
              key={params.value}
              cursor="pointer"
              fontSize="25px"
              bg="none"
              borderRadius="full"
              p={0}
            >
              <MdOutlineMoreHoriz />
            </Button>
          </PopoverTrigger>
          <PopoverContent zIndex={10}>
            <ActionMenu
              params={params.id}
              onConfirmDelete={handleDelete}
              handleDuplicate={handleDuplicate}
              status={params.row.status}
            />
          </PopoverContent>
        </Popover>
      );
    },
  },
];

export default function DataTable({
  startDate,
  endDate,
  tags,
  showImportModal,
  handleOnclose,
  searchValue,
  rows,
  setRows,
}: {
  rows: SyllabusList[];
  setRows: (rows: SyllabusList[]) => void;
  startDate?: Date;
  endDate?: Date;
  showImportModal?: boolean;
  handleOnclose?: any;
  tags: Array<string>;
  searchValue: string;
}) {
  const { isLoading, setIsLoading } = useLoading();
  const [rowsPerPage, setRowsPerPage] = useState(25);
  const navigate = useNavigate();
  let apiClient = new ApiClient();

  useEffect(() => {
    apiClient.getToken();
  }, []);

  const handleCellClick = (params: { id: any }) => {
    // Extract the id from the row data and process it as needed
    const id = params.id;
    navigate(`/syllabus/detail/${id}`);
  };

  const [zIndex, setZIndex] = useState(1);

  useEffect(() => {
    const checkVisibility = () => {
      const dayPicker = document.querySelector(".css-l13vig") as HTMLElement;
      if (dayPicker && dayPicker.offsetParent !== null) {
        setZIndex(-1);
      } else {
        setZIndex(1);
      }
    };

    checkVisibility();
    // Run checkVisibility whenever the DOM updates
    const observer = new MutationObserver(checkVisibility);
    observer.observe(document, { childList: true, subtree: true });

    // Clean up the observer when the component unmounts
    return () => observer.disconnect();
  }, []);

  return (
    <>
      {isLoading ? <GlobalLoading isLoading={isLoading} /> : null}
      <Box
        className="mb-2"
        zIndex={zIndex}
        position={"relative"}
        style={{ width: "100%" }}
      >
        {rows.length === 0 ? (
          <Empty image={Empty.PRESENTED_IMAGE_SIMPLE} />
        ) : (
          <DataGrid
            sx={{
              "& .MuiDataGrid-columnHeader": {
                backgroundColor: "#2D3748",
                color: "#ffffff",
                border: "none",
                // padding: '5px 20px',
              },
              "& .MuiDataGrid-columnSeparator": {
                display: "none",
              },
              "& .MuiDataGrid-columnHeader:first-of-type": {
                borderTopLeftRadius: "10px",
              },
              "& .MuiDataGrid-columnHeader:last-child": {
                borderTopRightRadius: "10px",
              },
              "& .MuiDataGrid-row": {
                border: "0.5px solid #2D3748;",
                color: "#2D3748",
                fontWeight: "500",
                backgroundColor: "#ffffff",
              },
              ".MuiDataGrid-iconButtonContainer": {
                visibility: "visible",
              },
              ".MuiDataGrid-sortIcon": {
                opacity: "inherit !important",
              },
              "& .MuiDataGrid-row:hover": {
                backgroundColor: "transparent", // Or 'transparent' or whatever color you'd like
              },
              "& .MuiDataGrid-Header:hover": {
                backgroundColor: "transparent", // Or 'transparent' or whatever color you'd like
              },
            }}
            rows={rows}
            columns={columns}
            columnHeaderHeight={40}
            slots={{
              columnSortedAscendingIcon: WhiteSortIcon,
              columnSortedDescendingIcon: WhiteSortIcon,
            }}
            showCellVerticalBorder={false}
            pageSizeOptions={[5, 10, 15]}
            autoHeight
            loading={isLoading}
            disableColumnMenu
            hideFooter
            onCellDoubleClick={handleCellClick}
          />
        )}

        <ThemeProvider theme={theme}>
          <RowsPerPage setRowsPerPage={setRowsPerPage} />
          <PaginationButtons
            searchValue={searchValue}
            isLoading={isLoading}
            setIsLoading={setIsLoading}
            rows={rows}
            setRows={setRows}
            rowsPerPage={rowsPerPage}
            startDate={startDate}
            endDate={endDate}
            tags={tags}
          />
          <ImportButton
            onClose={handleOnclose}
            visible={showImportModal ?? false}
            setIsLoading={setIsLoading}
          />
        </ThemeProvider>
      </Box>
    </>

  );
}
