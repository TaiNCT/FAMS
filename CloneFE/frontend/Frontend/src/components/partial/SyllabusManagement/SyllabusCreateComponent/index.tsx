import { Input, Text, Box, Flex, Button, FormControl } from "@chakra-ui/react";
import { Tabs, TabList, TabPanels, Tab, TabPanel } from "@chakra-ui/react";
import { useEffect, useState } from "react";
import SyllabusGeneral from "./SyllabusGeneral";
import SyllabusOutline from "./SyllabusOutline";
import SyllabusOther from "./SyllabusOther";
import { CreateSyllabus } from "../types";
import { Link } from "react-router-dom";
import {
  UseSyllabusGeneral,
  UseSyllabusHeader,
  UseSyllabusOther,
  UseSyllabusOutline,
} from "../states/States";
import ApiClient from "../ApiClient";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { useLoading } from "../TableComponent";
import GlobalLoading from "../../../global/GlobalLoading";

let apiClient = new ApiClient();

let Syllabus: CreateSyllabus = {
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

export default function SyllabusCreateComponent() {
  const [tabIndex, setTabIndex] = useState(0);
  const {
    topicCode,
    topicName,
    version,
    setTopicName,
    setTopicCode,
    setVersion,
  } = UseSyllabusHeader();
  const {
    level,
    attendeeNumber,
    technicalRequirement,
    courseObjective,
    setLevel,
    setAttendeeNumber,
    setTechnicalRequirement,
    setCourseObjective,
  } = UseSyllabusGeneral();
  const {
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
  } = UseSyllabusOther();
  const { syllabusDays, setSyllabusDays } = UseSyllabusOutline();
  const [createdBy, setCreatedBy] = useState<string>(
    localStorage.getItem("username") || "NPL"
  );
  const navigate = useNavigate();
  const { isLoading, setIsLoading } = useLoading();

  const handleSliderChange = (event: any) => {
    setTabIndex(parseInt(event.target.value, 10));
  };

  const handleTabsChange = (index: number) => {
    setTabIndex(index);
  };

  const saveSyllabus = async (isDraft?: boolean) => {
    Syllabus = {
      topicCode: topicCode,
      topicName: topicName,
      version: version,
      createdBy: createdBy,
      attendeeNumber: attendeeNumber,
      level: level,
      courseObjective: courseObjective,
      deliveryPrinciple: deliveryPrinciple,
      technicalRequirement: technicalRequirement,
      days: syllabusDays.length,
      hours: syllabusDays.reduce(
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
      syllabusDays: syllabusDays,
      assessmentSchemes: [
        {
          assignment: assignment,
          final: final,
          finalPractice: finalPractice,
          finalTheory: finalTheory,
          gpa: gpa,
          quiz: quiz,
        },
      ],
    };
    if (isDraft !== undefined) {
      setIsLoading(true);
      let response = await apiClient.createSyllabus(isDraft, Syllabus);
      if (response) {
        setIsLoading(false);
        toast.success("Create syllabus successfully.", {
          position: "top-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
          theme: "light",
        });
        setTimeout(() => {
          navigate("/syllabus");
        }, 500);
      }
      else {
        setIsLoading(false);
      }
    }
  };

  return (
    <>
      {isLoading ? <GlobalLoading isLoading={isLoading} /> : null}
      <Box>
        <h3
          style={{
            color: "#fff",
            margin: "2px 0px 0px",
            fontSize: "2rem",
            background: "#2d3748",
            width: "100%",
            height: "80px",
            letterSpacing: "6px",
            alignContent: "center",
            padding: "30px",
          }}
        >
          Create Syllabus
        </h3>
      </Box>
      <div
        style={{ width: "100%", height: "1px", backgroundColor: "#1a202c" }}
      ></div>
      <FormControl>
        <Box p={4} display="flex" alignItems="center">
          <Text fontWeight={700} mr={5}>
            Syllabus Name*
          </Text>
          <Input
            width="300px"
            fontWeight={600}
            onChange={(value) => {
              setTopicName(value.target.value);
            }}
            type="text"
            value={topicName}
          // isInvalid={topicNameIsEmpty}
          />
          <Text fontWeight={700} ml="200px" mr={5}>
            Code
          </Text>
          <Input
            width="100px"
            fontWeight={600}
            defaultValue="NPL"
            onChange={(value) => {
              setTopicCode(value.target.value);
            }}
            type="text"
            value={topicCode}
          />
          <Text fontWeight={700} ml="200px" mr={5}>
            Version
          </Text>
          <Input
            width="100px"
            fontWeight={600}
            defaultValue="1.0"
            onChange={(value) => {
              setVersion(value.target.value);
            }}
            type="text"
            value={version}
          />
        </Box>
      </FormControl>
      <Box p={4}>
        <Tabs>
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
                attendeeNumber={attendeeNumber}
                courseObjective={courseObjective}
                level={level}
                technicalRequirement={technicalRequirement}
                setAttendeeNumber={setAttendeeNumber}
                setCourseObjective={setCourseObjective}
                setLevel={setLevel}
                setTechnicalRequirement={setTechnicalRequirement}
              />
            </TabPanel>
            <TabPanel>
              <SyllabusOutline
                setSyllabusDays={setSyllabusDays}
                syllabusDays={syllabusDays}
              />
            </TabPanel>
            <TabPanel>
              <SyllabusOther
                assignment={assignment}
                deliveryPrinciple={deliveryPrinciple}
                final={final}
                finalPractice={finalPractice}
                finalTheory={finalTheory}
                gpa={gpa}
                quiz={quiz}
                setAssignment={setAssignment}
                setDeliveryPrinciple={setDeliveryPrinciple}
                setFinal={setFinal}
                setFinalPractice={setFinalPractice}
                setFinalTheory={setFinalTheory}
                setGpa={setGpa}
                setQuiz={setQuiz}
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
            background="#474747"
            borderRadius="10px"
            pl={10}
            pr={10}
            pt={0}
            pb={0}
            _hover={{
              color: "#ccc",
            }}
            onClick={() => {
              saveSyllabus(true);
            }}
          >
            Save as draft
          </Button>
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
            onClick={() => {
              saveSyllabus(false);
            }}
          >
            Save
          </Button>
        </Flex>
      </Box>
    </>
  );
}
