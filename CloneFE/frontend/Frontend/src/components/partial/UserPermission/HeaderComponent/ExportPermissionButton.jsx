import React, { useState } from "react";
import { Button } from "@chakra-ui/react";
import { LuArrowUpToLine } from "react-icons/lu";
import axios from "axios";
import { Bounce, toast } from "react-toastify";

const ExportButton = () => {
  const [isLoading, setIsLoading] = useState(false);

  const backend_api = `${import.meta.env.VITE_API_HOST}:${
    import.meta.env.VITE_API_PORT
  }`;

  const token = localStorage.getItem("token");

  const handleExport = async () => {
    setIsLoading(true);
    try {
      const response = await axios({
        url: `${backend_api}/api/export-user-perms`,
        method: "GET",
        responseType: "blob",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      const href = URL.createObjectURL(response.data);
      const link = document.createElement("a");
      link.href = href;
      link.setAttribute("download", "ExportUserPermList.xlsx");
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
      URL.revokeObjectURL(href);
      toast.success("Export user permissions data success.", {
        position: "top-right",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "light",
        transition: Bounce,
      });
    } catch (error) {
      toast.error("Export user permissions failed.", {
        position: "top-right",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "light",
        transition: Bounce,
      });
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <Button
      onClick={handleExport}
      bg="#d45b13"
      color="white"
      px={4}
      py={2}
      display="flex"
      alignItems="center"
      justifyContent="center"
      _hover={{ bg: "DarkOrange" }}
      _active={{ bg: "#d45b13" }}
      _focus={{ boxShadow: "none" }}
      transition="background-color 0.3s"
      isLoading={isLoading}
    >
      <div style={{ display: "flex", alignItems: "center", gap: "4px" }}>
        <LuArrowUpToLine style={{ fontSize: "1.3em" }} />
        <span>Export</span>
      </div>
    </Button>
  );
};

export default ExportButton;
