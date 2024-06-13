import { baseUrl } from "../../../assert/config";
const config = {
  headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
};
export const GetListSyllabus = async (trainingProgramCode) =>
{
  const responeUrl = `${baseUrl}/api/Class/ListSyllabusRemaining?trainingCode=${trainingProgramCode}`;
  if (!responeUrl)
  {
    throw new Error(`HTTP error! status: ${responeUrl.status}`);
  }
  const response = await fetch(responeUrl,config);
  const responeJson = await response.json();
  return responeJson.data.items;
};

export const CreateProgramSyllabus = async (programCode, syllabusId) =>
{
  const value = { trainingProgramCode: programCode, syllabusId: syllabusId }
  try
  {
    const url = `${baseUrl}/api/Class/AddTrainingProgramSyllabus`;
    const request = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${localStorage.getItem("token")}`
      },
      body: JSON.stringify(value),
    };
    const response = await fetch(url, request);
    return response;
  } catch (err)
  {
  }
};

export const CreateSyllabus = async (values) =>
{
  try
  {
    const url = `${baseUrl}/api/Class/AddSyllabus`;
    const request = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${localStorage.getItem("token")}`
      },
      body: JSON.stringify(values),
    };
    const response = await fetch(url, request);
    return response;
  } catch (err)
  {
  }
};

export const DeleteSyllabusCard = async (programCode, syllabusId) =>
{
  try
  {
    const url = `${baseUrl}/api/Class/${programCode}/${syllabusId}`;
    const request = {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${localStorage.getItem("token")}`
      },
    };
    const response = await fetch(url, request);
    return response;
  }
  catch (err)
  {
  }
}

export const GetTrainer = async () =>
{
  const responeUrl = `${baseUrl}/api/ExpandClass/GetAllTrainer`;
  if (!responeUrl)
  {
    throw new Error(`HTTP error! status: ${responeUrl.status}`);
  }
  const response = await fetch(responeUrl,config);
  const responeJson = await response.json();
  return responeJson.data.userBasicDto;
};