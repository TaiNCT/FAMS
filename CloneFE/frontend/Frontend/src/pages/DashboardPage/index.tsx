import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
import style from "./style.module.scss";
import { Box } from "@chakra-ui/react";
// Comment this out later... or get rid of it if you want
import { Dashboard } from "../../components/partial/Dashboard";
import Footer from "../../components/layouts/Footer";

function DashboardPage() {
  return (
    <div id={style.main}>
      <div className="flex flex-col min-h-screen overflow-y-hidden">
        <Navbar />
        <div className="flex flex-1 overflow-hidden">
          <Sidebar />
          <div className="flex-1 overflow-y-auto">
            <Box px={8} mb={5}>
              <Dashboard />
            </Box>
          </div>
        </div>
        <Footer />
      </div>
    </div>
  );
}

export { DashboardPage };
