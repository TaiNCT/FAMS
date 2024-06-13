import {
  Text,
  Box,
  Stack,
  Badge,
  Flex,
  Center,
  Heading,
} from "@chakra-ui/react";
import { FaEllipsisH } from "react-icons/fa";
import {
  Menu,
  MenuButton,
  MenuList,
  MenuItem,
  IconButton,
} from "@chakra-ui/react";
import { MdEdit } from "react-icons/md";
import { HiOutlineDocumentDuplicate } from "react-icons/hi";
import { FaEyeSlash } from "react-icons/fa";
import { MdOutlineDeleteForever } from "react-icons/md";
import { Tabs, TabList, TabPanels, Tab, TabPanel } from "@chakra-ui/react";
import SyllabusGeneral from "./SyllabusGeneral";
import SyllabusOutline from "./SyllabusOutline";
import SyllabusOther from "./SyllabusOther";
import ApiClient from "../ApiClient";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import {
  SyllabusDetailGeneral,
  SyllabusDetailHeader,
  SyllabusDetailOther,
  SyllabusDetailOutline,
} from "../types";
import { FaEye } from "react-icons/fa6";
import GlobalLoading from "../../../global/GlobalLoading";
import { useLoading } from "../TableComponent";

export default function SyllabusComponent() {
  let apiClient = new ApiClient();
  let { id } = useParams();
  const [syllabusHeader, setSyllabusHeader] = useState<SyllabusDetailHeader>();
  const [syllabusGeneral, setSyllabusGeneral] =
    useState<SyllabusDetailGeneral>();
  const [syllabusOther, setSyllabusOther] = useState<SyllabusDetailOther>();
  const [syllabusDetailOutline, setsyllabusDetailOutline] =
    useState<Array<SyllabusDetailOutline>>();
  const navigate = useNavigate();
  const location = useLocation();
  const { isLoading, setIsLoading } = useLoading();

  useEffect(() => {
    FetchSyllabusHeader();
    FetchSyllabusGeneral();
    FetchSyllabusOther();
    FetchSyllabusOutline();
  }, []);

  useEffect(() => {
    if (location.state?.isLoading == true) {
      FetchSyllabusHeader();
      FetchSyllabusGeneral();
      FetchSyllabusOther();
      FetchSyllabusOutline();
      location.state.isLoading == false;
    }
  }, [location]);

  const handleDuplicate = async () => {
    const timestamp: number = new Date().getTime();

    if (id) {
      const createTopicCode = `${syllabusHeader.topicName
        .split(" ")
        .map((word) => word.charAt(0).toUpperCase())
        .join("")}_${timestamp}`;

      try {
        const response = await apiClient.duplicateSyllabus(id, createTopicCode);
        if (response) {
          navigate(`/syllabus/detail/${response.result.data.syllabusId}`, {
            state: { isLoading: true },
          });
        }
      } catch (error) {
        console.error(error);
      }
    }
  };

  const handleActive = async () => {
    if (id) {
      if (syllabusHeader?.status.toLowerCase() === "active") {
        // If the menu item is currently active, deactivate it
        await apiClient.activeDeactiveSyllabus(id, false);
      } else {
        // If the menu item is currently inactive, activate it
        await apiClient.activeDeactiveSyllabus(id, true);
      }
      FetchSyllabusHeader();
    }
  };

  const handleDelete = async () => {
    if (id) {
      try {
        const response = await apiClient.deleteSyllabus(id);
        if (response) navigate(`/syllabus`);
      } catch (error) {
        console.error(error);
      }
    }
  };

  async function FetchSyllabusOutline() {
    try {
      if (id) {
        setIsLoading(true);
        const data = await apiClient.getSyllabusDetailOutline(id);
        if (data?.isSuccess) {
          setIsLoading(false);
          setsyllabusDetailOutline(data.result.data);
        }
      }
    } catch (error) {
      setIsLoading(false);
      console.error(error);
    }
  }

  async function FetchSyllabusHeader() {
    try {
      if (id) {
        setIsLoading(true);
        const data = await apiClient.getSyllabusDetailHeader(id);
        if (data?.isSuccess) {
          setIsLoading(false);
          setSyllabusHeader(data.result.data);
        }
      }
    } catch (error) {
      setIsLoading(false);
      console.error(error);
    }
  }

  async function FetchSyllabusGeneral() {
    try {
      if (id) {
        setIsLoading(true);
        const data = await apiClient.getSyllabusDetailGeneral(id);
        if (data?.isSuccess) {
          setIsLoading(false);
          setSyllabusGeneral(data.result.data);
        }
      }
    } catch (error) {
      setIsLoading(false);
      console.error(error);
    }
  }

  async function FetchSyllabusOther() {
    try {
      if (id) {
        setIsLoading(true);
        const data = await apiClient.getSyllabusDetailOther(id);
        if (data?.isSuccess) {
          setIsLoading(false);
          setSyllabusOther(data.result.data);
        }
      }
    } catch (error) {
      setIsLoading(false);
      console.error(error);
    }
  }

  return (
    <>
      {isLoading ? <GlobalLoading isLoading={isLoading} /> : null}
      <Box p={4}>
        <Flex alignItems="center" justifyContent={"space-between"}>
          <Box fontWeight={600}>
            <Text
              as="h3"
              mt={5}
              mb={5}
              fontSize="2rem"
              fontStyle={"normal"}
              lineHeight={1.5}
              letterSpacing="6px"
            >
              Syllabus Detail
            </Text>
            <Flex mb="10px" color="white" gap={3}>
              <Center gap={3}>
                <Text fontSize="3xl" color={"#1a202c"} lineHeight="32px">
                  {syllabusHeader?.topicName}
                </Text>
                {syllabusHeader?.status.toLowerCase() === "active" ? (
                  <Badge
                    fontWeight="400"
                    fontSize="10px"
                    bg="#2D3748"
                    color="#fff"
                    padding={2}
                    borderRadius="999px"
                    background="#2f913f"
                  >
                    Active
                  </Badge>
                ) : syllabusHeader?.status.toLowerCase() === "draft" ? (
                  <Badge
                    fontWeight="400"
                    fontSize="10px"
                    bg="#9ca3af"
                    color="#fff"
                    padding={2}
                    borderRadius="999px"
                  >
                    Draft
                  </Badge>
                ) : (
                  <Badge
                    fontWeight="400"
                    fontSize="10px"
                    bg="#9ca3af"
                    color="#fff"
                    padding={2}
                    borderRadius="999px"
                    background="#2d3748"
                  >
                    Deactive
                  </Badge>
                )}
              </Center>
            </Flex>
            <Text fontWeight={600} fontSize={16} color={"#1a202c"}>
              {syllabusHeader?.topicCode}
            </Text>
          </Box>
          <Menu>
            <MenuButton
              as={IconButton}
              aria-label="Options"
              icon={<FaEllipsisH />}
              variant="outline"
            />
            <MenuList>
              <MenuItem
                onClick={() => navigate(`/syllabus/edit/${id}`)}
                icon={<MdEdit />}
              >
                Edit
              </MenuItem>
              <MenuItem
                onClick={handleDuplicate}
                icon={<HiOutlineDocumentDuplicate />}
              >
                Duplicate
              </MenuItem>
              {syllabusHeader?.status.toLowerCase() === "active" ? (
                <MenuItem onClick={handleActive} icon={<FaEyeSlash />}>
                  De-active
                </MenuItem>
              ) : (
                <MenuItem onClick={handleActive} icon={<FaEye />}>
                  Active
                </MenuItem>
              )}
              <MenuItem
                onClick={handleDelete}
                isDisabled={syllabusHeader?.status.toLowerCase() === "active"}
                disabled={syllabusHeader?.status.toLowerCase() === "active"}
                icon={<MdOutlineDeleteForever />}
              >
                Delete
              </MenuItem>
            </MenuList>
          </Menu>
        </Flex>
      </Box>
      <div
        style={{ width: "100%", height: "1px", backgroundColor: "#1a202c" }}
      ></div>
      <Box fontWeight={500} p={4}>
        <Stack direction={"row"} alignItems={"center"}>
          <Heading>{syllabusGeneral?.days ?? 7}</Heading>
          <Text>
            {syllabusGeneral?.days && syllabusGeneral.days > 1 ? `days` : "day"}{" "}
          </Text>
          <Text fontStyle="italic">
            {syllabusGeneral?.days && syllabusGeneral.days > 1
              ? `${syllabusGeneral.hours} hours`
              : "hour"}
          </Text>
        </Stack>
        {/* <Text>
          Modified on {syllabusHeader?.modifiedDate} by{" "}
          <b>{syllabusHeader?.modifiedBy}</b>
        </Text> */}
      </Box>

      <Box p={4}>
        <Tabs variant="unstyled">
          <TabList>
            <Tab
              style={{
                width: "200px",
                height: "30px",
                margin: "2px",
                borderTopRightRadius: "20px",
                borderTopLeftRadius: "20px",
              }}
              _selected={{
                color: "white",
                bg: "#2d3748",
                borderTopRightRadius: "20px",
                borderTopLeftRadius: "20px",
              }}
            >
              General
            </Tab>
            <Tab
              style={{
                width: "200px",
                height: "30px",
                margin: "2px",
                borderTopRightRadius: "20px",
                borderTopLeftRadius: "20px",
              }}
              _selected={{
                color: "white",
                bg: "#2d3748",
                borderTopRightRadius: "20px",
                borderTopLeftRadius: "20px",
              }}
            >
              Outline
            </Tab>
            <Tab
              style={{
                width: "200px",
                height: "30px",
                margin: "2px",
                borderTopRightRadius: "20px",
                borderTopLeftRadius: "20px",
              }}
              _selected={{
                color: "white",
                bg: "#2d3748",
                borderTopRightRadius: "20px",
                borderTopLeftRadius: "20px",
              }}
            >
              Others
            </Tab>
          </TabList>
          <TabPanels>
            <TabPanel>
              <SyllabusGeneral syllabusGeneral={syllabusGeneral} />
            </TabPanel>
            <TabPanel>
              <SyllabusOutline syllabusDetailOutline={syllabusDetailOutline} />
            </TabPanel>
            <TabPanel>
              <SyllabusOther syllabusOther={syllabusOther} />
            </TabPanel>
          </TabPanels>
        </Tabs>
      </Box>
    </>
  );
}
