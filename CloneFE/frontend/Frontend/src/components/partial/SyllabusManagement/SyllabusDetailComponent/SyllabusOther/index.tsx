import {
  Flex,
  Card,
  CardBody,
  Box,
  Text,
  Stack,
  UnorderedList,
  ListItem,
} from "@chakra-ui/react";


import { DefaultizedPieValueType } from "@mui/x-charts";
import { PieChart, pieArcLabelClasses } from "@mui/x-charts/PieChart";
import { MdOutlineVerifiedUser } from "react-icons/md";
import { SyllabusDetailOther } from "../../types";

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

const SyllabusOther = ({ syllabusOther }: { syllabusOther: SyllabusDetailOther }) => {

  function removeHtmlTags() {
    const regex = /<[^>]*>/g;
    return syllabusOther?.deliveryPrinciple.replace(regex, '');
  }

  return (
    <Box fontWeight={500}>
      <Flex gap={3}>
        <Card width={"50%"} style={{ outline: "none" }}>
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
          <CardBody height="500px">
            <Stack direction={"row"}>
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

              <Box>
                {data.map((item) => (
                  <Stack direction={"row"} alignItems={"center"} gap={3} m={2}>
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
              </Box>
            </Stack>
          </CardBody>
        </Card>
        <Card width={"50%"} style={{ outline: "none" }}>
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
            Assessment scheme
          </Text>
          <CardBody style={{ padding: 0 }}>
            <Box
              style={{
                padding: "30px",
                border: "1px solid",
                borderRadius: "20px",
                margin: "10px",
              }}
            >
              <Flex
                direction={"row"}
                alignItems={"center"}
                justifyContent={"space-between"}
              >
                <Stack direction={"column"}>
                  <Stack direction={"row"}>
                    <Text fontWeight={600} marginBottom={"60px"}>
                      Quiz
                    </Text>
                    <Text fontStyle={"italic"}>{syllabusOther?.quiz}%</Text>
                  </Stack>
                  <Stack direction={"row"}>
                    <Text fontWeight={600}>Final</Text>
                    <Text fontStyle={"italic"}>{syllabusOther?.final}%</Text>
                  </Stack>
                </Stack>
                <Stack direction={"column"}>
                  <Stack direction={"row"} marginBottom={"60px"}>
                    <Text fontWeight={600}>Assignment</Text>
                    <Text fontStyle={"italic"}>
                      {syllabusOther?.assignment}%
                    </Text>
                  </Stack>
                  <Stack direction={"row"}>
                    <Text fontWeight={600}>Final Pratice</Text>
                    <Text fontStyle={"italic"}>
                      {syllabusOther?.finalPractice}%
                    </Text>
                  </Stack>
                </Stack>
              </Flex>
            </Box>
            <Box
              style={{
                padding: "30px",
                border: "1px solid",
                borderRadius: "20px",
                margin: "10px",
              }}
            >
              <Box>
                <Stack direction={"row"}>
                  <Text fontWeight={600}>Final Theory</Text>
                  <Text fontStyle={"italic"}>
                    {syllabusOther?.finalTheory}%
                  </Text>
                </Stack>
                <Stack direction={"row"} mt={18}>
                  <Text fontWeight={600}>GPA *</Text>
                  <Text fontStyle={"italic"}>{syllabusOther?.gpa}%</Text>
                </Stack>
              </Box>
            </Box>
          </CardBody>
        </Card>
      </Flex>
      <Card
        style={{
          outline: "none",
          marginTop: "20px",
          height: 700,
          overflowY: "scroll",
        }}
      >
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
          Training delivery principle
        </Text>
        <CardBody>
          <Flex alignItems={"flex-start"} m={10} gap={20}>
            <Stack
              style={{ minWidth: 300 }}
              direction={"row"}
              alignItems={"center"}
            >
              <MdOutlineVerifiedUser fontSize={20} />
              <Text fontWeight={"bold"}>Training</Text>
            </Stack>
            <UnorderedList>
              <ListItem>
                {removeHtmlTags()}
              </ListItem>
              <ListItem>
                Understand basic concepts of high-level programming languages
                (keyword, statement, operator, control-of-flow)
              </ListItem>
              <ListItem>
                Understand and distinguish two concepts: class (Class) and
                object (Object)
              </ListItem>
              <ListItem>
                Understand and apply object-oriented programming knowledge to
                resolve simple problems (Inheritance, Encapsulation,
                Abstraction, Polymorphism)
              </ListItem>
              <ListItem>
                Working with some of the existing data structures in C# (List,
                ArrayList, HashTable, Dictionary)
              </ListItem>
              <ListItem>
                Know how to control program errors (use try ... catch..finally,
                throw, throws)
              </ListItem>
              <ListItem>
                Be able to working with concurrency and multi-thread in C#
              </ListItem>
              <ListItem>
                Be able to working with common classes in ADO.net:
                SqlConnection, SqlCommand, SqlParameter, SqlDataAdapter,
                SqlDataReader{" "}
              </ListItem>
              <ListItem>
                Be able to manipulate SQL data from Window Form Application via
                4 basic commands: Add, Update, Delete, Select
              </ListItem>
              <ListItem>
                Know how to design UI screen in Window Form Application
              </ListItem>
              <ListItem>
                Know how to use approciate controls for each field/data type:
                Textbox, Label, Combobox, Radio, DateTimePicker, NumericUpDown,
                RichTextBox
              </ListItem>
            </UnorderedList>
          </Flex>
          <Flex alignItems={"flex-start"} m={10} gap={20}>
            <Stack
              style={{ minWidth: 300 }}
              direction={"row"}
              alignItems={"center"}
            >
              <MdOutlineVerifiedUser fontSize={20} />
              <Text fontWeight={"bold"}>Re-test</Text>
            </Stack>
            <UnorderedList>
              <ListItem>
                Only allow each student to retake the test up to 2 times
              </ListItem>
              <ListItem>Re-exam the same structure as the Final Test</ListItem>
            </UnorderedList>
          </Flex>
          <Flex alignItems={"flex-start"} m={10} gap={20}>
            <Stack
              style={{ minWidth: 300 }}
              direction={"row"}
              alignItems={"center"}
            >
              <MdOutlineVerifiedUser fontSize={20} />
              <Text fontWeight={"bold"}>Marking</Text>
            </Stack>
            <UnorderedList>
              <ListItem>Mentor review students on 2 Assignments</ListItem>
              <ListItem>
                Mentor marks the 3 Quizzes and Final Exam Theory
              </ListItem>
              <ListItem>Trainer marks the Final Exam Practice</ListItem>
              <ListItem>
                If the trainees have to retake test, the score will be
                calculated:
              </ListItem>
              <ListItem>The score =6, the score will be 6</ListItem>
              <ListItem>The scroe 6, the score will be that score</ListItem>
            </UnorderedList>
          </Flex>
          <Flex alignItems={"flex-start"} m={10} gap={20}>
            <Stack
              style={{ minWidth: 300 }}
              direction={"row"}
              alignItems={"center"}
            >
              <MdOutlineVerifiedUser fontSize={20} />
              <Text fontWeight={"bold"}>Waiver Criteria</Text>
            </Stack>
            <UnorderedList>
              <ListItem>Students pass the quick test</ListItem>
              <ListItem>Trainer Audit: rank B</ListItem>
            </UnorderedList>
          </Flex>
          <Flex alignItems={"flex-start"} m={10} gap={20}>
            <Stack
              style={{ minWidth: 300 }}
              direction={"row"}
              alignItems={"center"}
            >
              <MdOutlineVerifiedUser fontSize={20} />
              <Text fontWeight={"bold"}>Other</Text>
            </Stack>
            <UnorderedList>
              <ListItem>
                Trainers can allow students to complete homework and submit the
                next day
              </ListItem>
            </UnorderedList>
          </Flex>
        </CardBody>
      </Card>
    </Box>
  );
};

export default SyllabusOther;
