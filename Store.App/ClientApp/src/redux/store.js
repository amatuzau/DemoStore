import { combineReducers, createStore } from "redux";
import catalogReducer from "./catalog-reducer";

const reducers = combineReducers({
  catalog: catalogReducer,
});

const store = createStore(
  reducers,
  window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
);

export default store;
