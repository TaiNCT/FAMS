import { useEffect, useState } from "react";
import { NavLink, useNavigate, useLocation } from "react-router-dom";
import { useGlobalContext } from "../../partial/TrainingProgramManagement/contexts/DataContext";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSpinner } from '@fortawesome/free-solid-svg-icons';

// Importing assets
import home from "../../../../src/assets/public/home.svg";
import group from "../../../../src/assets/public/group.svg";
import syllabus from "../../../../src/assets/public/Syllasbus.svg";
import biotech from "../../../../src/assets/public/biotech.svg";
import school from "../../../../src/assets/public/school.svg";
import calendar from "../../../../src/assets/public/calendar-today.svg";
import role from "../../../../src/assets/public/role.svg";
import folder from "../../../../src/assets/public/folder.svg";
import setting from "../../../../src/assets/public/settings.svg";
import chartfill from "../../../../src/assets/public/pie-chart.png";
import control from "../../../../src/assets/public/control.png";
import navi_close from "../../../../src/assets/public/arrow-left.svg";
import axios from "axios";
import qs from "qs";
import { Bounce, toast } from "react-toastify";
import GlobalLoading from "../../global/GlobalLoading.jsx";

const App = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const [open, setOpen] = useState(true);
  const [isLoading, setIsLoading] = useState(true);
  const { setIsSidebarOpen } = useGlobalContext();
  const backend_api = `${import.meta.env.VITE_API_HOST}:${
    import.meta.env.VITE_API_PORT
  }`;
  const token = localStorage.getItem("token");
  const username = localStorage.getItem("username");
  const [permissions, setPermissions] = useState({
    syllabus: "",
    trainingProgram: "",
    class: "",
    learningMaterial: "",
    userManagement: "",
  });
  const [isDropdownOpen, setIsDropdownOpen] = useState({});
  const [tempDropdownState, setTempDropdownState] = useState({});

  useEffect(() => {
    fetchUserInfo(token, username);
  }, [navigate, location.pathname, username]);

  const fetchUserInfo = async (token, username) => {
    setIsLoading(true); // Start loading when fetch begins
    try {
      const response = await axios.get(
        `${backend_api}/api/user-info?username=${username}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      if (response.data.isSuccess && response.data.result.status) {
        setPermissions(response.data.result.permissions);
        setIsLoading(false); // Set loading to false when data is successfully fetched
      } else {
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
        handleLogout()
      }
    } catch (error) {
      setIsLoading(false); // Ensure loading is set to false on error
      if (error.response && error.response.status === 401) {
        await refreshToken();
      }
    }
  };

  const refreshToken = async () => {
    try {
      const refreshTokenValue = localStorage.getItem("refreshToken");
      const formData = {
        client_id: "webApp",
        client_secret: "FE_fams",
        username: username,
        grant_type: "refresh_token",
        scope: "FamsApp openid profile offline_access",
        refresh_token: refreshTokenValue,
        access_token: token,
      };
      const response = await axios.post(
        `${backend_api}/connect/token`,
        qs.stringify(formData),
        {
          headers: {
            "Content-Type": "application/x-www-form-urlencoded",
          },
        }
      );
      const { access_token, refresh_token } = response.data;
      localStorage.setItem("token", access_token);
      localStorage.setItem("refreshToken", refresh_token);
    } catch (error) {
      handleLogout();
    }
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("refreshToken");
    localStorage.removeItem("username");
    navigate("/login");
  };

  const toggleDropdown = (id) => {
    setIsDropdownOpen((prev) => ({
      ...prev,
      [id]: !prev[id],
    }));
  };

  const handleSidebarToggle = () => {
    if (!open) {
      setIsDropdownOpen(tempDropdownState);
    }
    setOpen((prevOpen) => {
      const newOpenState = !prevOpen;
      setIsSidebarOpen(newOpenState);

      if (!newOpenState) {
        setTempDropdownState(isDropdownOpen);
        setIsDropdownOpen({});
      }
      return newOpenState;
    });
  };

  const Menus = [
    {
      title: "Home",
      path: "/",
      icon: home,
      id: 0,
      src: "/",
    },
    {
      title: "Students",
      path: "",
      icon: group,
      id: 1,
      iconOpened: navi_close,
      subnav: [
        {
          title: "Student list",
          src: "/system-view",
        },
        {
          title: "Reserve list",
          src: "/reservation-management",
        },
      ],
      permissionKey: "class",
    },
    {
      title: "Syllabus",
      path: "",
      icon: syllabus,
      id: 2,
      iconOpened: navi_close,
      subnav: [
        {
          title: "View Syllabus",
          src: "/syllabus",
        },
        {
          title: "Create Syllabus",
          src: "/syllabus/create",
        },
      ],
      permissionKey: "syllabus",
    },
    {
      title: "Training Program",
      path: "",
      icon: biotech,
      id: 3,
      iconOpened: navi_close,
      subnav: [
        {
          title: "View Program",
          src: "/trainingprogram",
        },
        {
          title: "Create Program",
          src: "/trainingprogram/create",
        },
      ],
      permissionKey: "trainingProgram",
    },
    {
      title: "Class",
      path: "",
      icon: school,
      id: 4,
      iconOpened: navi_close,
      subnav: [
        {
          title: "View Class",
          src: "/classList",
        },
        {
          title: "Create Class",
          src: "/createclass",
        },
      ],
      permissionKey: "class",
    },
    {
      title: "Training Calendar",
      path: "/calendar",
      icon: calendar,
      id: 5,
      permissionKey: "class",
    },
    {
      title: "User Management",
      path: "",
      icon: role,
      id: 6,
      iconOpened: navi_close,
      subnav: [
        {
          title: "User List",
          src: "/user-management",
        },
        {
          title: "User Permissions",
          src: "/user-permission",
        },
      ],
      permissionKey: "userManagement",
    },
    {
      title: "Learning Material",
      path: "/learning-material",
      icon: folder,
      id: 7,
      permissionKey: "learningMaterial",
    },
    {
      title: "Setting",
      icon: setting,
      id: 8,
      iconOpened: navi_close,
      subnav: [
        {
          title: "Calendar",
          src: "/user-calendar",
        },
        {
          title: "Email Configuration",
          src: "/EmailConfiguration",
        },
      ],
    },
  ];

  return (
    <div className="flex">
      <GlobalLoading isLoading={isLoading}/>
      <div className="bg-slate-200 ring-offset-blue-950''">
        <div
          className={`${
            open ? "w-72" : "w-20"
          } p-5 pt-8 relative duration-300 bg-slate-200`}
        >
          <img
            src={control}
            alt="control"
            className={`absolute cursor-pointer -right-3 top-9 w-7 border-dark-purple border-2 rounded-full ${
              !open && "rotate-180"
            }`}
            onClick={handleSidebarToggle}
          />
          <div className="flex gap-x-4 items-center">
            <img
              src={chartfill}
              className={`cursor-pointer duration-500 ${
                !open && "rotate-[360deg]"
              }`}
              onClick={() => fetchUserInfo(token, username)}
            />
          </div>
          {isLoading ? (
             <div className="flex justify-center items-center h-full mt-10">
             <FontAwesomeIcon icon={faSpinner} spin size="2x" />
           </div>
          ) : (
            <ul className="pt-6">
              {Menus.map((menu, index) => {
                const menuPermission = menu.permissionKey
                  ? permissions[menu.permissionKey] || "Access Denied"
                  : "Full Access";

                return menuPermission !== "Access Denied" ? (
                  <li key={index} className="mb-2">
                    <NavLink
                      to={menu.path}
                      className={({ isActive }) =>
                        isActive ? "active" : "inactive"
                      }
                      onClick={(e) => {
                        if (menu.subnav && menu.id !== 0) {
                          e.preventDefault();
                          toggleDropdown(menu.id);
                        }
                      }}
                    >
                      <div className="flex items-center">
                        <img src={menu.icon} alt="" className="icon" />
                        <span
                          className={`${
                            !open && "hidden"
                          } origin-left duration-200 flex items-center justify-between w-full`}
                        >
                          {menu.title}
                          {menu.subnav && (
                            <img
                              src={menu.iconOpened}
                              className={`transition-transform ${
                                isDropdownOpen[menu.id]
                                  ? "rotate-180"
                                  : "rotate-0"
                              }`}
                              alt="toggle"
                            />
                          )}
                        </span>
                      </div>
                    </NavLink>
                    {menu.subnav && (
                      <ul
                        className={`pl-12 overflow-hidden transition-max-height duration-100 ease-in-out ${
                          isDropdownOpen[menu.id] ? "max-h-96" : "max-h-0"
                        }`}
                      >
                        {menu.subnav.map((subItem, subIndex) => (
                          <li key={subIndex} className="mt-1 bg-slate-200">
                            <NavLink
                              to={subItem.src}
                              className="block p-2 hover:bg-blue-200 text-sm transition-colors duration-100 ease-in-out"
                            >
                              {subItem.title}
                            </NavLink>
                          </li>
                        ))}
                      </ul>
                    )}
                  </li>
                ) : null;
              })}
            </ul>
          )}
        </div>
      </div>
    </div>
  );
};

export default App;
