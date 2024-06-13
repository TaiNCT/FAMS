import { getAttendeeNameToIdMapping } from "../api/ListApi";
import { baseUrl } from "../../../assert/config";
const config = {
    headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
  };
export const fetchFilteredData = async (checkedBox, fsu_id, trainingProgramCode,
    checkedBoxAttendee, selectedLocationIds, slotTimes,
    fromStartDate, toEndDate) =>
{

    // Get the checked parameters
    const parameters = Object.keys(checkedBox).filter((label) => checkedBox[label]);

    let responseUrl = `${baseUrl}/api/Class/Filter?${parameters.map(status => `class_status=${status}`).join('&')}`;


    if (selectedLocationIds)
    {
        responseUrl += selectedLocationIds.map(id => `&Locations_id=${id}`).join('');
    }

    // Add fsu_id to the API call if it is not null
    if (trainingProgramCode)
    {
        responseUrl += `&TrainingProgramCode=${trainingProgramCode}`;
    }

    if (fsu_id)
    {
        responseUrl += `&fsu_id=${fsu_id}`;
    }
    if (checkedBoxAttendee)
    {
        const nameToId = await getAttendeeNameToIdMapping();
        const attendeeLevelIDs = Object.keys(checkedBoxAttendee).filter((label) => checkedBoxAttendee[label]).map(name => nameToId[name]);
        responseUrl += attendeeLevelIDs.map(id => `&attendeeLevelID=${id}`).join('');
    }

    if (slotTimes)
    {
        responseUrl += slotTimes.map(time => `&SlotTimes=${time}`).join('');
    }

    if (fromStartDate && toEndDate)
    {
        responseUrl += `&FromDate=${fromStartDate}`;
        responseUrl += `&ToDate=${toEndDate}`;
    }

    const response = await fetch(responseUrl,config);
    const data = await response.json();

    return data;
};

export const fetchFilteredDataWeek = async (checkedBox, fsu_id, trainingProgramCode,
    checkedBoxAttendee, selectedLocationIds, slotTimes,
    fromStartDate, toEndDate, startOfWeek, endOfWeek) =>
{
    // Get the checked parameters
    const parameters = Object.keys(checkedBox).filter((label) => checkedBox[label]);

    let responseUrl = `${baseUrl}/api/Class/FilterWeek?${parameters.map(status => `class_status=${status}`).join('&')}`;


    if (selectedLocationIds)
    {
        responseUrl += selectedLocationIds.map(id => `&Locations_id=${id}`).join('');
    }

    // Add fsu_id to the API call if it is not null
    if (trainingProgramCode)
    {
        responseUrl += `&TrainingProgramCode=${trainingProgramCode}`;
    }

    if (fsu_id)
    {
        responseUrl += `&fsu_id=${fsu_id}`;
    }
    if (checkedBoxAttendee)
    {
        const nameToId = await getAttendeeNameToIdMapping();
        const attendeeLevelIDs = Object.keys(checkedBoxAttendee).filter((label) => checkedBoxAttendee[label]).map(name => nameToId[name]);
        responseUrl += attendeeLevelIDs.map(id => `&attendeeLevelID=${id}`).join('');
    }

    if (slotTimes)
    {
        responseUrl += slotTimes.map(time => `&SlotTimes=${time}`).join('');
    }

    if (fromStartDate && toEndDate)
    {
        responseUrl += `&FromDate=${fromStartDate}`;
        responseUrl += `&ToDate=${toEndDate}`;
    }

    if (startOfWeek && endOfWeek)
    {
        responseUrl += `&StartOfWeek=${startOfWeek}`;
        responseUrl += `&EndOfWeek=${endOfWeek}`;
    }

    const response = await fetch(responseUrl,config);
    const data = await response.json();

    return data;
};