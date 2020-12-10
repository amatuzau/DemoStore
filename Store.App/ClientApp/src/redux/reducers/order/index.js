export const OPEN_MODAL = "order/OPEN_MODAL";
export const CLOSE_MODAL = "order/CLOSE_MODAL";
export const GET_NEW_ORDER = "order/GET_NEW_ORDER";
export const GET_LOCKED_ORDER = "order/GET_LOCKED_ORDER";
export const LOCK_ORDER = "order/LOCK_ORDER";

const initialState = {
  isModalOpened: false,
  orders: [],
  lockedOrders: {},
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
    case GET_NEW_ORDER:
      return {
        ...state,
        orders: [...state.orders, action.order],
      };
    case GET_LOCKED_ORDER:
      return {
        ...state,
        lockedOrders: {
          ...state.lockedOrders,
          [action.order.orderId]: action.order.userId,
        },
      };
    default:
      return state;
  }
};

export default orderReducer;
