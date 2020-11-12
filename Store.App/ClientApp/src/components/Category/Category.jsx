import React from "react";
import style from "../Categories/Categories.module.css";

const Category = (props) => {
  const handleChange = (e) => {
    props.onCategoryChange(props.id, e.target.checked);
  };

  return (
    <div className={style.item}>
      <label>
        <span>{props.name}</span>
        <input
          type="checkbox"
          onChange={handleChange}
          checked={props.selected}
        />
      </label>
    </div>
  );
};

export default Category;
