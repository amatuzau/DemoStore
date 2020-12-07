import { storeAPI } from "../../../API/store";
import { GET_CART, TOGGLE_CART_LOADING } from "./index";

export const getProductsActionCreator = (cart) => ({
  type: GET_CART,
  cart,
});

export const toggleCartLoading = () => ({
  type: TOGGLE_CART_LOADING,
});

export const getCart = () => {
  return async (dispatch, getState) => {
    const { cartId } = getState().oidc.user.profile;
    dispatch(toggleCartLoading());

    const data = await storeAPI.loadCart(cartId);
    dispatch(toggleCartLoading());
    dispatch(getProductsActionCreator(data));
  };
};

export const addItemToCart = (productId) => {
  return async (dispatch, getState) => {
    const state = getState();

    await storeAPI.addToCart(state.oidc.user.profile.cartId, [
      { productId, amount: 1 },
    ]);
  };
};
