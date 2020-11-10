import React from "react";
import Logo from "../Logo/Logo";
import Navigation from "../Navigation/Navigation";
import style from "./Header.module.css"

const Header = () => {
    return (
        <div className={style.header}>
            <Logo/>
            <Navigation/>
        </div>
    );
}

export default Header;