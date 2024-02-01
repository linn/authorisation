﻿import React from 'react';
import { Route, Routes } from 'react-router';
import { Navigate, unstable_HistoryRouter as HistoryRouter } from 'react-router-dom';
import PropTypes from 'prop-types';
import App from './App';
import 'typeface-roboto';
import NotFoundPage from './NotFoundPage';
import history from '../history';
import ExampleComponent from './ExampleComponent';
import Privileges from './Privileges';
import CreatePrivilege from './CreatePrivilege';
import Privilege from './Privilege';

function Root() {
    return (
        <div>
            <div className="padding-top-when-not-printing">
                <div>
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
                                path="/authorisation/privileges"
                                element={<Privileges />}
                            />
                            <Route
                                exact
                                path="/authorisation/privileges/create"
                                element={<CreatePrivilege />}
                            />
                            <Route
                                exact
                                path="authorisation/privileges/:id"
                                element={<Privilege />}
                            />
                            <Route element={<NotFoundPage />} />
                        </Routes>
                    </HistoryRouter>
                </div>
            </div>
        </div>
    );
}

Root.propTypes = {
    store: PropTypes.shape({}).isRequired
};

export default Root;
