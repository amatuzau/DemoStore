import React from "react";
import {
  changeCategoryActionCreator,
  clearCategoriesActionCreator,
} from '../../redux/catalog-reducer';
import CatalogItems from "../CatalogItems/CatalogItems";
import Categories from "../Categories/Categories";
import Filter from "../Filter/Filter";
import style from "./Catalog.module.css";

const Catalog = (props) => {

  const onCategoryChange = (id, value) => {
    props.dispatch(changeCategoryActionCreator(id, value));
  }

  const clearSelectedCategories = () => {
    props.dispatch(clearCategoriesActionCreator());
  }

  return (
    <div className={style.catalog}>
      <div className={style.categories}>
        <Categories
          categories={props.state.categories}
          selectedCategories={props.state.selectedCategories}
          onCategoryChange={onCategoryChange}
          clearSelectedCategories={clearSelectedCategories}
        />
      </div>
      <div className={style.filters}>
        <Filter />
      </div>
      <div className={style.items}>
        <CatalogItems products={props.state.products} />
      </div>
    </div>
  );
};

export default Catalog;
