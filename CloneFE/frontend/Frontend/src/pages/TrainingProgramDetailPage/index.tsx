// @ts-nocheck
import style from "./style.module.scss";
import Footer from "../../components/layouts/Footer";
import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
import useAxiosFetch from "../../components/partial/TrainingProgramManagement/TrainingProgramDetail/hooks/useFetchAxios";
import { useParams } from "react-router-dom";
import TrainingProgramDetail from "../../components/partial/TrainingProgramManagement/TrainingProgramDetail";
import { ToastContainer } from "react-toastify";
import { MyGlobalContext } from "../../components/partial/TrainingProgramManagement/contexts/DataContext";
import { useState } from "react";

const TrainingProgramDetailPage: React.FC = () => {
	// Params properties
	const { id } = useParams();
	// Doest not exist ReactNode error <- solve later
	const { trainingProgramData, setTrainingProgramData, isLoading, refreshData } = useAxiosFetch(id ?? "");
	const [showForm, setShowForm] = useState<number>(1);
	const [isSidebarOpen, setIsSidebarOpen] = useState<boolean>(true);

	return (
		<MyGlobalContext.Provider
			value={{
				trainingProgramData,
				setTrainingProgramData,
				showForm,
				setShowForm,
				isSidebarOpen,
				setIsSidebarOpen,
				refreshData,
			}}
		>
			<main className={style.main}>
				<Navbar />
				<Sidebar />
				<TrainingProgramDetail item={trainingProgramData} setItem={setTrainingProgramData} isLoading={isLoading} />
				<Footer />
			</main>
		</MyGlobalContext.Provider>
	);
};

export default TrainingProgramDetailPage;
