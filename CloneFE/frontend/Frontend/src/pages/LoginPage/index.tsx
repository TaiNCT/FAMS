import Footer from "../../components/layouts/Footer";
import Navbar from "../../components/layouts/Navbar";
import React from "react";
import { Login } from "@/components/partial/Login";

function LoginPage() {
  return (
    <div className="page-container">
      <Navbar />
      <div className="content-container">
        <Login />
      </div>
      <Footer />
    </div>
  );
}

export { LoginPage };
