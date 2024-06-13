import {
  Flex,
  Card,
  CardBody,
  Box,
  Text,
  Stack,
  Input,
} from "@chakra-ui/react";

import { DefaultizedPieValueType } from "@mui/x-charts";
import { PieChart, pieArcLabelClasses } from "@mui/x-charts/PieChart";
import { SyllabusDetailOther } from "../../types";
import { CKEditor } from "@ckeditor/ckeditor5-react";
import ClassicEditor from "@ckeditor/ckeditor5-build-classic";

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

const SyllabusOther = ({
  syllabusOther,
  setSyllabusOther,
}: {
  syllabusOther: SyllabusDetailOther;
  setSyllabusOther: (syllabusOther: SyllabusDetailOther) => void;
}) => {
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
                  <Flex alignItems="center">
                    <Text fontWeight={600} mr="76px">
                      Quiz*
                    </Text>
                    <Input
                      type="number"
                      min={1}
                      max={100}
                      value={syllabusOther?.quiz}
                      width="100px"
                      onChange={(value) => {
                        setSyllabusOther({
                          ...syllabusOther,
                          quiz: parseInt(value.target.value),
                        });
                      }}
                    />
                  </Flex>
                  <Flex alignItems="center">
                    <Text fontWeight={600} mr={5}>
                      Assignment*
                    </Text>
                    <Input
                      type="number"
                      min={1}
                      max={100}
                      value={syllabusOther?.assignment}
                      width="100px"
                      onChange={(value) => {
                        setSyllabusOther({
                          ...syllabusOther,
                          assignment: parseInt(value.target.value),
                        });
                      }}
                    />
                  </Flex>
                  <Flex alignItems="center">
                    <Text fontWeight={600} mr="76px">
                      Final*
                    </Text>
                    <Input
                      type="number"
                      min={1}
                      max={100}
                      defaultValue={syllabusOther?.final}
                      width="100px"
                      onChange={(value) => {
                        setSyllabusOther({
                          ...syllabusOther,
                          final: parseInt(value.target.value),
                        });
                      }}
                    />
                  </Flex>
                </Stack>
                <Stack direction={"column"}></Stack>
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
              <Flex alignItems="center" justifyContent="space-between">
                <Flex alignItems="center">
                  <Text fontWeight={600} mr={4}>
                    Theory Final*
                  </Text>
                  <Input
                    type="number"
                    min={1}
                    max={100}
                    value={syllabusOther?.finalTheory}
                    width="100px"
                    onChange={(value) => {
                      setSyllabusOther({
                        ...syllabusOther,
                        finalTheory: parseInt(value.target.value),
                      });
                    }}
                  />
                </Flex>
                <Flex alignItems="center">
                  <Text fontWeight={600} mr={5}>
                    Practice Final*
                  </Text>
                  <Input
                    type="number"
                    min={1}
                    max={100}
                    value={syllabusOther?.finalPractice}
                    width="100px"
                    onChange={(value) => {
                      setSyllabusOther({
                        ...syllabusOther,
                        finalPractice: parseInt(value.target.value),
                      });
                    }}
                  />
                </Flex>
              </Flex>
            </Box>
            <Box
              padding="30px"
              border="1px solid"
              borderRadius="20px"
              margin="10px"
            >
              <Text fontWeight="700" mb={5}>
                Passing criteria
              </Text>
              <Flex alignItems="center">
                <Text fontWeight={600} mr="80px">
                  GPA*
                </Text>
                <Input
                  type="number"
                  min={1}
                  max={100}
                  value={syllabusOther?.gpa}
                  width="100px"
                  onChange={(value) => {
                    setSyllabusOther({
                      ...syllabusOther,
                      gpa: parseInt(value.target.value),
                    });
                  }}
                />
              </Flex>
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
        <CKEditor
          editor={ClassicEditor}
          data={syllabusOther?.deliveryPrinciple}
          onChange={(event, editor) => {
            setSyllabusOther({
              ...syllabusOther,
              deliveryPrinciple: editor.getData(),
            });
          }}
        ></CKEditor>
      </Card>
    </Box>
  );
};

export default SyllabusOther;
