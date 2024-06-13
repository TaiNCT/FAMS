import React from "react";
import Button from "@mui/material/Button";
function ButtonUtil({ name, icon, style, onClick, startOfWeek, endOfWeek })
{
  return (
    <div className={style.button}>
      <Button variant="text" onClick={onClick} disabled={!startOfWeek || !endOfWeek}>
        {icon}
        {name}
      </Button>
    </div>
  );
}

export default ButtonUtil;
