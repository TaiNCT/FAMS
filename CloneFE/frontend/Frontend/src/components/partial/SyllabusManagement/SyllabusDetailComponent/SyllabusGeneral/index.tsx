import {
  Flex,
  Card,
  CardBody,
  Box,
  Text,
  Stack,
  Badge,
  UnorderedList,
  ListItem,
} from "@chakra-ui/react";
import { FaRegStar } from "react-icons/fa";
import { MdOutlinePeopleAlt } from "react-icons/md";
import { AiOutlineFileProtect } from "react-icons/ai";
import { IoSettingsOutline } from "react-icons/io5";
import { MdFilterCenterFocus } from "react-icons/md";
import { SyllabusDetailGeneral } from "../../types";

const SyllabusGeneral = ({ syllabusGeneral }: { syllabusGeneral: SyllabusDetailGeneral }) => {
  function removeHtmlTags() {
    const regex = /<[^>]*>/g;
    return syllabusGeneral?.courseObjective.replace(regex, '');
  }
  return (
    <Box fontWeight={500}>
      <Flex gap={3}>
        <Card minW={400}>
          <CardBody>
            <Stack
              direction={"row"}
              justifyContent={"space-between"}
              alignItems={"center"}
              p={2}
            >
              <Stack direction={"row"}>
                <FaRegStar />
                <Text fontWeight={600} fontSize={16}>
                  Level
                </Text>
              </Stack>
              <Text fontSize={16}>{syllabusGeneral?.level}</Text>
            </Stack>

            <Stack
              direction={"row"}
              justifyContent={"space-between"}
              alignItems={"center"}
              p={2}
            >
              <Stack direction={"row"}>
                <MdOutlinePeopleAlt />
                <Text fontWeight={600} fontSize={16}>
                  Attendee number
                </Text>
              </Stack>
              <Text fontSize={16}>{syllabusGeneral?.attendeeNumber}</Text>
            </Stack>
            <Stack
              direction={"row"}
              justifyContent={"space-between"}
              alignItems={"center"}
              p={2}
            >
              <Stack direction={"row"}>
                <AiOutlineFileProtect />
                <Text fontWeight={600} fontSize={16}>
                  Output Standard
                </Text>
              </Stack>
              <Stack direction={"row"}>
                <Badge
                  style={{
                    padding: "5px 10px",
                    background: "#2D3748",
                    borderRadius: 10,
                    color: "#fff",
                  }}
                >
                  {syllabusGeneral?.outputStandardCode}
                </Badge>
                {/* <Badge
                  style={{
                    padding: "5px 10px",
                    background: "#2D3748",
                    borderRadius: 10,
                    color: "#fff",
                  }}
                >
                  H4SD
                </Badge>
                <Badge
                  style={{
                    padding: "5px 10px",
                    background: "#2D3748",
                    borderRadius: 10,
                    color: "#fff",
                  }}
                >
                  H4SD
                </Badge> */}
              </Stack>
            </Stack>
          </CardBody>
        </Card>
        <Card minW={600}>
          <CardBody lineHeight={2}>
            <Stack direction={"row"} alignItems={"center"}>
              <IoSettingsOutline />
              <Text fontWeight={600} fontSize={16}>
                Technical Requirement(s)
              </Text>
            </Stack>
            <Text>
              Trainees’ PCs need to have following software installed & run
              without any issues:
            </Text>
            <UnorderedList>
              <ListItem>{syllabusGeneral?.technicalRequirement}</ListItem>
              <ListItem>Microsoft Visual Studio 2017</ListItem>
              <ListItem>
                Microsoft Office 2007 (Visio, Word, PowerPoint)
              </ListItem>
            </UnorderedList>
          </CardBody>
        </Card>
      </Flex>
      <Stack direction={"row"} alignItems={"center"} mt={4}>
        <MdFilterCenterFocus />
        <Text fontWeight={500} fontSize={16}>
          Course objectives
        </Text>
      </Stack>

      <Text lineHeight={2}>
        This topic is to introduce about C# programming language knowledge;
        adapt trainees with skills, lessons and practices which is specifically
        used in the Fsoft projects. In details, after completing the topic,
        trainees will:
      </Text>

      <UnorderedList lineHeight={2}>
        <ListItem>
          <Text>
            {removeHtmlTags()}
          </Text>
        </ListItem>
        <ListItem>
          <Text>
            Understand and distinguish two concepts: class (Class) and object
            (Object)
          </Text>
        </ListItem>
        <ListItem>
          <Text>
            Understand and apply object-oriented programming knowledge to
            resolve simple problems (Inheritance, Encapsulation, Abstraction,
            Polymorphism)
          </Text>
        </ListItem>
        <ListItem>
          <Text>
            Working with some of the existing data structures in C# (List,
            ArrayList, HashTable, Dictionary)
          </Text>
        </ListItem>
        <ListItem>
          <Text>
            Know how to control program errors (use try ... catch..finally,
            throw, throws)
          </Text>
        </ListItem>
        <ListItem>
          <Text>
            Be able to working with concurrency and multi-thread in C#
          </Text>
        </ListItem>
        <ListItem>
          <Text>
            Be able to working with common classes in ADO.net: SqlConnection,
            SqlCommand, SqlParameter, SqlDataAdapter, SqlDataReader{" "}
          </Text>
        </ListItem>
        <ListItem>
          <Text>
            Be able to manipulate SQL data from Window Form Application via 4
            basic commands: Add, Update, Delete, Select
          </Text>
        </ListItem>
        <ListItem>
          <Text>Know how to design UI screen in Window Form Application</Text>
        </ListItem>
        <ListItem>
          <Text>
            Know how to use approciate controls for each field/data type:
            Textbox, Label, Combobox, Radio, DateTimePicker, NumericUpDown,
            RichTextBox
          </Text>
        </ListItem>
      </UnorderedList>
    </Box>
  );
};

export default SyllabusGeneral;
