import NavigationIcons from "./navigation-icons";
import { MenuItem } from "./menuItems";
import { useState } from "react";
import MenuButton from "./menu-collapse-button";
import MenuItemGroup from "./menu-item-group";
import { useAppDispatch, useAppSelector } from "./useRedux";
import { toggleSidebar } from "./sidebarSlice";

const menuItems: MenuItem[] = [
  {
    icon: <NavigationIcons icon="home" />,
    label: "Home",
    link: "/",
  },
  {
    icon: <NavigationIcons icon="book-open" />,
    label: "Syllabus",
    link: "/syllabus",
    children: [
      {
        label: "View Syllabus",
        link: "/syllabus",
      },
      {
        label: "Create Syllabus",
        link: "/syllabus/create",
      },
    ],
  },
  {
    icon: <NavigationIcons icon="biotech" />,
    label: "Training program",
    link: "/trainingprogram",
    children: [
      {
        label: "View program",
        link: "/trainingprogram",
      },
      {
        label: "Create program",
        link: "/",
      },
    ],
  },
  {
    icon: <NavigationIcons icon="school" />,
    label: "Class",
    link: "/classList",
    children: [
      {
        label: "View class",
        link: "/classList",
      },
      {
        label: "Create class",
        link: "/createclass",
      },
    ],
  },
  {
    icon: <NavigationIcons icon="calendar-today" />,
    label: "Training calendar",
    link: "/calendar",
  },
  {
    icon: <NavigationIcons icon="group" />,
    label: "User management",
    link: "/user-management",
    children: [
      {
        label: "User list",
        link: "/user-management",
      },
      {
        label: "User permission",
        link: "/",
      },
    ],
  },
  {
    icon: <NavigationIcons icon="folder" />,
    label: "Learning materials",
    link: "/learning-materials",
  },
  {
    icon: <NavigationIcons icon="settings" />,
    label: "Settings",
    link: "/settings",
    children: [
      {
        label: "Calendar",
        link: "/settings",
      },
    ],
  },
];

export default function SyllabusSideBar() {
  //const isOpen = true;
  const { isOpen } = useAppSelector((state) => state.sidebar);
  const dispatch = useAppDispatch();

  return (
    <div
      className={` 
      ${isOpen ? "min-w-[256px]" : ""
        } h-full bg-gray-100 px-5 py-[30px] space-y-4 transition-all delay-150 duration-300 overflow-y-auto no-scrollbar z-30
      `}
    >
      <MenuButton isOpen={isOpen} onClick={() => dispatch(toggleSidebar())} />
      {menuItems.map((menuItem, index) => (
        <MenuItemGroup menuItem={menuItem} key={index} />
      ))}
    </div>
  );
}
