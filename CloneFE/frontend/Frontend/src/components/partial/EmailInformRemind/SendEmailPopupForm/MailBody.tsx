import style from "./style.module.scss";
import { context } from ".";
import { useContext, useMemo } from "react";

export interface IScore {
	// Quiz
	html: number | null;
	css: number | null;
	quiz3: number | null;
	quiz4: number | null;
	quiz5: number | null;
	quiz6: number | null;
	quiz_ave: number | null;

	// ASM
	practice1: number | null;
	practice2: number | null;
	practice3: number | null;
	asm_ave: number | null;

	// what section is this ???
	quizfinal: number | null;
	audit: number | null;
	practicefinal: number | null;
	status: boolean | null; // true = passed, false = failed

	// Mock scores
	mock: number | null;
	gpa: number | null;
}

interface IMailBody {
	// Body parameters
	first_message: string; // the first section of the Body
	scores: IScore | null; // if there are scores, display the HTML part, otherwise do nothing
	last_message: string | null; // The message which will be displayed after the HTML elements
}

export default function MailBody({ first_message, scores = null, last_message }: IMailBody) {
	const ispassedMock = scores?.mock > 50 && scores?.gpa;
	const ispassedFEE = scores?.quiz_ave > 50 && scores?.asm_ave > 50;
	const coreContext = useContext(context);
	const message = coreContext.content;

	return (
		<div ref={coreContext.emailRef} className={style.mailbody}>
			<div dangerouslySetInnerHTML={{ __html: message }} />
			{scores !== null && Object.keys(scores).length > 0 && coreContext.score_options.attendscore && (
				<>
					<section>
						<div>
							<h1>FEE</h1>
							<div className={ispassedFEE ? style.ok : style.error}></div>
						</div>
						<div className={style.mailcard}>
							{/* Quiz card */}
							<div className={style.card}>
								<h1>Quiz</h1>
								<div>
									<label>HTML</label>
									<label>{scores?.html}</label>
								</div>
								<div>
									<label>CSS</label>
									<label>{scores?.css}</label>
								</div>
								<div>
									<label>Quiz 3</label>
									<label>{scores?.quiz3}</label>
								</div>
								<div>
									<label>Quiz 4</label>
									<label>{scores?.quiz4}</label>
								</div>
								<div>
									<label>Quiz 5</label>
									<label>{scores?.quiz5}</label>
								</div>
								<div>
									<label>Quiz 6</label>
									<label>{scores?.quiz6}</label>
								</div>
								<div className={style.ave}>
									<label>Ave.</label>
									<label>{scores?.quiz_ave}</label>
								</div>
							</div>

							{/* ASM card */}
							<div className={style.card}>
								<h1>ASM</h1>
								<div>
									<label>Practice 1</label>
									<label>{scores?.practice1}</label>
								</div>
								<div>
									<label>Practice 2</label>
									<label>{scores?.practice2}</label>
								</div>
								<div>
									<label>Practice 3</label>
									<label>{scores?.practice3}</label>
								</div>
								<div className={style.ave}>
									<label>Ave.</label>
									<label>{scores?.asm_ave}</label>
								</div>
							</div>

							{/* what kind of card is this ? */}
							<div className={style.card}>
								<h1></h1>
								<div>
									<label>Quiz Final</label>
									<label>{scores?.quizfinal}</label>
								</div>
								{coreContext.score_options.isaudit && (
									<div>
										<label>Audit</label>
										<label>{scores?.audit}</label>
									</div>
								)}

								<div>
									<label>Practice Final</label>
									<label>{scores?.practicefinal}</label>
								</div>
								{coreContext.score_options.finalstatus && (
									<div className={style.ave}>
										<label>Status</label>
										<label className={ispassedMock && ispassedFEE && scores?.quizfinal > 50 && scores?.audit > 50 && scores?.practicefinal > 50 ? style.ok : style.error}></label>
									</div>
								)}
							</div>
						</div>
					</section>
					<section>
						<div>
							<h1>Mock</h1>
							<div className={ispassedMock > 50 ? style.ok : style.error}></div>
						</div>
						<div className={style.mailcard}>
							<div className={`${style.card} ${style.mock}`}>
								<h1>MOCK</h1>
								<div>
									<label>MOCK</label>
									<label>{scores?.mock}</label>
								</div>
								{coreContext.score_options.isgpa && (
									<div>
										<label>GPA</label>
										<label>{scores?.gpa}</label>
									</div>
								)}
							</div>
							<div className={style.card}></div>
							<div className={style.card}></div>
						</div>
					</section>
					<br></br>
				</>
			)}
			{last_message}
		</div>
	);
}
