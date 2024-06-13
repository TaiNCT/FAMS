// @ts-nocheck
import ScoreForm from "./ScoreForm";
import style from "./style.module.scss";
import { createContext, useContext, useMemo, useRef, useState } from "react";
import { DataContext } from "../StudentManagement";

export const mockContext = createContext();

const UpdateScoreManagement = ({ edit }) => {
	const context = useContext(DataContext);

	const [aveAsm, setAveAsm] = useState(0);
	const [aveQuiz, setAveQuiz] = useState(0);

	const quiz = useMemo(() => {
		return [
			{
				name: "HTML",
				score: context.data.quizes?.html,
			},
			{
				name: "CSS",
				score: context.data.quizes?.css,
			},
			{
				name: "Quiz 3",
				score: context.data.quizes?.quiz3,
			},
			{
				name: "Quiz 4",
				score: context.data.quizes?.quiz4,
			},
			{
				name: "Quiz 5",
				score: context.data.quizes?.quiz5,
			},
			{
				name: "Quiz 6",
				score: context.data.quizes?.quiz6,
			},
		];
	}, [context.data]);
	const asm = useMemo(() => {
		return [
			{
				name: "Pratice 1",
				score: context.data.asm?.practice1,
			},
			{
				name: "Pratice 2",
				score: context.data.asm?.practice2,
			},
			{
				name: "Pratice 3",
				score: context.data.asm?.practice3,
			},
		];
	}, [context.data]);

	const Mocks = useMemo(() => {
		return [
			{
				name: "MOCK",
				score: context.data?.mock,
			},
			{
				name: "Final",
				score: context.data.quizes?.quizfinal,
			},
			{
				name: "GPA",
				score: context.data?.gpa,
			},
		];
	}, [context.data]);

	// Calculate fee status based on averages
	const quizAvg = quiz.reduce((sum, score) => sum + (score.score || 0), 0) / quiz.length;
	const asmAvg = asm.reduce((sum, score) => sum + (score.score || 0), 0) / asm.length;
	const feeStatus = quizAvg >= 50 && asmAvg >= 50 ? "Passed" : "Failed";

	const MockStatus = (context.data?.mock + context.data.quizes?.quizfinal) / 2 >= 50 && context.data?.gpa >= 60 ? "Passed" : "Failed";

	return (
		<mockContext.Provider
			value={{
				ave_asm: useRef(0),
				ave_quiz: useRef(0),
				aveAsm: aveAsm,
				setAveAsm: setAveAsm,
				aveQuiz: aveQuiz,
				setAveQuiz: setAveQuiz,
			}}
		>
			<div className={edit ? "" : style.forbidedit}>
				<article className="mb-5">
					<div className="flex gap-2">
						<p className="font-bold uppercase">Fee</p>
						<div className="flex items-center">
							<span className={`text-xs ${feeStatus === "Passed" ? "bg-green-600" : "bg-red-600"} text-white px-2 py-1 rounded-full`}>{feeStatus}</span>
						</div>
					</div>
					<div className="w-100% overflow-auto flex p-4 gap-8">
						<ScoreForm title="Quiz" hasAverage assignmentScores={quiz} />
						<ScoreForm title="ASM" assignmentScores={asm} hasAverage />
					</div>
				</article>
				<article>
					<div className="flex gap-2">
						<p className="font-bold uppercase">Mock</p>
						<div className="flex items-center">
							<span className={`text-xs ${MockStatus === "Passed" ? "bg-green-600" : "bg-red-600"} text-white px-2 py-1 rounded-full`}>{MockStatus}</span>
						</div>
					</div>
					<div className="w-100% overflow-auto flex p-4 gap-8">
						<ScoreForm title="MOCK" assignmentScores={Mocks} color="secondary" />
					</div>
				</article>
			</div>
		</mockContext.Provider>
	);
};

export { UpdateScoreManagement };
