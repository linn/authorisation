import React from 'react';
import { Provider } from 'react-redux';
import { Route, Redirect, Switch } from 'react-router';
import { OidcProvider } from 'redux-oidc';
import { Router } from 'react-router-dom';
import history from '../history';
import App from './App';
import Callback from '../containers/Callback';
import userManager from '../helpers/userManager';
import PropTypes from 'prop-types';
import CssBaseline from '@material-ui/core/CssBaseline';

const Root = ({ store }) => (
    <div>
        <div style={{ paddingTop: '40px' }}>
            <Provider store={store}>
                <OidcProvider store={store} userManager={userManager}>
                    <Router history={history}>
                        <div>
                            <CssBaseline />

                            <Route
                                exact
                                path="/"
                                render={() => <Redirect to="/authorisation" />}
                            />

                            <Route
                                path="/"
                                render={() => {
                                    document.title = 'Authorisation';
                                    return false;
                                }}
                            />

                            <Switch>
                                <Route exact path="/authorisation" component={App} />

                                <Route
                                    exact
                                    path="/authorisation/signin-oidc-client"
                                    component={Callback}
                                />

                                
                            </Switch>
                        </div>
                    </Router>
                </OidcProvider>
            </Provider>
        </div>
    </div>
);

Root.propTypes = {
    store: PropTypes.shape({}).isRequired
};

export default Root;