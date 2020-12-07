import { applyMiddleware, combineReducers, compose, createStore } from "redux";
import { loadUser, reducer as oidcReducer } from "redux-oidc";
import { reducer as formReducer } from 'redux-form'
import thunk from "redux-thunk";
import userManager from "../authorization/userManager";
import cartReducer from "./reducers/cart";
import catalogReducer from "./reducers/catalog";
import orderReducer from "./reducers/order";

const reducers = combineReducers({
  catalog: catalogReducer,
  cart: cartReducer,
  order: orderReducer,
  oidc: oidcReducer,
  form: formReducer
});

const enhancers = compose(
  applyMiddleware(thunk),
  window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
);

const store = createStore(reducers, enhancers);
loadUser(store, userManager);

export default store;
