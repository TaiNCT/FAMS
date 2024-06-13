// @ts-nocheck
// import chakra
import { Flex, Card, CardBody, Box, Text, Stack, Accordion } from "@chakra-ui/react";
import { DefaultizedPieValueType } from "@mui/x-charts";
import { PieChart, pieArcLabelClasses } from "@mui/x-charts/PieChart";
import Unit from "./syllabusDays.tsx";
import { SyllabusDay } from "../../types";

// ======= Pie Chart =======
const data = [
	{ label: "Assignment/Lab(54%)", value: 54, color: "#F4BE37" },
	{ label: "Concept/Lecture(29%)", value: 29, color: "#FF9F40" },
	{ label: "Guide/Review(9%)", value: 9, color: "#0D2535" },
	{ label: "Test/Quiz (1%)", value: 1, color: "#5388D8" },
	{ label: "Exam (6%)", value: 6, color: "#206EE5" },
];

const sizing = {
	margin: { right: 5 },
	width: 200,
	height: 200,
	legend: { hidden: true },
};
const TOTAL = data.map((item) => item.value).reduce((a, b) => a + b, 0);

const getArcLabel = (params: DefaultizedPieValueType) => {
	const percent = params.value / TOTAL;
	return `${(percent * 100).toFixed(0)}`;
};

// ======= Syllabus Day =======
const SyllabusOutline = ({ syllabusDays, setSyllabusDays }: { syllabusDays: SyllabusDay[]; setSyllabusDays: (syllabusDays: SyllabusDay[]) => void }) => {
	return (
		<Box fontWeight={500}>
			<Flex gap={3}>
				<Card
					width={"80%"}
					style={{
						outline: "none",
						overflowY: "scroll",
						height: 500,
					}}
				>
					<CardBody style={{ padding: 0 }}>
						<Accordion defaultIndex={[0]} allowMultiple>
							<Unit setSyllabusDays={setSyllabusDays} syllabusDays={syllabusDays} />
						</Accordion>
					</CardBody>
				</Card>
				<Card width={300} style={{ outline: "none" }}>
					<Text
						style={{
							borderTopLeftRadius: "10px",
							borderTopRightRadius: "10px",
							backgroundColor: "#2d3748",
							color: "#fff",
							padding: 10,
						}}
						align={"center"}
						fontWeight={600}
						fontSize={16}
					>
						Time Allocation
					</Text>
					<CardBody>
						<Stack>
							<PieChart
								series={[
									{
										outerRadius: 80,
										data,
										arcLabel: getArcLabel,
									},
								]}
								sx={{
									[`& .${pieArcLabelClasses.root}`]: {
										fill: "white",
										fontSize: 14,
									},
								}}
								{...sizing}
							/>

							{data.map((item) => (
								<Stack direction={"row"} alignItems={"center"}>
									<Box
										style={{
											height: 20,
											width: 20,
											borderRadius: "100%",
											backgroundColor: `${item.color}`,
										}}
									></Box>
									<Text>{item.label}</Text>
								</Stack>
							))}
						</Stack>
					</CardBody>
				</Card>
			</Flex>
		</Box>
	);
};

export default SyllabusOutline;
