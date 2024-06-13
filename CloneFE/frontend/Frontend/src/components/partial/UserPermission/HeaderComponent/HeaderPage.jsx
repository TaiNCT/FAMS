import React from "react";
import { Flex, Text } from "@chakra-ui/react";
import UpdateButton from "./UpdateButton";
import ImportRoleButton from "./ImportNewRoleButton";
import ImportPermissionButton from "./ImportPermissionButton";
import ExportPermissionButton from "./ExportPermissionButton";

const HeaderPage = ({ onToggleEditMode, onImportData, onUpdateData }) => {
  return (
    <React.Fragment>
      <h3
        style={{
          color: "#fff",
          padding: "30px",
          width: "100%",
          margin: "2px 0px 30px",
          alignContent: "center",
          height: "80px",
          background: "#2d3748",
          fontSize: "32px",
          letterSpacing: "6px",
        }}
      >
        User Permissions
      </h3>
      <Flex px={8} justifyContent="space-between" w="full" mb={5}>
        <Flex alignItems="center">
          <ImportRoleButton
            onImportData={onImportData}
            onUpdateData={onUpdateData}
          />
        </Flex>
        <Flex justifyContent="flex-end" gap={2}>
          <ImportPermissionButton
            onImportData={onImportData}
            onUpdateData={onUpdateData}
          />
          <ExportPermissionButton onUpdateData={onUpdateData} />
          <UpdateButton onEditModeChange={onToggleEditMode} />
        </Flex>
      </Flex>
    </React.Fragment>
  );
};

export default HeaderPage;
