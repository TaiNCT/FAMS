import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import style from "./style.module.scss";
import { toast, Bounce } from "react-toastify";
import { ViewIcon, ViewOffIcon } from "@chakra-ui/icons";
import { Input, InputGroup, InputRightElement, Button } from "@chakra-ui/react";

const backend_api = `${import.meta.env.VITE_API_HOST}:${
  import.meta.env.VITE_API_PORT
}`;

function RecoverPassword() {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [code, setCode] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [passwordError, setPasswordError] = useState("");
  const [step, setStep] = useState(1);
  const [loading, setLoading] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const toggleShowPassword = () => setShowPassword(!showPassword);
  const [cooldown, setCooldown] = useState(0);
  const fams_token = localStorage.getItem("fams_token");

  useEffect(() => {
    const token = localStorage.getItem("token");
    const refreshToken = localStorage.getItem("refreshToken");
    if (token && refreshToken) {
      navigate("/");
    }
  }, [navigate]);

  useEffect(() => {
    let interval = null;
    if (cooldown > 0) {
      interval = setInterval(() => {
        setCooldown(cooldown - 1);
      }, 1000);
    }
    return () => clearInterval(interval);
  }, [cooldown]);

  const validatePassword = (password) => {
    const regex =
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/;
    return regex.test(password);
  };

  const handleNewPasswordChange = (e) => {
    const newPass = e.target.value;
    setNewPassword(newPass);
    if (!validatePassword(newPass)) {
      setPasswordError(
        "Password must be at least 8 characters, include at least one uppercase letter, one lowercase letter, one number, and one special character."
      );
    } else {
      setPasswordError("");
    }
  };

  const handleConfirmPasswordChange = (e) => {
    const confirmPass = e.target.value;
    setConfirmPassword(confirmPass);
    if (newPassword !== confirmPass) {
      setPasswordError("Confirm password does not match.");
    } else if (!passwordError) {
      setPasswordError("");
    }
  };

  const handleReturnLoginClick = () => {
    navigate("/login");
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);

    try {
      if (step === 1) {
        const response = await axios.post(`${backend_api}/api/send-code`, {
          email,
        });
        toast.success("Recovery code sent! Please check your email.", {
          position: "top-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
          theme: "light",
          transition: Bounce,
        });
        localStorage.setItem("fams_token", response.data.fams_token);
        setStep(2);
      } else if (step === 2) {
        await axios.post(`${backend_api}/api/verify-code`, {
          email,
          code,
          Token: fams_token,
        });
        setStep(3);
      } else if (step === 3) {
        await axios.put(`${backend_api}/api/recover-pass`, {
          email,
          password: newPassword,
          confirmPassword: confirmPassword,
          Token: fams_token,
        });
        toast.success("Password changed successfully!", {
          position: "top-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
          theme: "light",
          transition: Bounce,
        });
        localStorage.removeItem("fams_token");
        navigate("/login");
      }
    } catch (error) {
      toast.error(error.response?.data?.message || "An error occurred.", {
        position: "top-right",
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "light",
        transition: Bounce,
      });
    } finally {
      setLoading(false);
    }
  };

  const resendCode = async () => {
    if (cooldown === 0) {
      setCooldown(60);
      try {
        const response = await axios.post(`${backend_api}/api/send-code`, {
          email,
        });
        toast.success("Recovery code resent. Please check your email.", {
          position: "top-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
          theme: "light",
          transition: Bounce,
        });
        localStorage.setItem("fams_token", response.data.fams_token);
      } catch (error) {
        toast.error("Failed to resend code.", {
          position: "top-right",
          autoClose: 5000,
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

  const renderStep = () => {
    switch (step) {
      case 1:
        return (
          <>
            <input
              className={style["input-email"]}
              type="email"
              placeholder="Email"
              required
              onChange={(e) => setEmail(e.target.value)}
            />
            <br />
            <br />
          </>
        );
      case 2:
        return (
          <>
            <div className={style["input-resend-container"]}>
              <input
                className={style["input-code"]}
                type="text"
                placeholder="Enter your code"
                required
                onChange={(e) => setCode(e.target.value)}
              />
              <input
                className={style["button-resend"]}
                disabled={cooldown > 0}
                onClick={resendCode}
                value={cooldown > 0 ? `Wait ${cooldown}s` : "Resend Code"}
              />
            </div>
          </>
        );
      case 3:
        return (
          <>
            <InputGroup size="md">
              <Input
                className={style["input-new-password"]}
                pr="4.5rem"
                type={showPassword ? "text" : "password"}
                placeholder="New Password"
                required
                onChange={handleNewPasswordChange}
              />
              <InputRightElement width="4.5rem">
                <Button h="4rem" size="sm" onClick={toggleShowPassword}>
                  {showPassword ? <ViewOffIcon /> : <ViewIcon />}
                </Button>
              </InputRightElement>
            </InputGroup>
            <InputGroup size="md">
              <Input
                className={style["input-confirm-password"]}
                pr="4.5rem"
                type={showPassword ? "text" : "password"}
                placeholder="Confirm New Password"
                required
                onChange={handleConfirmPasswordChange}
              />
              <InputRightElement width="4.5rem">
                <Button h="4rem" size="sm" onClick={toggleShowPassword}>
                  {showPassword ? <ViewOffIcon /> : <ViewIcon />}
                </Button>
              </InputRightElement>
            </InputGroup>
            {passwordError && (
              <p className={style["password-error"]}>{passwordError}</p>
            )}
          </>
        );

      default:
        return null;
    }
  };

  return (
    <div className={style.background}>
      <div className={style.recover}>
        <div>
          <h4 className={style.title}>Password Recovery</h4>
          <p className={style.contact}>
            {step === 1 && "Enter your email to reset your password."}
            {step === 2 && "Enter the code sent to your email."}
            {step === 3 && "Enter your new password."}
          </p>
          <br />
          <div className={style.recoverblock}>
            <form onSubmit={handleSubmit}>
              {renderStep()}
              <button className={style["button-resetpass"]} disabled={loading}>
                {loading
                  ? "Processing..."
                  : step === 3
                  ? "Change Password"
                  : "Submit"}
              </button>
              <div className={style["rt-login"]}>
                <button onClick={handleReturnLoginClick}>
                  Return to Login
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
}

export { RecoverPassword };
