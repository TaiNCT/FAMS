import React from "react";
import { Flex } from "@chakra-ui/react";

const PermsHeader = ({ text, size }) => {
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
    >
      {text}
    </Flex>
  );
};

export default PermsHeader;
