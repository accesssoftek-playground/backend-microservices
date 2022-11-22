import React from 'react';
import {ReactKeycloakProvider} from '@react-keycloak/web';
import {BrowserRouter, Route, Routes} from 'react-router-dom';
import SecuredPage from './SecuredPage';
import keycloakClient from './keycloak/keycloakClient';
import KeycloakLogin from './KeycloakLogin';
import './App.css';

function App() {
    return (
      <div className="App">
        <ReactKeycloakProvider authClient={keycloakClient}>
          <header className="App-header">
              <KeycloakLogin />
              <p>
                  Edit <code>src/App.tsx</code> and save to reload.
              </p>

              <BrowserRouter>
                  <Routes>
                      <Route path="/" element={
                          <a className="App-link" href="/secured">
                              Go To Secured Page
                          </a>
                      }/>
                      <Route path="/secured" element={ <SecuredPage/> }/>
                  </Routes>
              </BrowserRouter>
              
          </header>
        </ReactKeycloakProvider>
      </div>
  );
}

export default App;
