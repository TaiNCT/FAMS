import React, { useEffect, useState } from "react";
import Collapse from "../../../../../global/Collapse";
import { formatDate } from "../../../../../../utils/DateUtils";
import CalendarTodayIcon from "@/assets/icons/nav-menu-icons/CalendarTodayIcon";
import AlarmIcon from "@/assets/icons/other-icons/AlarmIcon";
import DomainIcon from "@/assets/icons/other-icons/DomainIcon";
import LectureIcon from "@/assets/icons/delivery-types-icons/LectureIcon";
import WarningLogoIcon from "@/assets/icons/indicator-icons/WarningLogoIcon";
import GradeIcon from "@/assets/icons/indicator-icons/GradeIcon";
import SupplierIcon from "@/assets/icons/indicator-icons/SupplierIcon";
import { useParams } from "react-router-dom";
import axios from "axios";
import { useAppSelector } from "@/hooks/useRedux";
import { useContext } from "react";
import { classContext } from "..";

// const data = {
//   classTime: "09:00 - 12:00",
//   locations: ["Ftown2", "Ftown1"],
//   trainers: [
//     {
//       id: 1,
//       name: "Dinh Vu Quoc Trung",
//       profileURL: "https://www.google.com",
//     },
//     {
//       id: 2,
//       name: "Ba Chu Heo",
//       profileURL: "https://www.google.com",
//     },
//     {
//       id: 3,
//       name: "Hu Cheo Ba",
//       profileURL: "https://www.google.com",
//     },
//     {
//       id: 4,
//       name: "Tap The Lop",
//       profileURL: "https://www.google.com",
//     },
//   ],
//   admins: [
//     {
//       id: 1,
//       name: "Ly Lien Lien Dung",
//       profileURL: "https://www.google.com",
//     },
//     {
//       id: 2,
//       name: "Dung Lien Lien Ly",
//       profileURL: "https://www.google.com",
//     },
//   ],
//   FSU: {
//     name: "FHM",
//     contact: "BaCH@fsoft.com.vn",
//   },
//   createdAt: "2022-03-25",
//   createdBy: "DanPL",
//   reviewedAt: "2022-03-30",
//   reviewedBy: "TrungDVQ",
//   approvedAt: "2022-03-02",
//   approvedBy: "VongNT",
// };
function convertTime(time: string) {
	var parts = time.split(":");
	var hours = parts[0];
	var minutes = parts[1];

	return hours + ":" + minutes;
}

const General = ({ ...props }: React.HTMLAttributes<HTMLDivElement>) => {
	// Grabbing the context
	const context = useContext(classContext);
	const [general, setGeneral] = useState<any | null>({});
	const [isLoading, setIsLoading] = useState(false);

	if (isLoading) {
		return <>...Loading</>;
	}
	const data = useAppSelector((state) => state.class.data);
	if (!data) return;
	return (
		<Collapse icon={<CalendarTodayIcon />} title="General" {...props}>
			{/* CLASS TIME */}
			<div className="p-5">
				<div className="grid grid-cols-3">
					<div className="col-span-1  font-bold">
						<div className="flex items-center">
							<AlarmIcon className="mr-2 text-blue-700" /> Class time
						</div>
					</div>
					<div className="col-span-2">
						{convertTime(data.startTime)} - {convertTime(data.endTime)}
					</div>
				</div>
				{/* LOCATION */}
				<div className="grid grid-cols-3 py-3">
					<div className="col-span-1 font-bold">
						<div className="flex items-center">
							<DomainIcon className="mr-2 text-blue-700" /> location
						</div>
					</div>
					<div className="col-span-2 flex flex-col gap-2">
						<div>{data.locationName}</div>
					</div>
				</div>
				{/* TRAINER */}
				<div className="grid grid-cols-3 py-3">
					<div className="col-span-1 font-bold">
						<div className="flex items-center">
							<LectureIcon className="mr-2 text-blue-700" /> Trainer
						</div>
					</div>
					<div className="col-span-2 flex flex-col gap-2">
						{data?.users?.map((trainer: any, index: number) => (
							<div key={`trainer-${index}`} className="flex gap-1">
								{trainer.roleName === "Trainer" && (
									<div className="text-blue-500 flex gap-1 underline">
										{trainer.fullName}
										<WarningLogoIcon className="w-2 text-green-600" />
									</div>
								)}
							</div>
						))}
					</div>
				</div>
				{/* ADMIN */}
				<div className="grid grid-cols-3 py-3">
					<div className="col-span-1 font-bold">
						<div className="flex items-center">
							<GradeIcon className="mr-2 text-blue-700" /> Admin
						</div>
					</div>
					<div className="col-span-2 flex flex-col gap-2">
						{data?.users?.map((admin: any, index: number) => (
							<div key={`admin-${index}`} className="flex gap-1">
								{admin.roleName === "Admin" && (
									<div
										style={{ cursor: "pointer" }}
										className="text-blue-500 flex gap-1 underline"
										onClick={() => {
											context.popup(admin);
										}}
									>
										{admin.fullName}
										<WarningLogoIcon className="w-2 text-green-600" />
									</div>
								)}
							</div>
						))}
					</div>
				</div>
			</div>
			{/* FSU */}
			<div className="p-5">
				<div className="grid grid-cols-3">
					<div className="col-span-1  font-bold">
						<div className="flex items-center">
							<SupplierIcon className="mr-2 text-blue-700" /> FSU
						</div>
					</div>
					<div className="col-span-2 flex flex-col gap-2">
						<div>{data.fsu.name}</div>
						<div>{data.fsu.email}</div>
					</div>
				</div>

				<div className="border-b border-b-black my-3" />
				{/* CREATED */}
				<div className="grid grid-cols-3 my-2">
					<div className="col-span-1  font-bold">Created</div>
					<div className="col-span-2">
						<div>
							{formatDate(new Date(data.createdDate))} by {data.createdBy}
						</div>
					</div>
				</div>
				{/* REVIEW */}
				<div className="grid grid-cols-3 my-2">
					<div className="col-span-1  font-bold">Review</div>
					<div className="col-span-2">
						<div>
							{formatDate(new Date(data.reviewDate))} by {data.reviewBy}
						</div>
					</div>
				</div>
				{/* APPROVE */}
				<div className="grid grid-cols-3 my-2">
					<div className="col-span-1  font-bold">Approve</div>
					<div className="col-span-2">
						<div>
							{formatDate(new Date(data.approvedDate))} by {data.approvedBy}
						</div>
					</div>
				</div>
			</div>
		</Collapse>
	);
};

export default General;
