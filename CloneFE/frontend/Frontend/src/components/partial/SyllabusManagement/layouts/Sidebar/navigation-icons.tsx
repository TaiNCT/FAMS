import BiotechIcon from "../../../../../assets/icons/nav-menu-icons/BiotechIcon";
import BookOpenIcon from "../../../../../assets/icons/nav-menu-icons/BookOpenIcon";
import CalendarTodayIcon from "../../../../../assets/icons/nav-menu-icons/CalendarTodayIcon";
import CloseChipIcon from "../../../../../assets/icons/nav-menu-icons/CloseChipIcon";
import FolderIcon from "../../../../../assets/icons/nav-menu-icons/FolderIcon";
import GroupIcon from "../../../../../assets/icons/nav-menu-icons/GroupIcon";
import HomeIcon from "../../../../../assets/icons/nav-menu-icons/HomeIcon";
import MenuCloseIcon from "../../../../../assets/icons/nav-menu-icons/MenuCloseIcon";
import MenuOpenIcon from "../../../../../assets/icons/nav-menu-icons/MenuOpenIcon";
import NavCollapseIcon from "../../../../../assets/icons/nav-menu-icons/NavCollapseIcon";
import NavDefaultIcon from "../../../../../assets/icons/nav-menu-icons/NavDefaultIcon";
import SchoolIcon from "../../../../../assets/icons/nav-menu-icons/SchoolIcon";
import SettingsIcon from "../../../../../assets/icons/nav-menu-icons/SettingsIcon";

type NavigationIconsPropsType = {
    icon:
    | "biotech"
    | "book-open"
    | "calendar-today"
    | "close-chip"
    | "folder"
    | "group"
    | "home"
    | "menu-close"
    | "menu-open"
    | "nav-collapse"
    | "nav-default"
    | "school"
    | "settings";
};
const NavigationIcons = ({
    icon,
    ...props
}: NavigationIconsPropsType & React.HTMLAttributes<SVGSVGElement>) => {
    switch (icon) {
        case "biotech": {
            return <BiotechIcon {...props} />;
        }
        case "book-open": {
            return <BookOpenIcon {...props} />;
        }
        case "calendar-today": {
            return <CalendarTodayIcon {...props} />;
        }
        case "close-chip": {
            return <CloseChipIcon {...props} />;
        }
        case "folder": {
            return <FolderIcon {...props} />;
        }
        case "group": {
            return <GroupIcon {...props} />;
        }
        case "home": {
            return <HomeIcon {...props} />;
        }
        case "menu-close": {
            return <MenuCloseIcon {...props} />;
        }
        case "menu-open": {
            return <MenuOpenIcon {...props} />;
        }
        case "nav-collapse": {
            return <NavCollapseIcon {...props} />;
        }
        case "nav-default": {
            return <NavDefaultIcon {...props} />;
        }
        case "school": {
            return <SchoolIcon {...props} />;
        }
        case "settings": {
            return <SettingsIcon {...props} />;
        }
    }
};

export default NavigationIcons;
