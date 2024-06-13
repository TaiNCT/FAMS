import React from "react";
import Button from "@mui/material/Button";
function ButtonUtil({ name, icon, style, onClick }) {
  return (
    <div className={style.button}>
      <Button  variant="text" onClick={onClick}>
        {icon}
        {name}
      </Button>
    </div>
  );
}

export default ButtonUtil;
