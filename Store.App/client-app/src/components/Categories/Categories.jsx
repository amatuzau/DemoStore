import React from "react";
import style from "./Categories.module.css";

const Categories = () => {
  return (
    <div>
      <div>
        <span>Clear</span>
      </div>
      <div className={style.item}>
        <label>
          <span>Electronics</span>
          <input type="checkbox" />
        </label>
      </div>
      <div className={style.item}>
        <label>
          <span>Sport</span>
          <input type="checkbox" />
        </label>
      </div>
      <div className={style.item}>
        <label>
          <span>Appliances</span>
          <input type="checkbox" />
        </label>
      </div>
    </div>
  );
};

export default Categories;
