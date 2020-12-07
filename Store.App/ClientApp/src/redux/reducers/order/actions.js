import { storeAPI } from "../../../API/store";
import { getCart } from "../cart/actions";
import { CLOSE_MODAL, OPEN_MODAL } from "./index";

export const openModal = () => ({
  type: OPEN_MODAL,
});

export const closeModal = () => ({
  type: CLOSE_MODAL,
});

export const sendOrder = (orderDetails) => {
  return async (dispatch) => {
    await storeAPI.sendOrder(orderDetails);
    dispatch(closeModal());
    dispatch(getCart());
  };
};
