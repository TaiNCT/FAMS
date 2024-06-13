import Sidebar from "../../components/layouts/Sidebar";
import Navbar from "../../components/layouts/Navbar/index";
import Footer from "../../components/layouts/Footer";
import ActivityLogList from "@/components/partial/EmailInformRemind/ActivityLogTable";

export default function ActivityLogPage() {
  return (
    <div className="font-inter h-screen flex flex-col overflow-y-hidden no-scrollbar">
      <Navbar />
      <div className="flex h-[calc(100vh-68px)] overflow-y-hidden no-scrollbar">
        <Sidebar />
        <div className="flex-1 overflow-y-scroll">
          <ActivityLogList />
        </div>
      </div>
      <Footer />
    </div>
  );
}
