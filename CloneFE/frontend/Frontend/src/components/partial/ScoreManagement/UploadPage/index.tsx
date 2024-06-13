import React, { useState, useRef } from "react";
import styles from "./style.module.scss"; // Import CSS file
import { Bounce, toast } from "react-toastify";
import axios from "axios";

// Đường dẫn đến file template để tải xuống
import fileURL from "../../../../assets/template/TemplateScore.xlsx";

const handleDownload = async () => {
  // const link = document.createElement("a");
  // link.href = formFile;
  // link.download = "ImportUserTemplate.xlsx";
  // link.click();

  const link = document.createElement("a");
  link.target = "_blank";
  link.download = "TemplateScore.xlsx";

  // Fetch the file
  const response = await fetch(fileURL);
  if (!response.ok) {
    throw new Error("Failed to fetch the file");
  }

  // Convert the response to blob
  const blob = await response.blob();

  // Create object URL for the blob
  link.href = URL.createObjectURL(blob);

  // Trigger download
  link.click();
};

// @ts-ignore
const backend_api: string = `${import.meta.env.VITE_API_HOST}:${
  import.meta.env.VITE_API_PORT
}`;

interface UploadPageProps {
  onClose: () => void;
}
// Modify this line

const UploadPage: React.FC<{ onClose: () => void }> = ({ onClose }) => {
  const [fileName, setFileName] = useState<string>("Filename");
  const [duplicateOption, setDuplicateOption] = useState<string>("");
  const fileInputRef = useRef<HTMLInputElement>(null);
  const handleDuplicateOptionChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => setDuplicateOption(event.target.value);
  const [error, setError] = useState(null);

  // Upload file functionality
  const uploadExcelFile = async () => {
    const files = fileInputRef.current.files;

    if (files.length) {
      const formData = new FormData();
      formData.append("file", files[0]);

      try {
        const resp = await axios.post(
          `${backend_api}/api/ImportExcelScore?option=${duplicateOption.toUpperCase()}`,
          formData,
          {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          }
        );

        if (!resp.data.isSuccess) {
          setError(resp.data.message);
          toast.error(resp.data.message === "DUP"
          ? "Duplicated data detected."
          : resp.data.message, {
            position: "top-right",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "light",
            transition: Bounce,
          });
        } else {
          toast.success("Successfully updated.", {
            position: "top-right",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "light",
            transition: Bounce,
          });
          setError(null);
          setFileName("Filename");
          setDuplicateOption("");
        }
      } catch (e) {
        console.error(e);
        toast.error( "An error occurred during the upload.", {
          position: "top-right",
          autoClose: 3000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
          theme: "light",
          transition: Bounce,
        });
      }
    }
  };
  // Main HTML and CSS

  return (
    <div className={styles["popup-syllabus"]}>
      <input
        accept=".xlsx"
        ref={fileInputRef}
        type="file"
        hidden
        onChange={() => {
          setFileName(fileInputRef.current.files[0].name);
        }}
      />

      <div className={styles["frame-8755"]}>
        <div className={styles["import-syllabus"]}>Import Syllabus</div>
      </div>
      {(error === null || error !== "DUP") && (
        <div className={styles["frame-8762"]}>
          <div className={styles["import-setting"]}>Import setting</div>
          <div className={styles["frame-8761"]}>
            <div className={styles["frame-8711"]}>
              <div className="">
                <div className={styles["file-csv"]}>
                  <span>
                    <span className={styles["file-csv-span"]}>File</span>
                    <span className={styles["file-csv-span2"]}>*</span>
                  </span>
                  <button
                    onClick={() => {
                      if (fileInputRef.current) {
                        fileInputRef.current.click();
                      }
                    }}
                  >
                    Select
                  </button>
                </div>
                {fileName && (
                  <span className={styles["file-name"]}>{fileName}</span>
                )}
              </div>
            </div>
            <div className={styles["frame-87552"]}>
              <div className={styles["frame-8760"]}>
                <div className={styles["import-template"]}>Import template</div>
              </div>
              <div className={styles["frame-87612"]}>
                <button className={styles["download"]} onClick={handleDownload}>
                  Download
                </button>
              </div>
            </div>
          </div>
        </div>
      )}

      {error === "DUP" && (
        <div className={styles["frame-8763"]}>
          <div className={styles["duplicate-control"]}>Duplicate control</div>
          <div className={styles["frame-8761"]}>
            <div className={styles["frame-8757"]}>
              <div className={styles["frame-8760"]}>
                <div className={styles["duplicate-handle"]}>
                  Duplicate handle
                </div>
              </div>
              <div className={styles["radio-group"]}>
                <label className={styles["_01"]}>
                  <input
                    type="radio"
                    name="duplicate-option"
                    value="Allow"
                    checked={duplicateOption === "Allow"}
                    onChange={handleDuplicateOptionChange}
                  />
                  Allow
                </label>
                <label className={styles["_02"]}>
                  <input
                    type="radio"
                    name="duplicate-option"
                    value="Replace"
                    checked={duplicateOption === "Replace"}
                    onChange={handleDuplicateOptionChange}
                  />
                  Replace
                </label>
                <label className={styles["_03"]}>
                  <input
                    type="radio"
                    name="duplicate-option"
                    value="Skip"
                    checked={duplicateOption === "Skip"}
                    onChange={handleDuplicateOptionChange}
                  />
                  Skip
                </label>
              </div>
            </div>
          </div>
        </div>
      )}
      <div className={styles["frame-110"]}>
        <div className={styles["add-day-button"]}>
          <button className={styles["cancel"]} onClick={onClose}>
            Cancel
          </button>
        </div>
        <div className={styles["frame-91"]}>
          <button className={styles["import"]} onClick={uploadExcelFile}>
            Import
          </button>
        </div>
      </div>
    </div>
  );
};
export { UploadPage };
