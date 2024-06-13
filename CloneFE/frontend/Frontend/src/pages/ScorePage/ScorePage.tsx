import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
// import style from "./style.module.scss";
import Footer from "../../components/layouts/Footer";

// Comment this out later... or get rid of it if you want
import { Dashboard } from "../../components/partial/Dashboard";
import { ViewTraineeScore } from "../../components/partial/ScoreManagement/ViewTraineeScore";
export default function ClassPage() {
  return (
    <div className="font-inter h-screen flex flex-col overflow-y-hidden no-scrollbar">
      <Navbar />
      <div className="flex h-[calc(100vh-68px)] overflow-y-hidden no-scrollbar">
        <Sidebar />
        <ViewTraineeScore />
      </div>
      <Footer />
    </div>
  );
}
