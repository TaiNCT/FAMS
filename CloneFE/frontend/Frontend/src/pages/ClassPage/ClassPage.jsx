import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar"
import Footer from "../../components/layouts/Footer";

import { Dashboard } from "../../components/partial/Dashboard";
import ListPage from "../../components/partial/ClassManagement/features/ClassList/pages/ListPage";
export default function ClassPage()
{
  return (
    <div className="flex flex-col min-h-screen overflow-y-hidden"
    style={{ overflowX: "hidden", boxSizing: "border-box" }}>
      <Navbar />
      <div className="flex flex-1 overflow-hidden"
        style={{ overflowY: "auto", overflowX: "hidden" }}
      >
        <Sidebar />
        <div className="flex-1 overflow-y-auto" style={{ overflowX: "hidden" }}>
          {/* <DemoPartial /> */}
          <ListPage />
        </div>
      </div>
      <Footer />
    </div>
  );
}
