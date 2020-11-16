﻿import React from "react";
import { NavLink } from "react-router-dom";
import { CART_PATH, CATALOG_PATH } from "../../constants";
import style from "./Navigation.module.css";

const Navigation = () => {
  return (
    <nav className={style.navigation}>
      <NavLink to={CATALOG_PATH}>Catalog</NavLink>
      <NavLink to={CART_PATH}>Cart</NavLink>
      <NavLink to="#">Login</NavLink>
    </nav>
  );
};

export default Navigation;