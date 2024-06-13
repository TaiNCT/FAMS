import React, { useMemo } from "react";
import { Stack, Text, Select } from "@chakra-ui/react";
import {
  ChevronLeftIcon,
  ChevronRightIcon,
  ArrowRightIcon,
} from "@chakra-ui/icons";
import { NavigationDotProps } from "./models/NavigationProp.model"
import PaginationButton from "./PaginationButton";
import styles from "./NavigationDot.module.scss";


const NavigationDot: React.FC<NavigationDotProps> = ({
  totalPages,
  currentPage,
  onPageChange,
  rowsPerPageOptions,
  onRowsPerPageChange,
}) => {
  const themeColors = {
    primaryColor: "#285D9A",
    secondaryColor: "#E2E8F0",
    whiteColor: "#fff",
  };

  const { primaryColor, secondaryColor, whiteColor } = themeColors;

  const setRowsPerPageOptions = () => {
    rowsPerPageOptions
  }

  const handlePageChange = (newPage: number) => {
    if (newPage >= 1 && newPage <= totalPages && newPage !== currentPage) {
      onPageChange(newPage);
    }
  };

  const handleRowsPerPageChange = (
    event: React.ChangeEvent<HTMLSelectElement>
  ) => {
    const newRowsPerPage = parseInt(event.target.value, 10);
    onRowsPerPageChange(newRowsPerPage);
  };

  const buttons = useMemo(() => {
    const pagesToShow = 1;
    const ellipsisThreshold = 1;
    const minTotalPagesToShowEllipsis = 6;

    const result: JSX.Element[] = [];

    for (let i = 1; i <= totalPages; i++) {
      if (
        i === 1 ||
        i === totalPages ||
        totalPages < minTotalPagesToShowEllipsis ||
        (i >= currentPage - pagesToShow && i <= currentPage + pagesToShow)
      ) {
        result.push(
          <PaginationButton
            key={i}
            onClick={() => handlePageChange(i)}
            bgColor={currentPage === i ? primaryColor : secondaryColor}
            color={currentPage === i ? whiteColor : primaryColor}
            hoverStyles={{ bg: primaryColor, color: whiteColor }}
            text={i.toString()}
            icon={null}
            fontSize="12px"
          />
        );
      } else if (
        (i === currentPage - pagesToShow - 1 &&
          currentPage - i > ellipsisThreshold) ||
        (i === currentPage + pagesToShow + 1 &&
          i - currentPage > ellipsisThreshold)
      ) {
        result.push(
          <PaginationButton
            key={i}
            onClick={() => {}}
            bgColor={whiteColor}
            color={primaryColor}
            hoverStyles={{ bg: primaryColor, color: whiteColor }}
            text="..."
            icon={null}
            fontSize="30px"
          />
        );
      }
    }

    return result;
  }, [currentPage, totalPages, primaryColor, secondaryColor, whiteColor]);

  return (
    <Stack
      direction="row"
      spacing={2}
      justify="center"
      align="center"
      mb={20}
      mt={6}
    >
      <Stack
        direction="row"
        spacing={2}
        align="center"
        justify="center"
        flex="1"
        ml={10}
      >
        <PaginationButton
          onClick={() => handlePageChange(currentPage - 1)}
          isDisabled={currentPage === 1}
          bgColor={whiteColor}
          color={primaryColor}
          hoverStyles={
            currentPage === 1 ? { bg: "inherit", color: "inherit" } : { bg: primaryColor, color: whiteColor }
          }          
          text=""
          fontSize="30px"
          icon={<ChevronLeftIcon />}
        />
        {buttons}
        <PaginationButton
          onClick={() => handlePageChange(currentPage + 1)}
          isDisabled={currentPage === totalPages}
          bgColor={whiteColor}
          color={primaryColor}
          hoverStyles={
            currentPage === totalPages
              ? { bg: "inherit", color: "inherit" }
              : { bg: primaryColor, color: whiteColor }
          }
          text=""
          fontSize="30px"
          icon={<ChevronRightIcon />}
        />
        <PaginationButton
          onClick={() => handlePageChange(totalPages)}
          isDisabled={currentPage === totalPages}
          bgColor={whiteColor}
          color={primaryColor}
          hoverStyles={
            currentPage === totalPages
              ? { bg: "inherit", color: "inherit" }
              : { bg: primaryColor, color: whiteColor }
          }
          icon={<ArrowRightIcon />}
        />
      </Stack>

      <Stack direction="row" spacing={2} align="center" mr={10}>
        <Text className={styles.NavDotText}>Rows per page</Text>
        <Select
          onChange={handleRowsPerPageChange}
          width="70px"
          className={styles.NavDotSelect}
        >
          {rowsPerPageOptions.map((row, i) => (
            <option key={i} className={styles.NavDotSelect} value={row}>
              {row}
            </option>
          ))}
        </Select>
      </Stack>
    </Stack>
  );
};

export default NavigationDot;
