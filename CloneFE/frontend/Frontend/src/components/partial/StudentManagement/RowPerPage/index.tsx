import React from "react";
import style from "./style.module.scss";

interface Props {
  RowPerPage: number;
  setRowsPerPage: any;
}

export default function RowPerPage({ RowPerPage, setRowsPerPage }: Props) {
  // Define the onChange handler
  const handleChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedValue = event.target.value; // Get the selected value from the event
    setRowsPerPage(Number(selectedValue)); // Set the selected value using setRowsPerPage
  };

  return (
    <div className={style.main}>
      <p>Row per pages:</p>
      <select
        className={style.option}
        onChange={handleChange}
        value={RowPerPage}
      >
        <option value="5">5</option>
        <option value="10">10</option>
        <option value="15">15</option>
        <option value="20">20</option>
      </select>
    </div>
  );
}
