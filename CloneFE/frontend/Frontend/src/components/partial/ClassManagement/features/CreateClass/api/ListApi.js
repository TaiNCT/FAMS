import { baseUrl } from "../../../assert/config";
const config = {
  headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
};
export const GetListOfSyllabusByTPCode = async (trainingProgramCode) =>
{
  const responeUrl = `${baseUrl}/api/ExpandClass/GetSyllabiByTrainingProgramCode/${trainingProgramCode}`;
  if (!responeUrl)
  {
    throw new Error(`HTTP error! status: ${responeUrl.status}`);
  }
  const response = await fetch(responeUrl, config);
  const responeJson = await response.json();
  return responeJson.items;
};

export const CreateClassDetail = async (value) =>
{
  try
  {
    const url = `${baseUrl}/api/Class`;
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

export const CreateClassUser = async (classId, userId, userType) =>
{
  try
  {
    const url = `${baseUrl}/api/ExpandClass/CreateclassUser?ClassId=${classId}&UserId=${userId}&UserType=${userType}`;
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
