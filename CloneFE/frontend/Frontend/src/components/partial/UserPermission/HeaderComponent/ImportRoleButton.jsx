import React from "react";
import { Button } from "@chakra-ui/react";

const ImportRole = ({}) => {
  return (
    <Button
      onClick={onOpen}
      bg="#2f913f"
      color="white"
      px={4}
      py={2}
      display="flex"
      alignItems="center"
      justifyContent="center"
      _hover={{
        bg: "DarkGreen",
      }}
      _active={{
        bg: "#2f913f",
      }}
      _focus={{
        boxShadow: "none",
      }}
      transition="background-color 0.3s"
    >
      Import New Role
    </Button>
  );
};

export default ImportRole;
