import { ReactNode } from "react";
import Footer from "./components/Footer";
import Navbar from "./components/Navbar";
import SideBar from "./components/SideBar";
type LayoutProps = {
  children: ReactNode;
};
const Layout = ({ children }: LayoutProps) => {
  return (
    <div className="min-h-screen flex flex-col max-h-screen">
      <Navbar />
      <div className="flex flex-1 overflow">
        <SideBar />
        <div className="flex-1 border-t border-t-white">
          {children}
        </div>
      </div>
      <Footer />
    </div>
  );
};

export default Layout;
