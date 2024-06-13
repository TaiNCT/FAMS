import React from "react";
import "./ItemsPerPageDropdown.css";

const ItemsPerPageDropdown = ({ postsPerPage, onItemsPerPageChange }) => {
  const handleDropdownChange = (e) => {
    const newItemsPerPage = parseInt(e.target.value, 10);
    onItemsPerPageChange(newItemsPerPage);
  };

  return (
    <div className="items-per-page-dropdown">
      <label>Rows per page: </label>
      <select value={postsPerPage} onChange={handleDropdownChange}>
        {[10, 25, 50, 100].map((option) => (
          <option key={option} value={option}>
            {option}
          </option>
        ))}
      </select>
    </div>
  );
};

export default ItemsPerPageDropdown;