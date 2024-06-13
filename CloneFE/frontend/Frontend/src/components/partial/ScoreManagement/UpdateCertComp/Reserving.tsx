import ControlPointIcon from "@mui/icons-material/ControlPoint";
import style from "./style.module.scss";
import { Link, useNavigate } from "react-router-dom";

function Reserving() {
  return (
    <section className={`${style.body} ${style.overwrite_body}`}>
      <h2>Reserving</h2>
      <div className={style.reserve}>
        <Link to="/addnewreserve">
          <section>
            <ControlPointIcon />
            <label>Add reserving</label>
          </section>
        </Link>
      </div>
    </section>
  );
}

export { Reserving };
