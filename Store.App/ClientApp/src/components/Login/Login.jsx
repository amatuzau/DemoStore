import React, { Component } from "react";
import userManager from "../../authorization/userManager";

class Login extends Component {
  componentDidMount() {
    userManager.signinRedirect();
  }

  render() {
    return <div> Auth in progress...</div>;
  }
}

export default Login;
