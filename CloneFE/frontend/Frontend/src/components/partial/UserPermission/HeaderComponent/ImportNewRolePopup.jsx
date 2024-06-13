import React, { useState } from "react";
import { Upload, message, Button, List } from "antd";
import {
  InboxOutlined,
  DownloadOutlined,
  DeleteOutlined,
} from "@ant-design/icons";
import { Bounce, toast } from "react-toastify";
import axios from "axios";

const { Dragger } = Upload;

const backend_api = `${import.meta.env.VITE_API_HOST}:${
  import.meta.env.VITE_API_PORT
}`;

import fileURL from "../../../../assets/template/ImportNewRoleTemplate.xlsx";

const handleDownload = async () => {
  const link = document.createElement("a");
  link.target = "_blank";
  link.download = "ImportNewRoleTemplate.xlsx";
  const response = await fetch(fileURL);
  if (!response.ok) {
    toast.error("Getting template file failed", {
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
  const blob = await response.blob();
  link.href = URL.createObjectURL(blob);
  link.click();
};

const ImportPopup = ({ onCancel, onUpdateData }) => {
  const [fileList, setFileList] = useState([]);
  const [errorMessage, setErrorMessage] = useState(null);

  const props = {
    name: "file",
    multiple: false,
    fileList,
    onChange(info) {
      let newFileList = [...info.fileList];
      newFileList = newFileList.filter((file) => file.status !== "removed");
      newFileList = newFileList.map((file) => {
        if (file.originFileObj === info.file.originFileObj) {
          return { ...info.file, status: info.file.status };
        }
        return file;
      });
      setFileList(newFileList);
    },
    onDrop(e) {},
    beforeUpload(file) {
      const isXLSX = file.name.endsWith(".xlsx");
      const isXLS = file.name.endsWith(".xls");
      if (!isXLSX && !isXLS) {
        toast.error("You can upload only Excel file", {
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
        return Upload.LIST_IGNORE;
      }
      return true;
    },
  };

  const handleImport = () => {
    const formData = new FormData();
    formData.append("file", fileList[0].originFileObj);

    axios
      .post(`${backend_api}/api/import-new-role`, formData, {
        headers: {
          "Content-Type": "multipart/form-data",
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
      })
      .then((response) => {
        if (response.data.isSuccess) {
          toast.success("Import new role success", {
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
          onUpdateData();
        } else {
          const errorMessages = response.data.message.split(",");
          setErrorMessage(errorMessages.join(", "));
          toast.error("Import new role failed", {
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
      })
      .catch((error) => {
        if (error.response) {
          if (error.response.status === 400) {
            if (!error.response.data.isSuccess) {
              const errorMessages = error.response.data.message.split(",");
              setErrorMessage(errorMessages.join(", "));
              toast.error("Import new role failed", {
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
              toast.error("Import new role failed", {
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
          } else {
            toast.error("Import new role failed", {
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
        } else {
          toast.error("Import new role failed", {
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
      });
  };

  const handleRemoveFile = () => {
    setFileList([]);
    setErrorMessage(null); // Xóa thông báo lỗi khi xóa file
  };

  return (
    <div>
      {fileList.length === 0 && (
        <div>
          <Dragger {...props}>
            <p className="ant-upload-drag-icon">
              <InboxOutlined />
            </p>
            <p className="ant-upload-text">
              Click or drag file to this area to upload
            </p>
            <p className="ant-upload-hint">
              Support for a single Excel file upload.
            </p>
          </Dragger>
          <Button
            type="primary"
            style={{
              marginTop: "20px",
              marginLeft: "10px",
              background: "#2f913f",
              color: "white",
            }}
            onClick={handleImport}
            disabled
          >
            Import
          </Button>
          <Button
            style={{
              marginTop: "20px",
              marginLeft: "10px",
              background: "red",
              color: "white",
            }}
            onClick={onCancel}
          >
            Cancel
          </Button>
          <Button
            style={{
              marginTop: "20px",
              marginLeft: "10px",
              background: "black",
              color: "white",
              alignItems: "center",
            }}
            onClick={handleDownload}
          >
            <DownloadOutlined /> Download Template
          </Button>
        </div>
      )}
      {fileList.length > 0 && (
        <div>
          <List
            bordered
            dataSource={fileList}
            renderItem={(item) => (
              <List.Item
                style={{
                  display: "flex",
                  justifyContent: "space-between",
                  alignItems: "center",
                }}
              >
                {item.name}
                <Button type="primary" danger onClick={handleRemoveFile}>
                  <DeleteOutlined />
                </Button>
              </List.Item>
            )}
          />
          <Button
            type="primary"
            style={{
              marginTop: "20px",
              marginLeft: "10px",
              background: "#2f913f",
              color: "white",
            }}
            onClick={handleImport}
          >
            Import
          </Button>
          <Button
            style={{
              marginTop: "20px",
              marginLeft: "10px",
              background: "red",
              color: "white",
            }}
            onClick={onCancel}
          >
            Cancel
          </Button>
          <Button
            style={{
              marginTop: "20px",
              marginLeft: "10px",
              background: "black",
              color: "white",
            }}
            onClick={handleDownload}
          >
            <DownloadOutlined /> Download Template
          </Button>
        </div>
      )}
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
            {errorMessage.split(",").map((msg, index) => (
              <p key={index}>{msg.trim()}</p>
            ))}
          </div>
        </div>
      )}
    </div>
  );
};

export default ImportPopup;
