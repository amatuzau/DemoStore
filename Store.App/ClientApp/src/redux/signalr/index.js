import {
  HubConnectionBuilder,
  HubConnectionState,
  LogLevel,
} from "@microsoft/signalr";
import { USER_FOUND } from "redux-oidc";
import { LOCK_ORDER } from "../reducers/order";
import { getLockedOrder, getNewOrder } from "../reducers/order/actions";

const start = async (connection) => {
  try {
    await connection.start();
    console.log("SignalR Connected.");
  } catch (err) {
    console.log(err);
    setTimeout(this.start, 5000);
  }
};

export const signalRMiddleware = (storeAPI) => {
  const connection = new HubConnectionBuilder()
    .withUrl("/ordershub", {
      accessTokenFactory: () => {
        const state = storeAPI.getState();
        return state.oidc.user && state.oidc.user.access_token;
      },
    })
    .configureLogging(LogLevel.Information)
    .withAutomaticReconnect()
    .build();

  start(connection).then(() => {
    const state = storeAPI.getState();
    const role = state.oidc.user && state.oidc.user.profile.role;
    if (role) {
      joinGroup(connection, role);
    }
  });

  connection.on("GetNewOrder", (order) => {
    storeAPI.dispatch(getNewOrder(order));
  });

  connection.on("GetLockedOrder", (order) => {
    storeAPI.dispatch(getLockedOrder(order));
  });

  return (next) => (action) => {
    // eslint-disable-next-line default-case
    switch (action.type) {
      case LOCK_ORDER:
        connection.invoke("LockOrder", action.order);
        break;
      case USER_FOUND:
        joinGroup(connection, action.payload.profile.role);
        break;
    }
    next(action);
  };
};

const joinGroup = (connection, group) => {
  if (connection.state === HubConnectionState.Connected) {
    try {
      connection.invoke("JoinGroup", group);
    } catch (e) {
      console.error(e);
    }
  }
};
