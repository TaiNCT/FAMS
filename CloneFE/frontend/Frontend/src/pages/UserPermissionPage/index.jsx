import React, { useEffect, useState, useRef } from "react";
import axios from "axios";
import { Box } from "@chakra-ui/react";
import HeaderPage from "../../components/partial/UserPermission/HeaderComponent/HeaderPage";
import TablePerms from "../../components/partial/UserPermission/TableComponent/TablePerms";
import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
import Footer from "../../components/layouts/Footer";
import SaveCancelButtons from "../../components/partial/UserPermission/TableComponent/SaveCancelButtons";
import style from "./style.module.scss";
import useRolePermissionStore from "../../store/rolePermissionStore";
import useRoleStore from "../../store/roleStore";
import GlobalLoading from "../../components/global/GlobalLoading";
import { Bounce, toast } from "react-toastify";

const UserPermission = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [permissionsData, setPermissionsData] = useState([]);
  const { perms, setPerms } = useRolePermissionStore();
  const roles = useRoleStore((state) => state.roles);
  const setRoles = useRoleStore((state) => state.setRole);

  const [isEditing, setIsEditing] = useState(false);
  const [updatedPermissions, setUpdatedPermissions] = useState([]);
  const fetchDataRef = useRef();

  const toggleEditMode = () => setIsEditing(!isEditing);

  const backend_api = `${import.meta.env.VITE_API_HOST}:${
    import.meta.env.VITE_API_PORT
  }`;

  const saveChanges = async () => {
    const token = localStorage.getItem("token");
    await updateRolePermissions(updatedPermissions, token);
    fetchDataRef.current();
    toggleEditMode();
  };

  const updateRolePermissions = async (rolePermissions, token) => {
    const url = `${backend_api}/api/update-role-perms`;
    try {
      const response = await axios.put(
        url,
        { rolePermissions },
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
          },
        }
      );

      if (response.status === 200) {
        toast.success("Update role permissions success", {
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
      } else {
        toast.error("Update role permissions failed", {
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
      toast.error("Update role permissions failed", {
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
  };

  const handlePermissionUpdate = (roleId, newPermissions) => {
    setUpdatedPermissions((prev) => {
      const index = prev.findIndex((p) => p.roleId === roleId);
      if (index !== -1) {
        const updated = [...prev];
        updated[index] = { ...updated[index], ...newPermissions };
        return updated;
      }
      return [...prev, { roleId, ...newPermissions }];
    });
  };

  // Cancel changes
  const cancelChanges = () => {
    toggleEditMode();
  };

  useEffect(() => {
    setIsLoading(true);
    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
    };

    const fetchPermissionsData = async () => {
      try {
        const response = await axios.get(
          `${backend_api}/api/role-perms`,
          config
        );
        setPermissionsData(response.data.result);
      } catch (error) {
        console.error("Error fetching permissions data: ", error);
      }
    };

    const fetchRolePerms = async () => {
      try {
        const response = await axios.get(
          `${backend_api}/api/get-perms`,
          config
        );
        if (response.data && Array.isArray(response.data.result)) {
          setPerms(response.data.result);
        } else {
          console.error(
            "Data received from get-user-perms is not an array",
            response.data
          );
          setPerms([]);
        }
      } catch (error) {
        setPerms([]);
      } finally {
        setIsLoading(false);
      }
    };

    const fetchRoles = async () => {
      try {
        const response = await axios.get(`${backend_api}/api/get-role`, config);
        if (response.data && Array.isArray(response.data.result)) {
          setRoles(response.data.result);
        } else {
          console.error(
            "Data received from GetAllRole is not an array",
            response.data
          );
          setRoles([]);
        }
      } catch (error) {
        setRoles([]);
      }
    };

    fetchDataRef.current = fetchPermissionsData;
    fetchPermissionsData();
    fetchRolePerms();
    fetchRoles();
  }, []);

  return (
    <div
      className="flex flex-col min-h-screen overflow-y-hidden"
      style={{ overflowX: "hidden", boxSizing: "border-box" }}
    >
      <GlobalLoading isLoading={isLoading} />
      <Navbar />
      <div
        className="flex flex-1 overflow-hidden"
        style={{ overflowY: "auto", overflowX: "hidden" }}
      >
        <Sidebar />
        <div className="flex-1 overflow-y-auto" style={{ overflowX: "hidden" }}>
          <Box mb={5}>
            <HeaderPage onToggleEditMode={toggleEditMode} />
            <TablePerms
              permissionsData={permissionsData}
              isEditing={isEditing}
              onSave={saveChanges}
              onCancel={cancelChanges}
              onUpdate={handlePermissionUpdate}
            />
            {isEditing && (
              <SaveCancelButtons
                onSave={saveChanges}
                onCancel={cancelChanges}
              />
            )}
          </Box>
        </div>
      </div>
      <Footer />
    </div>
  );
};

export { UserPermission };
