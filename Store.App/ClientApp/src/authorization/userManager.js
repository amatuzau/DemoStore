// Required Settings
//   authority (string): The URL of the OIDC/OAuth2 provider.
//   client_id (string): Your client application's identifier as registered with the OIDC/OAuth2 provider.
//   redirect_uri (string): The redirect URI of your client application to receive a response from the OIDC/OAuth2 provider.
//   response_type (string, default: 'id_token'): The type of response desired from the OIDC/OAuth2 provider.
//   scope (string, default: 'openid'): The scope being requested from the OIDC/OAuth2 provider.

import { createUserManager } from "redux-oidc";

const settings = {
  authority: "https://localhost:5001",
  client_id: "Store.App",
  redirect_uri:
    "https://localhost:5001/ClientApp/authentication/login-callback",
  response_type: "code",
  scope: "Store.AppAPI openid profile",
};

export default createUserManager(settings);
