import React, { useState, useEffect } from 'react';
import { useAuth } from 'react-oidc-context';
import Typography from '@mui/material/Typography';
import {
    Loading,
    Dropdown,
    SnackbarMessage,
    PermissionIndicator,
    utilities
} from '@linn-it/linn-form-components-library';
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
    const [permissionsWithView, setPermissionsWithView] = useState([]);

    const {
        send: fetchPermissions,
        isLoading: isGetLoading,
        result: permissions
    } = useGet(itemTypes.permissions.url, true);

    const auth = useAuth();
    const token = auth.user?.access_token;

    useEffect(() => {
        if (permissions) {
            const withView = permissions.filter(permission =>
                utilities.getHref(permission, 'view')
            );
            setPermissionsWithView(withView);
        }
    }, [permissions]);

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
                {employeeInput && (
                    <Grid item xs={1}>
                        <PermissionIndicator
                            hasPermission={permissionsWithView.length > 0}
                            hasPermissionMessage="You can only view these privileges"
                            noPermissionMessage="You do not have permission to view any privileges"
                        />
                    </Grid>
                )}
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
                            displayText: `${employee?.firstName} ${employee?.lastName}`
                        }))}
                        required
                        label="Choose an Employee"
                        fullWidth
                        onChange={(propertyName, newValue) => {
                            setEmployeeInput(newValue);
                            if (token) {
                                fetchPermissions(null, `?who=/employees/${newValue}`);
                            }
                        }}
                        value={employeeInput}
                    />
                </Grid>

                <Grid item xs={12}>
                    <List>{permissionsWithView?.map(renderEmployeesPermission)}</List>
                </Grid>

                {console.log(permissionsWithView)}

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
