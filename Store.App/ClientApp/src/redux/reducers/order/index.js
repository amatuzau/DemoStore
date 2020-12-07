export const OPEN_MODAL = "order/OPEN_MODAL";
export const CLOSE_MODAL = "order/CLOSE_MODAL";

const initialState = {
  isModalOpened: false,
};

const orderReducer = (state = initialState, action) => {
  switch (action.type) {
    case OPEN_MODAL:
      return {
        ...state,
        isModalOpened: true,
      };
    case CLOSE_MODAL:
      return {
        ...state,
        isModalOpened: false,
      };
    default:
      return state;
  }
};

export default orderReducer;
