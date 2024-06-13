import { NavLink } from "react-router-dom";
import styles from "./style.module.scss";

export default function SyllabusTab() {
  return (
    <nav className={styles.syllabusNav}>
      <ul className={styles.syllabusTab}>
        <li>
          <NavLink className={styles.syllabusTab__item} to={"/test1"}>
            Training Program
          </NavLink>
        </li>
        <li>
          <NavLink className={styles.syllabusTab__item} to={"/studentinclass"}>
            Student list
          </NavLink>
        </li>
        <li>
          <NavLink className={styles.syllabusTab__item} to={"/test3"}>
            Budget
          </NavLink>
        </li>
        <li>
          <NavLink className={styles.syllabusTab__item} to={"/test4"}>
            Scores
          </NavLink>
        </li>
        <li>
          <NavLink className={styles.syllabusTab__item} to={"/"}>
            Activities logs
          </NavLink>
        </li>
      </ul>
    </nav>
  );
}
