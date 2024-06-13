import {
  Box,
  Button,
  Input,
  Radio,
  RadioGroup,
  Select,
  Stack,
  Text,
} from "@chakra-ui/react";
import { Bounce, toast } from "react-toastify";
import React, { useEffect, useState } from "react";
import SwitchButton from "../layouts/SwitchButton";
import useRoleStore from "../../../../store/roleStore";
import {
  isValidPhoneNumber,
  isValidName,
  calculateAge,
  formatPhoneNumber,
} from "../utils/validationUtils";
import axios from "axios";

const EditUserForm = ({ user, onClose, onUpdateData }) => {
  const roles = useRoleStore((state) => state.roles);
  const [fullName, setFullName] = useState(user.fullName);
  const [roleName, setRoleName] = useState(user.roleName);
  const [phone, setPhone] = useState(user.phone);
  const [dob, setDob] = useState(user.dob);
  const [gender, setGender] = useState(user.gender);
  const dobForInput = dob.split("T")[0];
  const [status, setStatus] = useState(user.status || false);
  const [roleId, setRoleId] = useState(user.roleId);

  const handlePhoneChange = (e) => {
    const formattedInput = formatPhoneNumber(e.target.value);
    setPhone(formattedInput.replace(/\s+/g, ""));
  };

  const backend_api = `${import.meta.env.VITE_API_HOST}:${
    import.meta.env.VITE_API_PORT
  }`;

  useEffect(() => {
    const selectedRole = roles.find((role) => role.roleName === roleName);
    if (selectedRole) {
      setRoleId(selectedRole.roleId);
    }
  }, [roleName, roles]);

  return (
    <div>
      <Box as="table" w="full">
        <tbody>
          <tr>
            <td>
              <Text as="div" fontWeight="600" py={3} pr={3}>
                User Type
              </Text>
            </td>
            <td>
              <Select
                id="userRole"
                value={roleName}
                shadow="lg"
                onChange={(e) => {
                  setRoleName(e.target.value);
                  const selectedRole = roles.find(
                    (role) => role.roleName === e.target.value
                  );
                  if (selectedRole) {
                    setRoleId(selectedRole.roleId);
                  }
                }}
              >
                {roles.map((role) => (
                  <option key={`role-${role.roleId}`} value={role.roleName}>
                    {role.roleName === "Admin" ? "Class Admin" : role.roleName}
                  </option>
                ))}
              </Select>
            </td>
          </tr>
          <tr>
            <td>
              <Text as="div" fontWeight="600" py={3} pr={3}>
                Full Name
              </Text>
            </td>
            <td>
              <Input
                id="fullname"
                value={fullName}
                pl={3}
                w="full"
                borderColor="black"
                onChange={(e) => {
                  setFullName(e.target.value);
                }}
              />
            </td>
          </tr>
          {!fullName && (
            <tr>
              <td></td>
              <td>
                <Text color="red" fontSize="15px" fontStyle="italic">
                  This field is required.
                </Text>
              </td>
            </tr>
          )}

          {fullName && !isValidName(fullName) && (
            <tr>
              <td></td>
              <td>
                <Text color="red" fontSize="15px" fontStyle="italic">
                  The name must contain only letters and spaces.
                </Text>
              </td>
            </tr>
          )}

          <tr>
            <td>
              <Text as="div" fontWeight="600" py={3} pr={3}>
                Email Address
              </Text>
            </td>
            <td id="email">{user.email}</td>
          </tr>
          <tr>
            <td>
              <Text as="div" fontWeight="600" py={3} pr={3}>
                Phone
              </Text>
            </td>
            <td>
              <Input
                id="phone"
                value={formatPhoneNumber(phone)}
                borderColor="black"
                onChange={handlePhoneChange}
              />
            </td>
          </tr>
          {!phone && (
            <tr>
              <td></td>
              <td>
                <Text color="red" fontSize="15px" fontStyle="italic">
                  This field is required.
                </Text>
              </td>
            </tr>
          )}
          {phone && !isValidPhoneNumber(phone) && (
            <tr>
              <td></td>
              <td>
                <Text color="red" fontSize="15px" fontStyle="italic">
                  This field must start at 0 and have 10 digits.
                </Text>
              </td>
            </tr>
          )}
          <tr>
            <td>
              <Text as="div" fontWeight="600" py={3} pr={3}>
                Date of Birth
              </Text>
            </td>

            {/* DoB */}
            <td>
              <Input
                id="dob"
                type="date"
                value={dobForInput}
                borderColor="black"
                onChange={(e) => {
                  setDob(e.target.value);
                }}
              />
            </td>
          </tr>
          {calculateAge(dobForInput) < 18 && (
            <tr>
              <td></td>
              <td>
                <Text color="red" fontSize="15px" fontStyle="italic">
                  The age must be larger than 18.
                </Text>
              </td>
            </tr>
          )}
          <tr>
            <td>
              <Text as="div" fontWeight="600" py={3} pr={3}>
                Gender
              </Text>
            </td>
            <td>
              <RadioGroup
                id="gender"
                value={gender === "Male" ? "1" : "2"}
                onChange={(e) => {
                  setGender(e === "1" ? "Male" : "Female");
                }}
              >
                <Stack spacing={5} direction="row">
                  <Radio value="1">Male</Radio>
                  <Radio value="2">Female</Radio>
                </Stack>
              </RadioGroup>
            </td>
          </tr>
          <tr>
            <td>
              <Text as="div" fontWeight="600" py={3} pr={3}>
                Status
              </Text>
            </td>
            <td>
              <SwitchButton
                checked={status}
                onToggle={() => {
                  setStatus(!status); // Chỉnh sửa ở đây
                }}
              />
            </td>
          </tr>
        </tbody>
      </Box>
      <Box display="flex" justifyContent="center">
        <Button
          variant="ghost"
          _hover={{ background: "none" }}
          color="red"
          textDecoration="underline"
          onClick={onClose}
          py={2}
        >
          Cancel
        </Button>
        <Button
          colorScheme="blue"
          mr={3}
          onClick={async () => {
            if (
              !fullName ||
              !phone ||
              !isValidPhoneNumber(phone) ||
              calculateAge(dobForInput) < 18
            ) {
              toast.error("Unable to update information.", {
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
              try {
                const config = {
                  headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`,
                  },
                };
                const url = `${backend_api}/api/update-user`;
                const response = await axios.put(
                  url,
                  {
                    userId: user.userId,
                    fullName: fullName,
                    phone: phone,
                    dob: dobForInput,
                    gender: gender,
                    roleId: roleId,
                    roleName: roleName,
                    status: status,
                  },
                  config
                );
                if (response.data.isSuccess) {
                  toast.success("Update user information success.", {
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
                  onClose();
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
                toast.error("Update user infomation failed.", {
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
            }
          }}
          bgColor="#2D3748"
        >
          Save
        </Button>
      </Box>
    </div>
  );
};

export default EditUserForm;
