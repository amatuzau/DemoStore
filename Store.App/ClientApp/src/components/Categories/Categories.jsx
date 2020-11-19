import React from "react";
import Category from "../Category/Category";

const Categories = (props) => {
  const createItems = () => {
    const { categories, selectedCategories, onCategoryChange } = props;

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

  const handleClear = () => {
    const { clearSelectedCategories } = props;
    clearSelectedCategories();
  };

  return (
    <div>
      <div>
        <button className=".clear" onClick={handleClear}>
          Clear
        </button>
      </div>
      {createItems()}
    </div>
  );
};

export default Categories;
