export const ADD_CATEGORY = "ADD_CATEGORY";
export const REMOVE_CATEGORY = "REMOVE_CATEGORY";
export const CLEAR_CATEGORIES = "CLEAR_CATEGORIES";
export const GET_PRODUCTS = "GET_PRODUCTS";
export const TOGGLE_IS_LOADING = "TOGGLE_IS_LOADING";
export const TOGGLE_IS_CATEGORIES_LOADING = "TOGGLE_IS_CATEGORIES_LOADING";
export const LOAD_CATEGORIES = "LOAD_CATEGORIES";

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

export default catalogReducer;
