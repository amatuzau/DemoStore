import React, { Component } from "react";
import { connect } from "react-redux";
import {
  changeCategoryActionCreator,
  clearCategoriesActionCreator,
  getCategories,
} from "../../redux/catalog-reducer";
import Category from "../Category/Category";
import Preloader from "../Preloader/Preloader";

class Categories extends Component {
  componentDidMount() {
    this.props.getCategories();
  }

  createItems = () => {
    const { categories, filters, onCategoryChange } = this.props;

    return categories.map((category) => {
      const selected = filters.categories.indexOf(category.id) >= 0;
      return (
        <Category
          id={category.id}
          name={category.name}
          onCategoryChange={onCategoryChange}
          selected={selected}
        />
      );
    });
  };

  handleClear = () => {
    const { clearSelectedCategories } = this.props;
    clearSelectedCategories();
  };

  render() {
    return (
      <div>
        <div>
          <button className=".clear" onClick={this.handleClear}>
            Clear
          </button>
        </div>
        {this.props.isCategoriesLoading ? <Preloader /> : this.createItems()}
      </div>
    );
  }
}

const mapStateToProps = (state) => ({
  categories: state.catalog.categories,
  filters: state.catalog.filters,
  isCategoriesLoading: state.catalog.isCategoriesLoading,
});

export default connect(mapStateToProps, {
  getCategories,
  onCategoryChange: changeCategoryActionCreator,
  clearSelectedCategories: clearCategoriesActionCreator,
})(Categories);
