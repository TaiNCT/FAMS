import style from "./style.module.scss";
import React from "react";
import Loading from "./components/Loading";
import format from "date-fns/format";
import { TrainingProgram } from "./models/trainingprogram.model";
import SyllabusList from "../CreateTrainingProgram/components/SyllabusList";
import ActionMenu from "../components/ActionMenu/ActionMenu";
import { ChakraProvider } from "@chakra-ui/react";
import { useGlobalContext } from "../contexts/DataContext";
import GlobalLoading from "../../../global/GlobalLoading.jsx";

interface TrainingProgramDetailProps {
  item: TrainingProgram | undefined;
  // setItem: React.Dispatch<React.SetStateAction<TrainingProgram | undefined>>;
  setItem: (tp: TrainingProgram) => void;
  isLoading: boolean;
}

const TrainingProgramDetail: React.FC<TrainingProgramDetailProps> = ({
  item,
  setItem,
  isLoading,
}) => {
  const handleDelete = (id: number) => {
  };

  const { isSidebarOpen } = useGlobalContext();

  return (
    <div
      className={style.content}
      style={{
        marginLeft: isSidebarOpen ? "-1rem" : "-14rem",
        transition: "margin-left 0.3s ease-in-out",
      }}
    >
      {item && (
        <>
          <h1 className={style.header}>
            <span>Training Program</span>
            <div className={style.trainingProgram}>
              <p>
                {item.name}
                <small
                  style={{
                    backgroundColor: `${
                      item.status === "Active"
                        ? "#2f913f"
                        : item.status === "InActive"
                        ? "#b9b9b9"
                        : "#285d9a"
                    }`,
                  }}
                >
                  {item.status}
                </small>
              </p>
              {/* <DetailMenu /> */}
              <div className={style.menuAction}>
                <ChakraProvider>
                  <ActionMenu
                    isDetail={true}
                    setItem={setItem}
                    trainingProgramCode={item.trainingProgramCode}
                    trainingProgramName={item.name}
                    id={item.id}
                    status={item.status}
                  />
                </ChakraProvider>
              </div>
            </div>
          </h1>
          {isLoading && <GlobalLoading isLoading={isLoading} />}
          <article className={style.detail}>
            <div>
              <b>{item.days}</b> days
              <i style={{ marginLeft: "0.4rem" }}>({item.hours} hours)</i>
            </div>
            <div>
              <span>
                Modified on <b>{format(item.createdDate, "dd/MM/yyyy")}</b> by{" "}
                {item.updateBy ? item.updateBy : item.createdBy}
              </span>
            </div>
          </article>
          <div className={style.generalInformation}>
            <h2>General Information</h2>
            <div>
              <ul>
                <li>
                  Leverage DevOps practices to transform processes with Lean,
                  Agile, ITSM
                </li>
                <li>
                  Learn how to break the silos between Development and
                  Operations
                </li>
                <li>
                  Experimental learning with case studies, real-world success
                  stories, engaging activities, more
                </li>
              </ul>
            </div>
          </div>
          <div className={style.syllabiContent}>
            <h2>Content</h2>

            {item && (
              <SyllabusList
                syllabi={item.syllabi}
                handleDelete={handleDelete}
                isDetailPage={true}
              />
            )}
          </div>
        </>
      )}
    </div>
  );
};

export default TrainingProgramDetail;
