import {
  Box,
  Button,
  Flex,
  Menu,
  MenuButton,
  MenuItem,
  MenuList,
  Modal,
  ModalBody,
  ModalContent,
  ModalHeader,
  ModalOverlay,
  PopoverBody,
  PopoverContent,
  Text,
  useDisclosure,
} from "@chakra-ui/react";
import { LuPencil } from "react-icons/lu";
import { IoPersonOutline } from "react-icons/io5";
import { BiHide } from "react-icons/bi";
import { FaChevronRight } from "react-icons/fa6";
import {
  IoMdCloseCircleOutline,
  IoMdCheckmarkCircleOutline,
} from "react-icons/io";
import React, { useEffect, useState } from "react";
import axios from "axios";
import useRoleStore from "../../../../store/roleStore";
import EditUserForm from "./EditUserForm";
import { Bounce, toast } from "react-toastify";

const ActionMenu = ({ user, onUpdateData }) => {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const roles = useRoleStore((state) => state.roles);
  const config = {
    headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
  };

  const backend_api = `${import.meta.env.VITE_API_HOST}:${
    import.meta.env.VITE_API_PORT
  }`;

  const handleChangeRole = async (roleId, roleName) => {
    try {
      const url = `${backend_api}/api/change-role`;
      const response = await axios
        .put(
          url,
          {
            UserId: user.userId,
            RoleId: roleId,
          },
          config
        )
        .then((response) => {
          if (response.data.isSuccess) {
            toast.success("Update role success.", {
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
            onUpdateData();
          } else {
            toast.error(response.data.message, {
              position: "top-right",
              autoClose: 3000,
              hideProgressBar: false,
              closeOnClick: true,
              pauseOnHover: true,
              draggable: true,
              progress: 1,
              theme: "light",
              transition: Bounce,
            });
          }
        })
        .catch((error) => {
          toast.error("Change role of user failed.", {
            position: "top-right",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: 1,
            theme: "light",
            transition: Bounce,
          });
        });
    } catch (error) {
      toast.error("Change role of user failed.", {
        position: "top-right",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: 1,
        theme: "light",
        transition: Bounce,
      });
    }
  };

  const handleChangeStatus = async () => {
    try {
      const url = `${backend_api}/api/change-status`;
      const response = await axios.put(
        url,
        {
          userId: user.userId,
          status: !user.status, // Thay đổi trạng thái từ true thành false hoặc ngược lại
        },
        config
      );

      if (response.data.isSuccess) {
        toast.success("Update status of user success.", {
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
        onUpdateData();
      } else {
        toast.error(response.data.message, {
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
      }
    } catch (error) {
      toast.error("Change status of user failed.", {
        position: "top-right",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: 1,
        theme: "light",
        transition: Bounce,
      });
    }
  };

  return (
    <React.Fragment>
      <PopoverContent>
        <PopoverBody>
          <Button
            w="full"
            bg="none"
            borderRadius={0}
            onClick={() => {
              onOpen();
              document.body.setAttribute("style", "margin-right: 0 !important");
            }}
          >
            <Flex w="full" gap={4} alignItems="center">
              <Box fontSize="20px">
                <LuPencil />
              </Box>
              <Text color="blue.700">Edit user</Text>
            </Flex>
          </Button>
          <Menu>
            <MenuButton
              as={Button}
              rightIcon={<FaChevronRight />}
              w="full"
              borderRadius={0}
              bg="none"
            >
              <Flex w="full" gap={4} alignItems="center">
                <Box fontSize="20px">
                  <IoPersonOutline />
                </Box>
                <Text color="blue.700">Change role</Text>
              </Flex>
            </MenuButton>
            <MenuList>
              {roles.map((role) => (
                <MenuItem
                  key={role.roleId}
                  onClick={() => handleChangeRole(role.roleId, role.roleName)}
                >
                  <Text color="blue.700" fontWeight="500">
                    {role.roleName}
                  </Text>
                </MenuItem>
              ))}
            </MenuList>
          </Menu>
          <Button
            w="full"
            bg="none"
            borderRadius={0}
            onClick={handleChangeStatus}
          >
            <Flex w="full" gap={4} alignItems="center">
              <Box fontSize="20px">
                {user.status ? <BiHide /> : <IoMdCheckmarkCircleOutline />}
              </Box>
              <Text color="blue.700">
                {user.status ? "De-activate user" : "Activate user"}
              </Text>
            </Flex>
          </Button>
        </PopoverBody>
      </PopoverContent>

      <Modal isOpen={isOpen} onClose={onClose}>
        <ModalOverlay />
        <ModalContent overflow="hidden" maxW="500px">
          <ModalHeader
            bgColor="#2D3748"
            color="white"
            textAlign="center"
            py={2}
            position="relative"
          >
            Update User
            <Button
              bg="none"
              p={0}
              color="white"
              fontSize="25px"
              position="absolute"
              right={0}
              top={1}
              _hover={{ background: "none" }}
              onClick={onClose}
            >
              <IoMdCloseCircleOutline />
            </Button>
          </ModalHeader>
          <ModalBody>
            <EditUserForm
              user={user}
              onClose={onClose}
              onUpdateData={onUpdateData}
            />
          </ModalBody>
        </ModalContent>
      </Modal>
    </React.Fragment>
  );
};

export default ActionMenu;
