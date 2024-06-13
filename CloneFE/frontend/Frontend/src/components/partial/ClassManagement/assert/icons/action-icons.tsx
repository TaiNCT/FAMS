import AddIcon from "../../../../../assets/icons/action-icons/AddIcon";
import ArrowDropDownCircleIcon from "../../../../../assets/icons/action-icons/ArrowDropDownCircleIcon";
import CancelIcon from "../../../../../assets/icons/action-icons/CancelIcon";
import DragIndicatorIcon from "../../../../../assets/icons/action-icons/DragIndicatorIcon";
import FilterListIcon from "../../../../../assets/icons/action-icons/FilterListIcon";
import MoreHorizontalIcon from "../../../../../assets/icons/action-icons/MoreHorizontalIcon";
import PersonAddIcon from "../../../../../assets/icons/action-icons/PersonAddIcon";
import RemoveCircleOutlineIcon from "../../../../../assets/icons/action-icons/RemoveCircleOutlineIcon";
import SortIcon from "../../../../../assets/icons/action-icons/SortIcon";
import ToggleOffIcon from "../../../../../assets/icons/action-icons/ToggleOffIcon";
import VisibilityIcon from "../../../../../assets/icons/action-icons/VisibilityIcon";
import VisibilityOffIcon from "../../../../../assets/icons/action-icons/VisibilityOffIcon";

type ActionIconsPropsType = {
    icon:
    | "add"
    | "arrow-dropdown-circle"
    | "cancel"
    | "drag-indicator"
    | "filter-list"
    | "more-horizontal"
    | "person-add"
    | "remove-circle-outline"
    | "sort"
    | "toggle-off"
    | "visibility"
    | "visibility-off";
};

const ActionIcons = ({
    icon,
    ...props
}: ActionIconsPropsType & React.HTMLAttributes<SVGSVGElement>) => {
    switch (icon) {
        case "add": {
            return <AddIcon {...props} />;
        }
        case "arrow-dropdown-circle": {
            return <ArrowDropDownCircleIcon {...props} />;
        }
        case "cancel": {
            return <CancelIcon {...props} />;
        }
        case "drag-indicator": {
            return <DragIndicatorIcon {...props} />;
        }
        case "filter-list": {
            return <FilterListIcon {...props} />;
        }
        case "more-horizontal": {
            return <MoreHorizontalIcon {...props} />;
        }
        case "person-add": {
            return <PersonAddIcon {...props} />;
        }
        case "remove-circle-outline": {
            return <RemoveCircleOutlineIcon {...props} />;
        }
        case "sort": {
            return <SortIcon {...props} />;
        }
        case "toggle-off": {
            return <ToggleOffIcon {...props} />;
        }
        case "visibility": {
            return <VisibilityIcon {...props} />;
        }
        case "visibility-off": {
            return <VisibilityOffIcon {...props} />;
        }
    }
};

export default ActionIcons;
