import { useNavigate } from "react-router-dom";
import axios from "axios";
import style from "./style.module.scss";
import React, { useEffect, useState } from "react";
import eye from "../../../assets/LogoStManagement/visibility.png";
import eyeoff from "../../../assets/LogoStManagement/visibilityoff.png";
import { Bounce, toast } from "react-toastify";
import GlobalLoading from "../../global/GlobalLoading.jsx";
import qs from "qs";

const backend_api = `${import.meta.env.VITE_API_HOST}:${
  import.meta.env.VITE_API_PORT
}`;

function Login() {
  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);
  const [values, setValues] = useState({
    username: "",
    password: "",
  });
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const token = localStorage.getItem("token");
    const refreshToken = localStorage.getItem("refreshToken");
    if (token && refreshToken) {
      navigate("/");
    }
  }, [navigate]);

  const handleKeyDown = (e) => {
    if (e.key === "Enter") {
      handleSubmit(e);
    }
  };

  const handleForgotPasswordClick = () => {
    navigate("/recover-password");
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setLoading(true);

    const formData = {
      client_id: "webApp",
      client_secret: "FE_fams",
      username: values.username,
      password: values.password,
      grant_type: "password",
      scope: "FamsApp openid profile offline_access",
    };

    try {
      const response = await axios.post(
        `${backend_api}/connect/token`,
        qs.stringify(formData),
        {
          headers: {
            "Content-Type": "application/x-www-form-urlencoded",
          },
        }
      );

      if (response.data.refresh_token && response.data.access_token) {
        const username = values.username;
        localStorage.setItem("token", response.data.access_token);
        const config = {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        };
        const userInfoResponse = await axios.get(
          `${backend_api}/api/user-info?username=${encodeURIComponent(
            username
          )}`,
          config
        );

        if (!userInfoResponse.data.result.status) {
          toast.error("Your account has been deactivated.", {
            position: "top-right",
            autoClose: 3000,
            hideProgressBar: true,
            closeOnClick: false,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "light",
            transition: Bounce,
          });
          localStorage.clear();
        } else {
          localStorage.setItem("token", response.data.access_token);
          localStorage.setItem("refreshToken", response.data.refresh_token);
          localStorage.setItem("username", values.username);

          // Display success toast
          toast.success("Login success", {
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

          navigate("/");
        }
      } else {
        toast.error("Wrong username or password.", {
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
      if (
        error.response &&
        error.response.data.error_description === "invalid_username_or_password"
      ) {
        toast.error("Wrong username or password.", {
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
        toast.error(
          "Login Failed! Please check your connection and try again.",
          {
            position: "top-right",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
            theme: "light",
            transition: Bounce,
          }
        );
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className={style.background}>
      <GlobalLoading isLoading={loading} />
      <div className={style.login}>
        <div>
          <h4 className={style.title}>FPT Fresh Academy Training Management</h4>
          <p className={style.contact}>
            If you do not have the account, please contact{" "}
            <a href="" className={style.link}>
              FA.HCM@fsoft.com.vn{" "}
            </a>
          </p>
          <br />
          <div className={style.loginblock}>
            <form onSubmit={handleSubmit}>
              <div>
                <input
                  className={style["input-email"]}
                  placeholder="Username"
                  required
                  name="userName"
                  onChange={(e) =>
                    setValues({ ...values, username: e.target.value })
                  }
                ></input>

                <br></br>
                <div className={style["password"]}>
                  <input
                    className={style["input-password"]}
                    type={showPassword ? "text" : "password"}
                    placeholder="Password"
                    required
                    name="password"
                    onChange={(e) =>
                      setValues({ ...values, password: e.target.value })
                    }
                    onKeyDown={handleKeyDown} // Thêm dòng này
                  ></input>
                  <div
                    onClick={() => setShowPassword(!showPassword)}
                    style={{
                      cursor: "pointer",
                      visibility: values.password ? "visible" : "hidden",
                    }}
                  >
                    {showPassword ? (
                      <img src={eye} className={style.img} />
                    ) : (
                      <img src={eyeoff} className={style.img} />
                    )}
                  </div>
                </div>
                <div className={style["fg-password"]}>
                  <button
                    onClick={handleForgotPasswordClick}
                    className={style["forgot-password-button"]}
                  >
                    Forgot password?
                  </button>
                </div>
                <br></br>
                <button className={style["button-login"]} disabled={loading}>
                  {loading ? "Logging in..." : "Sign In"}{" "}
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
}

export { Login };
