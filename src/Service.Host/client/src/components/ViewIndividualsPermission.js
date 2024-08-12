import React, { useState } from 'react';
import Typography from '@mui/material/Typography';
import { Loading, Dropdown } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import Button from '@mui/material/Button';
import config from '../config';
import history from '../history';
import useDelete from '../hooks/useDelete';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import useGet from '../hooks/useGet';
import Page from './Page';

function ViewIndividualsPermission() {
    const [employeeInput, setEmployeeInput] = useState('');

    const {
        send,
        isLoading: isGetLoading,
        result: permissions
    } = useGet(itemTypes.permissions.url);

    const {
        send: deleteSend,
        isLoading: isDeleteLoading,
        deleteResult
    } = useDelete(itemTypes.permissions.url, true);

    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );

    const renderEmployeesPermission = permission => (
        <Grid container spacing={1}>
            <Grid item xs={10}>
                <ListItem key={permission.privilegeId}>
                    <Typography color="primary">{permission.privilege}</Typography>
                </ListItem>
            </Grid>
            <Grid item xs={1}>
                <Button
                    variant="outlined"
                    onClick={() => {
                        deleteSend(permission.id);
                    }}
                >
                    Delete
                </Button>
            </Grid>
        </Grid>
    );

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                <Typography variant="h4">View an Employee&apos;s Permissions</Typography>
            </Grid>
            <Grid item xs={12}>
                {(isEmployeesLoading || isGetLoading || isDeleteLoading) && <Loading />}
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

            <Grid item xs={4}>
                <List>{permissions?.map(renderEmployeesPermission)}</List>
            </Grid>
        </Page>
    );
}

export default ViewIndividualsPermission;
