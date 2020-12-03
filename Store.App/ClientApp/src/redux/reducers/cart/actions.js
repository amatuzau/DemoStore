import { loadCart } from "../../../API/store";
import { GET_CART, TOGGLE_CART_LOADING } from "./index";

export const getProductsActionCreator = (cart) => ({
  type: GET_CART,
  cart,
});

export const toggleCartLoading = () => ({
  type: TOGGLE_CART_LOADING,
});

export const getCart = (id) => {
  return async (dispatch) => {
    dispatch(toggleCartLoading());

    const data = await loadCart(id);
    dispatch(toggleCartLoading());
    dispatch(getProductsActionCreator(data));
  };
};
