import React from "react";
import Button from "@mui/material/Button";
function ButtonChoose({ name, icon, style, onClick }) {
  return (
    <div className={style.button}>
      <Button style={{color: "red"}} variant="text" onClick={onClick}>
        {icon}
        {name}
      </Button>
    </div>
  );
}

export default ButtonChoose;
