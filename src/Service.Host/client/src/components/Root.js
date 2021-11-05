import React from 'react';
import { Provider } from 'react-redux';
import { Route, Redirect, Switch } from 'react-router';
import { OidcProvider } from 'redux-oidc';
import { Router } from 'react-router-dom';
import PropTypes from 'prop-types';
import CssBaseline from '@material-ui/core/CssBaseline';
import history from '../history';
import App from './App';
import Callback from '../containers/Callback';
import userManager from '../helpers/userManager';
import Privileges from '../containers/Privileges';
import Privilege from '../containers/Privilege';
import Groups from '../containers/Groups';
import Group from '../containers/Group';
import Permissions from '../containers/Permissions';

const Root = ({ store }) => (
    <div>
        <div style={{ paddingTop: '40px' }}>
            <Provider store={store}>
                <OidcProvider store={store} userManager={userManager}>
                    <Router store={store} history={history}>
                        <div>
                            <CssBaseline />

                            <Route exact path="/" render={() => <Redirect to="/authorisation" />} />

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

                                <Route
                                    exact
                                    path="/authorisation/viewprivileges"
                                    render={() => <Privileges store={store} />}
                                    store={store}
                                />

                                <Route
                                    exact
                                    path="/authorisation/privileges/:id"
                                    render={() => <Privilege store={store} />}
                                    store={store}
                                />

                                <Route
                                    exact
                                    path="/authorisation/groups/"
                                    render={() => <Groups store={store} />}
                                    store={store}
                                />

                                <Route
                                    exact
                                    path="/authorisation/groups/:id"
                                    render={() => <Group store={store} />}
                                    store={store}
                                />
                               
                                <Route
                                    exact
                                    path="/authorisation/viewpermissions"
                                    render={() => <Permissions store={store} />}
                                    store={store}
                                />

                                <Route
                                    exact
                                    path="/authorisation/permissions/:id"
                                    render={() => <Permissions store={store} />}
                                    store={store}
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
