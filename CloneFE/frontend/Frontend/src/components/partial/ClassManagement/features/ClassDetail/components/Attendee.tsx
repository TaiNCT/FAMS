import GradeIcon from "@/assets/icons/indicator-icons/GradeIcon";
import Collapse from "../../../../../global/Collapse";
import { useAppSelector } from "@/hooks/useRedux";

const Attendee = ({ ...props }: React.HTMLAttributes<HTMLDivElement>) => {
  const data = useAppSelector((state) => state.class.data);
  if (!data) return;
  return (
    <Collapse
      icon={<GradeIcon />}
      title="Attendee"
      description={data.attendeeTypeName}
      {...props}
    >
      <div className="grid grid-cols-3 rounded-xl overflow-hidden">
        <div className="bg-[#2D3748] p-5 border border-white flex flex-col items-center justify-between text-white gap-3">
          <div className="font-bold">Planned</div>
          <div className="text-4xl">{data.plannedAttendee}</div>
        </div>
        <div className="bg-blue-800 p-5 border border-white flex flex-col items-center justify-between text-white gap-3" >
          <div className="font-bold">Accepted</div>
          <div className="text-4xl">{data.acceptedAttendee}</div>
        </div>
        <div className="bg-secondary p-5 border-white flex flex-col items-center justify-between gap-3">
          <div className="font-bold">Actual</div>
          <div className="text-4xl">{data.actualAttendee}</div>
        </div>
      </div>
    </Collapse>
  );
};

export default Attendee;
