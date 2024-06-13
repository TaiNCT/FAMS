import * as React from "react";
import TextField from "@mui/material/TextField";
import Stack from "@mui/material/Stack";
import Autocomplete from "@mui/material/Autocomplete";
import SearchIcon from "@mui/icons-material/Search";
import { TrainingProgramListAPI1 } from "../../ClassList/api/ListApi";
import FormatDate from "../../../Utils/FormatDateInFigma";
import style from "../../../../ClassManagement/assert/css/SelectInClass.module.scss";
export default function MultiSelectProgram({ setProgramPicked })
{
  const [programList, setProgramList] = React.useState([]);
  React.useEffect(() =>
  {
    const fetchApiData = async () =>
    {
      const trainingProgrsmListData = await TrainingProgramListAPI1();
      setProgramList(trainingProgrsmListData);
    };
    fetchApiData();
  }, []);
  return (
    <div className={style.Autocomplete}>
      <Stack
        spacing={2}
        sx={{ width: 330, backgroundColor: "white", borderRadius: "8px" }}
      >
        <Autocomplete
          freeSolo
          id="free-solo-2-demo"
          onChange={(event, value) =>
          {
            setProgramPicked(value);
            // Store the selected program in localStorage
            localStorage.setItem('programPicked', JSON.stringify(value));
          }}
          disableClearable
          disabled={!programList || programList == []}
          options={programList}
          getOptionLabel={(option) => `${option.name}`}
          renderOption={(props, option) => (
            <li style={{ display: "block" }} {...props}>
              <span style={{ textAlign: "start", display: "block" }}>
                {option.name}
              </span>
              <span
                style={{
                  textAlign: "start",
                  display: "block",
                  fontSize: "12px",
                }}
              >
                {option.days} days ({option.hours} hours){" "}
                {FormatDate(option.createdDate)} by {option.userId}
              </span>
            </li>
          )}
          renderInput={(params) => (
            <TextField
              placeholder=" Select program"
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
