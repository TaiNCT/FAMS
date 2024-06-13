const config = {
	headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
};

// @ts-ignore
const backend_api = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;

export const GetReClassDialogInfo = async (reservedClassId) => {
	const responeUrl = `${backend_api}/ReClass/GetReClassDialogInfo/${reservedClassId}`;
	const respone = await fetch(responeUrl, config);
	const responeJson = await respone.json();
	return responeJson;
};

export const GetQuizListStudent = async (reservedClassId) => {
	const responeUrl = `${backend_api}/ReClass/GetQuizListStudent/${reservedClassId}`;
	const respone = await fetch(responeUrl, config);
	const responeJson = await respone.json();
	return responeJson;
};

export const GetAssignmentListStudent = async (reservedClassId) => {
	const responeUrl = `${backend_api}/ReClass/GetAssignmentListStudent/${reservedClassId}`;
	const respone = await fetch(responeUrl, config);
	const responeJson = await respone.json();
	return responeJson;
};

export const GetReClassPossibilies = async (reservedClassId) => {
	const responeUrl = `${backend_api}/ReClass/GetReClassPossibilities/${reservedClassId}`;
	const respone = await fetch(responeUrl, config);
	const responeJson = await respone.json();
	return responeJson;
};

// export const GetMock = async (reservedClassId) =>
// {
// 	const responeUrl = `${backend_api}/ReClass/GetMock/${reservedClassId}`;
// 	const respone = await fetch(responeUrl, config);
// 	const responeJson = await respone.json();
//   return responeJson;
// };

export const GetNextModuleList = async (reservedClassId, classId) => {
	const responeUrl = `${backend_api}/ReClass/GetNextClassModuleList/${reservedClassId}/${classId}`;
	const respone = await fetch(responeUrl, config);
	const responeJson = await respone.json();
	return responeJson;
};
