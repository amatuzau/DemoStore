import { storeAPI } from "../../../API/store";
import {
  ADD_CATEGORY,
  CLEAR_CATEGORIES,
  GET_PRODUCTS,
  LOAD_CATEGORIES,
  REMOVE_CATEGORY,
  TOGGLE_IS_CATEGORIES_LOADING,
  TOGGLE_IS_LOADING,
} from "./index";

export const changeCategoryActionCreator = (id, value) =>
  value ? { type: ADD_CATEGORY, id } : { type: REMOVE_CATEGORY, id };

export const clearCategoriesActionCreator = () => ({ type: CLEAR_CATEGORIES });

export const getProductsActionCreator = (products) => ({
  type: GET_PRODUCTS,
  products,
});

export const loadCategoriesActionCreator = (categories) => ({
  type: LOAD_CATEGORIES,
  categories,
});

export const toggleIsLoading = () => ({
  type: TOGGLE_IS_LOADING,
});

export const toggleIsCategoriesLoading = () => ({
  type: TOGGLE_IS_CATEGORIES_LOADING,
});

export const getProducts = (filters) => {
  return async (dispatch) => {
    dispatch(toggleIsLoading());

    const data = await storeAPI.loadProducts(filters);
    dispatch(toggleIsLoading());
    dispatch(getProductsActionCreator(data));
  };
};

export const getCategories = () => {
  return async (dispatch) => {
    dispatch(toggleIsCategoriesLoading());

    const data = await storeAPI.loadCategories();
    dispatch(toggleIsCategoriesLoading());
    dispatch(loadCategoriesActionCreator(data));
  };
};
