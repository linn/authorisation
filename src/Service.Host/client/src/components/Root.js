﻿import React from 'react';
import { Provider } from 'react-redux';
import { Route, Routes } from 'react-router';
import { OidcProvider } from 'redux-oidc';
import { Navigation } from '@linn-it/linn-form-components-library';
import { Navigate, unstable_HistoryRouter as HistoryRouter } from 'react-router-dom';
import PropTypes from 'prop-types';
import App from './App';
import Callback from './Callback';
import userManager from '../helpers/userManager';
import 'typeface-roboto';
import NotFoundPage from './NotFoundPage';
import history from '../history';
import ExampleComponent from './ExampleComponent';

function Root({ store }) {
    return (
        <div>
            <div className="padding-top-when-not-printing">
                <Provider store={store}>
                    <OidcProvider store={store} userManager={userManager}>
                        <div>
                            <Navigation />
                            <HistoryRouter history={history}>
                                <Routes>
                                    <Route
                                        exact
                                        path="/"
                                        element={<Navigate to="/authorisation" replace />}
                                    />
                                    <Route path="/authorisation" element={<App />} />
                                    <Route
                                        exact
                                        path="/authorisation/example-component"
                                        element={<ExampleComponent />}
                                    />
                                    <Route
                                        exact
                                        path="/authorisation/signin-oidc-client"
                                        element={<Callback />}
                                    />
                                    <Route element={<NotFoundPage />} />
                                </Routes>
                            </HistoryRouter>
                        </div>
                    </OidcProvider>
                </Provider>
            </div>
        </div>
    );
}

Root.propTypes = {
    store: PropTypes.shape({}).isRequired
};

export default Root;
