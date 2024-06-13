import React from "react";
import { Button, Flex } from "@chakra-ui/react";

const SaveCancelButtons = ({ onSave, onCancel }) => {
  return (
    <Flex justifyContent="flex-end" mt={4} paddingRight="40px">
      <Button
        onClick={onCancel}
        bg="transparent"
        color="red.500"
        _hover={{ bg: 'red.100', color: 'red.700' }}
        _active={{ bg: 'red.100', color: 'red.700' }}
        mr={3}
      >
        Cancel
      </Button>
      <Button
        onClick={onSave}
        bg="#2D3748"
        color="white"
        _hover={{ bg: 'blue.800' }}
        _active={{ bg: 'blue.900' }}
      >
        Save
      </Button>
    </Flex>
  );
};

export default SaveCancelButtons;
