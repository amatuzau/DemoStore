import React, { Component } from "react";
import { Redirect } from "react-router-dom";
import { CallbackComponent } from "redux-oidc";
import userManager from "../../authorization/userManager";

class LoginCallback extends Component {
  constructor(props) {
    super(props);
    this.state = { redirect: null };
  }

  onSuccess = () => {
    this.setState({ redirect: "/" });
  };

  render() {
    if (this.state.redirect) {
      return <Redirect to={this.state.redirect} />;
    } else {
      return (
        <CallbackComponent
          userManager={userManager}
          successCallback={this.onSuccess}
        >
          <div>Processing callback...</div>
        </CallbackComponent>
      );
    }
  }
}

export default LoginCallback;
