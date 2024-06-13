import React, { useState, useEffect, useRef } from "react";
import { Flex, Input, InputGroup, InputLeftElement } from "@chakra-ui/react";
import { FaSearch } from "react-icons/fa";

const SearchBar = ({ onAddTag, onSearch }) => {
  const [searchValue, setSearchValue] = useState("");
  const debounceTimeout = useRef(null);

  const handleKeyDown = (e) => {
    if (e.key === "Enter") {
      if (searchValue.trim() !== "") {
        onAddTag(searchValue.trim());
      }
      onSearch(searchValue.trim());
      setSearchValue("");
    }
  };

  const handleTyping = (e) => {
    setSearchValue(e.target.value);
  };

  useEffect(() => {
    if (debounceTimeout.current) {
      clearTimeout(debounceTimeout.current);
    }
    debounceTimeout.current = setTimeout(() => {
      onSearch(searchValue.trim());
    }, 100);
    return () => clearTimeout(debounceTimeout.current);
  }, [searchValue]);

  return (
    <Flex gap={2}>
      <InputGroup>
        <InputLeftElement pointerEvents="none">
          <FaSearch />
        </InputLeftElement>
        <Input
          placeholder="Search by..."
          border="1px solid black"
          value={searchValue}
          onChange={handleTyping}
          onKeyDown={handleKeyDown}
        />
      </InputGroup>
    </Flex>
  );
};

export default SearchBar;
