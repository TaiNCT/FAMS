import ClassHeader from "../ClassHeader";
import SyllabusTab from "../Syllabus_tab";
import ActionBar from "../ActionBar";
import CustomizedTables from "../Table";
import React, { useState } from "react";
import styles from "./style.module.scss";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useParams, useSearchParams } from "react-router-dom";
import moreIcon from "../../../../assets/LogoStManagement/morehorizontal.png";
import { useItems } from "@/config/StudentInformanagement/hooks";
import { studentApi } from "@/config/axios";

export default function StudentInClass() {
  const { classId } = useParams();
  const [openListStudent, setOpenListStudent] = React.useState(true);
  const [search] = useSearchParams();
  //Get List Student by custom UseItems Hook
  //Get Paginations from url
  const pageSizeUrl = search.get("pageSize")
    ? Number(search.get("pageSize"))
    : 5;
  const pageNumberUrl = search.get("pageNumber")
    ? Number(search.get("pageNumber"))
    : 1;
  const [pagination, setPagination] = React.useState({
    pageNumber: pageNumberUrl,
    pageSize: pageSizeUrl,
  });

  function handleChagePageNumber(value: number) {
    setPagination({
      ...pagination,
      pageNumber: value,
    });
  }

  const [selectedStudentIds, setSelectedStudentIds] = useState<string[]>([]);
  const [isCheckAllInPage, setIsCheckAllInPage] = useState(false);
  const [isCheckAll, setIsCheckAll] = useState(false);

  const studentsResponse = useItems(classId, pagination);
  const data = studentsResponse.data ?? {
    result: { students: [], totalCount: 0 },
    isSuccess: false,
    message: "",
  };

  const student = data.result?.students ?? [];
  const totalCount = data.result?.totalCount ?? 0;

  React.useEffect(() => {
    // Check if all checkboxes are selected after state update
    setIsCheckAllInPage(checkIsCheckedAllInPage());
    setIsCheckAll(checkIsCheckedAll());
  }, [selectedStudentIds]);

  const handleCheckboxChange = (studentId: string, checked: boolean) => {
    setSelectedStudentIds((prevSelectedStudentIds) =>
      checked
        ? [...prevSelectedStudentIds, studentId]
        : prevSelectedStudentIds.filter((id) => id !== studentId)
    );
  };

  const handleSelectAllInPage = (listId: string[]) => {
    const newState = !(isCheckAllInPage || isCheckAll);
    setIsCheckAllInPage(newState);
    setIsCheckAll(newState);

    setSelectedStudentIds(listId);
    if (isCheckAllInPage || isCheckAll) {
      setSelectedStudentIds([]);
    }
  };

  const handleDisableSelectAllInPage = () => {
    setIsCheckAllInPage(false);
    setIsCheckAll(false);
    setSelectedStudentIds([]);
  };

  const checkIsCheckedAllInPage = () => {
    return student.length > 0 && student.length == selectedStudentIds.length;
  };

  const checkIsCheckedAll = () => {
    return totalCount > 0 && totalCount === selectedStudentIds.length;
  };

  const handleSelectAll = () => {
    studentApi
      .get(`/SelectAllStudentsByClass`, {
        params: {
          classId: classId,
        },
      })
      .then((res) => {
        setSelectedStudentIds(res.data?.result ?? []);
        setIsCheckAllInPage(false);
        setIsCheckAll(true);
      })
      .catch(() => toast.error("Some thing wrong when select all!"));
  };

  const handleClickDisplayStudentList = () => {
    setOpenListStudent(!openListStudent);
  };

  return (
    <div className={styles.main}>
      <div className={styles.studentInClass}>
        <ToastContainer />
        <main className={styles.frameParent}>
          <section className={styles.caretDown}>
            <div style={{ width: "100%" }}>
              <div className={styles.syllabusTabParent}>
                <div className={styles.trainingProgramDetailWrapper}>
                  <div className={styles.trainingProgramDetail}>
                    <div className={styles.programName}>
                      <h1 className={styles.studentList}>Student List</h1>
                      <button
                        onClick={handleClickDisplayStudentList}
                        className={styles.moreHorizontal}
                      >
                        <img src={moreIcon} alt="" />
                      </button>
                    </div>
                  </div>
                </div>
              </div>
              {openListStudent && (
                <>
                  <ActionBar
                    handleChangePageNumber={handleChagePageNumber}
                    selectedStudentIds={selectedStudentIds}
                    disableCheckAll={handleDisableSelectAllInPage}
                  />
                  <CustomizedTables
                    pagination={pagination}
                    setPagination={setPagination}
                    onCheckboxChange={handleCheckboxChange}
                    selectedIds={selectedStudentIds}
                    onSelectedAllInPage={handleSelectAllInPage}
                    IsSelectAllInPage={isCheckAllInPage}
                    disableCheckAll={handleDisableSelectAllInPage}
                    onSelectAll={handleSelectAll}
                    isSelectAll={isCheckAll}
                  />
                </>
              )}
            </div>
          </section>
        </main>
      </div>
    </div>
  );
}
