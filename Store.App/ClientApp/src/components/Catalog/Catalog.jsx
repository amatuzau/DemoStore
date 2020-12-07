import React, { Component } from "react";
import { connect } from "react-redux";
import { addItemToCart } from "../../redux/reducers/cart/actions";
import { getProducts } from "../../redux/reducers/catalog/actions";
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

  onAddToCart = (productId) => {
    this.props.addItemToCart(productId);
  };

  render() {
    const {
      categories,
      filters,
      onCategoryChange,
      clearSelectedCategories,
      isLoading,
      products,
    } = this.props;

    return (
      <div className={style.catalog}>
        <div className={style.categories}>
          <Categories
            categories={categories}
            selectedCategories={filters.categories}
            onCategoryChange={onCategoryChange}
            clearSelectedCategories={clearSelectedCategories}
          />
        </div>
        <div className={style.filters}>
          <Filter />
        </div>
        <div className={style.items}>
          {isLoading ? (
            <Preloader />
          ) : (
            <CatalogItems products={products} onAddToCart={this.onAddToCart} />
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
  addItemToCart,
})(Catalog);
