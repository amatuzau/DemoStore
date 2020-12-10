import { storeAPI } from "../../../API/store";
import { getCart } from "../cart/actions";
import {
  CLOSE_MODAL,
  GET_LOCKED_ORDER,
  GET_NEW_ORDER,
  LOCK_ORDER,
  OPEN_MODAL,
} from "./index";

export const openModal = () => ({
  type: OPEN_MODAL,
});

export const closeModal = () => ({
  type: CLOSE_MODAL,
});

export const getNewOrder = (order) => ({
  type: GET_NEW_ORDER,
  order,
});

export const getLockedOrder = (order) => ({
  type: GET_LOCKED_ORDER,
  order,
});

export const lockOrder = (order) => ({
  type: LOCK_ORDER,
  order,
});

export const sendOrder = (orderDetails) => {
  return async (dispatch) => {
    await storeAPI.sendOrder(orderDetails);
    dispatch(closeModal());
    dispatch(getCart());
  };
};
