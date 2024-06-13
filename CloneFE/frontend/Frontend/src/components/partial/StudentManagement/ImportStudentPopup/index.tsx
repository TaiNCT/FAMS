import styles from "./style.module.scss";
import React, { useState, useRef, ChangeEvent } from "react";
import Popup from "reactjs-popup";
import styled from "styled-components";
import { studentApi } from "@/config/axios";
import { toast } from "react-toastify";

interface ImportPopupState {
  selectedFile: File | null;
  showImportSetting: boolean;
  showDuplicateControl: boolean;
  fileName: string;
  duplicateOption: string;
}

const StyledPopup = styled(Popup)`
  // use your custom style for ".popup-overlay"
  &-overlay {
    background-color: rgba(0, 0, 0, 0.5);
  }
`;
// @ts-ignore
import TEMPLATE_FILE_URL from "../../../../assets/template/StudentData.xlsx";

export default function ImportPopup({
  button,
  classId,
}: {
  button: JSX.Element;
  classId: string;
}) {
  const [popupOpen, setPopupOpen] = useState(false);
  const [state, setState] = useState<ImportPopupState>({
    selectedFile: null,
    showImportSetting: true,
    showDuplicateControl: false,
    fileName: "",
    duplicateOption: "",
  });

  const fileInputRef = useRef<HTMLInputElement>(null);

  const handleFileSelect = (event: ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    setState({
      ...state,
      selectedFile: file || null,
      fileName: file ? file.name : "",
    });
  };

  const downloadFileAtURL = async () => {
    const link = document.createElement("a");
    link.target = "_blank";
    link.download = "StudentData.xlsx";

    const response = await fetch(TEMPLATE_FILE_URL);
    if (!response.ok) {
      throw new Error("Failed to fetch the file");
    }

    const blob = await response.blob();

    link.href = URL.createObjectURL(blob);

    link.click();
  };

  const handleDuplicateOptionChange = (option: string) => {
    setState({
      ...state,
      duplicateOption: option,
    });
  };

  const handleCancelClick = () => {
    setState({
      ...state,
      showImportSetting: true,
      showDuplicateControl: false,
      selectedFile: null,
      fileName: "",
    });
    setPopupOpen(false);
  };

  const uploadFile = async () => {
    const { selectedFile, duplicateOption } = state;

    if (selectedFile.size === 0) {
      toast.error("File is empty.");
      return;
    }

    if (selectedFile) {
      const formData = new FormData();
      formData.append("file", selectedFile);
      formData.append("duplicateOption", duplicateOption);
      formData.append("classId", classId);

      await studentApi
        .post("UploadExcelFile", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        })
        .then((response) => {
          if (response.data.isSuccess === false) {
            toast.error("Data has been duplicated");
            setState({
              ...state,
              showImportSetting: false,
              showDuplicateControl: true,
              duplicateOption: "",
            });
          } else {
            if (state.duplicateOption === "Allow") {
              toast.success("Data uploaded successfully with 'Allow' option.");
            } else if (state.duplicateOption === "Replace") {
              toast.success(
                "Data uploaded successfully with 'Replace' option."
              );
            } else if (state.duplicateOption === "Skip") {
              toast.success("Data uploaded successfully with 'Skip' option.");
            } else {
              toast.success("File uploaded successfully!");
            }
            setState({
              ...state,
              showImportSetting: true,
              showDuplicateControl: false,
              duplicateOption: "",
            });
          }
        })
        .catch((error) => {
          toast.error("Error uploading file. Please try again.");
        });
    } else {
      toast.error("Please select a file.");
    }
  };

  return (
    <StyledPopup
      open={popupOpen}
      onOpen={() => setPopupOpen(true)}
      className={styles.filterPopup}
      trigger={button}
      arrow={false}
    >
      <div className={styles.frameParent}>
        <div className={styles.main}>
          <div className={styles.import}>
            <div className={styles.importStudentWrapper}>
              <b className={styles.importStudent}>Import Student</b>
            </div>
            {state.showImportSetting && (
              <div className={styles.importSettingParent}>
                <b className={styles.importStudent}>Import setting</b>
                <div className={styles.frameParent}>
                  <div className={styles.frameGsroup}>
                    <div className={styles.fileWrapper}>
                      <div className={styles.importStudent}>
                        <span className={styles.fileName}>
                          {"File"}
                          <span className={styles.redAsterisk}>*</span>
                          {`: ${
                            state.selectedFile
                              ? state.selectedFile.name
                              : "No file chosen"
                          }`}
                        </span>
                      </div>
                    </div>
                    <div
                      className={styles.selectWrapper}
                      onClick={() =>
                        document.getElementById("fileInput")?.click()
                      }
                    >
                      <div className={styles.select}>Select</div>
                      <input
                        type="file"
                        accept=".xlsx"
                        ref={fileInputRef}
                        id="fileInput"
                        hidden
                        className={styles.fileInput}
                        onChange={handleFileSelect}
                        style={{ display: "none" }}
                      />
                    </div>
                  </div>
                  <div className={styles.frameGroup}>
                    <div className={styles.fileWrapper}>
                      <div className={styles.select}>Import template</div>
                    </div>
                    <div
                      className={styles.downloadWrapper}
                      onClick={downloadFileAtURL}
                    >
                      <div className={styles.download}>Download</div>
                    </div>
                  </div>
                </div>
              </div>
            )}
            {state.showDuplicateControl && (
              <div className={styles.duplicateControlParent}>
                <b className={styles.select}>Duplicate control</b>
                <div className={styles.frameWrapper}>
                  <div className={styles.frameDiv}>
                    <div className={styles.fileWrapper}>
                      <div className={styles.select}>Duplicate handle</div>
                    </div>
                    <div className={styles.radioGroup}>
                      <div className={styles.div}>
                        <input
                          type="radio"
                          id="allow"
                          name="duplicateOption"
                          className={styles.radio1}
                          disabled={!state.selectedFile}
                          checked={state.duplicateOption === "Allow"}
                          onChange={() => handleDuplicateOptionChange("Allow")}
                        />
                        <label htmlFor="allow" className={styles.select}>
                          Allow
                        </label>
                      </div>
                      <div className={styles.div}>
                        <input
                          type="radio"
                          id="replace"
                          name="duplicateOption"
                          className={styles.radio1}
                          disabled={!state.selectedFile}
                          checked={state.duplicateOption === "Replace"}
                          onChange={() =>
                            handleDuplicateOptionChange("Replace")
                          }
                        />
                        <label htmlFor="replace" className={styles.select}>
                          Replace
                        </label>
                      </div>
                      <div className={styles.div}>
                        <input
                          type="radio"
                          id="skip"
                          name="duplicateOption"
                          className={styles.radio1}
                          disabled={!state.selectedFile}
                          checked={state.duplicateOption === "Skip"}
                          onChange={() => handleDuplicateOptionChange("Skip")}
                        />
                        <label htmlFor="skip" className={styles.select}>
                          Skip
                        </label>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            )}
            <img className={styles.importChild} alt="" src="/line-68.svg" />
            <div className={styles.addDayButtonParent}>
              <div className={styles.addDayButton}>
                <b className={styles.cancel} onClick={handleCancelClick}>
                  Cancel
                </b>
              </div>
              <div className={styles.importWrapper}>
                <b
                  className={styles.importStudent}
                  onClick={uploadFile}
                  style={{ cursor: "pointer" }}
                >
                  Import
                </b>
              </div>
            </div>
          </div>
        </div>
      </div>
    </StyledPopup>
  );
}
