import TrainingProgramCard from "./TrainingProgramCard";
type SyllabusListProps = {
  syllabus: any;
};
const SyllabusList = ({ syllabus }: SyllabusListProps) => {
  if (!syllabus) return <div>No syllabus available</div>;
  return (
    <div className="flex flex-col gap-5">
      {syllabus && syllabus.map((s: any, index: number) => (
        <TrainingProgramCard
          key={`traning-program-card-${index}`}
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
              profileURL: "https://i.pngimg.me/thumb/f/720/c3f2c592f9.jpg",
            },
          ]}
        />
      ))}
    </div>
  );
};

export default SyllabusList;
