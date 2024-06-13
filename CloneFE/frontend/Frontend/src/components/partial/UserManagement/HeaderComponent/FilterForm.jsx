import React, { useState } from "react";
import {
  Button,
  Checkbox,
  Menu,
  MenuButton,
  MenuList,
  Stack,
  Input,
  Flex,
  FormControl,
  FormLabel,
} from "@chakra-ui/react";
import { IoFilter } from "react-icons/io5";

const FilterForm = ({ onClose, onFilter , onUpdateData}) => {
  const [filterData, setFilterData] = useState({
    dob: "",
    isAdmin: false,
    isTrainer: false,
    isActive: false,
    isInactive: false,
    isMale: false,
    isFemale: false,
  });

  const handleInputChange = (e) => {
    const { name, type, checked, value } = e.target;
    const val = type === "checkbox" ? checked : value;
    setFilterData((prev) => ({ ...prev, [name]: val }));
  };

  const handleReset = () => {
    setFilterData({
      dob: "",
      isAdmin: false,
      isTrainer: false,
      isActive: false,
      isInactive: false,
      isMale: false,
      isFemale: false,
    });
    onFilter({
      dob: "",
      isAdmin: false,
      isTrainer: false,
      isActive: false,
      isInactive: false,
      isMale: false,
      isFemale: false,
    });
    onUpdateData();
  };
  

  const handleFilter = () => {
    onClose();
    onFilter(filterData);
    onUpdateData();
  };

  return (
  <Menu>
    <Flex alignItems="center">
      <MenuButton
        as={Button}
        bg="#2D3748"
        color="white"
        px={4}
        py={2}
        display="flex"
        alignItems="center"
        justifyContent="center"
        _hover={{
          bgColor: "#4A5568",
        }}
        _active={{
          bgColor: "#2D3748",
        }}
        _focus={{
          boxShadow: "none",
        }}
        transition="background-color 0.3s"
      >
        <div style={{ display: "flex", alignItems: "center", gap: "4px" }}>
          <IoFilter style={{ fontSize: "1.3em" }} />
          <span>Filter</span>
        </div>
      </MenuButton>

      <MenuList
        style={{
          backgroundColor: "white",
          borderRadius: "8px",
          boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
          padding: "16px",
          minWidth: "320px", // Đủ rộng để chứa các checkboxes hàng ngang
          zIndex: "999",
        }}
      >
        <Flex direction="column" gap={4}>
          <FormControl>
            <FormLabel>Date of Birth</FormLabel>
            <Input
              type="date"
              value={filterData.dob}
              name="dob"
              onChange={handleInputChange}
            />
          </FormControl>

          <Flex direction="row" gap="20px">
            <FormControl flex="1">
              <FormLabel>Type</FormLabel>
              <Flex direction="column" gap="2">
                <Checkbox
                  isChecked={filterData.isAdmin}
                  name="isAdmin"
                  onChange={handleInputChange}
                >
                  Admin
                </Checkbox>
                <Checkbox
                  isChecked={filterData.isTrainer}
                  name="isTrainer"
                  onChange={handleInputChange}
                >
                  Trainer
                </Checkbox>
              </Flex>
            </FormControl>

            <FormControl flex="1">
              <FormLabel>Status</FormLabel>
              <Flex direction="column" gap="2">
                <Checkbox
                  isChecked={filterData.isActive}
                  name="isActive"
                  onChange={handleInputChange}
                >
                  Active
                </Checkbox>
                <Checkbox
                  isChecked={filterData.isInactive}
                  name="isInactive"
                  onChange={handleInputChange}
                >
                  Inactive
                </Checkbox>
              </Flex>
            </FormControl>

            <FormControl flex="1" >
              <FormLabel>Gender</FormLabel>
              <Flex direction="column" gap="2">
                <Checkbox
                  isChecked={filterData.isMale}
                  name="isMale"
                  onChange={handleInputChange}
                >
                  Male
                </Checkbox>
                <Checkbox
                  isChecked={filterData.isFemale}
                  name="isFemale"
                  onChange={handleInputChange}
                >
                  Female
                </Checkbox>
              </Flex>
            </FormControl>
          </Flex>

          <Flex justifyContent="flex-end" gap="10px">
            <Button
              variant="ghost"
              onClick={handleReset}
              bg="#2D3748"
              color="white"
              _hover={{
                bg: "#4A5568",
              }}
            >
              Clear
            </Button>
            <Button
              onClick={handleFilter}
              bg="#2D3748"
              color="white"
              _hover={{
                bg: "#4A5568",
              }}
            >
              Search
            </Button>
          </Flex>
        </Flex>
      </MenuList>
    </Flex>
  </Menu>
);

  
  
};

export default FilterForm;
