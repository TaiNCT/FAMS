import React from "react";
import { Box, Grid, Center } from "@chakra-ui/react";
import PermsHeader from "./PermsHeader";
import PermsRow from "./PermsRow";
import { Empty } from 'antd';

const TablePerms = ({ permissionsData, isEditing, onSave, onCancel , onUpdate}) => {
  let tableRow;
  if (permissionsData && permissionsData.length > 0) {
    tableRow = permissionsData.map((permission, index) => (
      <PermsRow
        permission={permission}
        key={`table-row-${index}`}
        isEditing={isEditing}
        onSave={onSave}
        onCancel={onCancel}
        onUpdate={onUpdate}
      />
    ));
  } else {
    tableRow = (
      <Box
      gridColumn="1 / -1"
      className="h-[20vh]"
      display="flex"
      alignItems="center"
      justifyContent="center"
    >
      <Empty image={Empty.PRESENTED_IMAGE_SIMPLE} />
    </Box>
    );
  }

  return (
    <Box
      borderRadius="15px 15px 0 0"
      overflow="hidden"
      boxShadow="rgba(0, 0, 0, 0.24) 0px 3px 50px"
      bg="white"
    >
      <Grid
        templateColumns="repeat(6, 1fr)"
        borderRadius="15px 15px 0 0"
        overflow="hidden"
        w="full"
        boxShadow="rgba(0, 0, 0, 0.24) 0px 3px 50px"
      >
        <PermsHeader text="Role Name" />
        <PermsHeader text="Syllabus" />
        <PermsHeader text="Training Program" />
        <PermsHeader text="Class" />
        <PermsHeader text="Learning Material" />
        <PermsHeader text="User" />
      </Grid>
      {tableRow}
    </Box>
  );
};

export default TablePerms;
