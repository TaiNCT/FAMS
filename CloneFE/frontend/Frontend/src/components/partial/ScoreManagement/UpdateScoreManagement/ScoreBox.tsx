import { useContext, useEffect, useState } from "react";
import { DataContext } from "../StudentManagement";
import { Bounce, toast } from "react-toastify";

export type AssignmentScoreType = {
	name: string;
	score?: number;
};

type ScoreBoxProps = {
	scores: AssignmentScoreType;
};

const ScoreBox = ({ scores }: ScoreBoxProps) => {
	const context = useContext(DataContext);
	const [invalidScore, setInvalidScore] = useState(false);

	useEffect(() => {
		context.scoreRef.current[scores.name] = typeof scores.score === "number" ? scores.score : 0;
	}, [scores]);

	const handleScoreChange = (e) => {
		const value = parseFloat(e.target.value);
		context.scoreRef.current[scores.name] = value;
		context.setIsChange(context.ischange + 1);
	};

	return (
		<div className={`p-4 ${invalidScore ? "border-red-200" : "border-slate-400"}`}>
			<p className="mb-3">
				{scores.name} {invalidScore && <span className="text-red-500">*</span>}
			</p>
			<input type="number" className="w-[50px] h-[30px] shadow-[0px_0px_5px_0px_rgba(0,0,0,0.3)] rounded-md text-center font-bold" defaultValue={scores.score} min={0} max={100} onChange={handleScoreChange} />
		</div>
	);
};

export default ScoreBox;
