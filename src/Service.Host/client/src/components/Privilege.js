import React, { useEffect, useState } from 'react';
import { useAuth } from 'react-oidc-context';
import { useParams } from 'react-router-dom';
import Typography from '@mui/material/Typography';
import {
    Loading,
    InputField,
    OnOffSwitch,
    ErrorCard,
    SnackbarMessage,
    PermissionIndicator,
    utilities
} from '@linn-it/linn-form-components-library';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import PropTypes from 'prop-types';
import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';
import config from '../config';
import usePut from '../hooks/usePut';
import useGet from '../hooks/useGet';
import usePost from '../hooks/usePost';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import Page from './Page';

function Privilege({ creating }) {
    const { id } = useParams();

    const {
        send: fetchPrivileges,
        isLoading: isGetLoading,
        result: privilegeResult
    } = useGet(itemTypes.privileges.url, true);

    const auth = useAuth();
    const token = auth.user?.access_token;

    const [hasFetched, setHasFetched] = useState(false);

    if (!hasFetched && token && !creating) {
        fetchPrivileges(id);
        setHasFetched(true);
    }

    const { data: permissionEmployees, isGetLoading: isPermissionEmployeesLoading } = useInitialise(
        `${itemTypes.permissions.url}/privilege`,
        `?privilegeId=${id}`
    );
    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );
    const [privilege, setPrivilege] = useState();

    const [errorMessage, setErrorMessage] = useState();

    const hasPermission = creating ? true : utilities.getHref(privilege, 'edit');

    const {
        send: sendUpdate,
        isPutLoading,
        updateErrorMessage,
        putResult
    } = usePut(itemTypes.privileges.url, id, privilege, true);

    const {
        send: sendCreate,
        isCreateLoading,
        createErrorMessage,
        postResult
    } = usePost(itemTypes.privileges.url, null, privilege, true);

    const [snackbarVisible, setSnackbarVisible] = useState(false);

    useEffect(() => {
        setSnackbarVisible(!!putResult || !!postResult);
    }, [postResult, putResult]);

    useEffect(() => {
        if (updateErrorMessage) {
            setErrorMessage(updateErrorMessage);
        } else if (createErrorMessage) {
            setErrorMessage(createErrorMessage);
        }
    }, [updateErrorMessage, createErrorMessage]);

    useEffect(() => {
        if (privilegeResult) {
            setPrivilege(privilegeResult);
        }
    }, [privilegeResult, id]);

    const getPermissionEmployees = member => {
        const employee = employees?.items.find(i => member.granteeUri === i?.href);

        if (employee) {
            return (
                <ListItem key={employee?.href}>
                    <Typography color="primary">{`${employee?.firstName} ${employee?.lastName}`}</Typography>
                </ListItem>
            );
        }
        return null;
    };

    const handleActiveChange = (_, newValue) => {
        setPrivilege({ ...privilege, active: newValue });
    };

    const handleNameFieldChange = (_, newValue) => {
        setPrivilege({ ...privilege, name: newValue });
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={11}>
                    <Typography variant="h4">
                        {creating ? `Create Privilege` : `Edit Privilege`}
                    </Typography>
                </Grid>
                <Grid item xs={1}>
                    <PermissionIndicator
                        hasPermission={!!hasPermission}
                        hasPermissionMessage={
                            creating
                                ? 'You have create privilege permissions'
                                : 'You have update privilege permissions'
                        }
                        noPermissionMessage={
                            creating
                                ? 'You do not have create privilege permissions'
                                : 'You do not have update privilege permissions'
                        }
                    />
                </Grid>
                <Grid item xs={12}>
                    {(isGetLoading ||
                        isEmployeesLoading ||
                        isCreateLoading ||
                        isPermissionEmployeesLoading ||
                        isPutLoading) && <Loading />}
                </Grid>
                {hasPermission && (
                    <>
                        <Grid item xs={6}>
                            <InputField
                                propertyName="inputValue"
                                label="Name"
                                value={privilege?.name}
                                onChange={handleNameFieldChange}
                                fullWidth
                            />

                            <Typography color="black">
                                Inactive
                                <OnOffSwitch
                                    value={privilege?.active}
                                    onChange={handleActiveChange}
                                    inputProps={{ 'aria-label': 'Switch demo' }}
                                    defaultChecked={privilege?.active}
                                />
                                Active
                            </Typography>
                            <Button
                                variant="contained"
                                disabled={
                                    privilegeResult?.name === privilege?.name &&
                                    privilegeResult?.active === privilege?.active
                                }
                                onClick={() => {
                                    if (creating) {
                                        sendCreate(privilege);
                                    } else {
                                        sendUpdate(id, privilege);
                                    }
                                }}
                            >
                                Save
                            </Button>
                        </Grid>

                        <Grid item xs={12}>
                            <List>{permissionEmployees?.map(getPermissionEmployees)}</List>
                        </Grid>
                    </>
                )}
                {errorMessage && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={errorMessage} />
                    </Grid>
                )}
                <SnackbarMessage
                    visible={snackbarVisible}
                    onClose={() => setSnackbarVisible(false)}
                    message="Save Successful"
                />
            </Grid>
        </Page>
    );
}

Privilege.propTypes = { creating: PropTypes.bool };
Privilege.defaultProps = { creating: false };

export default Privilege;
