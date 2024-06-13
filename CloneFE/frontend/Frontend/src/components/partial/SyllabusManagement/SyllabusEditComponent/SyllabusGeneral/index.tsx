// @ts-nocheck
import {
  Flex,
  Card,
  CardBody,
  Box,
  Text,
  Stack,
  Select,
  Textarea,
  Input,
  NumberInputField,
} from "@chakra-ui/react";
import { SyllabusDetailGeneral } from "../../types";
import { CKEditor } from "@ckeditor/ckeditor5-react";
import ClassicEditor from "@ckeditor/ckeditor5-build-classic";
import { PieChart, pieArcLabelClasses } from "@mui/x-charts/PieChart";
import "../../SyllabusCreateComponent/SyllabusGeneral/style.scss";

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

const SyllabusGeneral = ({
  syllabusGeneral,
  setSyllabusGeneral,
}: {
  syllabusGeneral: SyllabusDetailGeneral;
  setSyllabusGeneral: (syllabusGeneral: SyllabusDetailGeneral) => void;
}) => {
  const handleEditorChange = (event: any, editor: ClassicEditor) => {
    const data = editor.getData();
    setSyllabusGeneral({ ...syllabusGeneral, courseObjective: data });
  };

  const Attendee = syllabusGeneral?.attendeeNumber;

  return (
    <Flex gap={3}>
      <Box fontWeight={500} width="75%">
        <Box gap={3}>
          <Flex align="center" mb={5}>
            <Text mr="20px" fontWeight={700}>
              Level
            </Text>
            <Select
              width="270px"
              borderRadius={10}
              value={syllabusGeneral?.level}
              boxShadow="2px 2px 5px rgba(0, 0, 0, 0.4)"
              onChange={(value) =>
                setSyllabusGeneral({
                  ...syllabusGeneral,
                  level: value.target.value,
                })
              }
            >
              <option key="Beginner" value="Beginner">
                Beginner
              </option>
              <option key="Advanced" value="Advanced">
                Advanced
              </option>
              <option key="Expert" value="Expert">
                Expert
              </option>
            </Select>
          </Flex>
          <Flex align="center" mb={5}>
            <Text mr="20px" fontWeight={700}>
              Attendee number
            </Text>
            <Input
              min={1}
              max={100}
              width="173px"
              value={Attendee}
              type="number"
              onChange={(value) => {
                setSyllabusGeneral({
                  ...syllabusGeneral,
                  attendeeNumber: parseInt(value.target.value),
                });
              }}
            ></Input>
          </Flex>
          <Box mb={5}>
            <Text fontWeight={700} mb={2}>
              Technical Requirement(s)
            </Text>
            <Textarea
              value={syllabusGeneral?.technicalRequirement}
              height="138px"
              borderRadius="10px"
              borderWidth={2}
              onChange={(value) => {
                setSyllabusGeneral({
                  ...syllabusGeneral,
                  technicalRequirement: value.target.value,
                });
              }}
            ></Textarea>
          </Box>
          <Box mb={5}>
            <Text fontWeight={700} mb={2}>
              Course Objectives
            </Text>
            <CKEditor
              editor={ClassicEditor}
              data={syllabusGeneral?.courseObjective}
              onChange={handleEditorChange}
            ></CKEditor>
          </Box>
        </Box>
      </Box>
      <Box width="20%" ml={5} fontWeight={500}>
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
      </Box>
    </Flex>
  );
};

export default SyllabusGeneral;
