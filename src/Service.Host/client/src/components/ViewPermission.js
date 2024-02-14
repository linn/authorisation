import React, { useState } from 'react';

import Typography from '@mui/material/Typography';
import { Loading, Dropdown } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import Page from './Page';

function ViewPermission() {
    const [employeeInput, setEmployeeInput] = useState('');
    const [fullName, setFullName] = useState('');

    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );

    const { data: permissions, isGetLoading: isPermissionsLoading } = useInitialise(
        itemTypes.permissions.url,
        employeeInput
    );

    const spinningWheel = () => {
        if (isPermissionsLoading || isEmployeesLoading) {
            return <Loading />;
        }
        return <div />;
    };

    const renderEmployeesPermission = permission => (
        <ListItem>
            <Typography color="primary">{permission.privilege}</Typography>
        </ListItem>
    );

    const getFullName = () => {
        console.log(employeeInput);
        employees.forEach(employee => {
            console.log(employee.id);
            if (employeeInput === employee.id) {
                console.log('match');
                setFullName(`${employee?.firstName} ${employee?.lastName}`);
            }
        });
    };

    const displayUserPermissions = () => {
        if (employeeInput) {
            if (permissions !== '[]') {
                return (
                    <Grid item xs={12}>
                        <Typography color="black" variant="h5">
                            {employeeInput
                                ? `Current Permissions available for ${employeeInput.firstName}`
                                : ''}
                        </Typography>
                        <List>{permissions?.map(renderEmployeesPermission)}</List>
                    </Grid>
                );
            }
            return (
                <Grid item xs={12}>
                    <Typography color="black" variant="h5">
                        {employeeInput
                            ? `${employeeInput.firstName} has no permissions currently`
                            : ''}
                    </Typography>
                </Grid>
            );
        }
        return null;
    };

    const handleEmployeeDropDownChange = (propertyName, newValue) => {
        setEmployeeInput(newValue);
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                {spinningWheel()}
                <Typography variant="h4">View an Employee&apos;s Permissions</Typography>
            </Grid>

            <Grid item xs={4}>
                <Dropdown
                    propertyName="employee choice"
                    items={employees?.items?.map(employee => ({
                        id: employee.id,
                        displayText: `${employee?.firstName} ${employee?.lastName}`
                    }))}
                    required
                    label="Choose an Employee"
                    fullWidth
                    onChange={handleEmployeeDropDownChange}
                    value={employeeInput}
                />
            </Grid>

            {employeeInput ? displayUserPermissions : null}
        </Page>
    );
}

export default ViewPermission;
