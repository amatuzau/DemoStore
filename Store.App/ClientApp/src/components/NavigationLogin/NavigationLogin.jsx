import React from "react";
import { connect } from "react-redux";
import { NavLink } from "react-router-dom";
import { CART_PATH, LOGIN_PATH, LOGOUT_PATH } from "../../constants";

const NavigationLogin = ({ user }) => {
  return user ? (
    <>
      <NavLink to={CART_PATH}>Cart</NavLink>
      <NavLink to={LOGOUT_PATH}>Logout</NavLink>
    </>
  ) : (
    <NavLink to={LOGIN_PATH}>Login</NavLink>
  );
};

const mapStateToProps = (state) => ({
  user: state.oidc.user,
});

export default connect(mapStateToProps)(NavigationLogin);
