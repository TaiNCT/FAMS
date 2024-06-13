import React, { useState, useEffect } from "react";
import axios from "axios";
import { Link } from "react-router-dom";

// @ts-ignore
const api_route: string = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`; // http://localhost:5271

function StudentList() {
	const [students, setStudents] = useState([]);
	const [isLoading, setIsLoading] = useState(false);

	const handleFetchStudents = async () => {
		setIsLoading(true);
		try {
			const response = await axios.get(`${api_route}/api/Students/all`);
			setStudents(response.data);
		} catch (error) {
			console.error("Error fetching students:", error);
		}
		setIsLoading(false);
	};

	return (
		<div>
			<h2>Student List</h2>
			<button onClick={handleFetchStudents}>Fetch Students</button>
			{isLoading && <p>Loading...</p>}
			<ul>
				{students.map((student, index) => (
					<li key={index}>
						ID: {student.id}, Name: {student.fullName}
						<Link to={`/app/${student.id}`}>
							<button>View Details</button>
						</Link>
					</li>
				))}
			</ul>
		</div>
	);
}

export default StudentList;
