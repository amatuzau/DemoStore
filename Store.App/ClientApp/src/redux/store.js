import { applyMiddleware, combineReducers, compose, createStore } from "redux";
import { loadUser, reducer as oidcReducer } from "redux-oidc";
import thunk from "redux-thunk";
import userManager from "../authorization/userManager";
import cartReducer from './reducers/cart';
import catalogReducer from './reducers/catalog';


const reducers = combineReducers({
  catalog: catalogReducer,
  cart: cartReducer,
  oidc: oidcReducer,
});

const enhancers = compose(
  applyMiddleware(thunk),
  window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
);

const store = createStore(reducers, enhancers);
loadUser(store, userManager);

export default store;
