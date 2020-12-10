import { applyMiddleware, combineReducers, createStore } from "redux";
import { composeWithDevTools } from "redux-devtools-extension";
import { reducer as formReducer } from "redux-form";
import { loadUser, reducer as oidcReducer } from "redux-oidc";
import thunk from "redux-thunk";
import userManager from "../authorization/userManager";
import cartReducer from "./reducers/cart";
import catalogReducer from "./reducers/catalog";
import orderReducer from "./reducers/order";
import { signalRMiddleware } from "./signalr";

const reducers = combineReducers({
  catalog: catalogReducer,
  cart: cartReducer,
  order: orderReducer,
  oidc: oidcReducer,
  form: formReducer,
});

let enhancers = applyMiddleware(thunk, signalRMiddleware);
enhancers = composeWithDevTools(enhancers);

const store = createStore(reducers, enhancers);
loadUser(store, userManager);

export default store;
