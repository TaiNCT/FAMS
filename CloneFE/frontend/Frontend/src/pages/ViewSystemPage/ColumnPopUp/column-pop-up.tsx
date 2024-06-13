import React, { useState, useMemo } from "react";
import "./column-pop-up.css";
import { Checkbox } from "@mui/material";

interface ColumnPopUpProps {
  onApply: (selectedColumns: string[]) => void;
  onReset: () => void;
}

const ColumnPopUp: React.FC<ColumnPopUpProps> = ({ onApply, onReset }) => {
  const defaultColumns = [
    "Full name",
    "Date of birth",
    "Gender",
    "Phone",
    "Email",
    "University",
    "Major",
    "Graduation Time",
    "GPA",
    "Address",
    "RECer",
    "Status",
  ];

  const [selectedColumns, setSelectedColumns] = useState(
    defaultColumns.reduce((acc, column) => {
      acc[column] = ["Full name", "Date of birth", "Gender", "RECer", "Status"].includes(column);
      return acc;
    }, {} as Record<string, boolean>)
  );

  const allSelected = useMemo(() => defaultColumns.every(column => selectedColumns[column]), [selectedColumns]);

  const handleCheckboxChange = (columnName: string) => {
    setSelectedColumns((prevSelectedColumns) => ({
      ...prevSelectedColumns,
      [columnName]: !prevSelectedColumns[columnName],
    }));
  };

  const handleSelectAllChange = () => {
    if (allSelected) {
      setSelectedColumns(defaultColumns.reduce((acc, column) => {
        acc[column] = false;
        return acc;
      }, {} as Record<string, boolean>));
    } else {
      setSelectedColumns(defaultColumns.reduce((acc, column) => {
        acc[column] = true;
        return acc;
      }, {} as Record<string, boolean>));
    }
  };

  const handleApply = () => {
    const selectedColumnsArray = Object.keys(selectedColumns).filter((col) => selectedColumns[col]);
    onApply(selectedColumnsArray);
  };

  const handleReset = () => {
    setSelectedColumns(defaultColumns.reduce((acc, column) => {
      acc[column] = ["Full name", "Date of birth", "Gender", "RECer", "Status"].includes(column);
      return acc;
    }, {} as Record<string, boolean>));
    onReset();
  };

  return (
    <div className="column-pop-up-container">
      <h3>Select columns</h3>
      <div className="column-pop-up-inner">
        <div className="filter-pop-up-item">
          <div className="check-box-container">
            <Checkbox
              checked={allSelected}
              onChange={handleSelectAllChange}
            />
            <span>Select All</span>
          </div>
        </div>
        {defaultColumns.map((column) => (
          <div key={column} className="filter-pop-up-item">
            <div className="check-box-container">
              <Checkbox
                checked={selectedColumns[column]}
                onChange={() => handleCheckboxChange(column)}
              />
              <span>{column}</span>
            </div>
          </div>
        ))}
      </div>
      <div className="column-pop-up-action">
        <button onClick={handleApply}>Apply</button>
        <button onClick={handleReset}>Reset</button>
      </div>
    </div>
  );
};

export default ColumnPopUp;
