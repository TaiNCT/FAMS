import { Avatar, Popover } from "antd";
import { formatDate } from "@/utils/DateUtils";
import CallIcon from "@/assets/icons/other-icons/CallIcon";
import MailIcon from "@/assets/icons/other-icons/MailIcon";
import HighlightOffIcon from "@mui/icons-material/HighlightOff";

type TrainerType = {
  id: number;
  name: string;
  profileURL: string;
};

type EditSyllabusCardPropsType = {
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
  handleDeleteSyllabus: (syllabusId: string) => void;
};

const EditSyllabusCard = ({
  syllabusId,
  syllabusName,
  syllabusStatus,
  syllabusShortName,
  duration,
  createdAt,
  createdBy,
  trainers,
  handleDeleteSyllabus,
}: EditSyllabusCardPropsType) => {
  return (
    <div className="flex shadow-[0px_0px_15px_rgba(0,0,0,0.3)] rounded-xl overflow-hidden">
      <div className="bg-[#2D3748] grid grid-cols-3 px-8 py-4 gap-2 items-center">
        {trainers.map((trainer, index) => {
          const popupContent = (
            <div>
              <div style={{ display: "flex" }}>
                <CallIcon style={{ color: "blue", paddingRight: "0.25rem" }} />
                097899084
              </div>
              <div style={{ display: "flex" }}>
                <MailIcon style={{ color: "blue", paddingRight: "0.25rem" }} />
                TrungDVQ@fsoft.com.vn
              </div>
            </div>
          );
          return (
            <Popover
              content={popupContent}
              trigger="hover"
              key={`avatar-${index}`}
            >
              <Avatar size={64} src={trainer.profileURL} />
            </Popover>
          );
        })}
      </div>
      <div
        className={`p-5 flex-1 ${
          syllabusStatus === "Inactive" ? "opacity-50" : ""
        }`}
      >
        <div className="flex items-center mb-3 justify-between">
          <div className="flex gap-5">
            <div className="text-3xl font-semibold text-primary tracking-widest">
              {syllabusName}
            </div>
            <div className="bg-[#2D3748] text-white px-3 py-1 rounded-3xl">
              {syllabusStatus}
            </div>
          </div>
          <button onClick={() => handleDeleteSyllabus(syllabusId)}>
            <HighlightOffIcon />
          </button>
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
            Created at <span className="italic">{formatDate(createdAt)}</span>{" "}
            by {createdBy}
          </div>
        </div>
      </div>
    </div>
  );
};

export default EditSyllabusCard;
