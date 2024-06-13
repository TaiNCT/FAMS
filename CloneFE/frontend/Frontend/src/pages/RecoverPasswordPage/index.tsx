import Footer from "../../components/layouts/Footer";
import Navbar from "../../components/layouts/Navbar";
import React from "react";
import { RecoverPassword } from "@/components/partial/RecoverPassword";

function RecoverPasswordPage() {
  return (
    <div className="page-container">
      <Navbar />
      <div className="content-container">
        <RecoverPassword />
      </div>
      <Footer />
    </div>
  );
}

export { RecoverPasswordPage };
