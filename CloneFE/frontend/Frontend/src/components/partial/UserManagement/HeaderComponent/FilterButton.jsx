import React from "react";
import {Flex, useDisclosure } from "@chakra-ui/react";
import FilterForm from "./FilterForm";

const FilterButton = ({onFilter, onUpdateData}) => {
  const {onClose } = useDisclosure();

  return (
    <Flex gap={2}>
      <FilterForm onClose={onClose} onFilter={onFilter} onUpdateData={onUpdateData} />
    </Flex>
  );
};

export default FilterButton;
