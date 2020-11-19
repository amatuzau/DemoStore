import React from "react";
import {
  BrowserRouter as Router,
  Redirect,
  Route,
  Switch,
} from "react-router-dom";
import "./App.css";
import Cart from "./components/Cart/Cart";
import CatalogContainer from "./components/Catalog/CatalogContainer";
import Header from "./components/Header/Header";
import { CART_PATH, CATALOG_PATH } from "./constants";

const App = (props) => {
  return (
    <Router basename="ClientApp">
      <div className="App">
        <Header />
        <Switch>
          <Route path="/" exact render={() => <Redirect to={CATALOG_PATH} />} />
          <Route path={CATALOG_PATH}>
            <CatalogContainer />
          </Route>
          <Route path={CART_PATH}>
            <Cart />
          </Route>
        </Switch>
      </div>
    </Router>
  );
};

export default App;
