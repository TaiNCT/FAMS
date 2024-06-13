import { useState, useEffect } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { Avatar, Heading, Text, VStack } from "@chakra-ui/react";
import avtImg from "../../../../assets/LogoStManagement/avt.jpg";

function Profile({ fullName }) {
  const [avatarUrl, setAvatarUrl] = useState(avtImg);
  const username = localStorage.getItem("username");
  const navigate = useNavigate();
  const location = useLocation();
  useEffect(() => {
    if (username) {
      setAvatarUrl(
        `https://api.dicebear.com/8.x/pixel-art/svg?seed=${encodeURIComponent(
          username
        )}`
      );
    }
  }, [navigate, location.pathname, username]);

  return (
    <VStack spacing={3} py={5} borderColor="brand.light">
      <Avatar
        size="2xl"
        name="avt"
        cursor="pointer"
        textAlign={"center"}
        src={avatarUrl}
      ></Avatar>
      <VStack spacing={1}>
        <Heading as="h3" fontSize="2xl" color="brand.dark">
          {fullName}
        </Heading>
        <Text color="brand.gray" fontSize="sm">
          @{username}
        </Text>
      </VStack>
    </VStack>
  );
}

export default Profile;
