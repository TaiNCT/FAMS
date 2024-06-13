import { useEffect, useState } from "react";
import styles from "./ReClassDialogStyle.module.scss";
import * as React from 'react';
import HighlightOffIcon from '@mui/icons-material/HighlightOff';
import { GetAssignmentListStudent, GetQuizListStudent, GetReClassDialogInfo, GetReClassPossibilies } from '../API/listApi'
import ReClassRecommend from "./ReClassRecommend";
import { useNavigate } from 'react-router-dom'

function ReClassDialog({ reservedClassId }) {

    const navigate = useNavigate();
    const handleNavigateClick = () => {
        navigate('/reservation-management')
    }

    const [reClassDialogInfo, setReClassDialogInfo] = useState([]);
    const [quizListData, setQuizListData] = useState([]);
    const [asmListData, setAsmListData] = useState([]);
    const [reClassList, setReClassList] = useState([]);
    const [mock, setMock] = useState([]);
    const [classInfo, setClassInfo] = useState([]);
    const [isPopUpOpenClassDetail, setIsPopUpOpenClassDetail] = useState(false);

    const openPopUpClassDetail = () => {
        setIsPopUpOpenClassDetail(true);
    };

    const closePopUpClassDetail = () => {
        setIsPopUpOpenClassDetail(false);
    };

    let quizListCountPercent = 100 / (quizListData?.length + 1);
    let asmListCountPercent = 100 / (asmListData?.length + 1);

    useEffect(() => {
        const fetchDataApi = async () => {
            const getReClassDialogInfo = await GetReClassDialogInfo(reservedClassId);
            setReClassDialogInfo(getReClassDialogInfo)

            const getQuizListStudent = await GetQuizListStudent(reservedClassId);
            setQuizListData(getQuizListStudent);

            const getAsignmentListStudent = await GetAssignmentListStudent(reservedClassId);
            setAsmListData(getAsignmentListStudent);

            /*const getMock = await GetMock(reservedClassId);
            setMock(getMock);*/

            const getReClassPosibilities = await GetReClassPossibilies(reservedClassId);
            setReClassList(getReClassPosibilities);

        };
        fetchDataApi();
    }, [reservedClassId])

    const calculateAverageQuizScore = () => {
        if (quizListData?.length === 0) {
            return 0;
        }
        const totalScore = quizListData?.reduce((sum, quiz) => {
            return sum + (quiz.quizScore || 0);
        }, 0);

        return (totalScore / quizListData?.length).toFixed(1);
    };
    const calculateAverageAsmScore = () => {
        if (asmListData?.length === 0) {
            return 0;
        }
        const totalScore = asmListData?.reduce((sum, asm) => {
            return sum + (asm.assignmentScore || 0);
        }, 0);

        return (totalScore / asmListData?.length).toFixed(1);
    };

    const handleChooseReClass = (value) => {
        setClassInfo(value);
    }


    return (
        <>
            <div className={styles.container}>
                <div className={styles.container1}>
                    <div className={styles.headerContent}>
                        <p className={styles.headerTitle}>Reserving details</p>
                        <HighlightOffIcon onClick={handleNavigateClick} className={styles.cancelButton} />
                    </div>
                    <div className={styles.container2}>
                        {reClassDialogInfo && (
                            <>
                                <div className={styles.classInfo}>
                                    <h1 className={styles.className}>{reClassDialogInfo.className}</h1>

                                    <div className={styles.classInfoContent}>
                                        <p className={styles.classCode}>{reClassDialogInfo.classCode} |</p>
                                        <p className={styles.timeReserve}>{reClassDialogInfo.classStartDate} - {reClassDialogInfo.classEndDate}</p>
                                    </div>
                                </div>


                                <p className={styles.partTitle}>Student score</p>
                                <div className={styles.moduleInfo}>
                                    <p className={styles.moduleTitle}>Current module</p>
                                    <p className={styles.moduleName}>{reClassDialogInfo.moduleName}</p>
                                </div>
                            </>
                        )}

                        <p className={styles.partTitle}>FEE</p>
                        <div className={styles.feeContainer}>
                            <div className={styles.feeContent}>
                                <div className={styles.listTable}>
                                    <p className={styles.listTableTitle}>Quiz</p>

                                    <div className={styles.listItemContainer}>

                                        {quizListData && (
                                            <>
                                                {quizListData.map((i) => (
                                                    <div className={styles.listTableItem} style={{ width: `${quizListCountPercent}%` }}>
                                                        <p className={styles.itemName}>{i.quizName}</p>
                                                        <p className={styles.itemScore}>{i.quizScore !== null ? i.quizScore : '-'}</p>
                                                    </div>
                                                ))}
                                                <div className={styles.listTableItem} style={{ width: `${quizListCountPercent}%` }}>
                                                    <p className={styles.avgScoreTitle}>Ave.</p>
                                                    <p className={styles.avgScore}>{calculateAverageQuizScore()}</p>
                                                </div>
                                            </>
                                        )}
                                    </div>
                                </div>

                                {/* table assignment */}

                                <div className={styles.listTable}>
                                    <p className={styles.listTableTitle}>Assignment</p>

                                    <div className={styles.listItemContainer}>

                                        {asmListData && (
                                            <>
                                                {asmListData.map((i) => (
                                                    <div className={styles.listTableItem} style={{ width: `${asmListCountPercent}%` }}>
                                                        <p className={styles.itemName}>{i.assignmentName}</p>
                                                        <p className={styles.itemScore}>{i.assignmentScore !== null ? i.assignmentScore : '-'}</p>
                                                    </div>
                                                ))}

                                                <div className={styles.listTableItem} style={{ width: `${asmListCountPercent}%` }}>
                                                    <p className={styles.avgScoreTitle}>Ave.</p>
                                                    <p className={styles.avgScore}>{calculateAverageAsmScore()}</p>
                                                </div>
                                            </>
                                        )}

                                    </div>
                                </div>


                            </div>
                        </div>

                        <p className={styles.partTitle}>MOCK</p>
                        <div className={styles.mockContent}>
                            <div className={styles.mockTable}>
                                <p className={styles.mockTableTitle}>MOCK</p>

                                <div className={styles.mockItemContainer}>
                                    {mock && (
                                        <>
                                            <div className={styles.mockTableItem} style={{ width: '25%' }}>
                                                <p className={styles.itemName}>Mock</p>
                                                <p className={styles.itemScore}>{mock.mock !== null ? mock.mock : "-"}</p>
                                            </div>

                                            <div className={styles.mockTableItem} style={{ width: '25%' }}>
                                                <p className={styles.itemName}>Final</p>
                                                <p className={styles.itemScore}>{mock.moduleScoreFinal !== null ? mock.moduleScoreFinal : "-"}</p>
                                            </div>

                                            <div className={styles.mockTableItem} style={{ width: '25%' }}>
                                                <p className={styles.itemName}>GPA</p>
                                                <p className={styles.itemScore}>{mock.gpa !== null ? mock.gpa : "-"}</p>
                                            </div>

                                            <div className={styles.mockTableItem} style={{ width: '25%' }}>
                                                <p className={styles.itemName}>Level</p>
                                                <p className={styles.itemScore}>{mock.moduleLevel !== null ? mock.moduleLevel : "-"}</p>
                                            </div>
                                        </>
                                    )}

                                </div>
                            </div>
                        </div>

                        <p className={styles.partTitle}>Reserving Information</p>
                        <div className={styles.reserveInfo}>
                            <p className={styles.reservePeriod}>Period<span>{reClassDialogInfo?.reservedStartDate} - {reClassDialogInfo?.reservedEndDate}</span></p>
                            <p className={styles.reserveReason}>Reason<span>{reClassDialogInfo?.reason}</span></p>
                        </div>

                        <p className={styles.reserveCondition}>Conditions</p>
                        <div className={styles.reserveConditionList}>
                            <div className={styles.leftReserveCondition}>
                                <label>
                                    <input type="checkbox" name="" checked /> Complete tuition payment
                                </label>
                                <label>
                                    <input type="checkbox" name="" checked /> Ensure the course has not progressed beyond 50%
                                </label>
                                <label>
                                    <input type="checkbox" name="" checked /> Determine retention fee payment
                                </label>
                            </div>

                            <div className={styles.rightReserveCondition}>
                                <label>
                                    <input type="checkbox" name="" checked /> Perform one-time retention check
                                </label>
                                <label>
                                    <input type="checkbox" name="" checked /> Identify the concluding module
                                </label>
                            </div>
                        </div>

                        <p className={styles.partTitle}>Re-class possibilities</p>

                        <div className={styles.reClassContainer}>
                            <div className={styles.reClassContent}>

                                {reClassList && (
                                    <>
                                        {reClassList.map((item, index) => (

                                            <div className={styles.classItem} style={{ cursor: "pointer" }} key={index}
                                                onClick={() => { handleChooseReClass(item); openPopUpClassDetail(); }}>
                                                <div className={styles.classItemHeader}>
                                                    <p className={styles.className} >{item.className}</p>
                                                    <p className={styles.classStatus}>{item.classStatus}</p>
                                                </div>
                                                <div className={styles.classItemInfo}>
                                                    <p className={styles.classCode}>{item.classCode} |</p>
                                                    <p className={styles.classPeriod}>{item.startDate} - {item.endDate}</p>
                                                </div>
                                            </div>

                                        ))}
                                    </>
                                )}

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            {isPopUpOpenClassDetail && (
                <>
                    <div
                        style={{
                            position: 'fixed',
                            top: 0,
                            left: 0,
                            width: '100%',
                            height: '100%',
                            backgroundColor: 'rgba(0, 0, 0, 0.5)', // Semi-transparent black
                            zIndex: 99, // Ensure it's above everything else
                        }}
                        onClick={() => setSelectedStudentId(null)} // Close the OptionBoard when overlay is clicked
                    />
                    <ReClassRecommend style={{ zIndex: 100 }} className={styles.popUpReClassRec} classInfo={classInfo} 
                    isOpen={isPopUpOpenClassDetail} reservedClassId={reservedClassId} 
                    onClose={closePopUpClassDetail} 
                    setReClassList= {setReClassList}/>
                </>
            )}


        </>
    )
};
export default ReClassDialog;