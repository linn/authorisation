import React, { useEffect, useState } from 'react';
import { useAuth } from 'react-oidc-context';
import { useParams } from 'react-router-dom';
import Typography from '@mui/material/Typography';
import {
    Loading,
    InputField,
    OnOffSwitch,
    ErrorCard,
    utilities,
    PermissionIndicator,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';
import PropTypes from 'prop-types';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import config from '../config';
import usePut from '../hooks/usePut';
import usePost from '../hooks/usePost';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import useGet from '../hooks/useGet';
import itemTypes from '../itemTypes';
import Page from './Page';

function Group({ creating }) {
    const { id } = useParams();
    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );
    const [group, setGroup] = useState();
    const [errorMessage, setErrorMessage] = useState();

    const {
        send: fetchGroup,
        isLoading: isGroupLoading,
        result: groupResult
    } = useGet(itemTypes.groups.url, true);

    const auth = useAuth();
    const token = auth.user?.access_token;

    const [hasFetched, setHasFetched] = useState(false);

    if (!hasFetched && token && creating) {
        fetchGroup(id);
        setHasFetched(true);
    }

    const hasPermission = creating ? true : utilities.getHref(group, 'edit');

    const {
        send: sendUpdate,
        isPutLoading,
        errorMessage: updateErrorMessage,
        putResult
    } = usePut(itemTypes.groups.url, id, group, true);

    const {
        send: sendCreate,
        isCreateLoading,
        errorMessage: createErrorMessage,
        postResult
    } = usePost(itemTypes.groups.url, null, group, true);

    useEffect(() => {
        if (groupResult) {
            setGroup(groupResult);
        }
    }, [groupResult]);

    useEffect(() => {
        if (updateErrorMessage) {
            setErrorMessage(updateErrorMessage);
        } else if (createErrorMessage) {
            setErrorMessage(createErrorMessage);
        }
    }, [createErrorMessage, updateErrorMessage]);

    const [snackbarVisible, setSnackbarVisible] = useState(false);

    useEffect(() => {
        setSnackbarVisible(!!putResult || !!postResult);
    }, [postResult, putResult]);

    const handleActiveChange = (_, newValue) => {
        setGroup({ ...group, active: newValue });
    };

    const handleNameFieldChange = (_, newValue) => {
        setGroup({ ...group, name: newValue });
    };

    const renderListItem = member => {
        const employee = employees?.items.find(i => member.memberUri === i.href);
        if (!employee) {
            return null;
        }
        return (
            <ListItem key={employee.href}>
                <Typography color="primary">{`${employee.firstName} ${employee.lastName}`}</Typography>
            </ListItem>
        );
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={11}>
                    <Typography variant="h4">{creating ? `Create Group` : `Edit Group`}</Typography>
                </Grid>
                <Grid item xs={1}>
                    <PermissionIndicator
                        hasPermission={!!hasPermission}
                        hasPermissionMessage={
                            creating ? 'You can create groups ' : 'You can update groups '
                        }
                        noPermissionMessage={
                            creating
                                ? 'You do not have create privilege permissions'
                                : 'You do not have update privilege permissions'
                        }
                    />
                </Grid>
                <Grid item xs={12}>
                    {(isGroupLoading || isPutLoading || isCreateLoading || isEmployeesLoading) && (
                        <Loading />
                    )}
                </Grid>
                {hasPermission && (
                    <Grid item xs={6}>
                        <InputField
                            propertyName="inputValue"
                            label="Name"
                            value={group?.name}
                            onChange={handleNameFieldChange}
                            fullWidth
                        />
                        <Typography color="black">
                            Inactive
                            <OnOffSwitch
                                value={group?.active}
                                onChange={handleActiveChange}
                                inputProps={{ 'aria-label': 'Switch demo' }}
                                defaultchecked={groupResult?.active}
                                propertyName="OnOffSwitch"
                            />
                            Active
                        </Typography>
                        <Button
                            variant="contained"
                            disabled={groupResult?.name === group?.name}
                            onClick={() => {
                                if (creating) {
                                    sendCreate(group);
                                } else {
                                    sendUpdate(id, group);
                                }
                            }}
                        >
                            Save
                        </Button>
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

                        {!creating && (
                            <Grid item xs={12}>
                                <Typography variant="h5">Group Members</Typography>

                                <List>{group?.members?.map(renderListItem)}</List>
                            </Grid>
                        )}
                    </Grid>
                )}
            </Grid>
        </Page>
    );
}

Group.propTypes = { creating: PropTypes.bool };
Group.defaultProps = { creating: false };

export default Group;
