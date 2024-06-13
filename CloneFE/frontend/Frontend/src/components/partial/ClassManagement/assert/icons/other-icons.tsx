import AccountBoxIcon from "../../../../../assets/icons/other-icons/AccountBoxIcon";
import AlarmIcon from "../../../../../assets/icons/other-icons/AlarmIcon";
import CallIcon from "../../../../../assets/icons/other-icons/CallIcon";
import ChromeReaderModeIcon from "../../../../../assets/icons/other-icons/ChromeReaderModeIcon";
import DomainIcon from "../../../../../assets/icons/other-icons/DomainIcon";
import FemaleIcon from "../../../../../assets/icons/other-icons/FemaleIcon";
import HelpOutlineIcon from "../../../../../assets/icons/other-icons/HelpOutlineIcon";
import HomeWorkIcon from "../../../../../assets/icons/other-icons/HomeWorkIcon";
import LocalLibraryIcon from "../../../../../assets/icons/other-icons/LocalLibraryIcon";
import MailIcon from "../../../../../assets/icons/other-icons/MailIcon";
import MaleIcon from "../../../../../assets/icons/other-icons/MaleIcon";
import OndemandVideoIcon from "../../../../../assets/icons/other-icons/OndemandVideoIcon";
import RoleIcon from "../../../../../assets/icons/other-icons/RoleIcon";

type OtherIconsPropsType = {
    icon:
    | "account-box"
    | "alarm"
    | "call"
    | "chrome-reader-mode"
    | "domain"
    | "female"
    | "help-outline"
    | "home-work"
    | "local-library"
    | "mail"
    | "male"
    | "ondemand-video"
    | "role";
};
const OtherIcons = ({
    icon,
    ...props
}: OtherIconsPropsType & React.HTMLAttributes<SVGSVGElement>) => {
    switch (icon) {
        case "account-box": {
            return <AccountBoxIcon {...props} />;
        }
        case "alarm": {
            return <AlarmIcon {...props} />;
        }
        case "call": {
            return <CallIcon {...props} />;
        }
        case "chrome-reader-mode": {
            return <ChromeReaderModeIcon {...props} />;
        }
        case "domain": {
            return <DomainIcon {...props} />;
        }
        case "female": {
            return <FemaleIcon {...props} />;
        }
        case "help-outline": {
            return <HelpOutlineIcon {...props} />;
        }
        case "home-work": {
            return <HomeWorkIcon {...props} />;
        }
        case "local-library": {
            return <LocalLibraryIcon {...props} />;
        }
        case "mail": {
            return <MailIcon {...props} />;
        }
        case "male": {
            return <MaleIcon {...props} />;
        }
        case "ondemand-video": {
            return <OndemandVideoIcon {...props} />;
        }
        case "role": {
            return <RoleIcon {...props} />;
        }
    }
};
export default OtherIcons;
