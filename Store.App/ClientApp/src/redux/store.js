import { combineReducers, createStore, applyMiddleware, compose } from "redux";
import catalogReducer from "./catalog-reducer";
import thunk from "redux-thunk";

const reducers = combineReducers({
  catalog: catalogReducer,
});

const enhancers = compose(
  applyMiddleware(thunk),
  window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
);

const store = createStore(reducers, enhancers);

export default store;
