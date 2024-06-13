import { Flex, Card, CardBody, Box, Text, Stack } from "@chakra-ui/react";
import { InputNumber } from "antd";
import { DefaultizedPieValueType } from "@mui/x-charts";
import { PieChart, pieArcLabelClasses } from "@mui/x-charts/PieChart";
import ApiClient from "../../ApiClient";
import { SyllabusDetailOther } from "../../types";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
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
  quiz,
  assignment,
  final,
  finalPractice,
  finalTheory,
  gpa,
  deliveryPrinciple,
  setFinal,
  setQuiz,
  setAssignment,
  setDeliveryPrinciple,
  setFinalPractice,
  setFinalTheory,
  setGpa,
}: {
  quiz: number;
  assignment: number;
  final: number;
  finalTheory: number;
  finalPractice: number;
  gpa: number;
  deliveryPrinciple: string;
  setQuiz: (quiz: number) => void;
  setAssignment: (assignment: number) => void;
  setFinal: (final: number) => void;
  setFinalTheory: (finalTheory: number) => void;
  setFinalPractice: (finalPractice: number) => void;
  setGpa: (gpa: number) => void;
  setDeliveryPrinciple: (deliveryPrinciple: string) => void;
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
                    <InputNumber
                      width="100px"
                      type="number"
                      min={0}
                      max={100}
                      onChange={(value) => {
                        if (value) setQuiz(parseInt(value.toString()));
                      }}
                    />
                  </Flex>
                  <Flex alignItems="center">
                    <Text fontWeight={600} mr={5}>
                      Assignment*
                    </Text>
                    <InputNumber
                      width="100px"
                      type="number"
                      min={0}
                      max={100}
                      onChange={(value) => {
                        if (value) setAssignment(parseInt(value.toString()));
                      }}
                    />
                  </Flex>
                  <Flex alignItems="center">
                    <Text fontWeight={600} mr="76px">
                      Final*
                    </Text>
                    <InputNumber
                      width="100px"
                      type="number"
                      min={0}
                      max={100}
                      onChange={(value) => {
                        if (value) setFinal(parseInt(value.toString()));
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
                  <InputNumber
                    width="100px"
                    type="number"
                    min={0}
                    max={100}
                    onChange={(value) => {
                      if (value) setFinalTheory(parseInt(value.toString()));
                    }}
                  />
                </Flex>
                <Flex alignItems="center">
                  <Text fontWeight={600} mr={5}>
                    Practice Final*
                  </Text>
                  <InputNumber
                    width="100px"
                    type="number"
                    min={0}
                    max={100}
                    onChange={(value) => {
                      if (value) setFinalPractice(parseInt(value.toString()));
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
                <InputNumber
                  width="100px"
                  type="number"
                  min={0}
                  max={100}
                  onChange={(value) => {
                    if (value) setGpa(parseInt(value.toString()));
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
          height: 500,
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
          data=""
          onChange={(event, editor) => {
            setDeliveryPrinciple(editor.getData());
          }}
        ></CKEditor>
      </Card>
    </Box>
  );
};

export default SyllabusOther;
