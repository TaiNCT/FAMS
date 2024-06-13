// @ts-nocheck
import { Flex, Card, CardBody, Box, Text, Stack, Badge, TableContainer, Tag, Table, Tbody, Tr, Td, useDisclosure, ChakraProvider } from "@chakra-ui/react";
import { Accordion, AccordionItem, AccordionButton, AccordionPanel, AccordionIcon } from "@chakra-ui/react";
import { BiUserVoice } from "react-icons/bi";
import { MdOutlineSnippetFolder } from "react-icons/md";
import { MdOutlineBackHand } from "react-icons/md";
import { DefaultizedPieValueType } from "@mui/x-charts";
import { PieChart, pieArcLabelClasses } from "@mui/x-charts/PieChart";
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import ApiClient from "../../ApiClient";
import { SyllabusDetailOutline } from "../../types";
import MaterialPopup from "../../../TrainingProgramManagement/TrainingProgramMaterial/components/TrainingMaterial";

const data = [
  { label: "Assignment/Lab(54%)", value: 54, color: "#F4BE37" },
  { label: "Concept/Lecture(29%)", value: 29, color: "#FF9F40" },
  { label: "Guide/Review(9%)", value: 9, color: "#0D2535" },
  { label: "Test/Quiz (1%)", value: 1, color: "#5388D8" },
  { label: "Exam (6%)", value: 6, color: "#206EE5" },
];

const sizing = {
  margin: { right: 5 },
  width: 200,
  height: 200,
  legend: { hidden: true },
};
const TOTAL = data.map((item) => item.value).reduce((a, b) => a + b, 0);

const getArcLabel = (params: DefaultizedPieValueType) => {
  const percent = params.value / TOTAL;
  return `${(percent * 100).toFixed(0)}`;
};

