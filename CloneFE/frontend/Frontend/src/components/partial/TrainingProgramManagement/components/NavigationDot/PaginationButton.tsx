import React from "react";
import { Button } from "@chakra-ui/react";
import {PaginationButtonProps} from "./models/PaginationButton.model";

const PaginationButton: React.FC<PaginationButtonProps> = ({
  onClick,
  isDisabled,
  bgColor,
  color,
  hoverStyles,
  text,
  fontSize,
  icon,
}) => {
  return (
    <Button
      onClick={onClick}
      isDisabled={isDisabled}
      bgColor={bgColor}
      color={color}
      _hover={hoverStyles}
      fontSize={fontSize}
      padding="0"
      borderRadius="full"
      mr={1}
      ml={1}
    >
      {icon}
      {text}
    </Button>
  );
};

export default PaginationButton;
