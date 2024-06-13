import { baseUrl } from "../../../assert/config";
const config = {
  headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
};

export const ClassListApi = async () =>
{
  const userName = localStorage.getItem("username");
  const responeUrl = `${baseUrl}/api/ExpandClass/GetClassByUserId?userName=${userName}`;
  const response = await fetch(responeUrl, config);
  if (!userName)
  {
    console.error("Username is null");
    return;
  }
  const responseJson = await response.json();
  return responseJson.data.classByUser;
}

export const FsuListApi = async () =>
{
  const responeUrl = `${baseUrl}/api/FSU/GetFSUList`;
  const respone = await fetch(responeUrl, config);
  const responeJson = await respone.json();
  return responeJson.items;
};

export const TrainingProgramListAPI = async () =>
{
  const responeUrl = `${baseUrl}/api/ExpandClass/GetAllTrainer`;
  if (!responeUrl)
  {
    throw new Error(`HTTP error! status: ${responeUrl.status}`);
  }
  const response = await fetch(responeUrl, config);
  const responeJson = await response.json();
  return responeJson.data.userBasicDto;
};

export const TrainingProgramListAPI1 = async () => {
  const responeUrl =
    `${baseUrl}/api/ExpandClass/GetTrainingProgramList`;
  const respone = await fetch(responeUrl,config);
  const responeJson = await respone.json();
  return responeJson.trainingList;
};


export async function getAttendeeNameToIdMapping()
{
  const response = await fetch(
    `${baseUrl}/api/AttendeeType/GetAttendeeTypeList`, config
  );
  const data = await response.json();

  const nameToId = {};
  for (const attendeeType of data.attendeeTypes)
  {
    nameToId[attendeeType.attendeeTypeName] = attendeeType.attendeeTypeId;
  }
  return nameToId;
}

export const GetFsuName = async () =>
{
  const response = await fetch(`${baseUrl}/api/FSU/GetFSUList`, config);
  const data = await response.json();

  const nameToId = {};
  for (const fsu of data.items)
  {
    nameToId[fsu.fsuId] = fsu.name;
  }
  return nameToId;
};

export const LocationList = async () =>
{
  const responeUrl = `${baseUrl}/api/Location/GetLocationList`;
  const respone = await fetch(responeUrl, config);
  const responeJson = await respone.json();
  return responeJson.items;
};

export const GetClassInfo = async (classId) =>
{
  const responeUrl = `${baseUrl}/api/Class/ViewInfoClassDetail?classId=${classId}`;
  const respone = await fetch(responeUrl, config);
  const responeJson = await respone.json();
  return responeJson.data;
};

export const AttendeeTypeList = async () =>
{
  const responeUrl = `${baseUrl}/api/AttendeeType/GetAttendeeTypeList`;
  const respone = await fetch(responeUrl, config);
  const responeJson = await respone.json();
  return responeJson.attendeeTypes;
};

export const GetClassesByWeek = async (startofWeek, endOfWeek) =>
{
  const responeUrl = `${baseUrl}/api/Class/Week?startDate=${startofWeek}&endDate=${endOfWeek}`;
  const response = await fetch(responeUrl, config);
  const responseJson = await response.json();
  return responseJson;
};

export const GetClassInfoWithTrainer = async (classId) =>
{
  const responeUrl = `${baseUrl}/api/Class/ViewInfo?classId=${classId}&userType=Trainer`;
  const respone = await fetch(responeUrl, config);
  const responeJson = await respone.json();
  return responeJson.data;
};

export const GetClassInfoWithAdmin = async (classId) =>
{
  const responeUrl = `${baseUrl}/api/Class/ViewInfo?classId=${classId}&userType=Admin`;
  const respone = await fetch(responeUrl, config);
  const responeJson = await respone.json();
  return responeJson.data;
};

export const GetUserBasic = async () =>
{
  const responeUrl = `${baseUrl}/api/ExpandClass/GetUserBasic`;
  const respone = await fetch(responeUrl, config);
  const responeJson = await respone.json();

  return responeJson.data.userBasicDto;
};

export const DoplicatedClass = async (id) =>
{
  try
  {
    const url = `${baseUrl}/api/Class/DuplicatedClass/${id}`;
    const request = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${localStorage.getItem("token")}`
      },
    };
    const response = await fetch(url, request);
    return response;
  } catch (err)
  {
  }
};

export const GetClass = async () =>
{
  const responeUrl = `${baseUrl}/api/Class`;
  const respone = await fetch(responeUrl, config);
  const responeJson = await respone.json();
  return responeJson.data.items;
};

export const GetClassInfoByCalendar = async (classId) =>
{
  const responeUrl = `${baseUrl}/api/Class/ViewInfo?classId=${classId}`;
  const respone = await fetch(responeUrl, config);
  const responeJson = await respone.json();
  return responeJson.data;
};

