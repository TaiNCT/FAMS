import React, { useEffect, useState, useRef } from "react";
import axios from "axios";
import { Box } from "@chakra-ui/react";
import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
import Footer from "../../components/layouts/Footer";
import UserCalendar from "../../components/partial/UserCalendar";

const UserCalendarPage = () => {
  return (
    <div className="flex flex-col min-h-screen overflow-y-hidden">
      <Navbar />
      <div className="flex flex-1 overflow-hidden">
        <Sidebar />
        <div className="flex-1 overflow-y-auto">
          <Box px={8} mb={5}>
            <UserCalendar />
          </Box>
        </div>
      </div>
      <Footer />
    </div>
  );
};

export { UserCalendarPage };
