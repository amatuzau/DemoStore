import { applyMiddleware, combineReducers, compose, createStore } from "redux";
import { reducer as oidcReducer, loadUser } from "redux-oidc";
import thunk from "redux-thunk";
import userManager from "../authorization/userManager";
import catalogReducer from "./catalog-reducer";

const reducers = combineReducers({
  catalog: catalogReducer,
  oidc: oidcReducer,
});

const enhancers = compose(
  applyMiddleware(thunk),
  window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
);

const store = createStore(reducers, enhancers);
loadUser(store, userManager);

export default store;
