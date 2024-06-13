import React from "react";
import { Text, Button } from "@chakra-ui/react";
import { IoIosAddCircleOutline } from "react-icons/io";
import { IoMdRemoveCircleOutline } from "react-icons/io";
import {
  Form,
  Input,
  Space,
  Select,
  Switch,
  SelectProps,
  InputNumber,
  Typography,
} from "antd";
import {
  Box,
  AccordionItem,
  AccordionButton,
  AccordionIcon,
  AccordionPanel,
  Accordion,
  Flex,
} from "@chakra-ui/react";
import { SyllabusDay, SyllabusDetailOutline } from "../../types";

const handleChange = (value: string[]) => {
};
const options: SelectProps["options"] = [
  {
    label: "Clear Learning Objectives",
    value: "outputStandardId1",
    desc: "Clear Learning Objectives",
  },
  {
    label: "Comprehensive Course Outline",
    value: "outputStandardId2",
    desc: "Comprehensive Course Outline",
  },
  {
    label: "Assessment Criteria and Grading Policies",
    value: "outputStandardId3",
    desc: "Assessment Criteria and Grading Policies",
  },
  {
    label: "Technical Requirements and Resources",
    value: "outputStandardId4",
    desc: "Technical Requirements and Resources",
  },
  {
    label: "Policies and Expectations",
    value: "outputStandardId5",
    desc: "Policies and Expectations",
  },
];

