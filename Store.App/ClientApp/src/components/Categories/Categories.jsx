import React from "react";
import Category from "../Category/Category";

class Categories extends React.Component {
  createItems = () => {
    const { categories, selectedCategories, onCategoryChange } = this.props;

    return categories.map((category) => {
      const selected = selectedCategories.indexOf(category.id) >= 0;
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
        {this.createItems()}
      </div>
    );
  }
}

export default Categories;
