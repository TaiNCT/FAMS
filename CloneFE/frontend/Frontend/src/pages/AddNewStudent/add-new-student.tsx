import React, { useEffect, useState } from "react";
import "./add-new-student.css";
import { Link, useParams } from "react-router-dom";
import { studentApi } from "../../config/axios";
import { universities } from "../../assets/vietnam_uni";
import { cities } from "../../assets/vietnam_cities";
import { Major } from "@/model/StudentLamNS";
import "react-toastify/dist/ReactToastify.css";

const AddStudent: React.FC = () => {

    const { classID } = useParams();

    // MUTATABLE STUDENT ID
    const [mutaStudentId, setMutaStudentId] = useState<string>("")
    const handleMutaChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setMutaStudentId(event.target.value);
    };
    // FULL NAME
    const [fullName, setFullName] = useState<string>('');
    const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setFullName(event.target.value);
    };
    // DATE OF BIRTH
    const [dateOfBirth, setDateOfBirth] = useState<string>("");
    const handleDobChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setDateOfBirth(event.target.value);
    };
    // GENDER
    const [gender, setGender] = useState<string>("");
    function handleGenderChange(value: string) {
        setGender(value);
    };
    // PHONE
    const [phone, setPhone] = useState<string>("");
    const handlePhoneChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setPhone(event.target.value);
    }
    // EMAIL
    const [email, setEmail] = useState<string>("");
    const handleEmailChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setEmail(event.target.value);
    };
    // MAJOR ID
    const [majorId, setMajorId] = useState<string>("");
    function handleMajorChange(value: string) {
        setMajorId(value);
    };
    const [majorList, setMajorList] = useState<Major[]>([])
    const fetchMajorData = async () => {
        try {
            const response = await studentApi.get("/GetAllMajor");
            setMajorList(response.data.result || []);
        } catch (error) {
            console.error("Error fetching student data:", error);
        }
    };
    useEffect(() => {
        fetchMajorData();
    }, []);
    // GPA
    const [gpa, setGpa] = useState<number>(0);
    const handleGpaChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const parsedGpa = parseFloat(event.target.value);
        setGpa(parsedGpa);
    };
    // ADDRESS
    const [address, setAddress] = useState<string>("");
    const handleAddressChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setAddress(event.target.value);
    };
    // FA ACCOUNT
    const [faAccount, setFaAccount] = useState<string>("");
    const handleFaAccountChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setFaAccount(event.target.value);
    };
    // STATUS
    const [attendingStatus, setAttendingStatus] = useState<string>('');
    function handleStatusChange(value: string) {
        setAttendingStatus(value);
    };
    // AREA
    const [area, setArea] = useState<string>("");
    function handleAreaChange(value: string) {
        setArea(value);
    };
    // RECER
    const [recer, setRecer] = useState<string>("");
    const handleRecerChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRecer(event.target.value);
    };
    // UNIVERSITY
    const [university, setUniversity] = useState<string>('');
    function handleUniversityChange(value: string) {
        setUniversity(value);
    };
    // CLASS ID
    const [classId, setClassId] = useState<string>(classID || "");
    function handleClassChange(value: string) {
        setClassId(value);
    };
    const [classList, setClassList] = useState<any[]>([])
    const fetchClassData = async () => {
        try {
            const response = await studentApi.get("/GetAllClass");
            setClassList(response.data.result || []);
        } catch (error) {
            console.error("Error fetching student data:", error);
        }
    };
    useEffect(() => {
        fetchClassData();
    }, []);

    const HandleClear = () => {
        setMutaStudentId("")
        setFullName("");
        setDateOfBirth("");
        setGender("");
        setPhone("");
        setEmail("");
        setMajorId("");
        setGpa(0);
        setAddress("");
        setFaAccount("")
        setArea("")
        setRecer("");
        setUniversity("");
        setClassId("");
        setAttendingStatus("");
    }
    const currentDate: Date = new Date();
    const graduationDate: Date = new Date(currentDate);
    graduationDate.setFullYear(graduationDate.getFullYear() + 4)

    const studentData = {
        mutatableStudentId: mutaStudentId,
        certificationDate: "", // Optional
        fullName: fullName,
        certificationStatus: 0, // Optional
        dob: dateOfBirth ? new Date(dateOfBirth).toISOString() : "",
        gender: gender,
        phone: phone,
        email: email,
        majorId: majorId,
        graduatedDate: graduationDate.toISOString(),
        gpa: gpa,
        address: address,
        faaccount: faAccount,
        type: 1, // put it a 1
        status: "Active",
        joinedDate: currentDate.toISOString(),
        area: area,
        recer: recer,
        university: university,
        audit: 0,
        mock: 0,
        addStudentClassDTOs: [
            {
                classId: classId,
                attendingStatus: attendingStatus,
                result: 0,
                finalScore: 0,
                gpalevel: Math.ceil(gpa / 2.5),
                certificationStatus: 0,
                certificationDate: "",
                method: 0
            }
        ]
    };

    const [validationMessage, setValidationMessage] = useState<string>("");
    const [validationMessageColor, setValidationMessageColor] = useState<string>("");
    const handleSuccess = () => {
        setValidationMessage("Student successfully added");
        setValidationMessageColor("green");
    };
    const handleRequiredFieldNotMet = (value: string) => {
        setValidationMessage(`The ${value.toUpperCase()} field is empty or in the wrong format`);
        setValidationMessageColor("red")
    };
    const clearMessage = () => {
        setValidationMessage("");
        setValidationMessageColor("");
    }

    const [currentError, setCurrentError] = useState<string[]>([]);

    const HandleAdd = () => {
        let errors = [];

        if (!fullName || typeof fullName !== 'string' || fullName.trim() === '' || !isNaN(Number(fullName)) || /[^a-zA-Z '-]/.test(fullName)) {
            errors.push('FULL NAME');
        }
        if (!mutaStudentId || typeof mutaStudentId !== 'string' || mutaStudentId.trim() === '' || !isNaN(Number(mutaStudentId))) {
            errors.push('MutaStudentId');
        }
        if (!dateOfBirth) {
            errors.push('DATE OF BIRTH');
        } else {
            const dateOfBirthDate = new Date(dateOfBirth);
            const currentDate = new Date();
            const eighteenYearsAgo = new Date(currentDate.getFullYear() - 18, currentDate.getMonth(), currentDate.getDate());
            const eightyYearsAgo = new Date(currentDate.getFullYear() - 80, currentDate.getMonth(), currentDate.getDate());

            if (dateOfBirthDate > eighteenYearsAgo || dateOfBirthDate < eightyYearsAgo) {
                errors.push('DATE OF BIRTH');
            }
        }
        if (!gender || gender.trim() === '') {
            errors.push('GENDER');
        }
        if (!email || typeof email !== 'string' || email.trim() === '' || !email.includes('@')) {
            errors.push('EMAIL');
        }
        if (!phone || typeof phone !== 'string' || phone.trim() === '' || !phone.startsWith('0') || !/^\d{10}$/.test(phone)) {
            errors.push('PHONE');
        }
        if (!majorId) {
            errors.push('MAJOR');
        }
        if (!gpa || isNaN(Number(gpa)) || gpa < 0 || gpa > 10) {
            errors.push('GPA');
        }
        if (!address || typeof address !== 'string' || address.trim() === '' || !isNaN(Number(address))) {
            errors.push('ADDRESS');
        }
        if (!faAccount || typeof faAccount !== 'string' || faAccount.trim() === '' || !isNaN(Number(faAccount))) {
            errors.push('FA ACCOUNT');
        }
        if (!area) {
            errors.push('AREA');
        }
        if (!recer || typeof recer !== 'string' || recer.trim() === '' || !isNaN(Number(recer)) || /[^a-zA-Z '-]/.test(recer)) {
            errors.push('RECER');
        }
        if (!university) {
            errors.push('UNIVERSITY');
        }
        if (!classId) {
            errors.push('CLASS');
        }
        if (!attendingStatus) {
            errors.push('ATTENDING STATUS');
        }

        if (errors.length > 0) {
            setCurrentError(errors);
            //   handleRequiredFieldNotMet(errors.join(', '));
        } else {
            clearMessage();
            studentApi
                .post(`/create-student`, studentData)
                .then((response) => {
                    handleSuccess();
                    setTimeout(() => {
                        window.location.reload();
                    }, 1000);
                })
                .catch((error) => {
                    console.error("Error creating student:", error);
                });
        }
    }


    return (
        <>
            <header className="add-student-header">Add a new student</header>
            <div className="add-student-container">
                <div className="add-student-main">
                    <div className="add-student-inner">
                        <h2>General</h2>
                        <div className="add-student-attribute-container">
                            <div className="add-student-attribute">
                                <div>Full name</div>
                                <input
                                    type='text'
                                    value={fullName}
                                    onChange={handleNameChange}
                                />
                                {(currentError.includes("FULL NAME")) && (
                                    <span>Full name is empty or in the wrong format</span>
                                )}
                            </div>
                            <div className="add-student-attribute">
                                <div>Gender</div>
                                <select className="add-student-dropdown" onChange={(e) => handleGenderChange(e.target.value)}>
                                    <option value="">Choose a gender</option>
                                    <option value="Male">Male</option>
                                    <option value="Female">Female</option>
                                </select>
                                {(currentError.includes("GENDER")) && (
                                    <span>Gender is empty</span>
                                )}
                            </div>
                            <div className="add-student-attribute">
                                <div>Date of birth</div>
                                <input
                                    type="date"
                                    value={dateOfBirth}
                                    onChange={handleDobChange}
                                />
                                {(currentError.includes("DATE OF BIRTH")) && (
                                    <span>Date of birth is empty or in the wrong format</span>
                                )}
                            </div>
                            <div className="add-student-attribute">
                                <div>Phone</div>
                                <input
                                    type="number"
                                    value={phone}
                                    onChange={handlePhoneChange}
                                />
                                {(currentError.includes("PHONE")) && (
                                    <span>Phone number is empty or in the wrong format</span>
                                )}
                            </div>
                            <div className="add-student-attribute">
                                <div>Email</div>
                                <input
                                    type="email"
                                    value={email}
                                    onChange={handleEmailChange}
                                />
                                {(currentError.includes("EMAIL")) && (
                                    <span>Email is empty or in the wrong format</span>
                                )}
                            </div>
                            <div className="add-student-attribute">
                                <div>Address</div>
                                <input
                                    type="text"
                                    value={address}
                                    onChange={handleAddressChange}
                                />
                                {(currentError.includes("ADDRESS")) && (
                                    <span>Address is empty or in the wrong format</span>
                                )}
                            </div>
                            <div className="add-student-attribute">
                                <div>Area</div>
                                <select className="add-student-dropdown" onChange={(e) => handleAreaChange(e.target.value)}>
                                    <option value="">Choose a city</option>
                                    {cities.map((city: any) => (
                                        <option value={city}>{city}</option>
                                    ))}
                                </select>
                                {(currentError.includes("AREA")) && (
                                    <span>Area is empty</span>
                                )}
                            </div>
                        </div>
                    </div>
                    <div className="add-student-inner">
                        <h2>Others</h2>
                        <div className="add-student-attribute-container">
                            <div className="add-student-attribute">
                                <div>Mutatable student ID</div>
                                <input
                                    type="text"
                                    value={mutaStudentId}
                                    onChange={handleMutaChange}
                                />
                                {(currentError.includes("MutaStudentId")) && (
                                    <span>MutaStudentId is empty or in the wrong format</span>
                                )}
                            </div>
                            <div className="add-student-attribute">
                                <div>University</div>
                                <select className="add-student-dropdown" onChange={(e) => handleUniversityChange(e.target.value)}>
                                    <option value="">Choose a university</option>
                                    {universities.map((university: any) => (
                                        <option value={university}>{university}</option>
                                    ))}
                                </select>
                                {(currentError.includes("UNIVERSITY")) && (
                                    <span>University is empty</span>
                                )}
                            </div>
                            <div className="add-student-attribute">
                                <div>Major</div>
                                <select className="add-student-dropdown" onChange={(e) => handleMajorChange(e.target.value)}>
                                    <option value="">Choose a major</option>
                                    {majorList.map((major: Major) => (
                                        <option value={major.majorId}>{major.name}</option>
                                    ))}
                                </select>
                                {(currentError.includes("MAJOR")) && (
                                    <span>Major is empty</span>
                                )}
                            </div>
                            <div className="add-student-attribute">
                                <div>GPA</div>
                                <input
                                    type='number'
                                    value={gpa}
                                    onChange={handleGpaChange}
                                />
                                {(currentError.includes("GPA")) && (
                                    <span>GPA is empty or in the wrong value</span>
                                )}
                            </div>
                            <div className="add-student-attribute">
                                <div>FA Account</div>
                                <input
                                    type="text"
                                    value={faAccount}
                                    onChange={handleFaAccountChange}
                                />
                                {(currentError.includes("FA ACCOUNT")) && (
                                    <span>Fa account is empty or in the wrong format</span>
                                )}
                            </div>

                            <div className="add-student-attribute">
                                <div>REcer</div>
                                <input
                                    type="text"
                                    value={recer}
                                    onChange={handleRecerChange}
                                />
                                {(currentError.includes("RECER")) && (
                                    <span>REcer is empty or in the wrong format</span>
                                )}
                            </div>
                        </div>
                    </div>

                    <div className="add-student-inner">
                        <h2>Class Info</h2>
                        <div className="add-student-attribute-container">
                            <div className="add-student-attribute">
                                <div>Class</div>
                                <select className="add-student-dropdown" onChange={(e) => handleClassChange(e.target.value)}>
                                    <option value="">Choose a class</option>
                                    {classList.map((classItem: any) => (
                                        <option
                                            value={classItem.classId}
                                            selected={classItem.classId == classID}
                                        >
                                            {classItem.className}
                                        </option>
                                    ))}
                                </select>
                                {(currentError.includes("CLASS")) && (
                                    <span>Class is empty</span>
                                )}
                            </div>
                            <div className="add-student-attribute">
                                <div>Attending status</div>
                                <select className="add-student-dropdown" onChange={(e) => handleStatusChange(e.target.value)}>
                                    <option value="">Choose student attending status</option>
                                    <option value="InClass">In Class</option>
                                    <option value="DropOut">Drop Out</option>
                                    <option value="Finish">Finish</option>
                                    <option value="Reserve">Reserve</option>
                                </select>
                                {(currentError.includes("ATTENDING STATUS")) && (
                                    <span>Attending status is empty</span>
                                )}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            {validationMessage && (
                <div className="add-student-error-message" style={{ color: validationMessageColor }}>
                    {validationMessage}
                </div>
            )}
            <div className="add-student-action">
                <button onClick={HandleAdd}>Add</button>
                <button onClick={HandleClear}>Clear</button>
                {classID == undefined ? (
                    <Link to={`/system-view`}>
                        <button>Cancel</button>
                    </Link>
                ) : (
                    <Link to={`/studentInClass/${classID}`}>
                        <button>Cancel</button>
                    </Link>
                )}
            </div>
        </>
    );
};

export default AddStudent;
