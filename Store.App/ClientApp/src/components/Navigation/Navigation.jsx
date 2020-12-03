import React from "react";
import { NavLink } from "react-router-dom";
import { CATALOG_PATH } from "../../constants";
import NavigationLogin from "../NavigationLogin/NavigationLogin";
import style from "./Navigation.module.css";

const Navigation = () => {
  return (
    <nav className={style.navigation}>
      <div className={style.linksContainer}>
        <NavLink to={CATALOG_PATH}>Catalog</NavLink>
      </div>
      <div className={style.loginContainer}>
        <NavigationLogin />
      </div>
    </nav>
  );
};

export default Navigation;
