import React from 'react';
import keycloakClient from "./keycloak/keycloakClient";

const KeycloakLogin = () => {
    return (
        <div>
            {!keycloakClient.authenticated && (
                <button type="button" onClick={() => keycloakClient.login()}>
                    Login
                </button>
            )}
{/*
            {keycloak.authenticated && (
                <button type="button" onClick={() => keycloak.logout()}>
                    Logout
                </button>
            )}
*/}
        </div>
    );
};

export default KeycloakLogin;