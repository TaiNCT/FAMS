import { useEffect, useState } from "react";
import styles from "./style.module.scss";
import { studentApi } from "../../../../config/axios";
import { StudentClasses } from "../../../../model/StudentLamNS";
import AddClassInforPopup from "../AddClassInforPopup";
import AddIcon from "../../../../assets/LogoStManagement/add-PcH.png";
import whitePencilIcon from "../../../../assets/LogoStManagement/pencil.svg";
import Loading from "../Loading";
import { toast } from "react-toastify";
import { Empty } from "antd";

export function ListClassInfor({
  studentId,
}: // editMode,
{
  studentId: string;
  // editMode: boolean;
}) {
  const [classList, setClassList] = useState<StudentClasses[]>([]);
  const [loading, setLoading] = useState(false);
 

  useEffect(() => {
    fetchData();
  }, [studentId]);

  const fetchData = async () => {
    try {
      setLoading(true);
      const response = await studentApi.get(
        `/GetListClassInfor?studentId=${studentId}`
      );
      setClassList(response.data.result);
    } catch (error) {
      toast.error(error.message);
    } finally {
      setLoading(false);
    }
  };

  const classNameStatus: { [key: string]: string } = {
    InClass: styles.Inclass,
    Finish: styles.Finish,
    Dropout: styles.Finish,
    Reverse: styles.Finish,
  };

  function dateFormat(date: string | undefined) {
    if (date) {
      const dateObject = new Date(date);
      const day = String(dateObject.getDate()).padStart(2, "0"); // Ensure two digits with leading zero if necessary
      const month = String(dateObject.getMonth() + 1).padStart(2, "0"); // Month is zero-based, so add 1
      const year = dateObject.getFullYear();
      return `${day}/${month}/${year}`;
    }
    return "";
  }

  return (
    <section>
      <div className={styles.main}>
        <div className={styles.heading}>
          <h2>Class Information</h2>
        </div>
        {loading ? (
          <div className={styles.ListInforContainer}>
            <Loading />
          </div>
        ) : classList.length == 0 ? (
          <Empty
            style={{
              position: "absolute",
              top: "50%",
              left: "50%",
              transform: "translateY(-50%) translateX(-50%)",
            }}
            image={Empty.PRESENTED_IMAGE_SIMPLE}
          />
        ) : (
          <div className={styles.ListInforContainer}>
            <div className={styles.addNewClassInfor}>
              <AddClassInforPopup
                fetchdata={fetchData}
                studentId={studentId}
                button={
                  <button style={{ width: 24, height: 24 }}>
                    <img
                      style={{ width: 24, height: 24 }}
                      src={AddIcon}
                      alt=""
                    />
                  </button>
                }
              />
            </div>
            {classList?.map((studentClass) => {
              return (
                <div className={styles.ClassContainer}>
                  <div className={styles.ClassHeader}>
                    <h4 className={styles.ClassHeader_Name}>
                      {studentClass.class?.className}
                    </h4>
                    <div
                      className={`${styles.ClassHeader_Status} ${
                        styles[studentClass.attendingStatus]
                      }`}
                    >
                      {studentClass.attendingStatus == "InClass"
                        ? "In Class"
                        : studentClass.attendingStatus == "DropOut"
                        ? "Drop Out"
                        : studentClass.attendingStatus}
                    </div>
                  </div>
                  <div className={styles.ClassBody}>
                    <p className={styles.classCode}>
                      {studentClass.class?.classCode}
                    </p>
                    |
                    <div className={styles.classTime}>
                      <span>{dateFormat(studentClass.class?.startDate)}</span>-
                      <span>{dateFormat(studentClass.class?.endDate)}</span>
                    </div>
                  </div>
                  <div className={styles.ClassFooter}>
                    <p>Note about this</p>
                  </div>
                </div>
              );
            })}
          </div>
        )}
      </div>
    </section>
  );
}
