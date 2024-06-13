import * as React from "react";
import TextField from "@mui/material/TextField";
import Stack from "@mui/material/Stack";
import Autocomplete from "@mui/material/Autocomplete";
import SearchIcon from "@mui/icons-material/Search";
import FormatDate from "../../../Utils/FormatDateInFigma";
import style from "../../../../ClassManagement/assert/css/SelectInClass.module.scss";
import { GetListSyllabus } from "../api/ListApi";
export default function MultiSyllabus({ setSyllabusPicked, programPicked, counter }) {
  const [syllabusList, setSyllabusList] = React.useState([]);
  React.useEffect(() => {
    const fetchApiData = async () => {
      const syllabusList = await GetListSyllabus(programPicked?.trainingProgramCode);
      setSyllabusList(syllabusList);
    };
    fetchApiData();
  }, [counter]);
  return (
    <div className={style.Autocomplete}>
      <Stack
        spacing={2}
        sx={{ width: 330, backgroundColor: "white", borderRadius: "8px" }}
      >
        <Autocomplete
          freeSolo
          id="free-solo-2-demo"
          onChange={(event, value) => {
            setSyllabusPicked(value);
          }}
          disableClearable
          disabled={!syllabusList || syllabusList == []}
          options={syllabusList}
          getOptionLabel={(option) => `${option.topicName}`}
          renderOption={(props, option) => (
            <li style={{ display: "block" }} {...props}>
              <span style={{ textAlign: "start", display: "block" }}>
                {option.topicName}
              </span>
              <span
                style={{
                  textAlign: "start",
                  display: "block",
                  fontSize: "12px",
                }}
              >
                {option.topicCode} - Version {option.version}
                {" - "}
                {FormatDate(option.createdDate)} by {option.createdBy}
              </span>
            </li>
          )}
          renderInput={(params) => (
            <TextField
              placeholder=" Select Syllabus Program"
              {...params}
              InputProps={{
                ...params.InputProps,
                type: "search",
                startAdornment: <SearchIcon fontSize="small" />,
              }}
            />
          )}
        />
      </Stack>
    </div>
  );
}
