import React, { useEffect, useState, useRef } from "react";
import Navbar from "../../components/layouts/Navbar";
import Footer from "../../components/layouts/Footer";
import NotFound from "../../components/partial/NotFound";
import { Box } from "@chakra-ui/react";
import style from "./style.module.scss";

const NotFoundPage = () => {
  return (
    <div className="page-container">
      <Navbar />
      <div className="content-container">
        <NotFound/>
      </div>
      <Footer />
    </div>
  );
};

export { NotFoundPage };
