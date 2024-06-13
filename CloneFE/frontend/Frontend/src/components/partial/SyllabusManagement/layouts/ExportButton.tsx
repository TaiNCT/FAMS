import GetAppIcon from '@mui/icons-material/GetApp';
import { useState } from 'react';
import { CSVLink, CSVDownload } from "react-csv";
import { SyllabusList } from '../types';

const ExportButton = ({rows}: {rows: SyllabusList[]}) => {
	const [dataExport, setDataExport] = useState([]);

	const getClassExport = (event, done) => {
		let result = [];
		if (rows) {
			result.push(["Topic name", "Topic Code", "Created On", "Created By", "Duration (days)", "Attendee Number", "Status"]);
			rows.map((item, index) => {
				let arr = [];
				arr[0] = item.topicName
				arr[1] = item.topicCode
				arr[2] = item.createdDate
				arr[3] = item.createdBy
				arr[4] = item.days
				arr[5] = item.attendeeNumber
				arr[6] = item.status
				result.push(arr);
			})
	
			setDataExport(result)
			done();
		}
	}

	return (
		<div>
			<CSVLink filename='syllabusList.csv'
				className='btn btn-primary'
				data={dataExport}
				asyncOnClick={true}
				onClick={getClassExport}
			>
				<GetAppIcon />Export

			</CSVLink>
		</div>
		// 		<CSVLink 
		// 	filename = {"classList.csv"}
		// 	className = {btn btn-primary}
		// 	data={dataExport}
		// 	asyncOnClick = {true}
		// 	onClick = {getClassExport}
		// >
		//     	<GetAppIcon/>Export
		// </CSVLink>
	)

}
export default ExportButton;
