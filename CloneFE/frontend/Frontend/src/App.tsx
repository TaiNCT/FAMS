// @ts-nocheck
import { BrowserRouter, Route, Routes, useLocation, useNavigate } from "react-router-dom";
import { QueryClientProvider } from "react-query";
import { ChakraProvider } from "@chakra-ui/react";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import { LoginPage } from "./pages/LoginPage";
import { DashboardPage } from "./pages/DashboardPage";
import { UserManagement } from "./pages/UserManagement";
import { UserPermission } from "./pages/UserPermissionPage";
import { TrainingProgramPage } from "./pages/TrainingProgramPage";
import ClassDetailPage from "./pages/ClassPage/ClassDetailPage";

import "react-toastify/dist/ReactToastify.css";
import SystemViewPage from "./pages/ViewSystemPage/main.tsx";
import StudentInClass from "./pages/StudentInforManagement/StudentInClass";
// import { queryClient } from "../src/config/StudentInformanagement/StudentApi";
import EmailSend from "./pages/EmailSend";

import ReservationMangement from "../src/pages/ReservedListPage";
import InformEmail from "../src/components/partial/ReservationManagement/InformEmail";
import OptionList from "../src/components/partial/ReservationManagement/ViewListStudent/OptionList.jsx";
import AddNewStudentReserve from "../src/pages/AddNewReservedStudentPage";
import ReClass from "../src/pages/ReClassPage";
import { TestingPage } from "./pages/TestingPage";
import ClassPage from "./pages/ClassPage/ClassPage";
import CalendarPage from "./pages/ClassPage/TrainingCalendar";
import { SyllabusEdit } from "./pages/SyllabusPage/SyllabusEdit.tsx";
import { SyllabusList } from "./pages/SyllabusPage/SyllabusList";
import { EditSyllabus } from "./pages/ClassPage/EditSyllabusClassPage.jsx";
import { SyllabusDetail } from "./pages/SyllabusPage/SyllabusDetail";
import { EmailConfigurationPage } from "./pages/EmailConfigurationPage";
import { DetailPage } from "./pages/TemplateDetailPage";
import CreateClassPage from "./pages/ClassPage/CreateClassPage";
import TrainingProgramDetailPage from "./pages/TrainingProgramDetailPage";
import { SyllabusCreate } from "./pages/SyllabusPage/syllabusCreate";
import EditClassPage from "./pages/ClassPage/EditClassPage.tsx";
// It is recommended to import framer-motion for better UX, Ex : it gives smooth transition
// import { AnimatePresence } from 'framer-motion';

// Importing partial views in ScoreManagement
import { UpdateCertComp } from "./components/partial/ScoreManagement/UpdateCertComp";
import { StudentManagement } from "./components/partial/ScoreManagement/StudentManagement";
import { UploadPage } from "./components/partial/ScoreManagement/UploadPage";
import ScorePage from "./pages/ScorePage/ScorePage";
import { ListClassInfor } from "./components/partial/StudentManagement/ListClassInfor";
import AddStudentPage from "./pages/AddNewStudent/index";
import { queryClient } from "../src/config/axios";
import EditClassSyllabusPage from "./pages/ClassPage/EditClassSyllabusPage.tsx";
import { RecoverPasswordPage } from "./pages/RecoverPasswordPage/index.tsx";
import { UserCalendarPage } from "./pages/UserCalendarPage";
import { NotFoundPage } from "./pages/NotFoundPage/index.jsx";
import { UserProfilePage } from "./pages/UserProfilePage/index.jsx";

// Email testing
import { createContext, useEffect, useState } from "react";
import TestEmailPage from "./pages/TestEmailPage.tsx";
import ActivityLogPage from "./pages/ActivityLogPage/index.tsx";
import { CreateTrainingProgramPage } from "./pages/CreateTrainingProgramPage/index.tsx";
import { LearningMaterialPage } from "./pages/LearningMaterialPage/index.tsx";
import { LearningMaterialDetailPage } from "./pages/LearningMaterialDetailPage/index.tsx";
// Testing environment variable

