import { Button, InputAdornment, TextField } from "@mui/material";
import React, { useContext, useState } from "react";
import { FaSearch } from "react-icons/fa";
import {
  LuArrowBigDownDash,
  LuArrowBigUpDash,
  LuPlusCircle,
} from "react-icons/lu";
import { MdFilterList } from "react-icons/md";
import AddNewOtherTab from "./AddNewReserTab";
import FormFilter from "./FilterReserTab";
import { EmailTable, EmailTemplate, TableParams } from "./ReserTabConfigTable";
import FirstTabPanel from "../../EmailTable/FirstTabPanel";
import style from "../../EmailTable/style.module.scss";
// import axiosAuth from "../../api/axiosAuth";
import { createContext } from "react";
import ImportEmailTemplatePopup from "../../ImportEmailPopup";
import axios from "../.././../../../axiosAuth";

// eslint-disable-next-line react-refresh/only-export-components
export const context = createContext(null);

// eslint-disable-next-line react-refresh/only-export-components
export const useEmailContext = () => {
  const contextEmail = useContext(context);
  if (!context) {
  }
  return contextEmail;
};

// @ts-ignore
const backend_api: string = `${import.meta.env.VITE_API_HOST}:${
  import.meta.env.VITE_API_PORT
}`;

export default function OtherCategory() {
  const [change, setChange] = useState(0);
  const [value] = React.useState(0);
  const [, setEmailTemplates] = useState<EmailTemplate[]>([]);
  const [totalPage, setTotalPage] = useState<number>(0);
  const [updatedData, setUpdatedData] = useState<EmailTemplate[]>();
  const handleDataUpdate = (data: EmailTemplate[]) => {
    setEmailTemplates(data);
    setUpdatedData(data);
  };

  const [tableParams, setTableParams] = useState<TableParams>({
    pagination: {
      current: 1,
      pageSize: 10,
    },
  });
  const [searchParams, setSearchParams] = useState<Record<string, unknown>>({});

  const [isModalVisible3, setIsModalVisible3] = useState(false);
  const toggleModalFilter = () => {
    setIsModalVisible3((wasModalIsVisible) => !wasModalIsVisible);
  };

  const [isModalVisible6, setIsModalVisible6] = useState(false);
  const toggleModalCreate = () => {
    setIsModalVisible6((wasModalIsVisible) => !wasModalIsVisible);
  };

  const handleSearch = async (searchTerm: string) => {
    setSearchParams((prevSearchParams) => ({
      ...prevSearchParams,
      name: searchTerm,
      description: searchTerm,
    }));
  };

  let timeout: NodeJS.Timeout;

  const handleChangeSearch = (event: React.ChangeEvent<HTMLInputElement>) => {
    clearTimeout(timeout);

    timeout = setTimeout(() => {
      handleSearch(event.target.value);
    });
  };

  const handleExport = async () => {
    try {
      const response = await axios.get(
        `${backend_api}/api/Export/emailtemplates`,
        {
          responseType: "blob",
        }
      );
      const url = window.URL.createObjectURL(new Blob([response.data]));
      const link = document.createElement("a");
      link.href = url;
      link.setAttribute("download", "EmailTemplates.xlsx");
      link.click();
      link.parentNode.removeChild(link);
      window.URL.revokeObjectURL(url);
    } catch (error) {
      console.error("Export failed:", error);
    }
  };
  const [isImportModalVisible, setIsImportModalVisible] = useState(false);

  const toggleImportModal = () => {
    setIsImportModalVisible(!isImportModalVisible);
  };

  return (
    <context.Provider
      value={{
        change: change,
        setChange: setChange,
        totalPage: totalPage,
        setTotalPage: setTotalPage,
      }}
    >
      <FirstTabPanel value={value} index={0}>
        <div className={style.buttonContainer}>
          <div className={style.buttonSearch}>
            <TextField
              size="small"
              placeholder="Search by..."
              onChange={handleChangeSearch}
              id="outlined-start-adornment"
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <FaSearch color="#2D3748" />
                  </InputAdornment>
                ),
              }}
            />
            <Button
              onClick={toggleModalFilter}
              sx={{
                bgcolor: "#2D3748",
              }}
              variant="contained"
              className={style.buttonFilter}
              startIcon={<MdFilterList color="white" />}
            >
              Filter
            </Button>
            {isModalVisible3 && (
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
            )}
          </div>
          <div className={style.buttonExport_Create}>
            <div>
              <Button
                onClick={toggleImportModal}
                sx={{ bgcolor: "#2F913F", mr: 1 }}
                variant="contained"
                startIcon={<LuArrowBigUpDash color="white" />}
              >
                Import
              </Button>
              {isImportModalVisible && (
                <ImportEmailTemplatePopup onClose={toggleImportModal} />
              )}

              <Button
                onClick={handleExport}
                sx={{
                  bgcolor: "#d45b13",
                }}
                variant="contained"
                startIcon={<LuArrowBigDownDash />}
              >
                Export
              </Button>
            </div>

            <div className={style.buttonCreate}>
              <Button
                onClick={toggleModalCreate}
                sx={{
                  bgcolor: "#2D3748",
                }}
                variant="contained"
                startIcon={<LuPlusCircle color="white" />}
              >
                Add new
              </Button>
              {isModalVisible6 && (
                <div className={style.popup}>
                  <CreateFormPopUp onClose={toggleModalCreate} />
                </div>
              )}
            </div>
          </div>
        </div>
        <EmailTable
          onDataUpdate={handleDataUpdate}
          newDataUpdate={updatedData}
          tableParams={tableParams}
          setTableParams={setTableParams}
          searchParams={searchParams}
        />
      </FirstTabPanel>
    </context.Provider>
  );
}
