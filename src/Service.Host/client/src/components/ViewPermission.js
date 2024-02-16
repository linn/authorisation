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
import useGet from '../hooks/useGet';
import Page from './Page';

function ViewPermission() {
    const [employeeInput, setEmployeeInput] = useState('');

    const {
        send,
        isLoading: isGetLoading,
        result: permissions
    } = useGet(itemTypes.permissions.url);

    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );

    const spinningWheel = () => {
        if (isEmployeesLoading || isGetLoading) {
            return <Loading />;
        }
        return <div />;
    };

    const renderEmployeesPermission = permission => (
        <ListItem key={permission.privilegeId}>
            <Typography color="primary">{permission.privilege}</Typography>
        </ListItem>
    );

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
                    onChange={(propertyName, newValue) => {
                        setEmployeeInput(newValue);
                        send(null, `?who=/employees/${newValue}`);
                    }}
                    value={employeeInput}
                />
            </Grid>

            <Grid item xs={12}>
                <List>{permissions?.map(renderEmployeesPermission)}</List>
            </Grid>
        </Page>
    );
}

export default ViewPermission;
