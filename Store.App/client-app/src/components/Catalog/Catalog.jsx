import React from "react";
import Categories from "../Categories/Categories";
import Filter from "../Filter/Filter";
import style from "./Catalog.module.css";

const Catalog = () => {
  return (
    <div className={style.catalog}>
      <div className={style.categories}>
        <Categories />
      </div>
      <div className={style.filters}>
        <Filter />
      </div>
      <div className={style.items}>Items</div>
    </div>
  );
};

export default Catalog;
