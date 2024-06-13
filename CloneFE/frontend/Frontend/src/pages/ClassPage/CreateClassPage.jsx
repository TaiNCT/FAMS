import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
import style from "./style.module.scss";
import Footer from "../../components/layouts/Footer";

// Comment this out later... or get rid of it if you want
import CreatePage from "../../components/partial/ClassManagement/features/CreateClass/pages/CreateClass";
import EditSyllabusPage from "../../components/partial/ClassManagement/features/EditSyllabus/pages/EditSyllabusPage";
export default function CreateClassPage() {
  return (
    <div className="flex flex-col min-h-screen overflow-y-hidden"
    style={{ overflowX: "hidden", boxSizing: "border-box" }}>
      <Navbar />
      <div className="flex flex-1 overflow-hidden"
        style={{ overflowY: "auto", overflowX: "hidden" }}>
        <Sidebar />
        <div className="flex-1 overflow-y-auto" style={{ overflowX: "hidden" }}>
          <CreatePage />
          {/* <EditSyllabusPage /> */}
        </div>
      </div>
      <Footer />
    </div>
  );
}
