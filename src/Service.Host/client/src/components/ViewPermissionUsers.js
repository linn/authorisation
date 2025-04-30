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

function ViewPermissionUsers() {
    const [privilegeInput, setPrivilegeInput] = useState('');

    const { data: privileges, isGetLoading: isPrivilegeLoading } = useInitialise(
        itemTypes.privileges.url,
        null,
        true
    );

    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );

    const {
        send,
        isLoading: isGetLoading,
        result: permissionEmployeesUris
    } = useGet(`${itemTypes.permissions.url}/privilege`);

    const getPermissionEmployees = member => {
        const employee = employees?.items.find(i => member === i?.href);

        return (
            <ListItem key={employee?.href}>
                <Typography color="primary">{employee?.fullName}</Typography>
            </ListItem>
        );
    };

    privileges?.sort((a, b) => {
        const fa = a.name.toLowerCase();
        const fb = b.name.toLowerCase();

        if (fa < fb) {
            return -1;
        }
        if (fa > fb) {
            return 1;
        }
        return 0;
    });

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                <Typography variant="h4">View all Employees with a Permission</Typography>
            </Grid>
            <Grid item xs={12}>
                {(isEmployeesLoading || isPrivilegeLoading || isGetLoading) && <Loading />}
            </Grid>
            <Grid item xs={4}>
                <Dropdown
                    propertyName="privilege choice"
                    items={privileges?.map(privilege => ({
                        id: privilege.id,
                        displayText: privilege?.name
                    }))}
                    required
                    label="Choose a Privilege"
                    fullWidth
                    onChange={(propertyName, newValue) => {
                        setPrivilegeInput(newValue);
                        send(null, `?privilegeId=${newValue}`);
                    }}
                    value={privilegeInput}
                />
            </Grid>

            <Grid item xs={12}>
                <List>{permissionEmployeesUris?.map(getPermissionEmployees)}</List>
            </Grid>
        </Page>
    );
}

export default ViewPermissionUsers;
