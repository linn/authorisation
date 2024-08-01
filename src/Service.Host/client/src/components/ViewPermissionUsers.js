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
        itemTypes.privileges.url
    );

    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );

    const {
        send,
        isLoading: isGetLoading,
        result: permissionEmployees
    } = useGet(itemTypes.permissions.url);

    const spinningWheel = () => {
        if (isEmployeesLoading || isPrivilegeLoading || isGetLoading) {
            return <Loading />;
        }
        return <div />;
    };

    const getPermissionEmployees = member => {
        const employee = employees?.items.find(i => member.granteeUri === i.href);

        return (
            <ListItem key={employee.href}>
                <Typography color="primary">{`${employee.firstName} ${employee.lastName}`}</Typography>
            </ListItem>
        );
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                {spinningWheel()}
                <Typography variant="h4">View all Employees with a Permission</Typography>
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
                <List>{permissionEmployees?.map(getPermissionEmployees)}</List>
            </Grid>
        </Page>
    );
}

export default ViewPermissionUsers;
