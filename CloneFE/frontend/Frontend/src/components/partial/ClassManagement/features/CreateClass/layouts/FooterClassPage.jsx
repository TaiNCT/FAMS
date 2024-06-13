import React from "react";

function FooterClassPage({ children }) {
  return (
    <div
      style={{
        display: "flex",
        justifyContent: "end",
        marginRight: "24px",
        marginTop: "80px",
        gap: "10px",
        paddingBottom: "30px",
      }}
    >
      {children}
    </div>
  );
}

export default FooterClassPage;