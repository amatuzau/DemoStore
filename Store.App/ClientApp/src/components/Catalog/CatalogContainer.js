import React from "react";
import { getProducts } from "../../API/products";
import {
  changeCategoryActionCreator,
  clearCategoriesActionCreator,
  getProductsActionCreator,
} from "../../redux/catalog-reducer";
import StoreContext from "../../StoreContext";
import Catalog from "./Catalog";

const CatalogContainer = () => {
  return (
    <StoreContext.Consumer>
      {(store) => {
        const state = store.getState().catalog;

        const loadProducts = () => {
          const { selectedCategories } = state;

          const products = getProducts({ selectedCategories });
          store.dispatch(getProductsActionCreator(products));
        };

        const onCategoryChange = (id, value) => {
          store.dispatch(changeCategoryActionCreator(id, value));
        };

        const clearSelectedCategories = () => {
          store.dispatch(clearCategoriesActionCreator());
        };

        return (
          <Catalog
            products={state.products}
            categories={state.categories}
            selectedCategories={state.selectedCategories}
            onCategoryChange={onCategoryChange}
            clearSelectedCategories={clearSelectedCategories}
            loadProducts={loadProducts}
          />
        );
      }}
    </StoreContext.Consumer>
  );
};

export default CatalogContainer;
