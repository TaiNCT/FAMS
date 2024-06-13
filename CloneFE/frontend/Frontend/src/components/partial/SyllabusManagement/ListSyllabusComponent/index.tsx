/* eslint-disable react/prop-types */
import {
  Box,
  Button,
  Flex,
  Input,
  InputGroup,
  Tag,
  TagCloseButton,
  Text,
} from "@chakra-ui/react";
import React, { Fragment, useEffect, useState } from "react";
import { FaSearch } from "react-icons/fa";
import { faCalendar } from "@fortawesome/free-regular-svg-icons";
import { IoAddCircleOutline } from "react-icons/io5";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { CgPushUp } from "react-icons/cg";
import { TfiImport } from "react-icons/tfi";
import DataTable, { useSyllabusStore } from "../TableComponent";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "./style.scss";
import { useNavigate } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import ExportButton from "../layouts/ExportButton";

const Header = ({
  tags,
  setTags,
  startDate,
  setStartDate,
  endDate,
  setEndDate,
  setShowImportModal,
  searchValue,
  setSearchValue,
  rows,
}: any) => {
  const [isOpen, setIsOpen] = useState(false);
  const navigate = useNavigate();

  const addTags = (e: React.KeyboardEvent<HTMLInputElement>) => {
    // if (e.key === "Enter" && searchValue.trim()) {
    //   if (!tags.includes(searchValue.trim())) {
    //     setTags([...tags, searchValue.trim()]);
    //     setSearchValue("");
    //   }
    // }
  };

  const deleteTags = (value: any) => {
    let remindTags = tags.filter((t: any) => t !== value);
    setTags(remindTags);
  };

  const DatePickerCustom = () => {
    return (
      <DatePicker
        selected={startDate}
        onChange={handleDateChange}
        startDate={startDate}
        endDate={endDate}
        selectsRange
        inline={isOpen}
        onClickOutside={() => setIsOpen(false)}
        onSelect={() => setIsOpen(true)}
      />
    );
  };

  const handleDateChange = (dates: any) => {
    const [start, end] = dates;
    setStartDate(start);
    setEndDate(end);
    setIsOpen(false);
  };

  return (
    <React.Fragment>
      <h3
        style={{
          margin: "2px 0px 30px",
          padding: "30px",
          color: "#fff",
          background: "#2d3748",
          alignContent: "center",
          height: "80px",
          fontSize: "2rem",
          width: "100%",
          letterSpacing: "6px",
        }}
      >
        Syllabus
      </h3>
      <Flex width="100%" justifyContent="space-between" w="full" mb={3}>
        <Flex gap={2} marginLeft={5}>
          <InputGroup>
            <Input
              type="Text"
              w={300}
              h="36px"
              marginLeft="10px"
              paddingLeft="40px"
              placeholder="Search by..."
              borderRadius={8}
              border="1px solid #8B8B8B"
              fontSize={12}
              fontStyle="italic"
              fontWeight={500}
              lineHeight={1.5}
              boxShadow="2px 2px 5px rgba(0, 0, 0, 0.4)"
              value={searchValue}
              onChange={(e) => {
                setSearchValue(e.target.value);
              }}
              onKeyDown={addTags}
            />
            <Button
              pointerEvents="none"
              background="transparent"
              position="absolute"
              left="1px"
              top="45%"
              transform="translateY(-50%)"
            >
              <FaSearch />
            </Button>
          </InputGroup>
          <InputGroup>
            <Box
              position="relative"
              display={"block"}
              maxW={"100%"}
              marginLeft={"10px"}
            >
              <Input
                w={300}
                h="36px"
                paddingLeft="40px"
                placeholder="Created date"
                borderRadius={8}
                border="1px solid #8B8B8B"
                fontSize={12}
                fontStyle="italic"
                fontWeight={500}
                lineHeight={1.5}
                boxShadow="2px 2px 5px rgba(0, 0, 0, 0.4)"
                value={
                  startDate && endDate
                    ? `${startDate.toLocaleDateString()} - ${endDate.toLocaleDateString()}`
                    : ""
                }
                onClick={() => setIsOpen(true)}
              />
              {isOpen && (
                <Box
                  zIndex={"1000"}
                  position="absolute"
                  top={"105%"}
                  marginX={"5px"}
                  maxH={"90vh"}
                  maxW={"90vw"}
                  overflow={"auto"}
                >
                  <DatePickerCustom />
                </Box>
              )}
            </Box>
            <Button
              pointerEvents="none"
              background="transparent"
              position="absolute"
              left="1px"
              top="45%"
              transform="translateY(-50%)"
            >
              <FontAwesomeIcon icon={faCalendar} />
            </Button>
          </InputGroup>
        </Flex>
        <Flex gap={2} marginRight={5}>
          <Button
            bg="var(--orange)"
            color="white"
            px={3}
            fontSize={14}
            fontWeight={700}
            _hover={{
              bgColor: "gray",
            }}
            shadow="0px 1px 3px 1px rgba(0, 0, 0, 0.15), 0px 1px 2px 0px rgba(0, 0, 0, 0.30)"
          >
            <ExportButton rows={rows} />
          </Button>

          <Button
            bg="#2F903F"
            color="white"
            px={3}
            fontSize={14}
            fontWeight={700}
            _hover={{
              bgColor: "gray",
            }}
            shadow="0px 1px 3px 1px rgba(0, 0, 0, 0.15), 0px 1px 2px 0px rgba(0, 0, 0, 0.30)"
            onClick={() => setShowImportModal(true)}
          >
            <Text as="span" fontSize="25px" me={3}>
              <TfiImport />{" "}
            </Text>
            Import
          </Button>
          <Button
            bg="#2D3748"
            color="white"
            px={3}
            fontSize={14}
            fontWeight={700}
            _hover={{
              bgColor: "gray",
            }}
            shadow="0px 1px 3px 1px rgba(0, 0, 0, 0.15), 0px 1px 2px 0px rgba(0, 0, 0, 0.30)"
            onClick={() => {
              navigate("/syllabus/create");
            }}
          >
            <Text as="span" fontSize="25px" me={3}>
              <IoAddCircleOutline />{" "}
            </Text>
            Add Syllabus
          </Button>
        </Flex>
      </Flex>
      <Flex gap={2}>
        {tags.map((item, index) => {
          return (
            <Tag
              h="30px"
              borderRadius="8px"
              border="1px solid #fff"
              variant="solid"
              bgColor="#474747"
              fontSize={12}
              fontWeight={500}
              fontStyle="italic"
              key={index}
            >
              {item}
              <TagCloseButton onClick={() => deleteTags(item)} />
            </Tag>
          );
        })}
      </Flex>
    </React.Fragment>
  );
};

export default function SyllabusListing() {
  const [tags, setTags] = useState([]);
  const [startDate, setStartDate] = useState<Date | undefined>();
  const [endDate, setEndDate] = useState<Date | undefined>();
  const [showImportModal, setShowImportModal] = useState(false);
  const [searchValue, setSearchValue] = useState("");
  const { rows, setRows } = useSyllabusStore();
  const handleOnclose = () => setShowImportModal(false);
  return (
    <Fragment>
      <Box mb={5}>
        <Header
          rows={rows}
          tags={tags}
          setTags={setTags}
          startDate={startDate}
          setStartDate={setStartDate}
          endDate={endDate}
          setEndDate={setEndDate}
          setShowImportModal={setShowImportModal}
          searchValue={searchValue}
          setSearchValue={setSearchValue}
        />
      </Box>
      <Box>
        <DataTable
          rows={rows}
          setRows={setRows}
          startDate={startDate}
          endDate={endDate}
          tags={tags}
          showImportModal={showImportModal}
          handleOnclose={handleOnclose}
          searchValue={searchValue}
        />
      </Box>
    </Fragment>
  );
}
