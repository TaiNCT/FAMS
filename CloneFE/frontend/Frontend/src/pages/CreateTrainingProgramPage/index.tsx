// @ts-nocheck
import React, { useEffect } from "react";
import { CreateTrainingProgram } from "../../components/partial/TrainingProgramManagement/CreateTrainingProgram";
import Footer from "../../components/layouts/Footer";
import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
import style from "./style.module.scss";
import { MyGlobalContext } from "../../components/partial/TrainingProgramManagement/contexts/DataContext";
import { ToastContainer, toast } from "react-toastify";
import { useLocation } from "react-router-dom";
import { red } from "@mui/material/colors";

export function CreateTrainingProgramPage() {
  const [isSidebarOpen, setIsSidebarOpen] = React.useState<boolean>(true);
  const location = useLocation();

  return (
    <main className={style.main}>
      <MyGlobalContext.Provider
        value={{ isSidebarOpen, setIsSidebarOpen }}
      >
        <Sidebar />
        <Navbar />
        <ToastContainer />
        <div
          id={style.trainingProgramView}
          style={{
            marginLeft: isSidebarOpen ? "0" : "-13rem",
            transition: "margin-left 0.3s ease-in-out",
          }}
        >
          <CreateTrainingProgram />
        </div>
        <Footer />
      </MyGlobalContext.Provider>
    </main>
  );
}
