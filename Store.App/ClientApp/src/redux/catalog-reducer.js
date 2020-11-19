const ADD_CATEGORY = "ADD_CATEGORY";
const REMOVE_CATEGORY = "REMOVE_CATEGORY";
const CLEAR_CATEGORIES = "CLEAR_CATEGORIES";
const GET_PRODUCTS = "GET_PRODUCTS";

const initialState = {
  categories: [
    { id: 1, name: "Electronics" },
    { id: 2, name: "Sport" },
    { id: 3, name: "Appliances" },
  ],
  products: [],
  selectedCategories: [],
};

const catalogReducer = (state = initialState, action) => {
  switch (action.type) {
    case ADD_CATEGORY:
      return {
        ...state,
        selectedCategories: [...state.selectedCategories, action.id],
      };
    case REMOVE_CATEGORY:
      return {
        ...state,
        selectedCategories: state.selectedCategories.filter(
          (i) => i !== action.id
        ),
      };
    case CLEAR_CATEGORIES:
      return {
        ...state,
        selectedCategories: [],
      };
    case GET_PRODUCTS:
      return {
        ...state,
        products: action.products,
      };
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

export default catalogReducer;
