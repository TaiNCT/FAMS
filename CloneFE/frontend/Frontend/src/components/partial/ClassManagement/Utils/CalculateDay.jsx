export default function CalculateDay(startDate, endDate, currentDay) {
  const startDate1 = new Date(startDate);
  const endDate1 = new Date(endDate);
  const currentDay1 = new Date(currentDay);
  const totalDays =
    Math.floor((endDate1 - startDate1) / (1000 * 60 * 60 * 24)) + 1;
  const daysLeft =
    Math.floor((currentDay1 - startDate1) / (1000 * 60 * 60 * 24)) + 1;
  return `Day ${daysLeft} of ${totalDays}`;
}
