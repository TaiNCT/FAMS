import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
import Footer from "../../components/layouts/Footer";
import Header from "@/components/partial/ClassManagement/features/EditClass/Header";
import { useAppSelector } from "@/hooks/useRedux";
import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useRef, useState } from "react";
import { Button } from "antd";
import EditSyllabusCard from "@/components/partial/ClassManagement/features/EditClass/EditSyllabusCard";
import { getSyllabusByTrainingProgram } from "@/lib/api/TrainingProgramApi";
import { setSyllabusList } from "@/lib/redux/classSlice";
import { useDispatch } from "react-redux";
import { Autocomplete, TextField } from "@mui/material";
import { formatDate } from "@/utils/DateUtils";
import { SearchIcon } from "@chakra-ui/icons";
const EditClassSyllabusPage = () => {
  let { id } = useParams();
  const dispatch = useDispatch();
  const data = useAppSelector((state) => state.class.data);
  const syllabusList = useAppSelector((state) => state.class.SyllabusList);
  const selectedSyllabus = useRef(null);
  const trainingProgram = useAppSelector(
    (state) => state.class.trainingProgram
  );

  const fullSyllabusList = useRef([]);
  const remainingSyllabus = fullSyllabusList.current.filter((full: any) => {
    return !syllabusList.some(
      (item: any) => item.syllabusId == full.syllabusId
    );
  });

  useEffect(() => {
    const fetchData = async () => {
      if (trainingProgram) {
        const result = await getSyllabusByTrainingProgram(
          trainingProgram.trainingProgramCode
        );
        fullSyllabusList.current = result.data.items;
      }
    };
    fetchData();
  }, []);

  const navigate = useNavigate();
  useEffect(() => {
    if (!data) {
      navigate(`/class/${id}/edit`);
    }
  }, [data]);
  const handleBack = () => {
    navigate(`/class/${id}/edit`);
  };
  const handleCancel = () => {
    navigate(`/class-detail/${id}`);
  };

  const handleDeleteSyllabus = (syllabusId: string) => {
    const newSyllabusList = syllabusList.filter(
      (syllabus: any) => syllabus.syllabusId != syllabusId
    );
    dispatch(setSyllabusList(newSyllabusList));
  };

  const handleChangeSelectedSyllabus = (event: any, value: any) => {
    selectedSyllabus.current = value;
  };

  const handleAddSyllabus = () => {
    if (selectedSyllabus.current) {
      dispatch(setSyllabusList([...syllabusList, selectedSyllabus.current]));
      selectedSyllabus.current = null;
    }
  };

  return (
    <div className="min-h-[100vh] flex flex-col">
      <Navbar />
      <div className="flex-1 flex">
        <Sidebar />
        <div className="flex-1">
          <Header />
          <div className="p-5 ">
            <div className="flex flex-col gap-3">
              {syllabusList &&
                syllabusList.map((syllabus: any, index: number) => {
                  return (
                    <EditSyllabusCard
                      key={`syllabus-card-${index}`}
                      syllabusId={syllabus.syllabusId}
                      syllabusName={syllabus.topicName}
                      syllabusShortName={syllabus.topicCode}
                      syllabusStatus={syllabus.status}
                      createdAt={new Date(syllabus.modifiedDate)}
                      createdBy={syllabus.modifiedBy}
                      duration={{
                        days: syllabus.days,
                        hours: syllabus.hours,
                      }}
                      trainers={[
                        {
                          id: 1,
                          name: "Jason Voorhees",
                          profileURL:
                            "https://i.pngimg.me/thumb/f/720/c3f2c592f9.jpg",
                        },
                      ]}
                      handleDeleteSyllabus={handleDeleteSyllabus}
                    />
                  );
                })}
            </div>
            <div className="mt-5 flex gap-5 items-center">
              <h3 className="font-bold">Select syllabus: </h3>
              <Autocomplete
                freeSolo
                className="w-[400px]"
                id="free-solo-2-demo"
                onChange={handleChangeSelectedSyllabus}
                disableClearable
                disabled={!remainingSyllabus || remainingSyllabus.length == 0}
                options={remainingSyllabus}
                getOptionLabel={(option: any) => `${option.topicName}`}
                renderOption={(props, option) => {
                  return (
                    <li style={{ display: "block" }} {...props}>
                      <span style={{ textAlign: "start", display: "block" }}>
                        {option.topicName}
                      </span>
                      <div style={{ fontSize: "12px" }}>
                        {option.topicCode} - {option.days} days ({option.hours}{" "}
                        hours) - {formatDate(new Date(option.createdDate))} by{" "}
                        {option.createdBy}
                      </div>
                    </li>
                  );
                }}
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
              <Button onClick={handleAddSyllabus}>Add</Button>
            </div>
          </div>

          <div className="flex justify-between px-5 py-5">
            <Button
              size="large"
              style={{ background: "#30344c", color: "white" }}
              onClick={handleBack}
            >
              Back
            </Button>
            <div>
              <Button size="large" danger onClick={handleCancel}>
                Cancel
              </Button>
              <Button
                size="large"
                style={{ background: "#30344c", color: "white" }}
                htmlType="submit"
              >
                Save
              </Button>
            </div>
          </div>
        </div>
      </div>
      <Footer />
    </div>
  );
};

export default EditClassSyllabusPage;
