import React, { Component } from "react";
import userManager from "../../authorization/userManager";

class Logout extends Component {
  async componentDidMount() {
    await userManager.signoutRedirect();
  }

  render() {
    return <div> Logging out...</div>;
  }
}

export default Logout;
