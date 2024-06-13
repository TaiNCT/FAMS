import React from "react";
import { Box, Grid, Center } from "@chakra-ui/react";
import { WarningIcon } from "@chakra-ui/icons";
import TableHeader from "./TableHeader";
import TableRow from "./TableRow";
import { Empty } from "antd";

const Table = ({
  userData,
  sortBy,
  sortOrder,
  setSortBy,
  setSortOrder,
  onUpdateData,
}) => {
  var tableRow;
  if (userData.length) {
    tableRow = userData.map((user, index) => (
      <TableRow
        user={user}
        key={`table-row-${index}`}
        index={index + 1}
        onUpdateData={onUpdateData}
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
    <Box borderRadius="15px 15px 0 0">
      <Grid
        templateColumns="0.5fr 2fr 3fr 2fr 1fr 1.5fr 0.5fr"
        borderRadius="15px 15px 0 0"
        overflow="hidden"
        w="full"
        boxShadow="rgba(0, 0, 0, 0.24) 0px 3px 50px"
      >
        <TableHeader
          text="ID"
          size={0.5}
          setSortBy={setSortBy}
          sortOrder={sortOrder}
          setSortOrder={setSortOrder}
        />
        <TableHeader
          text="Full Name"
          size={2}
          setSortBy={setSortBy}
          sortOrder={sortOrder}
          setSortOrder={setSortOrder}
        />
        <TableHeader
          text="Email"
          size={2}
          setSortBy={setSortBy}
          sortOrder={sortOrder}
          setSortOrder={setSortOrder}
        />
        <TableHeader
          text="Date of Birth"
          size={2}
          setSortBy={setSortBy}
          sortOrder={sortOrder}
          setSortOrder={setSortOrder}
        />
        <TableHeader
          text="Gender"
          size={1}
          setSortBy={setSortBy}
          sortOrder={sortOrder}
          setSortOrder={setSortOrder}
        />
        <TableHeader
          text="Type"
          size={2}
          setSortBy={setSortBy}
          sortOrder={sortOrder}
          setSortOrder={setSortOrder}
        />
        <TableHeader
          size={0.5}
          setSortBy={setSortBy}
          sortOrder={sortOrder}
          setSortOrder={setSortOrder}
        />
        {tableRow}
      </Grid>
    </Box>
  );
};

export default Table;
