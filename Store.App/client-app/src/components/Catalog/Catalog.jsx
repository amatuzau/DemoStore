import React from "react";
import style from "./Catalog.module.css";

const Catalog = () => {
  return (
    <div className={style.catalog}>
      <div className={style.categories}>Categories</div>
      <div className={style.filters}>Filters</div>
      <div className={style.items}>Items</div>
    </div>
  );
};

export default Catalog;
