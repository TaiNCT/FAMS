import React from "react";

export default function Container({ children }) {
  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        flexDirection: "row",
        marginTop: "25px",
      }}
    >
      <div style={{ width: "100%" }}>{children}</div>
    </div>
  );
}
