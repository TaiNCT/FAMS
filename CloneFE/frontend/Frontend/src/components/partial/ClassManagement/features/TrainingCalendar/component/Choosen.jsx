import React from "react";

export default function Choosen({ children }) {
  return (
    <div style={{ display: "flex", marginTop: "20px", marginBottom: "20px", gap: "10px" }}>
      {children}
    </div>
  );
}
