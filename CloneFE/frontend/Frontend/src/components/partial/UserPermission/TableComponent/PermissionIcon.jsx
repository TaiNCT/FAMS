import React from "react";
import { Icon } from "@chakra-ui/react";
import { FaLock } from "react-icons/fa";
import { GoStar } from "react-icons/go";
import { AiOutlinePlusCircle } from "react-icons/ai";
import { GoPencil } from "react-icons/go";
import { MdOutlineRemoveRedEye } from "react-icons/md";
import { FaRegEyeSlash } from "react-icons/fa";

const PermissionIcon = ({ permissionType }) => {
  switch (permissionType) {
    case "Full Access":
      return <Icon as={GoStar} />;
    case "Create":
      return <Icon as={AiOutlinePlusCircle} />;
    case "Modify":
      return <Icon as={GoPencil} />;
    case "View":
      return <Icon as={MdOutlineRemoveRedEye} />;
    case "Access Denied":
      return <Icon as={FaRegEyeSlash} />
    default:
      return <Icon as={FaLock} />;
  }
};

export default PermissionIcon;
