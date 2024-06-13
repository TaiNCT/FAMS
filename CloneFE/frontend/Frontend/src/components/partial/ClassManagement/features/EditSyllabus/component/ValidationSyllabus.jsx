import * as yup from "yup";
export const validationSyllabus = yup.object().shape({
  topicName: yup
    .string()
    .required("Topic Name is required")
    .matches(/^[a-zA-Z\s-.']+(\d*)$/, "Topic Name is a string"),
  hours: yup
    .string()
    .required("Days is required")
    .matches(/^(0?[0-9]|1[0-9]|2[0-3])$/, "Invalid Hours"),
  days: yup
    .string()
    .required("Days is required")
    .matches(/^(0?[1-9]|[1-2][0-9]|3[0-1])$/, "Invalid Days"),
});
