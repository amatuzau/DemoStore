import React, { Component } from "react";
import CatalogItems from "../CatalogItems/CatalogItems";
import Categories from "../Categories/Categories";
import Filter from "../Filter/Filter";
import style from "./Catalog.module.css";

class Catalog extends Component {
  componentDidMount() {
    this.props.loadProducts();
  }

  componentDidUpdate(prevProps, prevState, snapshot) {
    if (prevProps.selectedCategories !== this.props.selectedCategories) {
      this.props.loadProducts();
    }
  }

  render() {
    return (
      <div className={style.catalog}>
        <div className={style.categories}>
          <Categories
            categories={this.props.categories}
            selectedCategories={this.props.selectedCategories}
            onCategoryChange={this.props.onCategoryChange}
            clearSelectedCategories={this.props.clearSelectedCategories}
          />
        </div>
        <div className={style.filters}>
          <Filter />
        </div>
        <div className={style.items}>
          <CatalogItems products={this.props.products} />
        </div>
      </div>
    );
  }
}

export default Catalog;
