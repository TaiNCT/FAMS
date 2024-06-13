import {
  Box,
  Button,
  Input,
  InputGroup,
  InputRightElement,
  Modal,
  ModalBody,
  ModalContent,
  ModalFooter,
  ModalHeader,
  ModalOverlay,
  Radio,
  RadioGroup,
  Select,
  Stack,
  Text,
  useDisclosure,
  Flex,
} from "@chakra-ui/react";
import { IoAddCircleOutline } from "react-icons/io5";
import { IoMdCloseCircleOutline } from "react-icons/io";
import React, { useEffect, useState } from "react";
import SwitchButton from "../layouts/SwitchButton";
import useRoleStore from "../../../../store/roleStore";
import debounce from "lodash/debounce";
import axios from "axios";
import { formatPhoneNumber } from "../utils/validationUtils";
import { ViewIcon, ViewOffIcon } from "@chakra-ui/icons";
import { Bounce, toast } from "react-toastify";

const AddUserForm = ({ onUpdateData }) => {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [fullName, setFullName] = useState("");
  const roles = useRoleStore((state) => state.roles);
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [address, setAddress] = useState("");
  const [dob, setDob] = useState("");
  const [gender, setGender] = useState("");
  const [status, setStatus] = useState(false);
  const [roleId, setRoleId] = useState("");
  const [username, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [fullNameError, setFullNameError] = useState("");
  const [usernameError, setUserNameError] = useState("");
  const [emailError, setEmailError] = useState("");
  const [passwordError, setPasswordError] = useState("");
  const [phoneError, setPhoneError] = useState("");
  const [dobError, setDobError] = useState("");
  const [genderError, setGenderError] = useState("");
  const [addressError, setAddressError] = useState("");
  const [roleIdError, setRoleIdError] = useState("");

  const currentDate = new Date();
  const userDob = new Date(dob);

  const backend_api = `${import.meta.env.VITE_API_HOST}:${
    import.meta.env.VITE_API_PORT
  }`;

  useEffect(() => {
    if (!isOpen) {
      handleClose();
    }
  }, [isOpen]);

  const [showPassword, setShowPassword] = useState(false);
  const toggleShowPassword = () => setShowPassword(!showPassword);

  // Sử dụng regex để kiểm tra điều kiện mật khẩu
  const validatePassword = (password) => {
    const passwordPattern =
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/;
    return passwordPattern.test(password);
  };

  // Cập nhật hàm xử lý thay đổi mật khẩu
  const handlePasswordChange = (e) => {
    const newPassword = e.target.value;
    setPassword(newPassword);
    if (!validatePassword(newPassword)) {
      setPasswordError(
        "Password must contain at least 8 characters, including at least one uppercase letter, one lowercase letter, one number, and one special character."
      );
    } else {
      setPasswordError("");
    }
  };

  // Token
  const config = {
    headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
  };
  // Email duplicate validation
  useEffect(() => {
    const debouncedCheckEmail = debounce(checkEmailExists, 1000);
    // Gọi hàm kiểm tra email sau 100ms khi người dùng dừng nhập
    if (email.trim() !== "") {
      debouncedCheckEmail(); // Gọi hàm debounce
    }
    // Hàm kiểm tra email tồn tại
    async function checkEmailExists() {
      try {
        const response = await axios.post(
          `${backend_api}/api/check-mail`,
          { email },
          config
        );
        if (response.data.message.includes("Email already exists")) {
          setEmailError("This email is already in use");
        }
        if (response.data.message.includes("Wrong format")) {
          setEmailError("Wrong email format (Ex: example@gmail.com)");
        } else {
          setEmailError(
            <span style={{ color: "green" }}>Email Available</span>
          );
        }
      } catch (error) {
        if (error.response.data.message.includes("Email already exists")) {
          setEmailError("This email is already in use");
        }
        if (error.response.data.message.includes("Wrong format")) {
          setEmailError("Wrong email format (Ex: example@gmail.com)");
        }
      }
    }
    // Hủy debounce khi component unmount
    return () => {
      debouncedCheckEmail.cancel();
    };
  }, [email]);

  // Username duplicate validation
  useEffect(() => {
    const debouncedCheckUsername = debounce(checkUsernameExists, 1000);
    // Gọi hàm kiểm tra username sau 100ms khi người dùng dừng nhập
    if (username.trim() !== "") {
      debouncedCheckUsername(); // Gọi hàm debounce
    }
    // Hàm kiểm tra username tồn tại
    async function checkUsernameExists() {
      try {
        const response = await axios.post(
          `${backend_api}/api/check-username`,
          { username },
          config
        );
        if (response.data.message.includes("Username already exists")) {
          setUserNameError("This username is already in use");
        } else {
          setUserNameError(
            <span style={{ color: "green" }}>Username Available</span>
          );
        }
      } catch (error) {
        if (error.response.data.message.includes("Username already exists")) {
          setUserNameError("This username is already in use");
        } else {
          setUserNameError(
            <span style={{ color: "green" }}>Username Available</span>
          );
        }
      }
    }
    // Hủy debounce khi component unmount
    return () => {
      debouncedCheckUsername.cancel();
    };
  }, [username]);

  // Phone duplicate validation
  useEffect(() => {
    const debouncedCheckPhone = debounce(checkPhoneExists, 1000);
    // Gọi hàm kiểm tra Phone sau 100ms khi người dùng dừng nhập
    if (phone.trim() !== "") {
      debouncedCheckPhone(); // Gọi hàm debounce
    }
    // Hàm kiểm tra Phone tồn tại
    async function checkPhoneExists() {
      try {
        const response = await axios.post(
          `${backend_api}/api/check-phone`,
          { phone },
          config
        );
        if (response.data.message.includes("Phone number already exists")) {
          setPhoneError("This phone is already in use");
        } else {
          setPhoneError(
            <span style={{ color: "green" }}>Phone Available</span>
          );
        }
      } catch (error) {
        if (
          error.response.data.message.includes("Phone number already exists")
        ) {
          setUserNameError("This phone is already in use");
        } else {
          setUserNameError(
            <span style={{ color: "green" }}>Phone Available</span>
          );
        }
      }
    }
    // Hủy debounce khi component unmount
    return () => {
      debouncedCheckPhone.cancel();
    };
  }, [phone]);

  const handleSave = () => {
    let formIsValid = true;

    if (!fullName) {
      setFullNameError("This field is required");
      formIsValid = false;
    } else if (!/^[a-zA-ZÀ-Ỹà-ỹ\s]+$/.test(fullName)) {
      setFullNameError("Full name cannot contain special characters");
      formIsValid = false;
    } else {
      setFullNameError("");
    }

    if (!username) {
      setUserNameError("This field is required");
      formIsValid = false;
    } else {
      setUserNameError("");
    }

    const emailPattern = /^[a-zA-Z0-9_.+-]+@gmail\.com$/;
    if (!email || !emailPattern.test(email)) {
      setEmailError("Wrong email format (Ex: example@gmail.com).");
      formIsValid = false;
    } else {
      setEmailError("");
    }

    if (!password || !validatePassword(password)) {
      setPasswordError(
        "Password must contain at least 8 characters, including at least one uppercase letter, one lowercase letter, one number, and one special character."
      );
      formIsValid = false; // Đảm bảo form không hợp lệ nếu mật khẩu không đáp ứng yêu cầu
    } else {
      setPasswordError(""); // Nếu mật khẩu hợp lệ, xóa thông báo lỗi
    }

    if (!/^[0-9]+$/.test(phone)) {
      setPhoneError("Phone number must contain only digits");
      formIsValid = false;
    } else if (phone.charAt(0) !== "0") {
      setPhoneError("Phone number must start with 0");
      formIsValid = false;
    } else if (!phone || isNaN(phone) || phone.length !== 10) {
      setPhoneError("Please enter a valid 10-digit phone number");
      formIsValid = false;
    } else {
      setPhoneError("");
    }

    if (address.length === 0) {
      setAddressError("Please enter a valid address");
      formIsValid = false;
    } else {
      setAddressError("");
    }

    if (!dob || userDob >= currentDate) {
      setDobError("Please enter a valid date of birth");
      formIsValid = false;
    } else {
      setDobError("");
    }

    if (!gender) {
      setGenderError("Please select your gender");
      formIsValid = false;
    } else {
      setGenderError("");
    }

    if (!roleId) {
      setRoleIdError("Please select a role");
      formIsValid = false;
    } else {
      setRoleIdError("");
    }

    const addedUser = {
      fullName,
      email,
      phone,
      address,
      dob,
      gender,
      status,
      roleId,
      username,
      password,
    };
    if (formIsValid) {
      axios
        .post(`${backend_api}/api/create-user`, addedUser, config)
        .then((response) => {
          if (response.data.isSuccess) {
            toast.success("Add new user success.", {
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
            if (response.data.message.includes("Email already exists")) {
              setEmailError("This email is already in use");
              formIsValid = false;
            } else {
              setEmailError("");
            }
            if (response.data.message.includes("Username already exists")) {
              setUserNameError("This username is already in use");
              formIsValid = false;
            } else {
              setUserNameError("");
            }
            if (response.data.message.includes("Phone number already exists")) {
              setPhoneError("This phone number is already in use");
              formIsValid = false;
            } else {
              setPhoneError("");
            }
          }
        })
        .catch((error) => {
          toast.error("Add new user failed.", {
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
    }
  };

  const handleClose = () => {
    setFullName("");
    setEmail("");
    setPhone("");
    setAddress("");
    setDob("");
    setGender("");
    setStatus(false);
    setRoleId("");
    setUserName("");
    setPassword("");
    setFullNameError("");
    setUserNameError("");
    setEmailError("");
    setPasswordError("");
    setPhoneError("");
    setAddressError("");
    setDobError("");
    setGenderError("");
    setRoleIdError("");
    onClose();
  };

  return (
    <>
      <Button
        bg="#2D3748"
        color="white"
        px={3}
        _hover={{ bgColor: "#2d3748" }}
        onClick={() => {
          onOpen();
          document.body.setAttribute("style", "margin-right: 0 !important");
        }}
      >
        <Text as="span" fontSize="25px" me={3}>
          <IoAddCircleOutline />
        </Text>
        Add User
      </Button>

      <Modal isOpen={isOpen} onClose={onClose}>
        <ModalOverlay />
        <ModalContent borderRadius="23px" maxW="40%">
          <ModalHeader
            bgColor="#2D3748"
            color="white"
            textAlign="center"
            py={2}
            position="relative"
            borderRadius="20px 20px 0 0"
            style={{ marginBottom: "20px" }}
          >
            Add a new user
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
            <Flex direction="column" alignItems="stretch">
              <tbody>
                <tr>
                  <Text as="div" fontWeight="600" py={3} pr={3} ml={2}>
                    User Type
                  </Text>
                  <td>
                    <Select
                      id="roleId"
                      value={roleId}
                      shadow="lg"
                      style={{
                        boxShadow: "0 2px 4px rgba(0, 0, 0, 0.4)",
                        color: "#1c1c1c",
                        fontStyle: "italic",
                      }}
                      onChange={(e) => {
                        setRoleId(e.target.value);
                        setRoleIdError("");
                      }}
                    >
                      <option disabled hidden value="">
                        Select one
                      </option>
                      {roles.map((role) => (
                        <option key={role.roleId} value={role.roleId}>
                          {role.roleName === "Admin"
                            ? "Class Admin"
                            : role.roleName}
                        </option>
                      ))}
                    </Select>
                    {roleIdError && (
                      <span
                        style={{
                          fontStyle: "italic",
                          fontSize: "0.8em",
                          color: "red",
                        }}
                      >
                        {roleIdError}
                      </span>
                    )}
                  </td>
                </tr>

                <Flex justify="space-between" mb={3}>
                  {/* Fullname */}
                  <Box flex="1" ml={2}>
                    <Text fontWeight="600" py={3} pr={3}>
                      Full Name
                    </Text>
                    <Input
                      placeholder="Full name"
                      _placeholder={{
                        fontStyle: "italic",
                        color: "#949494",
                        fontSize: "0.9em",
                      }}
                      value={fullName}
                      pl={3}
                      borderColor="black"
                      onChange={(e) => {
                        setFullName(e.target.value);
                        if (!e.target.value) {
                          setFullNameError("This field is required");
                        } else if (
                          !/^[a-zA-ZÀ-Ỹà-ỹ\s]+$/.test(e.target.value)
                        ) {
                          setFullNameError(
                            "Full name should not contain special characters"
                          );
                        } else {
                          setFullNameError("");
                        }
                      }}
                    />
                    {fullNameError && (
                      <span
                        style={{
                          fontStyle: "italic",
                          fontSize: "0.8em",
                          color: "red",
                        }}
                      >
                        {fullNameError}
                      </span>
                    )}
                  </Box>

                  {/* Email */}
                  <Box flex="1" ml={3}>
                    <Text fontWeight="600" py={3} pr={3}>
                      Email
                    </Text>
                    <Input
                      type="     "
                      placeholder="Email address"
                      _placeholder={{
                        fontStyle: "italic",
                        color: "#949494",
                        fontSize: "0.9em",
                      }}
                      value={email}
                      pl={3}
                      borderColor="black"
                      onChange={(e) => {
                        setEmail(e.target.value);
                        if (!e.target.value) {
                          setEmailError("This field is required");
                        } else {
                          const emailPattern = /^[a-zA-Z0-9_.+-]+@gmail\.com$/;
                          if (!emailPattern.test(e.target.value)) {
                            setEmailError("Please enter a valid email address");
                          } else {
                            setEmailError("");
                          }
                        }
                      }}
                    />
                    {emailError && (
                      <span
                        style={{
                          fontStyle: "italic",
                          fontSize: "0.8em",
                          color: "red",
                        }}
                      >
                        {emailError}
                      </span>
                    )}
                  </Box>
                </Flex>

                <Flex justify="space-between" mb={3}>
                  {/* Username */}
                  <Box flex="1" ml={2}>
                    <Text fontWeight="600" py={3} pr={3}>
                      Username
                    </Text>
                    <Input
                      placeholder="User name"
                      _placeholder={{
                        fontStyle: "italic",
                        color: "#949494",
                        fontSize: "0.9em",
                      }}
                      value={username}
                      pl={3}
                      borderColor="black"
                      onChange={(e) => {
                        setUserName(e.target.value);
                        setUserNameError("");
                      }}
                    />
                    {usernameError && (
                      <span
                        style={{
                          fontStyle: "italic",
                          fontSize: "0.8em",
                          color: "red",
                        }}
                      >
                        {usernameError}
                      </span>
                    )}
                  </Box>

                  {/* Password */}
                  <Box flex="1" ml={3}>
                    <Text fontWeight="600" py={3} pr={3}>
                      Password
                    </Text>
                    <InputGroup size="md">
                      <Input
                        pr="4.5rem"
                        type={showPassword ? "text" : "password"}
                        placeholder="Password"
                        value={password}
                        onChange={handlePasswordChange}
                        borderColor="black"
                      />
                      <InputRightElement width="4.5rem">
                        <Button
                          h="1.75rem"
                          size="sm"
                          onClick={toggleShowPassword}
                        >
                          {showPassword ? <ViewOffIcon /> : <ViewIcon />}
                        </Button>
                      </InputRightElement>
                    </InputGroup>
                    {passwordError && (
                      <Text color="red" mt={2} fontSize="sm" fontStyle="italic">
                        {passwordError}
                      </Text>
                    )}
                  </Box>
                </Flex>

                <Flex justify="space-between" mb={3} ml={2}>
                  {/* Phone */}
                  <Box flex="1">
                    <Text fontWeight="600" py={3} pr={3}>
                      Phone Number
                    </Text>
                    <Input
                      placeholder="Phone number"
                      _placeholder={{
                        fontStyle: "italic",
                        color: "#949494",
                        fontSize: "0.9em",
                      }}
                      value={formatPhoneNumber(phone)}
                      borderColor="black"
                      onChange={(e) => {
                        const formattedInput = formatPhoneNumber(
                          e.target.value
                        );
                        setPhone(formattedInput.replace(/\s+/g, ""));
                        if (!e.target.value) {
                          setPhoneError("This field is required");
                        } else if (!/^[0-9]+$/.test(e.target.value)) {
                          setPhoneError(
                            "Phone number must contain only digits"
                          );
                        } else if (e.target.value.charAt(0) !== "0") {
                          setPhoneError("Phone number must start with 0");
                        } else if (e.target.value.length !== 10) {
                          setPhoneError(
                            "Please enter a valid 10-digit phone number"
                          );
                        } else {
                          setPhoneError("");
                        }
                      }}
                    />
                    {phoneError && (
                      <span
                        style={{
                          fontStyle: "italic",
                          fontSize: "0.8em",
                          color: "red",
                        }}
                      >
                        {phoneError}
                      </span>
                    )}
                  </Box>

                  {/* Address */}
                  <Box flex="1" ml={3}>
                    <Text fontWeight="600" py={3} pr={3}>
                      Address
                    </Text>
                    <Input
                      placeholder="Address"
                      _placeholder={{
                        fontStyle: "italic",
                        color: "#949494",
                        fontSize: "0.9em",
                      }}
                      value={address}
                      borderColor="black"
                      onChange={(e) => {
                        setAddress(e.target.value);
                        setAddressError("");
                      }}
                    />
                    {addressError && (
                      <span
                        style={{
                          fontStyle: "italic",
                          fontSize: "0.8em",
                          color: "red",
                        }}
                      >
                        {addressError}
                      </span>
                    )}
                  </Box>
                </Flex>

                {/* Enter DoB */}
                <Flex justify="space-between" mb={3} ml={2}>
                  <Box flex="1">
                    <Text fontWeight="600" py={3} pr={3}>
                      Date of Birth
                    </Text>
                    <Input
                      type="date"
                      value={dob}
                      borderColor="black"
                      onChange={(e) => {
                        setDob(e.target.value);
                        setDobError("");
                      }}
                    />
                    {dobError && (
                      <span
                        style={{
                          fontStyle: "italic",
                          fontSize: "0.8em",
                          color: "red",
                        }}
                      >
                        {dobError}
                      </span>
                    )}
                  </Box>

                  <Box flex="1" ml={3}>
                    <Text fontWeight="600" py={3} pr={3} mb={2}>
                      Gender
                    </Text>
                    <RadioGroup
                      value={
                        gender === "Male"
                          ? "1"
                          : gender === "Female"
                          ? "2"
                          : undefined
                      }
                      onChange={(e) => {
                        setGender(e === "1" ? "Male" : "Female");
                        setGenderError("");
                      }}
                    >
                      <Stack spacing={5} direction="row" ml={3}>
                        <Radio value="1">
                          <Text fontWeight="600">Male</Text>
                        </Radio>
                        <Radio value="2">
                          <Text fontWeight="600">Female</Text>
                        </Radio>
                      </Stack>
                    </RadioGroup>
                    <Box mt={2}>
                      {" "}
                      {/* Thêm khoảng cách giữa RadioGroup và thông báo lỗi */}
                      {genderError && (
                        <span
                          style={{
                            fontStyle: "italic",
                            fontSize: "0.8em",
                            color: "red",
                          }}
                        >
                          {genderError}
                        </span>
                      )}
                    </Box>
                  </Box>
                </Flex>

                {/* Select status */}
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
                        setStatus(!status);
                      }}
                    />
                  </td>
                </tr>
              </tbody>
            </Flex>
          </ModalBody>

          <ModalFooter display="flex" justifyContent="center">
            <Button
              variant="ghost"
              _hover={{ background: "none" }}
              color="red"
              textDecoration="underline"
              onClick={handleClose}
              py={2}
            >
              Cancel
            </Button>

            <Button
              colorScheme="blue"
              mr={3}
              onClick={handleSave}
              bgColor="#2D3748"
            >
              Save
            </Button>
          </ModalFooter>
        </ModalContent>
      </Modal>
    </>
  );
};

export default AddUserForm;
