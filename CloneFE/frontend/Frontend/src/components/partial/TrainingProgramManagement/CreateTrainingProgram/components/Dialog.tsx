import React, { useState, useCallback } from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import Box from "@mui/material/Box";
import style from "../style.module.scss";
import { IoMdCloseCircleOutline } from "react-icons/io";
import FormControl from "@mui/material/FormControl";
import { toast } from "react-toastify";
import { ChakraProvider } from "@chakra-ui/react";
import {
  uploadCSVFile,
  paginationList,
} from "../../TrainingProgramList/services/TrainingProgramService";
import { useTrainingProgramDataContext } from "../../contexts/DataContext";
import { getOptions } from "../../TrainingProgramList/utils/functionHelper";
import Loading from "../../components/Loading/Loading";

interface ImportDialogProps {
  open: boolean;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

interface FormData {
  file: File | null;
  encodingType: string;
  columnSeperator: string;
  scanning: string[];
  duplicateHandle: string;
}

const initialFormData: FormData = {
  file: null,
  encodingType: "Auto detect",
  columnSeperator: ",",
  scanning: ["Program ID"],
  duplicateHandle: "Allow",
};

const ImportDialog: React.FC<ImportDialogProps> = (props) => {
  // Import training program
  const [formData, setFormData] = useState(initialFormData);
  const [showDuplicateHandleField, setShowDuplicateHandleField] =
    useState(false);
  const [messDuplicate, setMessDuplicate] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(false);
  const {
    setData,
    setTotalPages,
    setCurrentPage,
    setRowsPerPageOption,
    rowsPerPage,
    currentPage,
  } = useTrainingProgramDataContext();

  const handleFieldChange = useCallback(
    (event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
      const target = event.target as HTMLInputElement | HTMLSelectElement;
      const value =
        target instanceof HTMLInputElement && target.type === "checkbox"
          ? target.checked
          : target.value;

      setFormData((prevFormData) => {
        if (target instanceof HTMLInputElement && target.type === "file") {
          return {
            ...prevFormData,
            file: target.files ? target.files[0] : null,
          };
        }

        if (target.type === "checkbox") {
          const newScanningValues = value
            ? [...prevFormData.scanning, target.value]
            : prevFormData.scanning.filter((item) => item !== target.value);
          return { ...prevFormData, scanning: newScanningValues };
        }

        return { ...prevFormData, [target.name]: target.value };
      });
    },
    []
  );

  const handleResetAllFields = useCallback(() => {
    setMessDuplicate("");
    setFormData(initialFormData);
  }, []);

  const handleClose = () => {
    handleResetAllFields();
    setShowDuplicateHandleField(false);
    props.setOpen(false);

    if (error) setError(false);
  };

  const handleConfirmError = () => {
    setError(false);
    setShowDuplicateHandleField(false);
    setMessDuplicate("");
  };

  const handleImport = async (duplicateHandleValue?: string) => {
    setIsLoading(true);
    try {
      const { file, encodingType, columnSeperator, scanning } = formData;
      if (!file) {
        toast.error("Please select a file.");
        setIsLoading(false);
        return;
      }

      if (scanning.length === 0) {
        toast.error("Please select at least one scanning option.");
        setIsLoading(false);
        return;
      }

      const uploadData = new FormData();
      uploadData.append("File", file);
      uploadData.append("CreatedBy", localStorage.getItem("username"));
      uploadData.append("EncodingType", encodingType);
      uploadData.append("ColumnSeperator", columnSeperator);
      const scanningAsString = formData.scanning.join(",");

      uploadData.append("Scanning", scanningAsString);
      if (duplicateHandleValue != null) {
        uploadData.append("DuplicateHandle", duplicateHandleValue);
      }
      const result = await uploadCSVFile(uploadData);

      setTimeout(async () => {
        if (result.statusCode === 200) {
          handleClose();
          const newList = await paginationList(1, rowsPerPage);
          setData(newList.list);
          setCurrentPage(1);
          setTotalPages(newList.totalPage);
          setRowsPerPageOption(getOptions(newList.totalRecord));
          toast.success(`${result.message}`, {
            autoClose: 2500,
          });
          setIsLoading(false);
        } else if (result.statusCode === 409) {
          setShowDuplicateHandleField(true);
          setIsLoading(false);
          setMessDuplicate(result.message);
        } else if (result.statusCode === 400 && result.errors != null) {
          setShowDuplicateHandleField(true);
          setIsLoading(false);
          setError(true);

          let msg = "";
          for (const fieldName in result.errors) {
            if (
              Object.prototype.hasOwnProperty.call(result.errors, fieldName)
            ) {
              const errorMessages = result.errors[fieldName];

              msg += `${result.message}, with ${fieldName}: \n`;
              errorMessages.forEach((err) => {
                msg += `\n${err}`;
              });
            }
          }
          setMessDuplicate(msg);
        } else {
          setIsLoading(false);
          // toast.error(`${result.message}`, {
          //   autoClose: 2500,
          // });
        }
      }, 1000);
    } catch (error) {
      toast.error(`An error occurred: ${error}`, {
        autoClose: 2500,
      });
    }
  };

  const handleConfirm = () => {
    handleImport(formData.duplicateHandle);
  };

  return (
    <React.Fragment>
      {isLoading ? (
        <ChakraProvider>
          <Loading />
        </ChakraProvider>
      ) : (
        <Dialog
          fullWidth={true}
          maxWidth={"sm"}
          open={props.open}
          onClose={handleClose}
          className={style.dialog}
          sx={{ overflowX: "hidden" }}
        >
          <DialogTitle className={style.dialogTitle}>
            <p></p>
            <span>Import Training Programs</span>
            <IoMdCloseCircleOutline
              size={28}
              onClick={handleClose}
              cursor={"pointer"}
            />
          </DialogTitle>
          <DialogContent>
            <DialogContentText
              className={`${style.dialogContentText} ${
                showDuplicateHandleField ? style.formElementHide : ""
              }`}
            >
              <span>Import Settings</span>
            </DialogContentText>
            <Box
              noValidate
              component="form"
              sx={{
                display: "flex",
                flexDirection: "column",
                m: "auto",
                width: "fit-content",
              }}
            >
              <div className={style.dialogForm}>
                <FormControl
                  onSubmit={(e) => e.preventDefault()}
                  sx={{ mt: 2, minWidth: 500, mr: 0 }}
                >
                  <div
                    className={`${style.formElement} ${
                      showDuplicateHandleField ? style.formElementHide : ""
                    }`}
                  >
                    <label htmlFor="file">File(csv)</label>
                    <div className={style.formInputFile}>
                      {formData.file ? (
                        <div className={style.formRemoveFile}>
                          <Button
                            variant="contained"
                            component="label"
                            className={style.inputFileBtn}
                            onClick={() => (formData.file = null)}
                          >
                            Remove
                          </Button>
                          <p>{formData.file.name}</p>
                        </div>
                      ) : (
                        <Button
                          variant="contained"
                          component="label"
                          className={style.inputFileBtn}
                        >
                          Select
                          <input
                            type="file"
                            accept=".csv"
                            name="file"
                            hidden
                            onChange={handleFieldChange}
                          />
                        </Button>
                      )}
                    </div>
                  </div>
                  <div
                    className={`${style.formElement} ${
                      showDuplicateHandleField ? style.formElementHide : ""
                    }`}
                  >
                    <label htmlFor="encoding-type">Encoding Type </label>
                    <div className={style.formSelect}>
                      <select
                        id="encoding-type"
                        name="encodingType"
                        value={formData.encodingType}
                        onChange={handleFieldChange}
                      >
                        <option value="Auto Detect">Auto Detect</option>
                        <option value="UTF8">UTF8</option>
                        <option value="ASCII">ASCII</option>
                      </select>
                    </div>
                  </div>
                  <div
                    className={`${style.formElement} ${
                      showDuplicateHandleField ? style.formElementHide : ""
                    }`}
                  >
                    <label htmlFor="columnSeperator">Column Seperator </label>
                    <div className={style.formSelect}>
                      <select
                        id="columnSeperator"
                        name="columnSeperator"
                        value={formData.columnSeperator}
                        onChange={handleFieldChange}
                      >
                        <option value=",">Comma</option>
                        <option value=";">Semicolon</option>
                        <option value=".">Dot</option>
                        <option value="!">Tab</option>
                      </select>
                    </div>
                  </div>
                  <div
                    className={`${style.formElement} ${
                      showDuplicateHandleField ? style.formElementHide : ""
                    }`}
                    style={{ marginBottom: "2rem" }}
                  >
                    <label htmlFor="template">Import Template </label>
                    <div className={style.formSelect}>
                      <a
                        href="https://bird-farm-shop.s3.ap-southeast-1.amazonaws.com/import_trainningprogramm.xlsx"
                        className={style.importTemplate}
                      >
                        Download
                      </a>
                    </div>
                  </div>
                  <div>
                    {messDuplicate &&
                      messDuplicate.split("\n").map((part, index) => (
                        <p className={style.errorMsgDuplicate} key={index}>
                          {part.trim()}
                        </p>
                      ))}
                  </div>
                  {!error && (
                    <>
                      <DialogContentText className={style.dialogContentText}>
                        <span>Duplicate Control</span>
                      </DialogContentText>
                      <div
                        className={`${style.formElement} ${
                          showDuplicateHandleField ? style.formElementHide : ""
                        }`}
                      >
                        <label htmlFor="scanning">Scanning</label>
                        <div className={style.scanningElement}>
                          <input
                            checked={formData.scanning.includes("Program ID")}
                            type="checkbox"
                            value="Program ID"
                            id="programID"
                            name="scanning"
                            onChange={handleFieldChange}
                          />
                          <label htmlFor="programID">Program ID</label>
                        </div>
                        <div className={style.scanningElement}>
                          <input
                            type="checkbox"
                            value="Program Name"
                            id="programName"
                            name="scanning"
                            onChange={handleFieldChange}
                          />
                          <label htmlFor="programName">Program Name</label>
                        </div>
                      </div>
                      <div
                        className={`${style.formElement} ${
                          showDuplicateHandleField ? "" : style.formElementHide
                        }`}
                      >
                        <label htmlFor="duplicate">Duplicate Handle</label>
                        <div className={style.duplicateElement}>
                          <input
                            checked={formData.duplicateHandle === "Allow"}
                            type="radio"
                            name="duplicateHandle"
                            id="allow"
                            value="Allow"
                            onChange={handleFieldChange}
                          />
                          <label htmlFor="allow">Allow</label>
                          <input
                            checked={formData.duplicateHandle === "Replace"}
                            type="radio"
                            name="duplicateHandle"
                            id="replace"
                            value="Replace"
                            onChange={handleFieldChange}
                          />
                          <label htmlFor="replace">Replace</label>
                          <input
                            checked={formData.duplicateHandle === "Skip"}
                            type="radio"
                            name="duplicateHandle"
                            id="skip"
                            value="Skip"
                            onChange={handleFieldChange}
                          />
                          <label htmlFor="skip">Skip</label>
                        </div>
                      </div>
                    </>
                  )}
                </FormControl>
              </div>
            </Box>
          </DialogContent>
          {!error ? (
            <>
              <DialogActions className={style.dialogAction}>
                <Button onClick={handleClose} className={style.btnCancel}>
                  Cancel
                </Button>
                <Button
                  onClick={() => handleImport()}
                  className={`${style.btnImport} ${
                    showDuplicateHandleField ? style.formElementHide : ""
                  }`}
                >
                  Import
                </Button>
                <Button
                  onClick={() => handleConfirm()}
                  className={`${style.btnImport} ${
                    showDuplicateHandleField ? "" : style.formElementHide
                  }`}
                >
                  Confirm
                </Button>
              </DialogActions>
            </>
          ) : (
            <>
              <Button
                onClick={() => {
                  handleConfirmError();
                  handleClose();
                }}
                className={`${style.btnImport} ${
                  showDuplicateHandleField ? "" : style.formElementHide
                }`}
              >
                Confirm Error
              </Button>
            </>
          )}
        </Dialog>
      )}
    </React.Fragment>
  );
};

export default ImportDialog;
