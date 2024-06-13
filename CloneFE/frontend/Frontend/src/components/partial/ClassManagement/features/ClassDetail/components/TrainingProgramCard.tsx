import { Avatar, Popover } from "antd";
import { formatDate } from "../../../../../../utils/DateUtils";
import CallIcon from "@/assets/icons/other-icons/CallIcon";
import MailIcon from "@/assets/icons/other-icons/MailIcon";
import { Link } from "react-router-dom";

type TrainerType = {
  id: number;
  name: string;
  profileURL: string;
};
type TrainingProgramCardPropsType = {
  syllabusId: string;
  syllabusName: string;
  syllabusStatus: string;
  syllabusShortName: string;
  duration: {
    days: number;
    hours: number;
  };
  createdAt: Date;
  createdBy: string;
  trainers: TrainerType[];
};
const TrainingProgramCard = ({
  syllabusId,
  syllabusName,
  syllabusStatus,
  syllabusShortName,
  duration,
  createdAt,
  createdBy,
  trainers,
}: TrainingProgramCardPropsType) => {
  return (
    <div className="flex shadow-[0px_0px_15px_rgba(0,0,0,0.3)] rounded-xl overflow-hidden">
      <div className="bg-[#2D3748] grid grid-cols-3 px-8 py-4 gap-2 items-center">
        {trainers.map((trainer: any, index: number) => {
          const popupContent = (
            <div>
              <div style={{ display: "flex" }}>
                <CallIcon style={{color: "blue", paddingRight: "0.25rem"}}/>
                097899084
              </div>
              <div style={{ display: "flex" }}>
                <MailIcon style={{color: "blue", paddingRight: "0.25rem"}}/>
                TrungDVQ@fsoft.com.vn
              </div>
            </div>
          );
          return (
            <Popover content={popupContent} trigger="hover" key={`trainer-${index}`}>
              <Avatar size={64} src={trainer.profileURL} />
            </Popover>
          );
        })}
      </div>
      <div
        className={`p-5 ${syllabusStatus === "Inactive" ? "opacity-50" : ""}`}
      >
        <div className="flex gap-5 items-center mb-3">
          <div
            className="text-3xl font-semibold text-primary tracking-widest"
          >
            <Link to={`/syllabus/detail/${syllabusId}`}>
            {syllabusName}
            </Link>
          </div>
          <div className="bg-green-500 text-white px-3 py-1 rounded-3xl">
            {syllabusStatus}
          </div>
        </div>
        <div className="flex items-center">
          <div>{syllabusShortName}</div>
          <div className="border border-[#2D3748] mx-4 h-4" />
          <div>
            {duration.days} days{" "}
            <span className="italic">({duration.hours} hours)</span>
          </div>
          <div className="border border-[#2D3748] mx-4 h-4" />
          <div>
            Modified on <span className="italic">{formatDate(createdAt)}</span> by{" "}
            {createdBy}
          </div>
        </div>
      </div>
    </div>
  );
};

export default TrainingProgramCard;
