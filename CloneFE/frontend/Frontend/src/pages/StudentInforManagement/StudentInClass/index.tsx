import StudentInClassComponent from "../../../components/partial/StudentManagement/StudentInClass";
// import "./style.scss";
import Navbar from "../../../components/layouts/Navbar";
import Sidebar from "../../../components/layouts/Sidebar";
import Footer from "../../../components/layouts/Footer";

export default function StudentInClass() {
	return (
		<div className="font-inter h-screen flex flex-col overflow-y-hidden no-scrollbar">
			<Navbar />
			<div className="flex h-[calc(100vh-68px)] overflow-y-hidden no-scrollbar">
				<Sidebar />
				<div className="flex-1 overflow-y-scroll">
					<StudentInClassComponent />
				</div>
			</div>
			<Footer />
		</div>
	);
}
