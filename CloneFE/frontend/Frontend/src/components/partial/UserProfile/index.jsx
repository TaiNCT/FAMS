import { Container } from "@chakra-ui/layout";
import Content from "./Content/Content";
import Sidebar from "./Sidebar/Sidebar";
import { useEffect, useState} from "react";
import axios from 'axios';

export default function Main() {
  const username = localStorage.getItem("username");
  const [userDetails, setUserDetails] = useState({
    fullName: "",
    address: "",
    email: "",
    phone: "",
    gender: "",
    dob: "",
  });
  const [error, setError] = useState("");

  const backend_api = `${import.meta.env.VITE_API_HOST}:${
    import.meta.env.VITE_API_PORT
  }`;

  useEffect(() => {
    const fetchUserInfo = async () => {
      const token = localStorage.getItem("token"); 
      try {
        const response = await axios.get(
          `${backend_api}/api/user-info?username=${username}`, 
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        if (response.data && response.data.result) {
          setUserDetails({
            fullName: response.data.result.fullName || "",
            address: response.data.result.address || "",
            email: response.data.result.email || "",
            phone: response.data.result.phone || "",
            gender: response.data.result.gender || "",
            dob: response.data.result.dob.split("T")[0] || "",
          });
        }
      } catch (error) {
        console.error("Error fetching user info:", error);
        setError("Failed to load user details.");
      }
    };

    fetchUserInfo();
  }, [username]);

  return (
    <Container
      style={{ paddingTop: "1.5em" }}
      display={{ base: "block", md: "flex" }}
      maxW="container.xl"
    >
      <Sidebar fullName={userDetails.fullName} />
      <Content userDetails={userDetails} />
    </Container>
  );
}
