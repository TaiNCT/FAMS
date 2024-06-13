import React from "react";
import style from "../../assert/css/InputClass.module.scss";
import TextField from "@mui/material/TextField";
export default function Input({ onChange, onSubmit, name, error, msgClassName }) {
  return (
    <div>
      <div className={style.formContainer}>
        <TextField
          className={style.formControl}
          error={error}
          placeholder={name}
          onChange={onChange}
          onKeyDown={onSubmit}
          helperText={error ? "Class name is required" : ""}
          InputProps={{
            style: {
              color: "black",
            },
          }}
        />
      </div>
      {msgClassName != null && <span>{msgClassName}</span>}
    </div>
  );
}
