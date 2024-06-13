import React from "react";
function FormClass({ children }) {
  return (
    <div>
      <div
        style={{
          display: "flex",
          alignItems: "center",
          marginTop: "20px",
          paddingBottom: "10px",
        }}
      >
        {children}
      </div>
    </div>
  );
}
export default FormClass;
