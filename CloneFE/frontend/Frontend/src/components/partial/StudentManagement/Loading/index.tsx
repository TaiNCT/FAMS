import * as React from "react";
import Box from "@mui/material/Box";
import fptLogo from "../../../../assets/LogoStManagement/logo.png";

export default function CircularIndeterminate() {
  return (
    <Box
      sx={{
        position: "absolute",
        left: "0",
        right: "0",
        transform: "translateY(50%)",
        textAlign: "center",
      }}
    >
      <img src={fptLogo} style={{ margin: "0 auto" }} alt="" />
    </Box>
  );
}
