import { UpdateCertComp } from "./UpdateCertComp";
import { ViewTraineeScore } from "./ViewTraineeScore";
import { Route } from "react-router-dom";

const ScoreManagement = () => {
	return (
		<ViewTraineeScore></ViewTraineeScore>
		// <UpdateCertComp id="7a3e4b3c-5201-45fc-bb7d-18ab7e26cc50" /> // <Route path="/develop">
		// 	<Route path="view" element={<ViewTraineeScore />} />
		// </Route>
	);
};

export { ScoreManagement };
