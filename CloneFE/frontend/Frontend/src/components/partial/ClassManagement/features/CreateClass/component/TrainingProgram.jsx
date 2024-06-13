import React from "react";
import { Link } from "react-router-dom";
import DocumentManageIcons from "../../../assert/icons/document-manage-icons";
import FormatDate from "../../../Utils/FormatDate";
import ListCard from "./Card/Card";



function TrainingProgram({ children })
{
  return (
    <>
      <div
        style={{
          display: "flex",
          marginTop: "1px",
          padding: "12px 12px 12px 20px",
          backgroundColor: "#2d3748",
          alignItems: "center",
          borderRadius: "0px 16px 0px 0px",
          width: "100%",
          gap: "25px",
          color: "white",
        }}
      >
        <div style={{ display: "flex", alignItems: "center", gap: "25px" }}>{children}</div>
      </div>
    </>
  );
}

export default TrainingProgram;
