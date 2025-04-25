import React, { useState, useEffect } from 'react';
import Typography from '@mui/material/Typography';
import { Loading, Dropdown, SnackbarMessage } from '@linn-it/linn-form-components-library';
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
    const [snackbarVisible, setSnackbarVisible] = useState(false);

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

    useEffect(() => {
        setSnackbarVisible(!!deleteResult);
    }, [deleteResult]);

    const renderEmployeesPermission = permission => (
        <Grid item xs={12}>
            <ListItem key={permission.privilegeId}>
                <Grid item xs={4}>
                    <Typography color="primary">{permission.privilege}</Typography>
                </Grid>
                <Grid item xs={8}>
                    {permission.groupName ? (
                        <Typography color="primary">
                            [Has via group membership {permission.groupName}]
                        </Typography>
                    ) : (
                        <Button
                            variant="outlined"
                            onClick={() => {
                                deleteSend(permission.id);
                            }}
                        >
                            Delete
                        </Button>
                    )}
                </Grid>
            </ListItem>
        </Grid>
    );

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typography variant="h4">View an Employee&apos;s Permissions</Typography>
                </Grid>
                <Grid item xs={12}>
                    {(isEmployeesLoading || isGetLoading || isDeleteLoading) && <Loading />}
                </Grid>
                <Grid item xs={12}>
                    <Dropdown
                        propertyName="employee choice"
                        items={employees?.items?.map(employee => ({
                            id: employee.id,
                            displayText: employee?.fullName
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

                <SnackbarMessage
                    visible={snackbarVisible}
                    onClose={() => setSnackbarVisible(false)}
                    message="Delete Successful"
                />
            </Grid>
        </Page>
    );
}

export default ViewIndividualsPermission;
