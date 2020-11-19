import React from "react";
import style from "./Filter.module.css";

const Filter = () => {
  return (
    <div className={style.filters}>
      <label>
        Sort by
        <select>
          <option defaultValue>Default</option>
          <option>Name</option>
          <option>Price</option>
        </select>
      </label>
      <label>
        Sort order
        <select>
          <option defaultValue>Ascending</option>
          <option>Descending</option>
        </select>
      </label>
      <label>
        Page size
        <select>
          <option>5</option>
          <option>10</option>
          <option>20</option>
        </select>
      </label>
    </div>
  );
};

export default Filter;
