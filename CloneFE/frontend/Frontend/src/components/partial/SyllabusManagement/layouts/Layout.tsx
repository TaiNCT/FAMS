/* eslint-disable react/prop-types */
import { Box, Flex } from "@chakra-ui/react";
import Footer from "../../../layouts/Footer";
import Navbar from "../../../layouts/Navbar";
import Sidebar from "../../../layouts/Sidebar";

function MainLayout({ children }: { children: React.ReactNode }) {
  return (
    <Flex minH="100vh" flexDir="column">
      {/* <Navbar /> */}
      <Flex w="full" flex={1}>
        {/* <Sidebar /> */}
        <Box w="full">{children}</Box>
      </Flex>
      {/* <Footer /> */}
    </Flex>
  );
}

export default MainLayout;
