import * as React from "react";
import Button from "@mui/material/Button";
import { styled } from "@mui/material/styles";
import Dialog from "@mui/material/Dialog";
import DialogTitle from "@mui/material/DialogTitle";
import DialogContent from "@mui/material/DialogContent";
import DialogActions from "@mui/material/DialogActions";
import IconButton from "@mui/material/IconButton";
import CloseIcon from "@mui/icons-material/Close";
import Select, { SelectChangeEvent } from "@mui/material/Select";
import style from "./style.module.scss";
import Stack from "@mui/material/Stack";
import MenuItem from "@mui/material/MenuItem";
import DetailedPopup from "./DetailedPopup";
import { toast } from "react-toastify";
import { CircularProgress } from "@mui/material";
import { LoadingButton } from "@mui/lab";
import { FormatBodyToSend } from "./formatbody";
import axios from "../../../../axiosAuth";
import Loading from "../../TrainingProgramManagement/components/Loading/Loading";

const BootstrapDialog = styled(Dialog)(({ theme }) => ({
	"& .MuiDialogContent-root": {
		padding: theme.spacing(3),
	},
	"& .MuiDialogActions-root": {
		padding: theme.spacing(2),
	},
}));

// Grabbing the
// @ts-ignore
const backend_api: string = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;

export const context = React.createContext(null);
export default function SendEmailPopupForm({ open = null, setOpen, title, sendto, studentid }) {
	const [categoryNum, setCategoryNum] = React.useState(null);
	const [apply, setApply] = React.useState("Unknown");
	const handleClose = () => setOpen(null);
	const [select, setSelect] = React.useState("");
	const [templates, setTemplates] = React.useState([]);
	const [load, setLoad] = React.useState(false);
	const [scores, setAllScores] = React.useState({});
	const [currentContent, setcurrentContent] = React.useState<object | null>({});
	const [userinfo, setUserInfo] = React.useState({});

	// Data states go here
	const [subject, setSubject] = React.useState("Unknown");
	const [emailbody, setEmailbody] = React.useState("Unknown");

	React.useEffect(() => {
		if (studentid)
			axios.get(`${backend_api}/api/students/${studentid}`)
				.then((resp) => {
					const data = resp.data;
					setUserInfo(data);
					setAllScores({
						html: data.quizes.html,
						css: data.quizes.css,
						quiz3: data.quizes.quiz3,
						quiz4: data.quizes.quiz4,
						quiz5: data.quizes.quiz5,
						quiz6: data.quizes.quiz6,
						quiz_ave: parseFloat(((data.quizes.html + data.quizes.css + data.quizes.quiz3 + data.quizes.quiz4 + data.quizes.quiz5 + data.quizes.quiz6) / 6).toFixed(2)),
						practice1: data.asm.practice1,
						practice2: data.asm.practice2,
						practice3: data.asm.practice3,
						asm_ave: parseFloat(((data.asm.practice1 + data.asm.practice2 + data.asm.practice3) / 3).toFixed(2)),
						quizfinal: data.quizes.quizfinal,
						audit: data.audit,
						practicefinal: data.asm.practice_final,
						status: data.status !== "Disabled",
						mock: data.mock,
						gpa: data.gpa,
					});
				})
				.catch((e) => {
				});
	}, [studentid]);

	// Grabbing the templates
	React.useEffect(() => {
		axios.get(`${backend_api}/api/emailTemplates/list`).then((resp) => {
			setTemplates(resp.data.filter((e) => e.status === 1));
			setLoad(true);
		});
	}, []);

	// Create a ref to grab the data of the email
	const emailRef = React.useRef(null);

	// Content of the mail
	const content = React.useMemo(() => {
		if (currentContent.isdearname) return `<strong>Dear ${userinfo.fullName}</strong><br />${emailbody}`;
		return emailbody;
	}, [currentContent, emailbody]);

	// Category but in text
	const category = React.useMemo(() => (categoryNum === 1 ? "Reserve" : categoryNum === 2 ? "Remind" : categoryNum === 3 ? "Notice" : categoryNum === 4 ? "Other" : "Unknown"), [categoryNum]);

	const [isLoading, setIsLoading] = React.useState(false);

	// Whenever a user selects a new option
	const selectOption = (e) => {
		setLoad(false);

		if (e.target.value.length === 0) {
			setSelect("");
			setEmailbody("Unknown");
			setSubject("Unknown");
			setApply("Unknown");
			setCategoryNum(null);
			setLoad(true);
			return;
		}

		const event = e.target.value;
		axios.get(`${backend_api}/api/emailsend/preview/${event.temId}`)
			.then((resp) => {
				// add to template
				setLoad(true);
				const data = JSON.parse(resp.data.content);


				setcurrentContent(data);

				setEmailbody(data.body);
				setSubject(data.subject);
				setApply(resp.data.role);
				setCategoryNum(resp.data.receiverType);
			})
			.catch((e) => {
				// Error occur here, probably the record was not yet created
				setLoad(true);
			});

		setSelect(e.target.value);
	};

	// Send email functionality
	const send_email = (f) => {
		axios.post(`${backend_api}/api/Send/email`, {
			lastmsg: "",
			firstmsg: content,
			subject: subject,
			body: currentContent.attendscore && Object.keys(currentContent.attendscore).length > 0 ? scores : null,
			title: title,
			recipient: sendto,
			options: {
				isaudit: currentContent.isaudit,
				isgpa: currentContent.isgpa,
				isfinalstatus: currentContent.finalstatus,
			},
		})
			.then((resp) => {
				toast.success(resp.data.msg);
				f();
			})
			.catch((e) => {
				toast.error("Unable to send email.");
				f();
			});
	};

	return (
		<context.Provider
			value={{
				emailRef: emailRef,
				send: send_email,
				score_options: currentContent,
				userinfo: userinfo,
				content: content,
			}}
		>
			{/* Simple popup */}
			<React.Fragment>
				<BootstrapDialog onClose={handleClose} open={open === 0}>
					<DialogTitle sx={{ m: 0, p: 2 }} className={style.header}>
						Send remind email
					</DialogTitle>
					<IconButton
						aria-label="close"
						onClick={handleClose}
						sx={{
							position: "absolute",
							right: 8,
							top: 8,
							color: (theme) => theme.palette.grey[500],
						}}
					>
						<CloseIcon />
					</IconButton>

					{load && (
						<>
							<DialogContent className={style.cardholder}>
								<div>
									<h1>Categories</h1>
									<label>{category}</label>
								</div>
								<div>
									<h1>Apply to</h1>
									<label>{apply}</label>
								</div>
								<div>
									<h1>Send to</h1>
									<label style={{ fontStyle: "italic" }}>{sendto}</label>
								</div>
								<div>
									<h1>Template name</h1>
									<Select className={style.overwrite_dropdown} value={select} onChange={selectOption} displayEmpty inputProps={{ "aria-label": "Without label" }}>
										<MenuItem value="">
											<em>None</em>
										</MenuItem>

										{templates.map((e, index) => (
											<MenuItem key={index} value={e}>
												{e.name}
											</MenuItem>
										))}
									</Select>
								</div>
							</DialogContent>
							<DialogActions className={style.scaledown}>
								<Stack style={{ width: "100%" }} direction="row" justifyContent="center" alignItems="center">
									<Button style={{ color: "black", border: "1px solid black" }} variant="outlined" onClick={() => setOpen(1)}>
										Preview
									</Button>
									<LoadingButton
										loading={isLoading}
										style={isLoading ? {} : { backgroundColor: "grey" }}
										variant="contained"
										onClick={() => {
											setIsLoading(true);
											send_email(() => {
												setIsLoading(false);
											});
										}}
									>
										Send
									</LoadingButton>
								</Stack>
							</DialogActions>
						</>
					)}
					{!load && (
						<div className={style.loaderdiv}>
							<CircularProgress color="inherit" />
						</div>
					)}
				</BootstrapDialog>
			</React.Fragment>

			{/* Detailed popup */}
			<DetailedPopup to={sendto} template={select} subject={subject} open={open} setOpen={setOpen} lastmsg={""} firstmsg={emailbody} from={"fams.fpt.noreply@gmail.com"} scores={scores} />
		</context.Provider>
	);
}
