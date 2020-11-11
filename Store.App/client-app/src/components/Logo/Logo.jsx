import React from "react";
import logo from "../../assets/cart.png";
import style from "./Logo.module.css";

const Logo = () => {
  return (
    <div className={style.logo}>
      <img src={logo} alt="" />
    </div>
  );
};

export default Logo;
