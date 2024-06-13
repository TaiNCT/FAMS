import { Button, Upload, UploadProps } from "antd";
import { useEffect, useState } from "react";
import ApiClient from "../ApiClient";
import { toast } from "react-toastify";
import fileUrl from '../../../../assets/template/Template_Import_Syllabus.xlsx'
export default function ImportButton({
  visible,
  onClose,
  setIsLoading
}: {
  visible: boolean;
  onClose: () => void;
  setIsLoading?: (isLoading: boolean) => void;
}) {
  if (!visible) return null;

  const [uploading, setUploading] = useState(false);
  const [fileList, setFileList] = useState<any[]>([]);
  const [errorMessage, setErrorMessage] = useState<string[]>(null);
  const [duplicateHanding, setDuplicateHanding] = useState<string>('Allow');
  let apiClient = new ApiClient();

  const handleUpload = async () => {
    const response = await apiClient.importSyllaus(fileList, setUploading, setFileList, duplicateHanding);
    if (!response.ok) {
      const data = await response.json();;
      let error: string[] = [];
      for (let key in data.errors) {
        error.push(data.errors[key]);
      }
      setErrorMessage(error);
    }
    else {
      setIsLoading(true);
    }
  };

  const handleDownload = async () => {
    const link = document.createElement("a");
    // const response = await apiClient.downloadSyllabusTemplate();
    link.download = "Template_Import_Syllabus.xlsx";
    link.target = "_blank";

    // Fetch the file
    const response = await fetch(fileUrl);
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

  const props: UploadProps = {
    onRemove: (file) => {
      const index = fileList.indexOf(file);
      const newFileList = fileList.slice();
      newFileList.splice(index, 1);
      setFileList(newFileList);
    },
    beforeUpload: (file) => {
      let fileTypes = [
        "application/vnd.ms-excel",
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        "text/csv",
      ];
      if (file) {
        if (file && fileTypes.some((type) => file.type.includes(type))) {
          setFileList([file]);
        } else {
          toast.error("Invalid file type", {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "light",
          });
        }
      }
      return false;
    },
    fileList,
  };

  return (
    <div className="fixed inset-0 bg-black bg-opacity-60 backdrop-blur-sm flex justify-center items-center">
      <div className="flex pb-[10px] flex-col gap-[15px] bg-[#fff] rounded-[10px] mx-auto my-auto">
        <div className="flex p-[10px] justify-center bg-[#2d3748] rounded-[10px] rounded-br-none rounded-bl-none ">
          <span className="font-bold text-[#fff]  ">Import Syllabus</span>
        </div>
        <div className="flex w-[500px] text-[14px]">
          <p className=" font-bold pl-[20px]">Import setting</p>
          <div className="flex flex-col gap-[15px] pl-[40px] font-medium  ">
            <div>
              <span>File (csv)</span>
              <span className="text-[#fc5656]">*</span>
              <Upload
                {...props}
                className="pl-[100px] "
                name="file"
                accept=".xlsx"
                showUploadList={true}
                maxCount={1}
              >
                <Button className="flex w-[82px] h-[24px] items-center bg-[#2d3748] rounded-[5px] ">
                  <span className="normal-case text-[#fff]">Select</span>
                </Button>
              </Upload>
            </div>

            <div className="flex">
              <p className=" pr-[70px]">Encoding type</p>
              <div className="flex w-[140px] px-[5px] justify-between rounded-[5px] border border-[#acacac]">
                <select
                  className="w-full  appearance-none border-none focus:outline-none"
                  defaultValue="autodetect"
                >
                  <option value="autodetect">Auto detect</option>
                  <option value="utf">UTF-8</option>
                  <option value="ansi">ANSI</option>
                </select>
                <img
                  width="10px"
                  src="src\assets\Logo_Syllabus_Management\up-arrow-svgrepo-com.svg"
                />
              </div>
            </div>
            <div className="flex ">
              <p className=" pr-[47px]">Column separator</p>
              <div className="flex w-[140px] px-[5px] justify-between rounded-[5px] border border-[#acacac]">
                <div className="w-full h-full flex items-center">
                  <select
                    className="w-full  appearance-none bg-transparent focus:outline-none"
                    defaultValue="comma"
                  >
                    <option value="comma">Comma</option>
                    <option value="semoColon">Semi-colon</option>
                  </select>
                </div>
                <img
                  width="10px"
                  src="src\assets\Logo_Syllabus_Management\up-arrow-svgrepo-com.svg"
                />
              </div>
            </div>

            <div className="flex">
              <p className="w-[165px] ">Import template</p>
              <a
                className=" text-[#285d9a] underline"
                href="#"
                onClick={handleDownload}
              >
                Download
              </a>
            </div>
          </div>
        </div>
        <hr className=" w-[460px] mx-auto" />
        <div className="flex text-[14px]">
          <span className=" pl-[20px] font-bold">Duplicate control</span>
          <div className="flex w-[333px] flex-col gap-[15px] font-medium pl-[20px]">
            <div className="flex flex-col gap-[5px]">
              <span>Scanning</span>
              <div className="flex gap-4">
                <label className="flex gap-2">
                  <input
                    type="checkbox"
                    className="checkbox-group"
                    name="checkbox"
                    value="1"
                  />
                  Syllabus Code
                </label>

                <label className="flex gap-2 items-center  ">
                  <input
                    type="checkbox"
                    className="checkbox-group"
                    name="checkbox"
                    value="2"
                  />
                  Syllabus Name
                </label>
              </div>
            </div>
            <div className="flex flex-col gap-[5px]">
              <span>Duplicate handle</span>
              <div className="flex gap-4">
                <label className="flex gap-2">
                  <input
                    type="radio"
                    className="form-radio"
                    name="radio"
                    value="Allow"
                    checked={duplicateHanding === 'Allow'}
                    // disabled={!errorMessage ? true : false}
                    onChange={(e) => setDuplicateHanding(e.target.value)}
                  />
                  Allow
                </label>
                <label className="flex gap-2 ">
                  <input
                    type="radio"
                    className="form-radio"
                    name="radio"
                    value="Replace"
                    onChange={(e) => setDuplicateHanding(e.target.value)}
                  // disabled={!errorMessage ? true : false}
                  />
                  Replace
                </label>
                <label className="flex gap-2">
                  <input
                    type="radio"
                    className="form-radio"
                    name="radio"
                    value="Skip"
                    // disabled={!errorMessage ? true : false}
                    onChange={(e) => setDuplicateHanding(e.target.value)}
                  />
                  Skip
                </label>
              </div>
            </div>
            {errorMessage && (
              <div
                style={{
                  marginTop: "20px",
                  border: "1px solid silver",
                  padding: "10px",
                  borderRadius: "5%",
                }}
              >
                <h3>Error Log:</h3>
                <div
                  style={{
                    maxHeight: "200px",
                    overflowY: "auto",
                  }}
                >
                  {errorMessage.map((msg, index) => (
                    <p key={index}>{msg.trim()}</p>
                  ))}
                </div>
              </div>
            )}
          </div>
        </div>
        <hr className="w-[460px] mx-auto" />
        <div className="flex flex-row px-[20px] gap-[10px] justify-end border-none">
          <Button key="back" onClick={onClose} className="rounded-[10px]">
            <span className="font-bold normal-case text-[#e74a3b] underline">
              Cancel
            </span>
          </Button>
          <Button
            className="w-[96px] items-center bg-[#2d3748] rounded-[10px]"
            onClick={handleUpload}
            disabled={fileList.length === 0}
            loading={uploading}
          >
            <span className="font-bold normal-case text-[#fff]">Import</span>
          </Button>
        </div>
      </div>
    </div>
  );
}
