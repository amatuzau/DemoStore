import React from "react";
import ReactDOM from "react-dom";
import App from "./App";
import "./index.css";
import reportWebVitals from "./reportWebVitals";

const categories = [
  { id: 1, name: "Electronics" },
  { id: 2, name: "Sport" },
  { id: 3, name: "Appliances" },
];

const image =
  "https://img2.freepng.ru/20180717/ifk/kisspng-smartphone-feature-phone-multimedia-app-mockup-psd-5b4eb748bb7aa7.3479269715318853847679.jpg";

const products = [
  { image, name: "Iphone", price: 500, categoryId: 1 },
  { image, name: "Iphone 2g", price: 600, categoryId: 1 },
  { image, name: "Iphone 3g", price: 700, categoryId: 1 },
  { image, name: "Iphone 4", price: 800, categoryId: 1 },
  { image, name: "Iphone 5", price: 850, categoryId: 1 },
  { image, name: "Iphone 6", price: 860, categoryId: 1 },
  { image, name: "Sport 1", price: 100, categoryId: 2 },
  { image, name: "Sport 2", price: 150, categoryId: 2 },
  { image, name: "Appliance 1", price: 4000, categoryId: 3 },
  { image, name: "Appliance 2", price: 3000, categoryId: 3 },
];

const state = {
  categories,
  products,
  selectedCategories: [],
};

window.state = state;

const updateProductList = () => {
  if (state.selectedCategories.length > 0) {
    state.products = products.filter(
      (p) => state.selectedCategories.indexOf(p.categoryId) >= 0
    );
  } else {
    state.products = products;
  }
};

const onCategoryChange = (id, value) => {
  if (value) {
    state.selectedCategories.push(id);
  } else {
    state.selectedCategories = state.selectedCategories.filter((i) => i !== id);
  }
  updateProductList();

  rerender(state);
};

const clearSelectedCategories = () => {
  state.selectedCategories = [];
  updateProductList();
  rerender(state);
};

const rerender = (state) => {
  ReactDOM.render(
    <React.StrictMode>
      <App
        state={state}
        onCategoryChange={onCategoryChange}
        clearSelectedCategories={clearSelectedCategories}
      />
    </React.StrictMode>,
    document.getElementById("root")
  );
};

rerender(state);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
