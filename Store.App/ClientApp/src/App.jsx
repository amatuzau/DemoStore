import React from "react";
import {
  BrowserRouter as Router,
  Redirect,
  Route,
  Switch,
} from "react-router-dom";
import "./App.css";
import Cart from "./components/Cart/Cart";
import Catalog from "./components/Catalog/Catalog";
import Header from "./components/Header/Header";
import Login from "./components/Login/Login";
import LoginCallback from "./components/LoginCallback/LoginCallback";
import Logout from "./components/Logout/Logout";
import OrderModal from "./components/OrderModal";
import Orders from "./components/Orders";
import PrivateRoute from "./components/PrivateRoute/PrivateRoute";
import { CART_PATH, CATALOG_PATH, LOGIN_PATH, LOGOUT_PATH } from "./constants";

const App = () => {
  return (
    <Router basename="ClientApp">
      <div className="App">
        <Header />
        <Switch>
          <Route path="/" exact render={() => <Redirect to={CATALOG_PATH} />} />
          <Route path={CATALOG_PATH}>
            <Catalog />
          </Route>
          <PrivateRoute path={CART_PATH} component={Cart} />
          <PrivateRoute path={"/orders"} role={"Admin"} component={Orders} />
          <Route path={LOGIN_PATH}>
            <Login />
          </Route>
          <Route path={LOGOUT_PATH} component={Logout} />
          <Route
            path={"/authentication/login-callback"}
            component={LoginCallback}
          />
        </Switch>
      </div>
      <div className={"Modal"}>
        <OrderModal />
      </div>
    </Router>
  );
};

export default App;
