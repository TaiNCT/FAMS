import { Syllabus } from "../models/syllabus.model";
import style from "../style.module.scss";
import SyllabusDetail from "./SyllabusDetail";

interface SyllabusListProps {
  syllabi: Syllabus[];
  handleDelete: (id: number) => void;
  isDetailPage?: boolean | null;
}

const SyllabusList: React.FC<SyllabusListProps> = (props) => {
  return (
    <>
      {props.syllabi && (
        <div
          className={style.syllabi}
          style={props.isDetailPage ? { width: "100%" } : {}}
        >
          {props.syllabi.map((syllabus) => (
            <SyllabusDetail
              syllabus={syllabus}
              handleDelete={props.handleDelete}
              key={syllabus.id}
              isDetailPage={props.isDetailPage}
            />
          ))}
        </div>
      )}
    </>
  );
};

export default SyllabusList;
