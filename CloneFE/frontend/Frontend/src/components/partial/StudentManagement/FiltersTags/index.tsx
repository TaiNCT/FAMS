/* eslint-disable @typescript-eslint/no-explicit-any */
import { useSearchParams } from "react-router-dom";
import styles from "./style.module.scss";
import dayjs from "dayjs";
export function ListFilters({
  filters,
  setFilters,
  handleChangePageNumber,
}: {
  filters: any;
  setFilters: any;
  handleChangePageNumber: any;
}) {
  const [search, setSearch] = useSearchParams();

  function handleDeleteFiltersArrays(deteleValue: string, type: string) {
    const newFilters = filters[type].filter(
      (value: string) => value !== deteleValue
    );
    setFilters({
      ...filters,
      [type]: newFilters,
    });

    newFilters.length > 0 ? search.set(type, newFilters) : search.delete(type);

    search.set("pageNumber", "1");
    handleChangePageNumber(1);
    setSearch(search, { replace: true });
  }

  function handleDeleteFilterDob() {
    setFilters({
      ...filters,
      dob: dayjs(null),
    });

    search.delete("dob");
    search.set("pageNumber", "1");
    setSearch(search, { replace: true });
    handleChangePageNumber(1);
  }

  return (
    <section>
      <div className={styles.main}>
        {filters?.gender.map((filter: string) => {
          return (
            <div className={styles.ClassContainer}>
              <p>{filter}</p>
              <button
                onClick={() => handleDeleteFiltersArrays(filter, "gender")}
              >
                x
              </button>
            </div>
          );
        })}
        {filters?.status.map((filter: string) => {
          return (
            <div className={styles.ClassContainer}>
              <p>{filter}</p>
              <button
                onClick={() => handleDeleteFiltersArrays(filter, "status")}
              >
                x
              </button>
            </div>
          );
        })}
        {filters.dob.isValid() && (
          <div className={styles.ClassContainer}>
            <p>{filters.dob.format("MM-DD-YYYY")}</p>
            <button onClick={() => handleDeleteFilterDob()}>x</button>
          </div>
        )}
      </div>
    </section>
  );
}
