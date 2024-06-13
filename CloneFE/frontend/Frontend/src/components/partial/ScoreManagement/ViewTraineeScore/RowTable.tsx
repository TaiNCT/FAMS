import TableRow from "@mui/material/TableRow";
import TableCell from "@mui/material/TableCell";
import style from "./style.module.scss";
import { Popup } from "./Popup";
import { background } from "@chakra-ui/react";

function RowTable({ data }) {
  return (
    <TableRow className={style.row}>
      <TableCell
        sx={{
          position: "sticky",
          left: "0",
          zIndex: "10",
          background: "white",
        }}
      >
        {data.fullName}
      </TableCell>
      <TableCell
        sx={{
          position: "sticky",
          left: "210px",
          zIndex: "10",
          background: "white",
        }}
      >
        {data.faaccount}
      </TableCell>
      <TableCell>{data.html ?? "-"}</TableCell>
      <TableCell>{data.css ?? "-"}</TableCell>
      <TableCell>{data.quiz3 ?? "-"}</TableCell>
      <TableCell>{data.quiz4 ?? "-"}</TableCell>
      <TableCell>{data.quiz5 ?? "-"}</TableCell>
      <TableCell>{data.quiz6 ?? "-"}</TableCell>
      <TableCell>{data.ave1}</TableCell>
      <TableCell>{data.practice1 ?? "-"}</TableCell>
      <TableCell>{data.practice2 ?? "-"}</TableCell>
      <TableCell>{data.practice3 ?? "-"}</TableCell>
      <TableCell>{data.ave2}</TableCell>
      <TableCell>{data.quizfinal ?? "-"}</TableCell>
      <TableCell>{data.pracfinal ?? "-"}</TableCell>
      <TableCell>{data.finalmod}</TableCell>
      <TableCell>{data.gpa1 / 10}</TableCell>
      <TableCell>{data.level1 === 0 ? "N/A" : data.level1}</TableCell>
      <TableCell>
        <span className={data.status1 ? style.ok : style.fail}></span>
      </TableCell>
      <TableCell>{data.mock}</TableCell>
      <TableCell>{data.pracfinal2}</TableCell>
      <TableCell>{data.gpa2 / 10}</TableCell>
      <TableCell>{data.level2}</TableCell>
      <TableCell>
        <span className={data.status2 ? style.ok : style.fail}></span>
      </TableCell>
      <TableCell>
        <Popup uuid={data.uuid} />
      </TableCell>
    </TableRow>
  );
}

export { RowTable };
