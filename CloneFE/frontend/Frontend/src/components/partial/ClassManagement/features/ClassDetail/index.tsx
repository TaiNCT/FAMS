import General from "./components/General";
import Attendee from "./components/Attendee";
import { SyllabusTabs } from "./components/Tabs";
import StudentList from "@/components/partial/StudentManagement/StudentInClass";
import Budget from "./components/Tabs/Budget";
import Others from "./components/Tabs/Others";
import TrainingProgram from "./components/Tabs/TrainingProgram";
import TimeFrame from "./components/TimeFrame";
import Header from "./components/Header";
import { ViewTraineeScore } from "@/components/partial/ScoreManagement/ViewTraineeScore";
import { Link, useNavigate, useParams } from "react-router-dom";
import SendEmailPopupForm from "@/components/partial/EmailInformRemind/SendEmailPopupForm";
import { createContext } from "react";

export const classContext = createContext(null);

import ActivityLogTable from "../../../EmailInformRemind/ActivityLogTable/components/Table";
import { useState } from "react";
const ClassDetail = () => {
  const [open, setOpen] = useState(null);
  const [email, setEmail] = useState("");
  const navigate = useNavigate();
  const { classId } = useParams();

  // When the user clicks on an admin, the following function will be executed
  const popupClickSendEmail = (user) => {
    setEmail(user.email);
    setOpen(0);
  };

  return (
    <classContext.Provider
      value={{
        popup: popupClickSendEmail,
      }}
    >
      <div className="flex-1">
        <Header />
        <div className="p-3">
          <div className="grid-cols-3 grid gap-3 mb-5 ">
            <div className="col-span-1 flex flex-col gap-2">
              <General />
              <Attendee />
            </div>
            <div className="col-span-2">
              <TimeFrame />
            </div>
          </div>
          <SyllabusTabs
            tabs={[
              {
                label: "Training Program",
                content: <TrainingProgram />,
              },
              {
                label: "Attendee List",
                content: <StudentList />,
              },
              {
                label: "Scores",
                content: <ViewTraineeScore />,
                // onClick: () => navigate(`/score/${classId}`),
              },
              {
                label: "Activities logs",
                content: <ActivityLogTable />,
              },
            ]}
          />
        </div>
      </div>
      <SendEmailPopupForm
        setOpen={setOpen}
        open={open}
        title={"FAMS - score report"} // Grab from API
        sendto={email} // Dynamic
        studentid={null}
      />
    </classContext.Provider>
  );
};

export default ClassDetail;
