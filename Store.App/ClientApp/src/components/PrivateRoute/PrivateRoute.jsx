import React from "react";
import { connect } from "react-redux";
import { Route, Redirect } from "react-router-dom";
import { LOGIN_PATH } from "../../constants";

const PrivateRoute = ({ component: Component, auth, role, ...rest }) => (
  <Route
    {...rest}
    render={(props) =>
      !auth.isLoadingUser &&
      auth.user &&
      (role ? auth.user.profile.role === role : true) ? (
        <Component {...props} />
      ) : (
        <Redirect to={LOGIN_PATH} />
      )
    }
  />
);

const mapStateToProps = (state) => ({
  auth: state.oidc,
});

export default connect(mapStateToProps)(PrivateRoute);
