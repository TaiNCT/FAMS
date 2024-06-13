import React from "react";
import { Button } from "@chakra-ui/react";

const UpdateButton = ({ onEditModeChange }) => {
  return (
    <Button
      onClick={onEditModeChange} // Trigger the toggle edit mode function
      bg="#2D3748"
      color="white"
      px={4}
      py={2}
      display="flex"
      alignItems="center"
      justifyContent="center"
      _hover={{
        bg: "#4A5568",
      }}
      _active={{
        bg: "#2D3748",
      }}
      _focus={{
        boxShadow: "none",
      }}
      transition="background-color 0.3s"
    >
      Update Permission
    </Button>
  );
};

export default UpdateButton;
