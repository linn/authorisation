import React, { Component } from 'react';
import { Provider } from 'react-redux';
import { Route, Redirect } from 'react-router';
import { OidcProvider } from 'redux-oidc';
import { Router } from 'react-router-dom';
import history from '../history';
import App from './App';
import Callback from '../containers/Callback';
import userManager from '../helpers/userManager';

class Root extends Component {
    render() {
        const { store } = this.props;

        return (
            <Provider store={store}>
                <OidcProvider store={store} userManager={userManager}>
                    <Router history={history}>
                        <div>

                            <Route path="/" render={() => { document.title = 'Authorisation'; return false; }} />
                            <Route exact path="/" render={() => <Redirect to="/authorisation" />} />
                            <Route exact path="/authorisation" component={App} />
                            <Route exact path="/authorisation/signin-oidc-client" component={Callback} />
                        </div>
                    </Router>
                </OidcProvider>
            </Provider>
        );

    }
}

export default Root;