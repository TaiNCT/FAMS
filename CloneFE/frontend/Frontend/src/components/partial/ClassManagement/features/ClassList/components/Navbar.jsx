import React from "react";

export default function Navbar({ children }) {
  return (
    <div
      style={{
        display: "flex",
        marginBottom: "20px",
        justifyContent: "space-between",
      }}
    >
      {children}
    </div>
  );
}
