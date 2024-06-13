import { ChakraProvider } from "@chakra-ui/react";
import SyllabusComponent from "../../components/partial/SyllabusManagement/SyllabusDetailComponent";
import './style.scss';
import style from "./style.module.scss";
import { ToastContainer } from "react-toastify";
import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
import Footer from "../../components/layouts/Footer";

export function SyllabusDetail() {
	return (
		<div
			className="flex flex-col min-h-screen overflow-y-hidden"
			style={{ overflowX: "hidden", boxSizing: "border-box" }}
		>
			<Navbar />
			<div
				className="flex flex-1 overflow-hidden"
				style={{ overflowY: "auto", overflowX: "hidden" }}
			>
				<Sidebar />
				<div className="flex-1 overflow-y-auto" style={{ overflowX: "hidden" }}>
					<ChakraProvider>
						<SyllabusComponent />
					</ChakraProvider>
				</div>
			</div>
			<Footer />
			<ToastContainer
				position="top-right"
				autoClose={5000}
				hideProgressBar={false}
				newestOnTop={false}
				closeOnClick
				rtl={false}
				pauseOnFocusLoss
				draggable
				pauseOnHover
				theme="light"
			/>
		</div>
	)
}