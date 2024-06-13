import React from "react";
import {
  Text,
  Flex,
  Input,
  InputGroup,
  InputLeftElement,
  Button,
  Tag,
  TagCloseButton,
  TagLabel,
} from "@chakra-ui/react";
import { FaSearch } from "react-icons/fa";
import { IoAddCircleOutline } from "react-icons/io5";
import { MdFileUpload } from "react-icons/md";
import Filter from "./Filter";
import ImportDialog from "../../CreateTrainingProgram/components/Dialog";
import { ThemeProvider, createTheme } from "@mui/material/styles";
import { useNavigate } from "react-router-dom";
import { CiExport } from "react-icons/ci";
import { CiImport } from "react-icons/ci";
import axiosAuth from "../../api/axiosAuth";
import { toast } from "react-toastify";
import { ExportTrainingProgramRequest } from "../models/requests/exportTrainingProgram.model";

// const exportTrainingProgramRequest = (
//   status: string | string[],
//   createdBy: string | string[],
//   programTimeToFrameTo: Date,
//   programTimeToFrameFrom: Date,
//   sort: string | string[]
// ): ExportTrainingProgramRequest => {
//   return {
//     status,
//     createdBy,
//     programTimeToFrameTo,
//     programTimeToFrameFrom,
//     sort,
//   };
// };

interface HeaderProps {
  searchKeyword: string;
  tags: string[];
  setTags: React.Dispatch<React.SetStateAction<string[]>>;
  onFilter: (values: Record<string, string | string[]>) => void;
  handleSearch: (value: string) => void;
  handleResetTable: (filter?: boolean) => void;
  filterClearRef: React.RefObject<any>;
  filterValues: Record<string, string | string[]>;
  sort: string;
}

const Header: React.FC<HeaderProps> = ({
  searchKeyword,
  tags,
  setTags,
  onFilter,
  handleSearch,
  handleResetTable,
  filterClearRef,
  filterValues,
  sort,
}) => {
  const [open, setOpen] = React.useState<boolean>(false);
  const navigate = useNavigate();

  const handleAddTag = () => {
    if (!tags.includes(searchKeyword) && searchKeyword !== "") {
      setTags([...tags, searchKeyword.trim()]);
    }
  };

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    handleSearch(e.target.value);
  };

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleExport = async () => {
    const link = document.createElement("a");
    link.target = "_blank";
    link.download = "trainingprograms.xlsx";

    const { ...filterData } = filterValues;

    const params = {
      Status: filterData.status,
      CreatedBy: filterData.createdBy,
      ProgramTimeFrameFrom: filterData.programTimeFrameFrom
        ? new Date(filterData.programTimeFrameFrom.toString())
        : null,
      ProgramTimeFrameTo: filterData.programTimeFrameTo
        ? new Date(filterData.programTimeFrameTo.toString())
        : null,
      Sort: sort,
    };


    await axiosAuth
      .post(`/trainingprograms/export`, params, {
        responseType: "blob",
      })
      .then((res) => {
        link.href = URL.createObjectURL(
          new Blob([res.data], { type: "application/octet-stream" })
        );
        link.click();
      })
      .catch((error) => {
        if (error instanceof Error) {
          toast.error("File not found");
        } else {
        }
      });
  };

  return (
    <React.Fragment>
      <Text
        as="h3"
        mb={8}
        pl={10}
        py={4}
        letterSpacing={6}
        bg="#2d3748"
        color="#fff"
        fontSize="2rem"
        width="100%"
      >
        Training program
      </Text>

      <Flex justifyContent="space-between" w="full" mb={3}>
        <Flex gap={2} ml={10}>
          <InputGroup>
            <InputLeftElement cursor="pointer" onClick={handleAddTag}>
              <FaSearch color="#285D9A" />
            </InputLeftElement>
            <Input
              placeholder="Search by..."
              border="2px solid #2d3748"
              value={searchKeyword}
              onChange={handleSearchChange}
            />
          </InputGroup>
          <Filter
            onFilter={onFilter}
            handleResetTable={handleResetTable}
            ref={filterClearRef}
          />
        </Flex>
        <Flex gap={0} mr={0}>
          <Button
            bg="#D45B13"
            color="white"
            px={3}
            mr={3}
            _hover={{
              opacity: 0.9,
            }}
            onClick={handleExport}
          >
            <Text as="span" fontSize="25px" me={3}>
              <CiExport />{" "}
            </Text>
            Export
          </Button>
          <Button
            bg="#2f913f"
            color="white"
            px={3}
            mr={3}
            _hover={{
              opacity: 0.9,
            }}
            onClick={() => setOpen(true)}
          >
            <Text as="span" fontSize="25px" me={3}>
              <CiImport />{" "}
            </Text>
            Import
          </Button>
          <Button
            bg="#2d3748"
            color="white"
            px={3}
            mr={8}
            _hover={{
              opacity: 0.9,
            }}
            onClick={() => navigate(`/trainingprogram/create`)}
          >
            <Text as="span" fontSize="25px" me={3}>
              <IoAddCircleOutline />{" "}
            </Text>
            Add new
          </Button>
          <ThemeProvider theme={createTheme({})}>
            <ImportDialog open={open} setOpen={setOpen} />
          </ThemeProvider>
        </Flex>
      </Flex>
      <Flex gap={2} ml={10}>
        {tags.map((tag) => {
          return (
            <Tag
              borderRadius="5px"
              variant="solid"
              bgColor="#2D3748"
              key={`tag-${tag}`}
            >
              <TagLabel fontSize="12px">{tag}</TagLabel>
              <TagCloseButton
                onClick={() => {
                  setTags(tags.filter((t) => t !== tag));
                }}
              />
            </Tag>
          );
        })}
      </Flex>
    </React.Fragment>
  );
};

export default Header;