import PopupLogout from "./PopupLogout.tsx";
import axios from "./axiosAuth.ts";

// @ts-ignore
const backend_api: string = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;

export const context = createContext(null);

function App() {
	const [open, setOpen] = useState(false);

	return (
		<context.Provider value={{}}>
			<QueryClientProvider client={queryClient}>
				<Routes>
					<Route path="/login" element={<LoginPage />} />
					<Route path="/recover-password" element={<RecoverPasswordPage />} />
					<Route path="/" element={<DashboardPage />} />
					<Route path="*" element={<NotFoundPage />} />
					<Route path="/profile" element={<ChakraProvider>{<UserProfilePage />}</ChakraProvider>} />
					<Route path="/user-management" element={<ChakraProvider>{<UserManagement />}</ChakraProvider>} />
					<Route path="/user-permission" element={<ChakraProvider>{<UserPermission />}</ChakraProvider>} />
					<Route path="/activity-log" element={<ChakraProvider>{<ActivityLogPage />}</ChakraProvider>} />
					<Route path="/user-calendar" element={<UserCalendarPage />} />
					<Route path="/system-view" element={<SystemViewPage />} />
					<Route path="/add-student" element={<AddStudentPage />} />
					<Route path="/add-student/:classID" element={<AddStudentPage />} />
					<Route path="/syllabus" element={<SyllabusList />} />
					<Route path="/syllabus/detail/:id" element={<SyllabusDetail />} />
					<Route path="/syllabus/create" element={<SyllabusCreate />} />
					<Route path="/syllabus/edit/:id" element={<SyllabusEdit />} />
					<Route path="/test" element={<TestingPage />} />
					<Route path="/classList" element={<ClassPage />} />
					<Route path="/calendar" element={<CalendarPage />} />
					<Route path="/trainingprogram" element={<TrainingProgramPage />} />
					<Route path="/EmailConfiguration" element={<EmailConfigurationPage />} />
					<Route path="/EmailConfiguration/Detail/:temId" element={<DetailPage />} />
					<Route path="/reservation-management" element={<ReservationMangement />} />
					<Route path="/informEmail" element={<InformEmail />} />
					<Route path="/option" element={<OptionList />} />
					<Route path="/addnewreserve" element={<AddNewStudentReserve />} />
					<Route path="/reservation-management/:message" element={<ReservationMangement />}></Route>
					<Route path="/reclass/:reservedClassId/:message" element={<ReClass />}></Route>
					<Route path="/testListInfor/:id" element={<ListClassInfor />} />
					<Route path="/score/:classId/" element={<ScorePage />}>
						<Route path="edit/:id" element={<ChakraProvider><StudentManagement /></ChakraProvider>} />
					</Route>
					<Route path="/score" element={<ScorePage />}>
						<Route path="class/:classid/s/:student/edit" element={<UpdateCertComp />} />
					</Route>
					<Route path="/trainingprogram/:id" element={<TrainingProgramDetailPage />} />
					<Route path="/trainingprogram/create" element={<CreateTrainingProgramPage />} />
					<Route path="/createclass" element={<CreateClassPage />}></Route>
					<Route path="/class-detail/:classId" element={<ClassDetailPage />}></Route>
					<Route path="/EmailConfiguration/SendEmail" element={<EmailSend />} />
					<Route path="/createclass/editClassSyllabus" element={<EditSyllabus />}></Route>
					<Route path="/class/:id/edit" element={<EditClassPage />}></Route>
					<Route path="/class/:id/edit/syllabus" element={<EditClassSyllabusPage />}></Route>
					<Route path="studentInClass/:classId" element={<StudentInClass />} />
					<Route path="/learning-material" element={<LearningMaterialPage />} />
					<Route path="/learning-material/:id" element={<LearningMaterialDetailPage />} />
				</Routes>
			</QueryClientProvider>
			<ToastContainer position="top-right" autoClose={5000} hideProgressBar={false} newestOnTop={false} closeOnClick rtl={false} pauseOnFocusLoss draggable pauseOnHover theme="light" />
			<PopupLogout open={open} setOpen={setOpen} />
		</context.Provider>
	);
}

export default App;
