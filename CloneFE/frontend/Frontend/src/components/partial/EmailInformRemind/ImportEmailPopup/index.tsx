/* eslint-disable @typescript-eslint/no-unused-vars */
import axios from "../../../../axiosAuth";
import style from "./style.module.scss";

import React, { useState } from "react";
import { Button, Dialog, DialogActions, DialogTitle } from "@mui/material";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
// import { makeStyles } from "@mui/styles";

// @ts-ignore
import TEMPLATE_FILE_URL from "../../../../assets/template/EmailTemplates.xlsx";

// @ts-ignore
const backend_api: string = `${import.meta.env.VITE_API_HOST}:${
  import.meta.env.VITE_API_PORT
}`;

function ImportEmailTemplatePopup({ onClose }) {
  const [file, setFile] = useState(null);
  const [isFileSelected, setIsFileSelected] = useState(false);

  const handleFileChange = (event) => {
    const selectedFile = event.target.files[0];
    setFile(selectedFile);
    setIsFileSelected(true);
  };
  // Function to trigger file input click
  const handleCustomFileClick = () => {
    document.getElementById("fileimport").click();
  };

  const handleImport = async () => {
    if (!file) {
      toast.error("No file selected. Please select a file to import.");
      return;
    }

    const formData = new FormData();
    formData.append("file", file);

    try {
      const response = await axios.post(
        `${backend_api}/api/Import/emailtemplates/import`,
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );
      toast.success("Import successful!");
      onClose();
    } catch (error) {
      console.error("Import failed:", error);
      toast.error("Import failed. Please try again.");
    }
  };

  // Function to download Excel template
  const downloadFileAtURL = async () => {
    const link = document.createElement("a");
    link.target = "_blank";
    link.download = "EmailTemplate.xlsx";
    try {
      const response = await fetch(TEMPLATE_FILE_URL);
      if (!response.ok) {
        throw new Error("Failed to fetch the file");
      }

      const blob = await response.blob();
      link.href = URL.createObjectURL(blob);
      link.click();
    } catch (error) {
      console.error("Error downloading the file:", error);
    }
  };

  return (
    <Dialog open={true} onClose={onClose}>
      <DialogTitle className={style.titleImportPopUp}>
        Import Email Template
      </DialogTitle>
      <div className={style.uploadContainer}>
        <table>
          <tr>
            <th style={{ padding: "8px 30px" }}>Import setting</th>
            <td style={{ padding: "8px 30px", textAlign: "left" }}>
              {" "}
              {isFileSelected ? (
                <span>{file.name}</span>
              ) : (
                <span>
                  File<span style={{ color: "red" }}>*</span>
                </span>
              )}
            </td>
            <td style={{ padding: "8px 30px", textAlign: "left" }}>
              {" "}
              <button
                onClick={handleCustomFileClick}
                className={style.customFileButton}
              >
                Select
              </button>
              <input
                id="fileimport"
                type="file"
                onChange={handleFileChange}
                className={style.fileInput}
              />
            </td>
          </tr>
          <tr>
            <th></th>
            <td style={{ padding: "8px 30px", textAlign: "left" }}>
              Import template
            </td>
            <td style={{ padding: "8px 30px", textAlign: "left" }}>
              <Button
                onClick={downloadFileAtURL}
                className={style.downloadLink}
              >
                Download
              </Button>
            </td>
          </tr>
        </table>
      </div>
      <DialogActions>
        <Button className={style.cancelbtn} onClick={onClose}>
          Cancel
        </Button>
        <Button
          className={style.importbtn}
          onClick={handleImport}
          color="primary"
        >
          Import
        </Button>
      </DialogActions>
    </Dialog>
  );
}

export default ImportEmailTemplatePopup;
