import React from 'react';
import { Route, Routes } from 'react-router';
import { Navigate, unstable_HistoryRouter as HistoryRouter } from 'react-router-dom';
import App from './App';
import 'typeface-roboto';
import NotFoundPage from './NotFoundPage';
import history from '../history';
import ExampleComponent from './ExampleComponent';
import Privileges from './Privileges';
import CreatePrivilege from './CreatePrivilege';
import Privilege from './Privilege';
import CreateIndividualPermission from './CreateIndividualPermission';
import CreateGroupPermission from './CreateGroupPermission';
import ViewPermission from './ViewPermission';

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
                                path="/authorisation/privileges/:id"
                                element={<Privilege />}
                            />
                            <Route
                                exact
                                path="/authorisation/permission/create/individual"
                                element={<CreateIndividualPermission />}
                            />
                            <Route
                                exact
                                path="/authorisation/permission/create/group"
                                element={<CreateGroupPermission />}
                            />
                            <Route
                                exact
                                path="/authorisation/permission/view"
                                element={<ViewPermission />}
                            />
                            <Route element={<NotFoundPage />} />
                        </Routes>
                    </HistoryRouter>
                </div>
            </div>
        </div>
    );
}

export default Root;
