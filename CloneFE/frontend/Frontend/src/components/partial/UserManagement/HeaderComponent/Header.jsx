import React from "react";
import { Box, Flex, Text } from "@chakra-ui/react";
import SearchBar from "./SearchBar";
import TagList from "./TagList";
import AddUserForm from "./AddUserForm";
import FilterButton from "./FilterButton";
import ImportButton from "./ImportButton";
import ExportButton from "./ExportButton";

const Header = ({
  tags,
  setTags,
  onSearch,
  onFilter,
  onImportData,
  onUpdateData,
  filterData,
}) => {
  const handleAddTag = (tag) => {
    const updatedTags = [...tags, tag];
    setTags(updatedTags);
    localStorage.setItem("tags", JSON.stringify(updatedTags));
  };

  return (
    <React.Fragment>
      <h3
        style={{
          color: "#fff",
          padding: "30px",
          width: "100%",
          margin: "2px 0px 30px",
          alignContent: "center",
          height: "80px",
          background: "#2d3748",
          fontSize: "32px",
          letterSpacing: "6px",
        }}
      >
        User Management
      </h3>
      <Flex justifyContent="space-between" w="full" mb={3}>
        <Flex px={8} gap={2}>
          <SearchBar onAddTag={handleAddTag} onSearch={onSearch} />
          <FilterButton onFilter={onFilter} onUpdateData={onUpdateData} />
        </Flex>
        <Flex px={6} gap={2}>
          <ImportButton
            onImportData={onImportData}
            onUpdateData={onUpdateData}
          />
          <ExportButton filters={filterData}/>

          <AddUserForm onUpdateData={onUpdateData} />
        </Flex>
      </Flex>
      <TagList tags={tags} setTags={setTags} />
    </React.Fragment>
  );
};

export default Header;
