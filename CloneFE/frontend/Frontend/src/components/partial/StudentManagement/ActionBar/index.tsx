import styles from "./style.module.scss";
import searchIcon from "../../../../assets/LogoStManagement/search.png";
import fitlerIcon from "../../../../assets/LogoStManagement/filterlist.png";
import importIcon from "../../../../assets/LogoStManagement/publish.png";
import exportIcon from "../../../../assets/LogoStManagement/download.png";
import addNewIcon from "../../../../assets/LogoStManagement/add.png";
import ActionButton from "../ActionButton";
import Filter from "../Filter";
import { studentApi } from "../../../../config/axios";
import { ListFilters } from "../FiltersTags";
import ImportStudentPopup from "../ImportStudentPopup";
import { useState } from "react";
import { Link, useParams, useSearchParams } from "react-router-dom";
import dayjs from "dayjs";

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export default function ActionBar({
  handleChangePageNumber, // Proper destructuring here
  selectedStudentIds,
  disableCheckAll,
}: {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  handleChangePageNumber: any; // Specify the prop type
  selectedStudentIds: string[];
  disableCheckAll: () => void;
}) {
  const { classId } = useParams();
  const [search, setSearch] = useSearchParams();
  const searchKeyword = search.get("keyword") ?? "";
  const [searchInput, setSearchInput] = useState(searchKeyword);
  const filteredDob = dayjs(search.get("dob"), "YYYY-MM-DD");
  const filteredGender = search.get("gender")?.split(",") ?? [];
  const filteredStatus = search.get("status")?.split(",") ?? [];

  //Get filters value from url
  const [filters, setFilters] = useState({
    gender: filteredGender,
    dob: filteredDob,
    status: filteredStatus,
  });

  function handleSearch(text: string) {
    search.set("pageNumber", "1");
    search.set("keyword", text);
    setSearch(`?${search.toString()}`, { replace: true });
    setSearchInput(text);
    disableCheckAll();
  }

  function HandleClickExport() {
    //Get all filters for export
    const keyword = search.get("keyword");
    const dob = search.get("dob");
    const gender = search.get("gender");
    const status = search.get("status");
    const sortBy = search.get("sortBy");
    const sortOrder = search.get("sortOrder");

    studentApi
      .get("ExportStudentList", {
        params: {
          classId: classId,
          keyword: keyword,
          dob: dob,
          gender: gender,
          status: status,
          sortBy: sortBy,
          sortOrder: sortOrder,
        },
        responseType: "blob",
      })
      .then((res) => {
        const blob = new Blob([res.data], {
          type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        });
        const url = window.URL.createObjectURL(blob);
        const outputFilename = `${Date.now()}.xlsx`;
        const a = document.createElement("a");
        a.style.display = "none";
        a.href = url;
        a.setAttribute("download", outputFilename);
        // the filename you want
        document.body.appendChild(a);
        a.click();
      })
      .catch(function (e) {
        //handle error
      });
  }

  return (
    <div className={styles.actionBar}>
      <div className={styles.actionBar__upper}>
        <div className={styles.frameAttendee1}>
          <div className={styles.searchBar}>
            <img className={styles.searchIcon} alt="" src={searchIcon} />
            <input
              value={searchInput}
              onChange={(e) => handleSearch(e.target.value)}
              className={styles.searchBy}
              placeholder="Search by..."
              type="text"
            />
          </div>
          <Filter
            setFilters={setFilters}
            filters={filters}
            handleChangePageNumber={handleChangePageNumber}
            button={
              <button className={styles.button}>
                <img
                  className={styles.filterListIcon}
                  alt=""
                  src={fitlerIcon}
                />
                Fiter
              </button>
            }
            disableCheckAll={disableCheckAll}
          />
        </div>
        <div className={styles.filtersGroup}>
          <ImportStudentPopup
            classId={classId}
            button={
              <button className={styles.button1}>
                <img className={styles.publishIcon} alt="" src={importIcon} />
                <div className={styles.filter1}>Import</div>
              </button>
            }
          />
          <button onClick={HandleClickExport} className={styles.button2}>
            <img className={styles.downloadIcon} alt="" src={exportIcon} />
            <div className={styles.filter2}>Export</div>
          </button>
          <Link to={`/add-student/${classId}`}>
            <button className={styles.button3}>
              <img className={styles.addIcon} alt="" src={addNewIcon} />
              <div className={styles.filter3}>Add new</div>
            </button>
          </Link>
        </div>
      </div>
      <div className={styles.actionBar__lower}>
        <ActionButton selectedStudentIds={selectedStudentIds} />
        <ListFilters
          filters={filters}
          setFilters={setFilters}
          handleChangePageNumber={handleChangePageNumber}
        />
      </div>
    </div>
  );
}
