import MenuCloseIcon from "@/assets/icons/nav-menu-icons/MenuCloseIcon";
import HomeIcon from "@/assets/icons/nav-menu-icons/HomeIcon";
import BookOpenIcon from "@/assets/icons/nav-menu-icons/BookOpenIcon";
import { Link } from "react-router-dom";
import CollapseMenu, { CollapseItemType } from "./CollapseMenu";
import BiotechIcon from "@/assets/icons/nav-menu-icons/BiotechIcon";
import { useState } from "react";
import MenuOpenIcon from "@/assets/icons/nav-menu-icons/MenuOpenIcon";
import SchoolIcon from "@/assets/icons/nav-menu-icons/SchoolIcon";
import CalendarTodayIcon from "@/assets/icons/nav-menu-icons/CalendarTodayIcon";
import GroupIcon from "@/assets/icons/nav-menu-icons/GroupIcon";
import FolderIcon from "@/assets/icons/nav-menu-icons/FolderIcon";
import SettingsIcon from "@/assets/icons/nav-menu-icons/SettingsIcon";

const SyllabusItems: CollapseItemType[] = [
  {
    title: "View syllabus",
    href: "/view-syllabus",
  },
  {
    title: "Create syllabus",
    href: "/create-syllabus",
  },
];
const TrainingProgramItems: CollapseItemType[] = [
  {
    title: "View program",
    href: "/view-program",
  },
  {
    title: "Create program",
    href: "/create-program",
  },
];
const ClassItems: CollapseItemType[] = [
  {
    title: "View class",
    href: "/view-class",
  },
  {
    title: "Create class",
    href: "/create-class",
  },
];
const UserManagementItems: CollapseItemType[] = [
  {
    title: "User list",
    href: "/user-list",
  },
  {
    title: "User permission",
    href: "/user-permission",
  },
];
const SettingItems: CollapseItemType[] = [
  {
    title: "Calendar",
    href: "/setting-calendar",
  },
];
const SideBar = () => {
  const [isOpen, setIsOpen] = useState(true);
  const handleToggleMenu = () => {
    setIsOpen(!isOpen);
  };
  return (
    <div className="bg-[#EDF2F7] py-5 text-[#285D9A] flex flex-col gap-5  px-4">
      <div className="text-end">
        <button
          onClick={handleToggleMenu}
          className={`${isOpen && "rotate-180"} duration-200`}
        >
          {isOpen ? <MenuCloseIcon /> : <MenuOpenIcon />}
        </button>
      </div>
      <Link className="flex gap-4" to="/">
        <HomeIcon />
        {isOpen && <span>Home</span>}
      </Link>
      <CollapseMenu
        title="Syllabus"
        icon={<BookOpenIcon />}
        items={SyllabusItems}
        isOpen={isOpen}
      />
      <CollapseMenu
        title="Traning Program"
        icon={<BiotechIcon />}
        items={TrainingProgramItems}
        isOpen={isOpen}
      />
      <CollapseMenu
        title="Class"
        icon={<SchoolIcon />}
        items={ClassItems}
        isOpen={isOpen}
      />
      <Link className="flex gap-4" to="/training-calendar">
        <CalendarTodayIcon />
        {isOpen && <span>Training Calendar</span>}
      </Link>
      <CollapseMenu
        title="User management"
        icon={<GroupIcon />}
        items={UserManagementItems}
        isOpen={isOpen}
      />
      <Link className="flex gap-4" to="/learning-material">
        <FolderIcon />
        {isOpen && <span>Learning material</span>}
      </Link>
      <CollapseMenu
        title="Setting"
        icon={<SettingsIcon />}
        items={SettingItems}
        isOpen={isOpen}
      />
    </div>
  );
};

export default SideBar;
