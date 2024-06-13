import React, { useState } from "react";
import {
  Flex,
  Select,
  Box,
  Button,
  Text,
  Menu,
  MenuButton,
  Icon,
  MenuList,
  MenuItem,
} from "@chakra-ui/react";
import PermissionIcon from "./PermissionIcon";
import useRolePermissionStore from "../../../../store/rolePermissionStore";
import useRoleStore from "../../../../store/roleStore";

const PermsRow = ({ permission, isEditing, onUpdate}) => {
  const { perms } = useRolePermissionStore();
  const { roles } = useRoleStore();

  const getPermissionName = (permissionId) => {
    const perm = perms.find((p) => p.userPermsID === permissionId);
    return perm ? perm.userPermsName : "Unknown";
  };

  const getRoleName = (roleId) => {
    const role = roles.find((r) => r.roleId === roleId);
    return role ? role.roleName : "Unknown";
  };

  const renderPermission = (permissionType) => (
    <Flex alignItems="center">
      <PermissionIcon permissionType={permissionType} />
      <Text ml={2} isTruncated>
        {permissionType}
      </Text>
    </Flex>
  );

  const [selectedPermissions, setSelectedPermissions] = useState({
    syllabus: permission.syllabus,
    trainingProgram: permission.trainingProgram,
    class: permission.class,
    learningMaterial: permission.learningMaterial,
    userManagement: permission.userManagement,
  });

  

  const handleSelectChange = (permId, permissionType) => {
    setSelectedPermissions((prevPermissions) => {
      const newPermissions = { ...prevPermissions, [permissionType]: permId };
      if (isEditing) {
        onUpdate(permission.roleId, newPermissions);
      }
      return newPermissions;
    });
  };


  const renderPermissionDropdown = (permissionType) => {
    const selectedPermission = perms.find(
      (perm) => perm.userPermsID === selectedPermissions[permissionType]
    );
    return (
      <Menu>
        <MenuButton
          as={Button}
          justifyContent="space-between"
          p={1}
          minWidth="fit-content"
          height="auto"
          flexShrink={0}
          flexGrow={0}
          width="auto"
        >
          {selectedPermission ? (
            <Flex align="center">
              <PermissionIcon
                permissionType={selectedPermission.userPermsName}
              />
              <Text ml={2}>{selectedPermission.userPermsName}</Text>
            </Flex>
          ) : (
            "Permission"
          )}
        </MenuButton>
        <MenuList zIndex={100}>
          {perms.map((perm) => (
            <MenuItem
              key={perm.userPermsID}
              onClick={() =>
                handleSelectChange(perm.userPermsID, permissionType)
              }
              _focus={{ bg: "blue.100" }}
              _hover={{ bg: "blue.50" }}
              _active={{ bg: "blue.200" }}
            >
              <Flex alignItems="center">
                <PermissionIcon permissionType={perm.userPermsName} />
                <Text ml={2} fontSize="sm">
                  {perm.userPermsName}
                </Text>
              </Flex>
            </MenuItem>
          ))}
        </MenuList>
      </Menu>
    );
  };

  return (
    <Flex
      direction="row"
      alignItems="center"
      justifyContent="space-between"
      px={[2, 4]}
      py={3}
      borderBottom="1px solid black"
      fontWeight="500"
      fontSize={["sm", "md"]}
      whiteSpace="nowrap"
      bg="gray.50"
    >
      <Box flex="1">{getRoleName(permission.roleId)}</Box>
      <Box flex="1">
        {isEditing
          ? renderPermissionDropdown("syllabus")
          : renderPermission(getPermissionName(permission.syllabus))}
      </Box>
      <Box flex="1">
        {isEditing
          ? renderPermissionDropdown("trainingProgram")
          : renderPermission(getPermissionName(permission.trainingProgram))}
      </Box>
      <Box flex="1">
        {isEditing
          ? renderPermissionDropdown("class")
          : renderPermission(getPermissionName(permission.class))}
      </Box>
      <Box flex="1">
        {isEditing
          ? renderPermissionDropdown("learningMaterial")
          : renderPermission(getPermissionName(permission.learningMaterial))}
      </Box>
      <Box flex="1">
      {isEditing
          ? renderPermissionDropdown("userManagement")
          : renderPermission(getPermissionName(permission.userManagement))}
      </Box>
    </Flex>
  );
};

export default PermsRow;
