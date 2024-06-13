// @ts-nocheck
import { Box, Text, Input, Flex, RadioGroup, Radio, Stack, Button } from "@chakra-ui/react";
import React, { FC, useCallback, useEffect, useState } from "react";
import SyllabusList from "../../CreateTrainingProgram/components/SyllabusList";
import style from "./EditProgram.module.scss";
import SearchList from "../../CreateTrainingProgram/components/SearchList";
import { HiMiniMagnifyingGlass } from "react-icons/hi2";
import { Syllabus } from "../../CreateTrainingProgram/models/syllabus.model";
import { updateTrainingProgram } from "../models/updateTrainingProgram.model";
import { isLengthAtLeast, isNumeric } from "../utils/validation";
import { TrainingProgram } from "../../TrainingProgramDetail/models/trainingprogram.model";
// import axiosAuth from "../../api/axiosAuth";
import axiosAuth from "../../api/axiosAuth";

interface EditProps {
	dataUpdate: TrainingProgram | undefined;
	handleAction: (action: string, value?: updateTrainingProgram) => void;
	onCloseUpdateModal: () => void;
}

const EditProgramForm: FC<EditProps> = ({ dataUpdate, handleAction, onCloseUpdateModal }) => {
	const [programName, setProgramName] = useState<string>("");
	const [duration, setDuration] = useState<string>("");
	const [status, setStatus] = useState<string>("");
	const [searchValue, setSearchValue] = useState<string>("");
	const [syllabuses, setSyllabuses] = useState<Syllabus[]>([]);
	const [syllabusListSearch, setSyllabusListSearch] = useState<Syllabus[]>([]);
	const [errorMsg, setErrorMsg] = useState<string | null>(null);
	const [errorMsgName, setErrorMsgName] = useState<string | null>(null);
	const [errorMsgDuration, setErrorMsgDuration] = useState<string | null>(null);

	useEffect(() => {
		if (dataUpdate) {
			setProgramName(dataUpdate.name);
			setDuration(dataUpdate.days.toString());
			setStatus(dataUpdate.status);
			setSyllabuses(dataUpdate.syllabi);
		}
	}, [dataUpdate]);

	const handleGetSyllabusId = (syllabiList: Syllabus[]) => {
		return syllabiList.map((sy) => sy.syllabusId);
	};

	useEffect(() => {
		(async () => {
			try {
				const response = await axiosAuth.get(`trainingprograms/syllabi`);
				setSyllabusListSearch(response.data);
			} catch (error) {
				console.error("There was an error!", error);
			}
		})();
	}, []);

	const handleOnSearch = (syllabus: Syllabus, searchTerm: string) => {
		// check duplicate select item
		const syllabusItem = syllabuses.find((s) => s.id === syllabus.id);
		if (!syllabusItem) {
			// not exist
			// update selected list
			setSyllabuses([syllabus, ...syllabuses]);
			setErrorMsg(null);
		} else {
			setErrorMsg(`Syllabus (${syllabus.topicName}) is already selected`);
		}

		// update searcg result and its value
		setSearchValue(searchTerm);
	};

	const handleDelete = (id: number) => {
		setSyllabuses(syllabuses.filter((syllabus) => syllabus.id !== id));
	};

	const handleSave = () => {
		let isValid = true;

		if (!isLengthAtLeast(programName, 10)) {
			setErrorMsgName("Program name must be at least 10 characters.");
			isValid = false;
		} else {
			setErrorMsgName(null); // Clear error message
		}

		if (!isNumeric(duration)) {
			setErrorMsgDuration("Duration must be a number.");
			isValid = false;
		} else {
			setErrorMsgDuration(null); // Clear error message
		}

		if (!isValid) return;
		const updatedData = {
			trainingProgramCode: dataUpdate?.trainingProgramCode,
			id: dataUpdate?.id,
			name: programName,
			days: parseInt(duration, 10),
			updatedBy: localStorage.getItem("username"),
			status: status,
			syllabiIDs: handleGetSyllabusId(syllabuses),
		};
		handleAction("update", updatedData);
	};

	const handleProgramNameChange = useCallback((e: React.ChangeEvent<HTMLInputElement>) => {
		setProgramName(e.target.value);
		setErrorMsgName(null);
	}, []);

	const handleDurationChange = useCallback((e: React.ChangeEvent<HTMLInputElement>) => {
		setDuration(e.target.value);
		setErrorMsgDuration(null);
	}, []);

	return (
		<div>
			<Box as="table" w="full">
				<tbody>
					<tr>
						<td className={style.tableData}>
							<Text className={style.textField} py={3} pr={3}>
								Program Name
							</Text>
						</td>
						<td className={style.tableData}>
							<Input value={programName} w="full" className={style.inputField} onChange={handleProgramNameChange} />
						</td>
					</tr>
					{errorMsgName && <p className={style.errorMsgText}>{errorMsgName}</p>}
					<tr>
						<td className={style.tableData}>
							<Text className={style.textField} py={3} pr={3}>
								Duration
							</Text>
						</td>
						<td className={style.tableData}>
							<Flex align="center">
								<Input value={duration} w="70px" textAlign="center" className={style.inputField} onChange={handleDurationChange} />
								<Text className={style.textField} ml={2} py={3}>
									days
								</Text>
							</Flex>
						</td>
					</tr>
					{errorMsgDuration && <p className={style.errorMsgText}>{errorMsgDuration}</p>}
					<tr>
						<td className={style.tableData}>
							<Text className={style.textField} py={3} pr={3}>
								Status
							</Text>
						</td>
						<td className={style.tableData}>
							<RadioGroup
								value={status}
								onChange={(newValue) => {
									setStatus(newValue);
								}}
							>
								<Stack spacing={5} direction="row">
									<Radio size="lg" value="Active">
										Active
									</Radio>
									<Radio size="lg" value="InActive">
										InActive
									</Radio>
									<Radio size="lg" value="Draft">
										Draft
									</Radio>
								</Stack>
							</RadioGroup>
						</td>
					</tr>
				</tbody>
			</Box>
			<Flex justifyContent="center" alignItems="center" h="100%">
				<Box maxH="500px" overflowY="auto" w="full">
					{syllabuses && <SyllabusList syllabi={syllabuses} handleDelete={handleDelete} isDetailPage={false} />}
				</Box>
			</Flex>
			<div className={style.content}>
				<div className={style.contentRow}>
					<p>Content</p>
				</div>
				<div className={style.searchPart}>
					<div className={style.searchBox}>
						<label htmlFor="search-syllabus">Search Syllabus</label>
						<input
							id="search-syllabus"
							type="text"
							value={searchValue}
							onChange={(e) => {
								setSearchValue(e.target.value);
								setErrorMsg(null);
							}}
							style={{ paddingLeft: "2rem" }}
						/>
						<HiMiniMagnifyingGlass className={style.manifyingClass} size={24} />
					</div>
					{syllabusListSearch.length ? <SearchList items={syllabusListSearch} handleOnSearch={handleOnSearch} searchValue={searchValue} /> : <></>}
					{errorMsg && (
						<p className={style.errorMsg} style={{ fontWeight: "600", fontSize: "18px", color: "red", marginTop: "1rem" }}>
							{errorMsg}
						</p>
					)}
				</div>
			</div>
			<Box display="flex" justifyContent="center" mt={6}>
				<Button variant="ghost" _hover={{ background: "none" }} color="red" textDecoration="underline" py={2} size="lg" onClick={() => onCloseUpdateModal()}>
					Cancel
				</Button>
				<Button colorScheme="blue" mr={3} bgColor="#2D3748" size="lg" onClick={handleSave}>
					Save
				</Button>
			</Box>
		</div>
	);
};

export default EditProgramForm;
