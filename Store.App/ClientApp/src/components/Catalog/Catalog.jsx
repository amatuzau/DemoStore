import React from "react";
import CatalogItems from "../CatalogItems/CatalogItems";
import Categories from "../Categories/Categories";
import Filter from "../Filter/Filter";
import style from "./Catalog.module.css";

const Catalog = (props) => {
  return (
    <div className={style.catalog}>
      <div className={style.categories}>
        <Categories
          categories={props.categories}
          selectedCategories={props.selectedCategories}
          onCategoryChange={props.onCategoryChange}
          clearSelectedCategories={props.clearSelectedCategories}
        />
      </div>
      <div className={style.filters}>
        <Filter />
      </div>
      <div className={style.items}>
        <CatalogItems products={props.products} />
      </div>
    </div>
  );
};

export default Catalog;
