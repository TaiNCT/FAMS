import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from "@radix-ui/react-tooltip";
import { Avatar, AvatarFallback, AvatarImage } from "../../../../uis/avatar";
import { Badge } from "../../../../uis/badge";
import ActionIcons from "../../../../assert/icons/action-icons";
import { formatDate } from "../../../../../../../utils/DateUtils";
import { Button } from "@mui/material";
import avatarIMG from "../../../../assert/img/profile.jpg";

type Types = {
    id: number;
    name: string;
    profileURL: string;
};
type PropTypes = {
    syllabusName: string;
    syllabusStatus: string;
    syllabusShortName: string;
    duration: {
        days: number;
        hours: number;
    };
    createdAt: Date;
    createdBy: string;
    onButtonClick: () => void;
};

const ListCardTwo = ({ syllabusName,
    syllabusStatus,
    syllabusShortName,
    duration,
    createdAt,
    createdBy,
    onButtonClick }: PropTypes) => {

    return (
        <div className="flex shadow-[0px_0px_15px_rgba(0,0,0,0.3)] rounded-3xl overflow-hidden my-4 w-full text-nowrap">
            <div className="bg-gray-800 w-1/5 px-8 py-4 gap-2 items-center justify-center flex">
                <TooltipProvider>
                    <Tooltip>
                        <TooltipTrigger>
                            <Avatar className="h-16 w-16">
                                <AvatarImage src={avatarIMG} alt="My Avatar" />
                                <AvatarFallback>CN</AvatarFallback>
                            </Avatar>
                        </TooltipTrigger>
                    </Tooltip>
                </TooltipProvider>
            </div>

            <div
                className={`p-5 w-full ${syllabusStatus === "Inactive" ? "opacity-50" : ""}`}
            >
                <div className="flex gap-5 items-center mb-3 relative">
                    <a
                        className="text-3xl font-semibold text-primary tracking-widest"
                    >
                        {syllabusName}
                    </a>
                    <Badge className={`h-6 w-18 py-3 px-4 ${syllabusStatus === "Inactive" ? "bg-gray-800" : "bg-green-500"
                        } text-white`}>{syllabusStatus}</Badge> 
                    <Button className="top-0 right-0 absolute" onClick={onButtonClick}>
                        <ActionIcons icon="cancel" />
                    </Button>
                </div>
                <div className="flex items-center">
                    <div>{syllabusShortName}</div>
                    <div className="border border-primary mx-4 h-4" />
                    <div>
                        {duration.days} days{" "}
                        <span className="italic">({duration.hours} hours)</span>
                    </div>
                    <div className="border border-primary mx-4 h-4" />
                    <div>
                        on <span className="italic">{formatDate(createdAt)}</span> by{" "}
                        {createdBy}
                    </div>
                </div>
            </div>
        </div>
    );
}


export default ListCardTwo