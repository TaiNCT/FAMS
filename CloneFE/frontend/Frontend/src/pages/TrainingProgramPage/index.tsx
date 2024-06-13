// @ts-nocheck
import React, { useEffect } from "react";
import TrainingProgramList from "../../components/partial/TrainingProgramManagement/TrainingProgramList/TrainingProgramList";
import Footer from "../../components/layouts/Footer";
import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
import style from "./style.module.scss";
import { ChakraProvider, extendTheme } from "@chakra-ui/react";
import { MyGlobalContext } from "../../components/partial/TrainingProgramManagement/contexts/DataContext";
import { ToastContainer, toast } from "react-toastify";
import { useLocation } from "react-router-dom";
import { red } from "@mui/material/colors";

export function TrainingProgramPage() {
  const [isSidebarOpen, setIsSidebarOpen] = React.useState<boolean>(true);
  const location = useLocation();

  useEffect(() => {
    if (location.state?.toastMessage) {
      toast.success(location.state.toastMessage, {
        autoClose: 2500,
      });
    }
  }, [location.state]);

  const theme = extendTheme({
    styles: {
      global: (props) => ({
        body: {
          padding: 0,
          margin: 0,
        },
      }),
    },
  });

  return (
    <main className={style.main}>
      <MyGlobalContext.Provider value={{ isSidebarOpen, setIsSidebarOpen }}>
        <Sidebar />
        <Navbar />
        <div
          id={style.trainingProgramView}
          style={{
            marginLeft: isSidebarOpen ? "0" : "-13rem",
            transition: "margin-left 0.3s ease-in-out",
          }}
        >
          <ChakraProvider theme={theme}>
            <TrainingProgramList />
          </ChakraProvider>
        </div>
        <Footer />
      </MyGlobalContext.Provider>
    </main>
  );
}
