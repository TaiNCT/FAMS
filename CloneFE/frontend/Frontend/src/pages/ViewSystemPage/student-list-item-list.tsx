// @ts-nocheck
import { useEffect, useState } from "react";
import StudentListItem from "./student-list-item";
import React from "react";
import { StudentInfoDTO } from "model/student.ts";
import { studentApi } from "../../config/axios";

interface StudentListItemListProps {
	uri: string;
	displayedColumns: string[];
	onCheckboxChange: (studentId: string, checked: boolean) => void;
	selectedIds: string[];
}

const StudentListItemList: React.FC<StudentListItemListProps> = ({ uri, displayedColumns, onCheckboxChange, selectedIds }) => {
	const [StudentList, setStudentList] = useState<StudentInfoDTO[]>([]);
	const fetchBlogData = async () => {
		try {
			const response = await studentApi.get(uri);
			setStudentList(response.data.result.students);
		} catch (error) {
			console.error("Error fetching student data:", error);
		}
	};
	useEffect(() => {
		fetchBlogData();
	}, [uri]);

	return (
		<>
			{StudentList.map((student: any) => (
				<StudentListItem
					Student={student}
					displayedColumns={displayedColumns}
					onCheckboxChange={onCheckboxChange}
					selectedIds={selectedIds}
				/>
			))}
		</>
	);
};

export default StudentListItemList;
