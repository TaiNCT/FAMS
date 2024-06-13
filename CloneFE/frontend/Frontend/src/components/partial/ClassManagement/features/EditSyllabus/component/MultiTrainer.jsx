import * as React from "react";
import TextField from "@mui/material/TextField";
import Stack from "@mui/material/Stack";
import Autocomplete from "@mui/material/Autocomplete";
import SearchIcon from "@mui/icons-material/Search";
import { GetTrainer } from "../../EditSyllabus/api/ListApi";
import FormatDate from "../../../Utils/FormatDateInFigma";
import style from "../../../../ClassManagement/assert/css/SelectInClass.module.scss";
export default function MultiTrainer({ setTrainerPicked, trainerPicked })
{
  const [trainerList, setTrainerList] = React.useState([]);
  React.useEffect(() =>
  {
    const fetchApiData = async () =>
    {
      const trainerListData = await GetTrainer();
      setTrainerList(trainerListData);
    };
    fetchApiData();
  }, []);
  return (
    <div className={style.Autocomplete}>
      <Stack
        spacing={2}
        sx={{ width: 310, backgroundColor: "white", borderRadius: "8px" }}
      >
        <Autocomplete
          freeSolo
          id="free-solo-2-demo"
          onChange={(event, value) =>
          {
            setTrainerPicked(value);
            // Store the selected program in localStorage
            //localStorage.setItem('programPicked', JSON.stringify(value));
          }}
          value={trainerPicked ? trainerPicked : null}
          disableClearable
          disabled={!trainerList || trainerList == []}
          options={trainerList}
          getOptionLabel={(option) => `${option.fullName}`}
          renderOption={(props, option) => (
            <li style={{ display: "block" }} {...props}>
              <span style={{ textAlign: "start", display: "block" }}>
                {option.fullName}
              </span>
            </li>
          )}
          renderInput={(params) => (
            <TextField
              placeholder=" Select Trainer"
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
