import { loadProducts } from "../API/store";
import { loadCategories } from "../API/store";

const ADD_CATEGORY = "ADD_CATEGORY";
const REMOVE_CATEGORY = "REMOVE_CATEGORY";
const CLEAR_CATEGORIES = "CLEAR_CATEGORIES";
const GET_PRODUCTS = "GET_PRODUCTS";
const TOGGLE_IS_LOADING = "TOGGLE_IS_LOADING";
const TOGGLE_IS_CATEGORIES_LOADING = "TOGGLE_IS_CATEGORIES_LOADING";
const LOAD_CATEGORIES = "LOAD_CATEGORIES";

const initialState = {
  categories: [],
  products: [],
  filters: {
    categories: [],
  },
  isLoading: false,
  isCategoriesLoading: false,
};

const catalogReducer = (state = initialState, action) => {
  switch (action.type) {
    case ADD_CATEGORY:
      return {
        ...state,
        filters: {
          ...state.filters,
          categories: [...state.filters.categories, action.id],
        },
      };
    case REMOVE_CATEGORY:
      return {
        ...state,
        filters: {
          ...state.filters,
          categories: state.filters.categories.filter((i) => i !== action.id),
        },
      };
    case CLEAR_CATEGORIES:
      return {
        ...state,
        filters: {
          ...state.filters,
          categories: [],
        },
      };
    case GET_PRODUCTS:
      return {
        ...state,
        products: action.products,
      };
    case TOGGLE_IS_LOADING: {
      return { ...state, isLoading: !state.isLoading };
    }
    case TOGGLE_IS_CATEGORIES_LOADING: {
      return { ...state, isCategoriesLoading: !state.isCategoriesLoading };
    }
    case LOAD_CATEGORIES: {
      return { ...state, categories: action.categories };
    }
    default:
      return state;
  }
};

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

export default catalogReducer;

export const getProducts = (filters) => {
  return (dispatch) => {
    dispatch(toggleIsLoading());
    loadProducts(filters).then((data) => {
      dispatch(toggleIsLoading());
      dispatch(getProductsActionCreator(data));
    });
  };
};

export const getCategories = () => {
  return (dispatch) => {
    dispatch(toggleIsCategoriesLoading());
    loadCategories().then((data) => {
      dispatch(toggleIsCategoriesLoading());
      dispatch(loadCategoriesActionCreator(data));
    });
  };
};