const SyllabusOutline = ({syllabusDetailOutline}: {syllabusDetailOutline: SyllabusDetailOutline[]}) => {
  const { isOpen, onOpen, onClose } = useDisclosure();

  return (
    <Box fontWeight={500}>
      <Flex gap={3}>
        <Card
          width={"80%"}
          style={{ outline: "none", overflowY: "scroll", height: 500 }}
        >
          <CardBody style={{ padding: 0 }}>
            <Accordion defaultIndex={[0]} allowMultiple>
              {syllabusDetailOutline?.map((syllabusUnit, index) => (
                <AccordionItem>
                  <h2
                    style={{
                      borderTopLeftRadius: "10px",
                      borderTopRightRadius: "10px",
                      backgroundColor: "#2d3748",
                      color: "#fff",
                      fontWeight: "bold",
                    }}
                  >
                    <AccordionButton>
                      <Box fontWeight={600} as="span" flex="1" textAlign="left">
                        Day {syllabusUnit.dayNo}
                      </Box>
                      <AccordionIcon />
                    </AccordionButton>
                  </h2>
                  <AccordionPanel pb={4}>
                    <Accordion defaultIndex={[0]} allowMultiple>
                      {syllabusUnit.syllabusUnits?.map((unitChapter, index) => (
                        <AccordionItem>
                          <h2>
                            <AccordionButton>
                              <Box as="span" flex="1" textAlign="left">
                                <Flex gap={3}>
                                  <Text fontWeight={"bold"}>
                                    Unit {unitChapter.unitNo}
                                  </Text>
                                  <Text>{unitChapter.name}</Text>
                                </Flex>
                              </Box>
                              <AccordionIcon />
                            </AccordionButton>
                          </h2>
                          <AccordionPanel pb={4} ml={4}>
                            <Text style={{ fontStyle: "italic", fontSize: 13 }}>
                              {unitChapter.duration > 60 ? `${unitChapter.duration / 60} Hrs` : `${unitChapter.duration} Mins`}
                            </Text>
                            <TableContainer>
                              <Table variant="simple">
                                <Tbody>
                                  {unitChapter.unitChapters?.map(
                                    (unitItem, index) => (
                                      <Tr>
                                        <Td>{unitItem.name}</Td>
                                        <Td>
                                          <Badge
                                            style={{
                                              background: "#2D3748",
                                              color: "#fff",
                                              padding: "5px 10px",
                                              borderRadius: 10,
                                            }}
                                          >
                                            {unitItem.outputStandardName}
                                          </Badge>
                                        </Td>
                                        <Td>{unitItem.duration} mins</Td>
                                        <Td>
                                          {unitItem.isOnline ? (
                                            <Tag
                                              size={"md"}
                                              borderRadius="full"
                                              variant="solid"
                                              style={{
                                                color: "#fff",
                                                backgroundColor: "#2d3748",
                                              }}
                                            >
                                              Online
                                            </Tag>
                                          ) : (
                                            <Tag
                                              size={"md"}
                                              borderRadius="full"
                                              variant="solid"
                                              style={{
                                                color: "#D45B13",
                                                backgroundColor: "transparent",
                                                borderColor: "#D45B13",
                                                border: "1px solid",
                                              }}
                                            >
                                              Offline
                                            </Tag>
                                          )}
                                        </Td>
                                        <Td>
                                          <BiUserVoice fontSize={25} />
                                        </Td>
                                        <Td>
                                          <p onClick={onOpen}>
                                            <MdOutlineSnippetFolder
                                              fontSize={25}
                                            />
                                            <ChakraProvider>
                                              {/* <MaterialPopup
                                                isOpen={isOpen}
                                                onClose={onClose}
                                                dayNo={syllabusUnit.dayNo}
                                                unitChapter={unitChapter}
                                                unitChapterName={unitChapter.name}
                                              ></MaterialPopup> */}
                                            </ChakraProvider>
                                          </p>
                                        </Td>
                                      </Tr>
                                    )
                                  )}
                                  {/* <Tr>
                                    <Td>.NET Introduction</Td>
                                    <Td>
                                      <Badge
                                        style={{
                                          background: "#2D3748",
                                          color: "#fff",
                                          padding: "5px 10px",
                                          borderRadius: 10,
                                        }}
                                      >
                                        H4SD
                                      </Badge>
                                    </Td>
                                    <Td>30minus</Td>
                                    <Td>
                                      <Tag
                                        size={"md"}
                                        borderRadius="full"
                                        variant="solid"
                                        style={{
                                          color: "#D45B13",
                                          backgroundColor: "transparent",
                                          borderColor: "#D45B13",
                                          border: "1px solid",
                                        }}
                                      >
                                        Offline
                                      </Tag>
                                    </Td>
                                    <Td>
                                      <MdOutlineBackHand fontSize={25} />
                                    </Td>
                                    <Td>
                                      <MdOutlineSnippetFolder fontSize={25} />
                                    </Td>
                                  </Tr> */}
                                </Tbody>
                              </Table>
                            </TableContainer>
                          </AccordionPanel>
                        </AccordionItem>
                      ))}
                      {/* <AccordionItem>
                        <h2>
                          <AccordionButton>
                            <Box as="span" flex="1" textAlign="left">
                              <Flex gap={3}>
                                <Text fontWeight={"bold"}>Unit 5</Text>
                                <Text>Declaration & Assignment</Text>
                              </Flex>
                            </Box>

                            <AccordionIcon />
                          </AccordionButton>
                        </h2>
                        <AccordionPanel pb={4} ml={4}>
                          <Text style={{ fontStyle: "italic", fontSize: 13 }}>
                            3.5hrs
                          </Text>
                          <TableContainer>
                            <Table variant="simple">
                              <Tbody>
                                <Tr>
                                  <Td>.NET Introduction</Td>
                                  <Td>
                                    <Badge
                                      style={{
                                        background: "#2D3748",
                                        color: "#fff",
                                        padding: "5px 10px",
                                        borderRadius: 10,
                                      }}
                                    >
                                      H4SD
                                    </Badge>
                                  </Td>
                                  <Td>30minus</Td>
                                  <Td>
                                    <Tag
                                      size={"md"}
                                      borderRadius="full"
                                      variant="solid"
                                      style={{
                                        color: "#fff",
                                        backgroundColor: "#2d3748",
                                      }}
                                    >
                                      Online
                                    </Tag>
                                  </Td>
                                  <Td>
                                    <BiUserVoice fontSize={25} />
                                  </Td>
                                  <Td>
                                    <MdOutlineSnippetFolder fontSize={25} />
                                  </Td>
                                </Tr>
                                <Tr>
                                  <Td>.NET Introduction</Td>
                                  <Td>
                                    <Badge
                                      style={{
                                        background: "#2D3748",
                                        color: "#fff",
                                        padding: "5px 10px",
                                        borderRadius: 10,
                                      }}
                                    >
                                      H4SD
                                    </Badge>
                                  </Td>
                                  <Td>30minus</Td>
                                  <Td>
                                    <Tag
                                      size={"md"}
                                      borderRadius="full"
                                      variant="solid"
                                      style={{
                                        color: "#D45B13",
                                        backgroundColor: "transparent",
                                        borderColor: "#D45B13",
                                        border: "1px solid",
                                      }}
                                    >
                                      Online
                                    </Tag>
                                  </Td>
                                  <Td>
                                    <MdOutlineBackHand fontSize={25} />
                                  </Td>
                                  <Td>
                                    <MdOutlineSnippetFolder fontSize={25} />
                                  </Td>
                                </Tr>
                              </Tbody>
                            </Table>
                          </TableContainer>
                        </AccordionPanel>
                      </AccordionItem> */}
                    </Accordion>
                  </AccordionPanel>
                </AccordionItem>
              ))}

              {/* <AccordionItem>
                <h2
                  style={{
                    backgroundColor: "#2d3748",
                    color: "#fff",
                    fontWeight: "bold",
                  }}
                >
                  <AccordionButton>
                    <Box fontWeight={600} as="span" flex="1" textAlign="left">
                      Day 2
                    </Box>
                    <AccordionIcon />
                  </AccordionButton>
                </h2>
                <AccordionPanel pb={4}>
                  <Accordion defaultIndex={[0]} allowMultiple>
                    <AccordionItem>
                      <h2>
                        <AccordionButton>
                          <Box as="span" flex="1" textAlign="left">
                            <Flex gap={3}>
                              <Text fontWeight={"bold"}>Unit 5</Text>
                              <Text>.Net Introduction</Text>
                            </Flex>
                          </Box>

                          <AccordionIcon />
                        </AccordionButton>
                      </h2>
                      <AccordionPanel pb={4} ml={4}>
                        <Text style={{ fontStyle: "italic", fontSize: 13 }}>
                          3.5hrs
                        </Text>
                        <TableContainer>
                          <Table variant="simple">
                            <Tbody>
                              <Tr>
                                <Td>.NET Introduction</Td>
                                <Td>
                                  <Badge
                                    style={{
                                      background: "#2D3748",
                                      color: "#fff",
                                      padding: "5px 10px",
                                      borderRadius: 10,
                                    }}
                                  >
                                    H4SD
                                  </Badge>
                                </Td>
                                <Td>30minus</Td>
                                <Td>
                                  <Tag
                                    size={"md"}
                                    borderRadius="full"
                                    variant="solid"
                                    style={{
                                      color: "#fff",
                                      backgroundColor: "#2d3748",
                                    }}
                                  >
                                    Online
                                  </Tag>
                                </Td>
                                <Td>
                                  <BiUserVoice fontSize={25} />
                                </Td>
                                <Td>
                                  <MdOutlineSnippetFolder fontSize={25} />
                                </Td>
                              </Tr>
                              <Tr>
                                <Td>.NET Introduction</Td>
                                <Td>
                                  <Badge
                                    style={{
                                      background: "#2D3748",
                                      color: "#fff",
                                      padding: "5px 10px",
                                      borderRadius: 10,
                                    }}
                                  >
                                    H4SD
                                  </Badge>
                                </Td>
                                <Td>30minus</Td>
                                <Td>
                                  <Tag
                                    size={"md"}
                                    borderRadius="full"
                                    variant="solid"
                                    style={{
                                      color: "#D45B13",
                                      backgroundColor: "transparent",
                                      borderColor: "#D45B13",
                                      border: "1px solid",
                                    }}
                                  >
                                    Offline
                                  </Tag>
                                </Td>
                                <Td>
                                  <MdOutlineBackHand fontSize={25} />
                                </Td>
                                <Td>
                                  <MdOutlineSnippetFolder fontSize={25} />
                                </Td>
                              </Tr>
                            </Tbody>
                          </Table>
                        </TableContainer>
                      </AccordionPanel>
                    </AccordionItem>
                  </Accordion>
                </AccordionPanel>
              </AccordionItem> */}
            </Accordion>
          </CardBody>
        </Card>
        <Card width={300} style={{ outline: "none" }}>
          <Text
            style={{
              borderTopLeftRadius: "10px",
              borderTopRightRadius: "10px",
              backgroundColor: "#2d3748",
              color: "#fff",
              padding: 10,
            }}
            align={"center"}
            fontWeight={600}
            fontSize={16}
          >
            Time Allocation
          </Text>
          <CardBody>
            <Stack>
              <PieChart
                series={[
                  {
                    outerRadius: 80,
                    data,
                    arcLabel: getArcLabel,
                  },
                ]}
                sx={{
                  [`& .${pieArcLabelClasses.root}`]: {
                    fill: "white",
                    fontSize: 14,
                  },
                }}
                {...sizing}
              />

              {data.map((item) => (
                <Stack direction={"row"} alignItems={"center"}>
                  <Box
                    style={{
                      height: 20,
                      width: 20,
                      borderRadius: "100%",
                      backgroundColor: `${item.color}`,
                    }}
                  ></Box>
                  <Text>{item.label}</Text>
                </Stack>
              ))}
            </Stack>
          </CardBody>
        </Card>
      </Flex>
    </Box>
  );
};

export default SyllabusOutline;
