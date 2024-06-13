import {
  Text,
  Box,
  Input,
  Flex,
  Button,
  Center,
  Badge,
} from "@chakra-ui/react";
import { FaEllipsisH, FaEye } from "react-icons/fa";
import {
  Menu,
  MenuButton,
  MenuList,
  MenuItem,
  IconButton,
} from "@chakra-ui/react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { HiOutlineDocumentDuplicate } from "react-icons/hi";
import { FaEyeSlash } from "react-icons/fa";
import { MdOutlineDeleteForever } from "react-icons/md";
import { Tabs, TabList, TabPanels, Tab, TabPanel } from "@chakra-ui/react";

import SyllabusGeneral from "./SyllabusGeneral";
import SyllabusOutline from "./SyllabusOutline";
import SyllabusOther from "./SyllabusOther";
import ApiClient from "../ApiClient";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import {
  SyllabusDetailGeneral,
  SyllabusDetailHeader,
  SyllabusDetailOther,
  SyllabusDetailOutline,
  UpdateSyllabus,
} from "../types";
import { UseSyllabusOutline } from "../states/States";
import GlobalLoading from "../../../global/GlobalLoading";
import { useLoading } from "../TableComponent";

export default function SyllabusComponent() {
  let apiClient = new ApiClient();
  let { id } = useParams();
  let syllabus: UpdateSyllabus = {
    syllabusId: "",
    topicCode: "",
    topicName: "",
    version: "",
    createdBy: "",
    attendeeNumber: 0,
    level: "",
    technicalRequirement: "",
    courseObjective: "",
    deliveryPrinciple: "",
    days: 0,
    hours: 0,
    assessmentSchemes: [],
    syllabusDays: [],
  };
  const [syllabusHeader, setSyllabusHeader] = useState<SyllabusDetailHeader>();
  const [syllabusGeneral, setSyllabusGeneral] =
    useState<SyllabusDetailGeneral>();
  const [syllabusDetailOutline, setsyllabusDetailOutline] =
    useState<Array<SyllabusDetailOutline>>();
  const [syllabusOther, setSyllabusOther] = useState<SyllabusDetailOther>();
  const { syllabusDays, setSyllabusDays } = UseSyllabusOutline();
  const navigate = useNavigate();
  const location = useLocation();
  const { isLoading, setIsLoading } = useLoading();

  useEffect(() => {
    FetchSyllabusHeader();
    FetchSyllabusGeneral();
    FetchSyllabusOutline();
    FetchSyllabusOther();
  }, []);

  useEffect(() => {
    if (location.state?.isLoading == true) {
      FetchSyllabusHeader();
      FetchSyllabusGeneral();
      FetchSyllabusOutline();
      FetchSyllabusOther();
      location.state.isLoading == false;
    }
  }, [location]);

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

  const handleUpdate = async () => {
    syllabus = {
      syllabusId: id,
      topicCode: syllabusHeader?.topicCode,
      topicName: syllabusHeader?.topicName,
      version: syllabusHeader?.version,
      createdBy: localStorage.getItem("username") || "NPL",
      attendeeNumber: syllabusGeneral?.attendeeNumber || 30,
      level: syllabusGeneral?.level || "Beginner",
      courseObjective: syllabusGeneral?.courseObjective,
      deliveryPrinciple: syllabusOther?.deliveryPrinciple,
      technicalRequirement: syllabusGeneral?.technicalRequirement || "string1",
      days: syllabusDetailOutline.length,
      hours: syllabusDetailOutline.reduce(
        (acc, val) =>
          acc +
          val.syllabusUnits.reduce(
            (acc, val) =>
              acc +
              val.unitChapters.reduce((acc, val) => acc + val.duration, 0),
            0
          ),
        0
      ),
      syllabusDays: syllabusDetailOutline.map((syllabusUnit, index) => {
        // code logic here
        return {
          dayNo: syllabusUnit.dayNo,
          syllabusUnits: syllabusUnit.syllabusUnits.map(
            (unitChapter, index) => {
              return {
                name: unitChapter.name,
                unitNo: index + 1,
                duration: unitChapter.unitChapters.reduce(
                  (total, chapter) => total + chapter.duration,
                  0
                ),
                unitChapters: unitChapter.unitChapters.map((chapter, index) => {
                  if (chapter.isOnline === undefined) {
                    chapter.isOnline = false;
                  }
                  return {
                    chapterNo: index + 1,
                    duration: chapter.duration,
                    isOnline: chapter.isOnline,
                    name: chapter.name,
                    deliveryTypeId: chapter.deliveryTypeId,
                    outputStandardId: chapter.outputStandardId,
                    trainingMaterials: [
                      {
                        fileName: "filename",
                        isFile: false,
                        name: "filename",
                      },
                    ],
                  };
                }),
              };
            }
          ),
        };
      }),
      assessmentSchemes: [
        {
          assignment: syllabusOther?.assignment || 10,
          final: syllabusOther?.final || 10,
          finalPractice: syllabusOther?.finalPractice || 10,
          finalTheory: syllabusOther?.finalTheory || 10,
          gpa: syllabusOther?.gpa || 10,
          quiz: syllabusOther?.quiz || 10,
        },
      ],
    };
    setIsLoading(true);
    const response = await apiClient.updateSyllabus(syllabus);
    if (response) {
      setIsLoading(false);
    } else {
      setIsLoading(false);
    }
  };

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
          navigate(`/syllabus/edit/${response.result.data.syllabusId}`, {
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

  return (
    <>
      {isLoading ? <GlobalLoading isLoading={isLoading} /> : null}
      <Box p={4}>
        <Flex alignItems="center" justifyContent={"space-between"}>
          <Box fontWeight={500}>
            <Text
              mt={5}
              mb={5}
              fontSize="2rem"
              fontStyle={"normal"}
              lineHeight={1.5}
              letterSpacing="6px"
            >
              Edit Syllabus
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
                disabled={syllabusHeader?.status.toLowerCase() === "active"}
                isDisabled={syllabusHeader?.status.toLowerCase() === "active"}
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
      <Box p={4} display="flex" alignItems="center">
        <Text fontWeight={700} mr={5}>
          Syllabus Name*
        </Text>
        <Input
          width="300px"
          fontWeight={600}
          value={syllabusHeader?.topicName}
          onChange={(value) => {
            setSyllabusHeader({
              ...syllabusHeader,
              topicName: value.target.value.toString(),
            });
          }}
        />
        <Text fontWeight={700} ml="200px" mr={5}>
          Code
        </Text>
        <Input
          width="100px"
          fontWeight={600}
          value={syllabusHeader?.topicCode}
          onChange={(value) => {
            setSyllabusHeader({
              ...syllabusHeader,
              topicCode: value.target.value.toString(),
            });
          }}
        />
        <Text fontWeight={700} ml="200px" mr={5}>
          Version
        </Text>
        <Input
          width="100px"
          fontWeight={600}
          value={syllabusHeader?.version}
          onChange={(value) => {
            setSyllabusHeader({
              ...syllabusHeader,
              version: value.target.value.toString(),
            });
          }}
        />
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
              <SyllabusGeneral
                syllabusGeneral={syllabusGeneral}
                setSyllabusGeneral={setSyllabusGeneral}
              />
            </TabPanel>
            <TabPanel>
              <SyllabusOutline
                syllabusDays={syllabusDays}
                setSyllabusDays={setSyllabusDays}
                syllabusDetailOutline={syllabusDetailOutline}
                setsyllabusDetailOutline={setsyllabusDetailOutline}
              />
            </TabPanel>
            <TabPanel>
              <SyllabusOther
                syllabusOther={syllabusOther}
                setSyllabusOther={setSyllabusOther}
              />
            </TabPanel>
          </TabPanels>
        </Tabs>
        <Flex justifyContent="right" mt={5}>
          <Link to="/syllabus">
            <Button
              mr={2}
              color="red"
              textDecor="underline"
              background="none"
              _hover={{
                background: "none",
                color: "#ccc",
              }}
            >
              Cancel
            </Button>
          </Link>

          <Button
            mr={2}
            color="#fff"
            background="#2D3748"
            borderRadius="10px"
            pl={10}
            pr={10}
            pt={0}
            pb={0}
            _hover={{
              color: "#ccc",
            }}
            onClick={handleUpdate}
          >
            Save
          </Button>
        </Flex>
      </Box>
    </>
  );
}
