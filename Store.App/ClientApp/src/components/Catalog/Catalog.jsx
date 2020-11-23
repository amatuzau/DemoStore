import React, { Component } from "react";
import { connect } from "react-redux";
import { getProducts } from "../../redux/catalog-reducer";
import CatalogItems from "../CatalogItems/CatalogItems";
import Categories from "../Categories/Categories";
import Filter from "../Filter/Filter";
import Preloader from "../Preloader/Preloader";
import style from "./Catalog.module.css";

class Catalog extends Component {
  componentDidMount() {
    this.props.getProducts(this.props.filters);
  }

  componentDidUpdate(prevProps, prevState, snapshot) {
    if (prevProps.filters.categories !== this.props.filters.categories) {
      this.props.getProducts(this.props.filters);
    }
  }

  render() {
    return (
      <div className={style.catalog}>
        <div className={style.categories}>
          <Categories
            categories={this.props.categories}
            selectedCategories={this.props.filters.categories}
            onCategoryChange={this.props.onCategoryChange}
            clearSelectedCategories={this.props.clearSelectedCategories}
          />
        </div>
        <div className={style.filters}>
          <Filter />
        </div>
        <div className={style.items}>
          {this.props.isLoading ? (
            <Preloader />
          ) : (
            <CatalogItems products={this.props.products} />
          )}
        </div>
      </div>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    products: state.catalog.products,
    filters: state.catalog.filters,
    isLoading: state.catalog.isLoading,
  };
};

export default connect(mapStateToProps, {
  getProducts,
})(Catalog);