const Unit: React.FC = ({
  syllabusDays,
  setSyllabusDays,
  syllabusDetailOutline,
  setsyllabusDetailOutline,
}: {
  syllabusDays: SyllabusDay[];
  setSyllabusDays: (syllabusDays: SyllabusDay[]) => void;
  syllabusDetailOutline: SyllabusDetailOutline[];
  setsyllabusDetailOutline: (
    syllabusDetailOutline: Array<SyllabusDetailOutline>
  ) => void;
}) => {
  const [form] = Form.useForm();

  if (syllabusDetailOutline) {
    form.setFieldsValue({
      syllabusDays: syllabusDetailOutline.map((syllabusDay, index) => {
        return {
          dayNo: syllabusDay.dayNo,
          syllabusUnits: syllabusDay.syllabusUnits.map(
            (syllabusUnit, index) => {
              return {
                name: syllabusUnit.name,
                unitChapters: syllabusUnit.unitChapters.map(
                  (unitChapter, index) => {
                    return {
                      name: unitChapter.name,
                      outputStandardId: unitChapter.outputStandardId,
                      duration: unitChapter.duration,
                      deliveryTypeId: unitChapter.deliveryTypeId,
                      isOnline: unitChapter.isOnline,
                    };
                  }
                ),
              };
            }
          ),
        };
      }),
    });
  }

  return (
    <Form
      form={form}
      name="dynamic_form_complex"
      autoComplete="off"
      style={{ width: "100%" }}
      initialValues={{}}
      onValuesChange={(changedValues, allValues) => {
        setSyllabusDays(allValues.syllabusDays);
        setsyllabusDetailOutline(allValues.syllabusDays);
      }}
    >
      <Form.List name="syllabusDays">
        {(fields, { add, remove }) => (
          <div style={{ display: "flex", rowGap: 16, flexDirection: "column" }}>
            {fields.map((field) => (
              <AccordionItem key={field.key}>
                <h2
                  style={{
                    background: "#2d3748",
                    color: "#fff",
                    fontWeight: "bold",
                  }}
                >
                  <AccordionButton>
                    <Flex
                      fontWeight={600}
                      as="span"
                      flex="1"
                      textAlign="left"
                      alignItems="center"
                    >
                      <Text>Day {field.name + 1}</Text>
                      <Button
                        onClick={() => {
                          remove(field.name);
                        }}
                        style={{ background: "transparent" }}
                      >
                        <IoMdRemoveCircleOutline color="red" height="22px" />
                      </Button>
                    </Flex>
                    <AccordionIcon />
                  </AccordionButton>
                </h2>
                <AccordionPanel pb={4}>
                  <Accordion defaultIndex={[0]} allowMultiple>
                    <Flex gap={3} alignItems="center" margin="5px 0px">
                      <Form.Item
                        name={[field.name, "dayNo"]}
                        initialValue={field.name + 1}
                      ></Form.Item>
                      <Form.Item
                        name={[field.name, "duration"]}
                        initialValue={field.name + 1}
                      ></Form.Item>
                      <Form.List name={[field.name, "syllabusUnits"]}>
                        {(subfields, { add, remove }) => (
                          <div
                            style={{
                              display: "flex",
                              rowGap: 16,
                              flexDirection: "column",
                            }}
                          >
                            {subfields.map((subfield) => (
                              <AccordionItem
                                key={subfield.key}
                                style={{ width: "1000px" }}
                              >
                                <h2
                                  style={{
                                    background: "#285D9A",
                                    color: "#fff",
                                    fontWeight: "bold",
                                  }}
                                >
                                  <AccordionButton>
                                    <Flex
                                      fontWeight={600}
                                      as="span"
                                      flex="1"
                                      textAlign="left"
                                      alignItems="center"
                                    >
                                      <Text>Unit {subfield.name + 1}</Text>
                                      <Button
                                        onClick={() => {
                                          remove(subfield.name);
                                        }}
                                        style={{ background: "transparent" }}
                                      >
                                        <IoMdRemoveCircleOutline
                                          color="red"
                                          height="22px"
                                        />
                                      </Button>
                                    </Flex>
                                    <AccordionIcon />
                                  </AccordionButton>
                                </h2>
                                <AccordionPanel pb={4}>
                                  <Accordion defaultIndex={[0]} allowMultiple>
                                    <AccordionItem>
                                      <Box
                                        gap={3}
                                        alignItems="center"
                                        margin="5px 0px"
                                      >
                                        <Form.Item
                                          label="Name"
                                          name={[subfield.name, "name"]}
                                          rules={[
                                            {
                                              required: true,
                                              message: "Unit name is required",
                                            },
                                          ]}
                                          validateTrigger={[
                                            "onChange",
                                            "onBlur",
                                          ]}
                                        >
                                          <Input
                                            style={{
                                              width: "300px",
                                              height: "40px",
                                              marginLeft: "17px",
                                            }}
                                          />
                                        </Form.Item>
                                        <Form.Item label="Content">
                                          <Form.List
                                            name={[
                                              subfield.name,
                                              "unitChapters",
                                            ]}
                                          >
                                            {(subsubFields, subsubOpt) => (
                                              <div style={{}}>
                                                {subsubFields.map(
                                                  (subsubField) => (
                                                    <Space
                                                      key={subsubField.key}
                                                      style={{
                                                        display: "flex",
                                                        alignItems: "center",
                                                        justifyContent:
                                                          "space-around",
                                                      }}
                                                    >
                                                      <Form.Item
                                                        name={[
                                                          subsubField.name,
                                                          "name",
                                                        ]}
                                                        rules={[
                                                          {
                                                            required: true,
                                                            message:
                                                              "Unit Chapter is required",
                                                          },
                                                        ]}
                                                        validateTrigger={[
                                                          "onChange",
                                                          "onBlur",
                                                        ]}
                                                      >
                                                        <Input placeholder="Name" />
                                                      </Form.Item>
                                                      <Form.Item
                                                        name={[
                                                          subsubField.name,
                                                          "outputStandardId",
                                                        ]}
                                                      >
                                                        <Select
                                                          // mode="multiple"
                                                          style={{
                                                            minWidth: "200px",
                                                            borderRadius:
                                                              "10px",
                                                            boxShadow:
                                                              "2px 2px 5px rgba(0, 0, 0, 0.4)",
                                                          }}
                                                          placeholder="Select Output Standard"
                                                          onChange={
                                                            handleChange
                                                          }
                                                          optionLabelProp="label"
                                                          options={options}
                                                          optionRender={(
                                                            option
                                                          ) => (
                                                            <Space>
                                                              <span
                                                                role="img"
                                                                aria-label={
                                                                  option.data
                                                                    .label
                                                                }
                                                              >
                                                                {
                                                                  option.data
                                                                    .emoji
                                                                }
                                                              </span>
                                                              {option.data.desc}
                                                            </Space>
                                                          )}
                                                        />
                                                      </Form.Item>
                                                      <Form.Item
                                                        name={[
                                                          subsubField.name,
                                                          "duration",
                                                        ]}
                                                      >
                                                        <InputNumber
                                                          min={1}
                                                          max={60}
                                                          placeholder="Time(mins)"
                                                        />
                                                      </Form.Item>
                                                      <Form.Item
                                                        name={[
                                                          subsubField.name,
                                                          "deliveryTypeId",
                                                        ]}
                                                      >
                                                        <Select
                                                          style={{
                                                            minWidth: "160px",
                                                            borderRadius:
                                                              "10px",
                                                            boxShadow:
                                                              "2px 2px 5px rgba(0, 0, 0, 0.4)",
                                                          }}
                                                          placeholder="Delivery type"
                                                        >
                                                          <option value="deliveryTypeId1">
                                                            Assignment/Lab
                                                          </option>
                                                          <option value="deliveryTypeId2">
                                                            Concept/Lecture
                                                          </option>
                                                          <option value="deliveryTypeId3">
                                                            Guide/Review
                                                          </option>
                                                          <option value="deliveryTypeId4">
                                                            Test/Quiz
                                                          </option>
                                                          <option value="deliveryTypeId5">
                                                            Exam
                                                          </option>
                                                          <option value="deliveryTypeId6">
                                                            Seminar/Workshop
                                                          </option>
                                                        </Select>
                                                      </Form.Item>
                                                      <Form.Item
                                                        name={[
                                                          subsubField.name,
                                                          "isOnline",
                                                        ]}
                                                      >
                                                        <Switch
                                                          checkedChildren={
                                                            <span
                                                              style={{
                                                                color: "orange",
                                                              }}
                                                            >
                                                              Online
                                                            </span>
                                                          }
                                                          unCheckedChildren="Offline"
                                                          defaultChecked
                                                          className="custom-switch"
                                                        />
                                                      </Form.Item>
                                                      <Button
                                                        marginBottom="30px"
                                                        onClick={() => {
                                                          subsubOpt.remove(
                                                            subsubField.name
                                                          );
                                                        }}
                                                      >
                                                        <IoMdRemoveCircleOutline color="red" />
                                                      </Button>
                                                    </Space>
                                                  )
                                                )}
                                                <Button
                                                  onClick={() =>
                                                    subsubOpt.add()
                                                  }
                                                  background="#ccc"
                                                >
                                                  <IoIosAddCircleOutline
                                                    height="22px"
                                                    width="100%"
                                                  />
                                                </Button>
                                              </div>
                                            )}
                                          </Form.List>
                                        </Form.Item>
                                      </Box>
                                    </AccordionItem>
                                  </Accordion>
                                </AccordionPanel>

                                {/* Nest Form.List */}
                              </AccordionItem>
                            ))}

                            <Button
                              style={{
                                background: "#474747",
                                color: "#fff",
                                fontSize: "14px",
                                width: "150px",
                              }}
                              onClick={() => add()}
                            >
                              <Text as="span" fontSize="25px" me={3}>
                                <IoIosAddCircleOutline />{" "}
                              </Text>
                              Add Unit
                            </Button>
                          </div>
                        )}
                      </Form.List>
                    </Flex>
                  </Accordion>
                </AccordionPanel>
              </AccordionItem>
            ))}

            <Button
              style={{
                background: "#2D3748",
                color: "#fff",
                fontSize: "14px",
                width: "120px",
                margin: "4px",
              }}
              onClick={() => add()}
            >
              <Text as="span" fontSize="25px" me={3}>
                <IoIosAddCircleOutline />{" "}
              </Text>
              Add Day
            </Button>
          </div>
        )}
      </Form.List>
    </Form>
  );
};

export default Unit;
