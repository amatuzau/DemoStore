export const GET_CART = "cart/GET_CART";
export const TOGGLE_CART_LOADING = "cart/TOGGLE_CART_LOADING";

const initialState = {
  cartContent: { items: [] },
  isLoading: false,
};

const cartReducer = (state = initialState, action) => {
  switch (action.type) {
    case GET_CART:
      return {
        ...state,
        cartContent: action.cart,
      };
    case TOGGLE_CART_LOADING: {
      return { ...state, isLoading: !state.isLoading };
    }
    default:
      return state;
  }
};

export default cartReducer;

