import React from 'react';
import { Route, Routes } from 'react-router';
import { Navigate, unstable_HistoryRouter as HistoryRouter } from 'react-router-dom';
import App from './App';
import 'typeface-roboto';
import NotFoundPage from './NotFoundPage';
import history from '../history';
import ExampleComponent from './ExampleComponent';
import Privileges from './Privileges';
import Privilege from './Privilege';
import CreateIndividualPermission from './CreateIndividualPermission';
import CreateGroupPermission from './CreateGroupPermission';
import ViewEmployeesPermission from './ViewEmployeesPermission';
import Groups from './Groups';
import Group from './Group';
import AddIndividualMember from './AddIndividualMember';
import ViewPermissionUsers from './ViewPermissionUsers';

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
                                element={<Privilege creating />}
                            />
                            <Route
                                exact
                                path="/authorisation/privileges/:id"
                                element={<Privilege />}
                            />
                            <Route
                                exact
                                path="/authorisation/view-permission-users"
                                element={<ViewPermissionUsers />}
                            />
                            <Route
                                exact
                                path="/authorisation/create-individual-permission"
                                element={<CreateIndividualPermission />}
                            />
                            <Route
                                exact
                                path="/authorisation/create-group-permission"
                                element={<CreateGroupPermission />}
                            />
                            <Route
                                exact
                                path="/authorisation/view-individual-permission"
                                element={<ViewEmployeesPermission />}
                            />
                            <Route exact path="/authorisation/groups" element={<Groups />} />
                            <Route
                                exact
                                path="/authorisation/groups/create"
                                element={<Group creating />}
                            />
                            <Route
                                exact
                                path="/authorisation/groups/add-individual-member"
                                element={<AddIndividualMember />}
                            />
                            <Route exact path="/authorisation/groups/:id" element={<Group />} />
                            <Route element={<NotFoundPage />} />
                        </Routes>
                    </HistoryRouter>
                </div>
            </div>
        </div>
    );
}

export default Root;
