import React from "react";
import { Flex, Box } from "@chakra-ui/react";
import { MdOutlineSort } from "react-icons/md";

const TableHeader = ({ text, size, setSortBy, sortOrder, setSortOrder }) => {
  const handleClick = () => {
    let sortByValue = "";
    switch (text) {
      case "ID":
        sortByValue = "id";
        break;
      case "Full Name":
        sortByValue = "name";
        break;
      case "Email":
        sortByValue = "email";
        break;
      case "Date of Birth":
        sortByValue = "dob";
        break;
      case "Gender":
        sortByValue = "gender";
        break;
      case "Type":
        sortByValue = "type";
        break;
      default:
        sortByValue = "";
        break;
    }
    setSortBy(sortByValue);
    // Xác định sortOrder tại đây và cập nhật giá trị của nó
    const newSortOrder = sortOrder === "asc" ? "desc" : "asc";
    setSortOrder(newSortOrder);
  };

  return (
    <Flex
      fontWeight="500"
      alignItems="center"
      gap={2}
      flex={size || 0}
      bgColor="#2D3748"
      color="white"
      p={2}
      cursor="pointer"
      onClick={handleClick}
    >
      {text}
      {text && (
        <Box>
          <MdOutlineSort />
        </Box>
      )}
    </Flex>
  );
};

export default TableHeader;
