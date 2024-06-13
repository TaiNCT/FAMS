import React from "react";
import ClearIcon from "@mui/icons-material/Clear";
import ButtonUtil from "../../ClassList/components/Button";

export default function SearchKey({ keySearch, onDelete }) {
  const handleDelete = () => {
    onDelete(keySearch);
  };
  return (
    <div
      style={{
        height: "30px",
        backgroundColor: "#5b5b5b",
        color: "white",
        borderRadius: "10px",
        display: "flex",
        overflow: "hidden",
        alignItems: "center",
        justifyContent: "center",
        padding: "3px 10px",
        gap: "5px",
      }}
    >
      <p>{keySearch}</p>
      <span
        onClick={handleDelete}
        style={{ width: "16px", height: "16px", cursor: "pointer" }}
      >
        <ClearIcon color="white" fontSize="100px" />
      </span>
    </div>
  );
}
