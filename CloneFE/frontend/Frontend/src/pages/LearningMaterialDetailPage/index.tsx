import { ChakraProvider } from "@chakra-ui/react";
import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
import Footer from "../../components/layouts/Footer";
import { ToastContainer } from "react-toastify";
import LearningMaterialDetail from "@/components/partial/LearningMaterialDetail";

export function LearningMaterialDetailPage() {
  return (
    <ChakraProvider>
      <div className="font-inter h-screen flex flex-col overflow-y-hidden no-scrollbar">
        <Navbar />
        <div className="flex h-[calc(100vh-68px)] overflow-y-hidden no-scrollbar">
          <Sidebar />
          <div className="flex-1 overflow-y-scroll">
            <LearningMaterialDetail />
          </div>
        </div>
        <Footer />
      </div>
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
    </ChakraProvider>
  )
}