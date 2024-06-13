import { formatDate } from "../../../../../../../utils/DateUtils";
import { courses } from "../../data/data";
import TrainingProgramCard from "../TrainingProgramCard";
import { useAppSelector } from "@/hooks/useRedux";

const TrainingProgram = () => {
  const data = useAppSelector((state) => state.class.data);
  if (!data) return;
  if (!data.trainingProgram) return;
  if (!data.syllabus) return;
  return (
    <div>
      <div className="bg-[#2D3748] text-white px-10 py-5 mb-3 border-t border-t-white">
        <h2 className="text-3xl mb-2">{data.trainingProgram.name}</h2>
        <div className="flex">
          <div>
            {data.trainingProgram.days} days{" "}
            <span className="italic">({data.trainingProgram.hours} hours)</span>
          </div>
          <div className="border border-white mx-5"></div>
          <div>
            Modified on {formatDate(new Date(data.trainingProgram.updatedDate))}
            &nbsp;by&nbsp;
            <span className="font-bold">{data.trainingProgram.updatedBy}</span>
          </div>
        </div>
      </div>
      <div className="flex flex-col gap-5">
        {data.syllabus.map((s: any, index: number) => (
          <TrainingProgramCard
            key={`traning-program-card-${index}`}
            syllabusId={s.syllabusId}
            syllabusName={s.topicName}
            syllabusShortName={s.topicCode}
            syllabusStatus={s.status}
            createdAt={new Date(s.modifiedDate)}
            createdBy={s.modifiedBy}
            duration={{
              days: s.days,
              hours: s.hours,
            }}
            trainers={[
              {
                id: 1,
                name: "Jason Voorhees",
                profileURL:
                  "https://i.pngimg.me/thumb/f/720/c3f2c592f9.jpg",
              },             
            ]}
          />
        ))}
      </div>
      <div className="p-3 bg-[#2D3748] mt-3 rounded-b-xl"></div>
    </div>
  );
};

export default TrainingProgram;
