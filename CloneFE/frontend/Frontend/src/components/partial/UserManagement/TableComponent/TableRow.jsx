import { Box, Button, Flex, Popover, PopoverTrigger } from "@chakra-ui/react";
import moment from "moment";
import React, { useState } from "react";
import { IoMdPerson } from "react-icons/io";
import { MdOutlineMoreHoriz } from "react-icons/md";
import ActionMenu from "../ActionComponent/ActionMenu";
import useRoleStore from "../../../../store/roleStore";
const formatDate = (date) => {
  return moment(date).format("DD/MM/YYYY");
};
const TableRow = ({ user, index, onUpdateData }) => {
  const roles = useRoleStore((state) => state.roles);

  const getRoleName = (roleId) => {
    const role = roles.find((r) => r.roleId === roleId);
    return role ? role.roleName : "";
  };

  const getRoleColor = (roleName) => {
    return roleName === "Admin" ? "#4db848" : "#2D3748";
  };

  return (
    <React.Fragment key={`${user.userId}`}>
      <Flex
        alignItems="center"
        justifyContent="center"
        p={1}
        borderBottom="1px solid black"
        fontWeight="500"
        whiteSpace="nowrap"
      >
        {index}
      </Flex>
      <Flex
        borderBottom="1px solid black"
        alignItems="center"
        p={1}
        fontWeight="bold"
        whiteSpace="nowrap"
      >
        {user.fullName}
      </Flex>
      <Flex
        borderBottom="1px solid black"
        alignItems="center"
        p={1}
        fontWeight="500"
        whiteSpace="nowrap"
      >
        {user.email}
      </Flex>

      <Flex
        borderBottom="1px solid black"
        alignItems="center"
        p={1}
        fontWeight="500"
        whiteSpace="nowrap"
      >
        {formatDate(user.dob)}
      </Flex>

      <Flex
        borderBottom="1px solid black"
        alignItems="center"
        p={1}
        fontSize="20px"
        color={user.gender === "Female" ? "red" : "#2D3748"}
        whiteSpace="nowrap"
      >
        <IoMdPerson />
      </Flex>
      <Flex
        borderBottom="1px solid black"
        alignItems="center"
        p={1}
        color="white"
        whiteSpace="nowrap"
      >
        <Box
          as="span"
          bgColor={getRoleColor(getRoleName(user.roleId))}
          py={1}
          px={3}
          borderRadius="15px"
        >
          {getRoleName(user.roleId)}
        </Box>
      </Flex>
      <Flex
        borderBottom="1px solid black"
        p={1}
        justifyContent="flex-end"
        px={4}
        alignItems="center"
      >
        <Popover>
          <PopoverTrigger>
            <Button
              cursor="pointer"
              fontSize="25px"
              bg="none"
              borderRadius="full"
              p={0}
            >
              <MdOutlineMoreHoriz />
            </Button>
          </PopoverTrigger>
          <ActionMenu
            user={user}
            rolename={user.roleName}
            onUpdateData={onUpdateData}
          />
        </Popover>
      </Flex>
    </React.Fragment>
  );
};

export default TableRow;
