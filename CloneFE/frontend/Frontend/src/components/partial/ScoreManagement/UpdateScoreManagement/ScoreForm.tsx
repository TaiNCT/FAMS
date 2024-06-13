// @ts-nocheck
import { useEffect, useMemo, useState } from "react";
import { DataContext } from "../StudentManagement";
import ScoreBox, { AssignmentScoreType } from "./ScoreBox";
import { useContext } from "react";
import { mockContext } from ".";

type ScoreFormProps = {
	color?: "primary" | "secondary";
	hasAverage?: boolean;
	assignmentScores: AssignmentScoreType[];
	title: string;
};
const ScoreForm = ({ color = "primary", hasAverage = false, assignmentScores, title }: ScoreFormProps) => {
	const context = useContext(DataContext);
	const [avg, setAvg] = useState(0);
	const mockcontext = useContext(mockContext);
	const [forceUpdate, setForceUpdate] = useState(0);

	useEffect(() => {
		let defaultMock = assignmentScores.find((e) => e.name === "MOCK");
		let ave = mockcontext.aveAsm * 0.6 + mockcontext.aveQuiz * 0.4;
		if (defaultMock) {
			defaultMock.score = parseFloat(ave.toFixed(2));
			setForceUpdate(forceUpdate + 1);
		}
	}, [mockcontext.aveAsm, mockcontext.aveQuiz]);

	useEffect(() => {
		if (assignmentScores.filter((e) => e.name === "HTML").length > 0)
			// Average score of Quiz
			mockcontext.setAveQuiz(avg);

		if (assignmentScores.filter((e) => e.name === "Pratice 1").length > 0)
			// Average score of ASM
			mockcontext.setAveAsm(avg);
	}, [avg]);

	useEffect(() => {
		// initialize average score
		let ave = 0;
		const avg_init = assignmentScores.map((e) => e.score);
		avg_init.forEach((e) => (ave += e));
		setAvg(ave / assignmentScores.length);
	}, []);

	useEffect(() => {
		const ret = assignmentScores.map((e) => context.scoreRef.current[e.name]);
		let ave = 0;
		ret.forEach((e) => (ave += e));
		let avg = ave / assignmentScores.length;
		setAvg(isNaN(avg) ? 0 : avg);
	}, [context]);

	return (
		<div className="shadow-[-1px_2px_5px_2px_rgba(0,0,0,0.3)] flex flex-col rounded overflow-hidden flex-shrink-0">
			<p className={`${color === "primary" && "bg-slate-700"} ${color === "secondary" && "bg-orange-600"} text-white p-2 font-normal text-center`}>{title}</p>
			<div className="flex bg-slate-200">
				{assignmentScores.map((score, index) => {
					return <ScoreBox scores={score} key={`score-box-${score.name}-${index}`} />;
				})}
				{hasAverage && (
					<div className="p-4 border border-slate-300 bg-gray-300 font-bold">
						<p className="mb-3">Ave.</p>
						<p className="text-center">{avg.toFixed(2)}</p>
					</div>
				)}
			</div>
		</div>
	);
};

export default ScoreForm;
