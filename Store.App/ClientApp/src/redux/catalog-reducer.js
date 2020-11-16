const ADD_CATEGORY = "ADD_CATEGORY";
const REMOVE_CATEGORY = "REMOVE_CATEGORY";
const CLEAR_CATEGORIES = "CLEAR_CATEGORIES";

const catalogReducer = (state, action) => {
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
    default:
      return state;
  }
};

export const changeCategoryActionCreator = (id, value) =>
  value ? { type: ADD_CATEGORY, id } : { type: REMOVE_CATEGORY, id };

export const clearCategoriesActionCreator = () => ({ type: CLEAR_CATEGORIES });

export default catalogReducer;
