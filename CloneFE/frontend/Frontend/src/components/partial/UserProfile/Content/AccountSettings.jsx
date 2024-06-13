import {
  Box,
  Button,
  FormControl,
  FormLabel,
  Grid,
  Input,
  Select,
} from "@chakra-ui/react";
import { useState, useEffect } from "react";
import axios from "axios";
import { Bounce, toast } from "react-toastify";
import debounce from "lodash/debounce";

function AccountSettings({ userDetails }) {
  const [user, setUser] = useState(userDetails);
  const [errors, setErrors] = useState({});
  const [emailError, setEmailError] = useState("");
  const [phoneError, setPhoneError] = useState("");

  const backend_api = `${import.meta.env.VITE_API_HOST}:${
    import.meta.env.VITE_API_PORT
  }`;

  const config = {
    headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
  };

  // Debounced check for email uniqueness

  useEffect(() => {
    setUser(userDetails);
  }, [userDetails]);

  const formatPhoneNumber = (value) => {
    const numericValue = value.replace(/\D/g, "");
    const match = numericValue.match(/^(\d{1,4})(\d{1,3})?(\d{1,3})?$/);
    if (match) {
      return `${match[1] || ""} ${match[2] ? match[2] + " " : ""}${
        match[3] || ""
      }`.trim();
    }
    return numericValue;
  };

  const validateAge = (dob) => {
    const currentDate = new Date();
    const birthDate = new Date(dob);
    const age = currentDate.getFullYear() - birthDate.getFullYear();
    const monthDiff = currentDate.getMonth() - birthDate.getMonth();
    if (
      monthDiff < 0 ||
      (monthDiff === 0 && currentDate.getDate() < birthDate.getDate())
    ) {
      return age - 1;
    }
    return age;
  };

  const validateField = (name, value) => {
    switch (name) {
      case "fullName":
        if (!value) {
          return "Full Name is required";
        } else if (!value.match(/^[a-zA-ZÀ-Ỹà-ỹ\s]+$/)) {
          return "Full Name cannot contain special characters";
        } else {
          return "";
        }
      case "email":
        if (!value.match(/^[a-zA-Z0-9_.+-]+@gmail\.com$/)) {
          return "Email format is not valid (Ex: example@gmail.com)";
        } else {
          return "";
        }
      case "phone":
        const plainPhone = value.replace(/\D/g, "");
        if (!plainPhone.match(/^[0-9]+$/) || plainPhone.length !== 10) {
          return "Invalid phone number, must be 10 digits";
        } else {
          return "";
        }
      case "address":
        if (!value) {
          return "Address cannot be null.";
        } else {
          return "";
        }
      case "dob":
        const age = validateAge(user.dob);
        if (age < 18) {
          return "You must be at least 18 years old.";
        } else {
          return "";
        }
      default:
        return "";
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    let formattedValue = value;
    if (name === "email") {
      setEmailError(''); // Xóa lỗi email ngay khi người dùng sửa đổi trường email
    }
    if (name === "phone") {
      formattedValue = formatPhoneNumber(value);
      setPhoneError('');  // Clear phone error on change
    }
    setUser((prev) => ({
      ...prev,
      [name]: name === "phone" ? value.replace(/\s/g, "") : value,
    }));
    const error = validateField(name, value);
    if (error) {
      setErrors((prev) => ({
        ...prev,
        [name]: error,
      }));
    } else {
      setErrors((prev) => ({
        ...prev,
        [name]: "",
      }));
    }
  };

  const handleValidation = () => {
    let formIsValid = true;
    const newErrors = {};

    // Validate all fields
    Object.keys(user).forEach((key) => {
      const error = validateField(key, user[key]);
      if (error) {
        formIsValid = false;
        newErrors[key] = error;
      }
    });

    setErrors(newErrors);
    return formIsValid;
  };

  const handleSubmit = async () => {
    if (handleValidation()) {
      try {
        const token = localStorage.getItem("token");
        const config = {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        };

        const response = await axios.put(
          `${backend_api}/api/update-info`,
          { ...user, username: localStorage.getItem("username") },
          config
        );

        if (response.data.message.includes("Updated successfully")) {
          toast.success("Update information success", {
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
          // toast.error(response.data.message, {
          //   position: "top-right",
          //   autoClose: 3000,
          //   hideProgressBar: false,
          //   closeOnClick: true,
          //   pauseOnHover: true,
          //   draggable: true,
          //   progress: undefined,
          //   theme: "light",
          //   transition: Bounce,
          // });
          if (response.data.message.includes("Email already exists")) {
            setEmailError("This email is already in use");
            formIsValid = false;
          } else {
            setEmailError("");
          }
          if (response.data.message.includes("Phone number already exists")) {
            setPhoneError("This phone number is already in use");
            formIsValid = false;
          } else {
            setPhoneError("");
          }
        }
      } catch (error) {
        toast.success(error.response.data.message, {
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
  };

  return (
    <Grid
      templateColumns={{ base: "repeat(1, 1fr)", md: "repeat(2, 1fr)" }}
      gap={6}
    >
      <FormControl id="fullName" isInvalid={!!errors.fullName}>
        <FormLabel>Full Name</FormLabel>
        <Input
          name="fullName"
          focusBorderColor="brand.blue"
          type="text"
          value={user.fullName || ""}
          placeholder="Enter Fullname"
          onChange={handleChange}
        />
        {errors.fullName && <p style={{ color: "red" }}>{errors.fullName}</p>}
      </FormControl>

      <FormControl id="phoneNumber" isInvalid={!!errors.phone || !!phoneError}>
        <FormLabel>Phone Number</FormLabel>
        <Input
          name="phone"
          focusBorderColor="brand.blue"
          type="tel"
          value={formatPhoneNumber(user.phone || "")}
          placeholder="Enter Phone"
          onChange={handleChange}
        />
          <p style={{ color: "red" }}>{errors.phone || phoneError}</p>
      </FormControl>

      <FormControl id="emailAddress" isInvalid={!!errors.email || !!emailError}>
        <FormLabel>Email Address</FormLabel>
        <Input
          name="email"
          focusBorderColor="brand.blue"
          type="email"
          value={user.email || ""}
          placeholder="Enter Email"
          onChange={handleChange}
        />
        <p style={{ color: "red" }}>{errors.email || emailError}</p>
      </FormControl>

      <FormControl id="Address">
        <FormLabel>Address</FormLabel>
        <Input
          name="address"
          focusBorderColor="brand.blue"
          type="text"
          value={user.address || ""}
          placeholder="Enter Address"
          onChange={handleChange}
        />
        {errors.address && <p style={{ color: "red" }}>{errors.address}</p>}
      </FormControl>

      <FormControl id="dob" isInvalid={!!errors.dob}>
        <FormLabel>Date of Birth</FormLabel>
        <Input
          name="dob"
          focusBorderColor="brand.blue"
          type="date"
          value={user.dob || ""}
          onChange={handleChange}
        />
        {errors.dob && <p style={{ color: "red" }}>{errors.dob}</p>}
      </FormControl>

      <FormControl id="gender">
        <FormLabel>Gender</FormLabel>
        <Select
          name="gender"
          focusBorderColor="brand.blue"
          value={user.gender || ""}
          placeholder="Select gender"
          onChange={handleChange}
        >
          <option value="Male">Male</option>
          <option value="Female">Female</option>
        </Select>
      </FormControl>

      <Box pt={4}>
        <Button colorScheme="blue" onClick={handleSubmit}>
          Update Info
        </Button>
      </Box>
    </Grid>
  );
}

export default AccountSettings;
