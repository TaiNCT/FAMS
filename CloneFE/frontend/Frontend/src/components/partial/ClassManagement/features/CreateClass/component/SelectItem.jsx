import React from "react";
import style from "../../../assert/css/SelectInClass.module.scss";
function SelectItem({ setSelectItem, selectItem })
{
  return (
    <>
      <div className={style.container}>
        <div
          className={
            selectItem == "Training Program" ? style.active : style.items
          }
          onClick={() => setSelectItem("Training Program")}
        >
          Training Program
        </div>
        <div
          className={selectItem == "Attendee list" ? style.active : style.items}
        // onClick={() => setSelectItem("Attendee list")}
        >
          Attendee list
        </div>
        <div
          className={selectItem == "Budget" ? style.active : style.items}
          //onClick={() => setSelectItem("Budget")}
        >
          Budget
        </div>
        <div
          className={selectItem == "Others" ? style.active : style.items}
          //onClick={() => setSelectItem("Others")}
        >
          Others
        </div>
      </div>
    </>
  );
}

export default SelectItem;
