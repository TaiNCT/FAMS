import { Box, Flex } from "@chakra-ui/react";
import React, { useEffect } from "react";
import { MdOutlineSort } from "react-icons/md";
import styles from "./TableHeader.module.scss";

interface TableHeaderProps {
  text: string;
  size?: number;
  field: string;
  handleClick: (text: string, field: string) => void;
  selectedColumn: string;
  oldSelectedColumn: string;
  rotation: number;
  setRotation: React.Dispatch<React.SetStateAction<number>>;
}

const TableHeader: React.FC<TableHeaderProps> = ({
  text,
  size,
  field,
  handleClick,
  selectedColumn,
  oldSelectedColumn,
  rotation,
  setRotation,
}) => {
  useEffect(() => {
    if (oldSelectedColumn === text && selectedColumn !== text) {
      setRotation(0);
    }
  }, [oldSelectedColumn, selectedColumn, setRotation, text]);

  const handleIconClick = () => {
    handleClick(text, field);
    const newRotation =
      selectedColumn === text ? (rotation === 180 ? 360 : 180) : 0;
    setRotation(newRotation);
  };

  return (
    <Flex
      fontWeight="500"
      alignItems="center"
      gap={2}
      flex={size || 0}
      bgColor="#2D3748"
      color="white"
      py={3}
      pl={10}
    >
      {text}
      {text && (
        <Box
          className={`${styles.sortIcon}`}
          style={{
            opacity: selectedColumn === text ? 1 : undefined,
            transform: `rotate(${rotation}deg)`,
            transition: "transform 0.3s ease-in-out",
          }}
          onClick={handleIconClick}
        >
          <MdOutlineSort />
        </Box>
      )}
    </Flex>
  );
};

export default TableHeader;
