import React, { useEffect, useState, useRef } from "react";
import axios from "axios";
import { Box } from "@chakra-ui/react";
import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
import Footer from "../../components/layouts/Footer";
import UserProfile from "../../components/partial/UserProfile";

const UserProfilePage = () => {
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
          <Box px={8} mb={5}>
            <UserProfile />
          </Box>
        </div>
      </div>
      <Footer />
    </div>
  );
};

export { UserProfilePage };
