import React from "react";
import "./App.css";
import Cart from "./components/Cart/Cart";
import Catalog from "./components/Catalog/Catalog";
import Header from "./components/Header/Header";
import {BrowserRouter as Router, Switch, Route, Redirect} from "react-router-dom";
import {CATALOG_PATH, CART_PATH} from "./constants";

const App = () => {
    return (
        <Router>
            <div className="App">
                <Header/>
                <Switch>
                    <Route path="/" exact render={() => {
                        return (
                            <Redirect to={CATALOG_PATH}/>
                        )
                    }}/>
                    <Route path={CATALOG_PATH}><Catalog/></Route>
                    <Route path={CART_PATH}><Cart/></Route>
                </Switch>
            </div>
        </Router>
    );
}

export default App;
