import { formatDate } from "@/utils/DateUtils";
import { useAppSelector } from "@/hooks/useRedux";
import CreateIcon from "@/assets/icons/document-manage-icons/CreateIcon";
import { useEffect, useState } from "react";
import { Autocomplete, TextField } from "@mui/material";
import {
  getAllTrainingProgram,
  getSyllabusByTrainingProgram,
} from "@/lib/api/TrainingProgramApi";
import { SearchIcon } from "@chakra-ui/icons";
import SyllabusList from "./SyllabusList";
import { useDispatch } from "react-redux";
import { setTrainingProgram, setSyllabusList } from "@/lib/redux/classSlice";
import { Button, Form } from "antd";
import { Link } from "react-router-dom";

const TrainingProgram = ({ classId, form }: any) => {
  const data = useAppSelector((state) => state.class.data);
  const syllabusList = useAppSelector((state) => state.class.SyllabusList);
  const dispatch = useDispatch();
  const [programPicked, setProgramPicked] = useState(data?.trainingProgram);
  const [isEditing, setIsEditing] = useState(false);
  const programList = useAppSelector((state) => state.trainingPrograms.trainingPrograms);
  const handleEditClick = () => {
    setIsEditing(!isEditing);
  };
  const handleChangeTrainingProgram = async (event: any, value: any) => {
    const newSyllabusData = await getSyllabusByTrainingProgram(
      value.trainingProgramCode
    );
    setIsEditing(false);
    setProgramPicked(value);
    dispatch(setTrainingProgram(value));
    dispatch(setSyllabusList(newSyllabusData.data.items));
  };
  if (!data || !programList) return;
  return (
    <div>
      <div className="bg-[#2D3748] text-white px-10 py-5 mb-3 border-t border-t-white">
        {isEditing ? (
          <div>
            <h2 className="text-2xl mb-2">Select training program</h2>
            <div className="w-[50%] bg-white rounded overflow-hidden">
              <Form.Item
                name="trainingProgramId"
                noStyle
                initialValue={data.trainingProgram.trainingProgramCode}
              >
                <Autocomplete
                  freeSolo
                  id="free-solo-2-demo"
                  onChange={handleChangeTrainingProgram}
                  disableClearable
                  disabled={!programList || programList.length == 0}
                  options={programList}
                  getOptionLabel={(option: any) => `${option.name}`}
                  renderOption={(props, option) => {
                    return (
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
                          {option.days} days ({option.hours} hours)
                          {formatDate(new Date(option.createdDate))} by{" "}
                          {option.userId}
                        </span>
                      </li>
                    );
                  }}
                  renderInput={(params) => (
                    <TextField
                      placeholder="Select program"
                      {...params}
                      InputProps={{
                        ...params.InputProps,
                        type: "search",
                        startAdornment: <SearchIcon fontSize="small" />,
                      }}
                    />
                  )}
                />
              </Form.Item>
            </div>
          </div>
        ) : (
          <div>
            <div className="flex items-center">
              <h2 className="text-3xl mb-2">{programPicked.name}</h2>
              <button onClick={handleEditClick}>
                <CreateIcon />
              </button>
            </div>
            <div className="flex">
              <div>
                {programPicked.days} days{" "}
                <span className="italic">({programPicked.hours} hours)</span>
              </div>
              <div className="border border-white mx-5"></div>
              <div>
                Modified on {formatDate(new Date(programPicked.updatedDate))}
                &nbsp;by&nbsp;
                <span className="font-bold">{programPicked.updatedBy}</span>
              </div>
            </div>
          </div>
        )}
      </div>
      {isEditing ? "" : <SyllabusList syllabus={syllabusList} />}

      <div className="p-3 bg-[#2D3748] mt-3 rounded-b-xl"></div>

      <div className="flex justify-end p-3 gap-2">
        <Link to={`/class-detail/${classId}`}>
          <Button size="large" danger>
            Cancel
          </Button>
        </Link>
        {/* <Button size="large">Save as draft</Button> */}
        <Button
          size="large"
          style={{ background: "#30344c", color: "white" }}
          disabled={isEditing}
          htmlType="submit"
        >
          Save
        </Button>
      </div>
    </div>
  );
};

export default TrainingProgram;
