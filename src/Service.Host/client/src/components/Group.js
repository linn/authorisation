import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Typography from '@mui/material/Typography';
import {
    Loading,
    InputField,
    OnOffSwitch,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import { DataGrid } from '@mui/x-data-grid';
import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import config from '../config';
import usePut from '../hooks/usePut';
import usePost from '../hooks/usePost';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import Page from './Page';

function Group({ creating }) {
    const { id } = useParams();
    const {
        data,
        isGetLoading,
        errorMessage: getErrorMessage
    } = useInitialise(itemTypes.groups.url, id, true);
    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );

    const [group, setGroup] = useState();
    const [errorMessage, setErrorMessage] = useState();

    const {
        send: putSend,
        isPutLoading,
        errorMessage: putErrorMessage,
        putResult
    } = usePut(itemTypes.groups.url, id, group, true);

    const {
        send: postSend,
        isLoading: isPostLoading,
        errorMessage: postErrorMessage,
        postResult
    } = usePost(itemTypes.groups.url, null, group, true);

    useEffect(() => {
        if (!creating && data) {
            setGroup(data);
        }
    }, [data, creating]);

    useEffect(() => {
        if (getErrorMessage) {
            setErrorMessage(getErrorMessage);
        } else if (putErrorMessage) {
            setErrorMessage(putErrorMessage);
        } else if (postErrorMessage) {
            setErrorMessage(postErrorMessage);
        }
    }, [putErrorMessage, postErrorMessage, getErrorMessage]);

    const [snackbarVisible, setSnackbarVisible] = useState(false);

    useEffect(() => {
        if (putResult) {
            setSnackbarVisible(!!putResult);
        } else if (postResult) {
            setSnackbarVisible(!!postResult);
        }
    }, [postResult, putResult]);

    const handleActiveChange = (_, newValue) => {
        setGroup({ ...group, active: newValue });
    };

    const handleNameFieldChange = (_, newValue) => {
        setGroup({ ...group, name: newValue });
    };

    const groupMembers = employees?.items?.filter(employee =>
        group?.members?.some(member => member?.memberUri === employee?.href)
    );

    const employeeColumns = [
        {
            field: 'id',
            headerName: 'ID',
            width: 75
        },
        {
            field: 'fullName',
            headerName: 'Employee Name',
            width: 150
        }
    ];

    const privilegeColumns = [
        {
            field: 'privilegeId',
            headerName: 'ID',
            width: 75
        },
        {
            field: 'privilege',
            headerName: 'Privilege Name',
            width: 350
        }
    ];

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                <Typography variant="h4">{creating ? `Create a Group` : `Edit Group`}</Typography>
            </Grid>
            <Grid item xs={12}>
                {(isGetLoading || isPutLoading || isEmployeesLoading || isPostLoading) && (
                    <Loading />
                )}
            </Grid>
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
                        defaultchecked={data?.active}
                        propertyName="OnOffSwitch"
                    />
                    Active
                </Typography>
                <Button
                    variant="contained"
                    disabled={
                        data?.name === group?.name && data?.active === group?.active && !group?.name
                    }
                    onClick={creating ? postSend : putSend}
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

                <Box mt={3} />

                {!creating && groupMembers && group && (
                    <>
                        <Grid item xs={12}>
                            <Typography variant="h5">Group Members</Typography>

                            <Grid item xs={12}>
                                <DataGrid
                                    rows={
                                        groupMembers?.map(e => ({
                                            ...e,
                                            id: e?.id
                                        })) || []
                                    }
                                    columns={employeeColumns}
                                    density="comfortable"
                                    rowHeight={34}
                                    disableMultipleSelection
                                    hideFooter
                                />
                            </Grid>
                        </Grid>

                        <Box mt={3} />

                        <Grid item xs={12}>
                            <Typography variant="h5">Privileges</Typography>

                            <Grid item xs={12}>
                                <DataGrid
                                    rows={
                                        group.permission?.map(e => ({
                                            ...e,
                                            id: e?.privilegeId
                                        })) || []
                                    }
                                    columns={privilegeColumns}
                                    density="comfortable"
                                    rowHeight={34}
                                    disableMultipleSelection
                                    hideFooter
                                />
                            </Grid>
                        </Grid>
                    </>
                )}
            </Grid>
        </Page>
    );
}

Group.propTypes = { creating: PropTypes.bool };
Group.defaultProps = { creating: false };

export default Group;
