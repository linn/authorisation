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
import DeleteIcon from '@mui/icons-material/Delete';
import Tooltip from '@mui/material/Tooltip';
import IconButton from '@mui/material/IconButton';
import Box from '@mui/material/Box';
import moment from 'moment';
import config from '../config';
import usePut from '../hooks/usePut';
import usePost from '../hooks/usePost';
import useDelete from '../hooks/useDelete';
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

    const {
        send: deleteMemberSend,
        isLoading: isDeleteMemberLoading,
        deleteResult: deleteMemberResult,
        errorMessage: deleteMemberErrorMessage
    } = useDelete(itemTypes.members.url, true);

    const {
        send: deletePermissionSend,
        isLoading: isDeletePermissionLoading,
        deleteResult: deletePermissionResult,
        errorMessage: deletePermissionErrorMessage
    } = useDelete(itemTypes.permissions.url, true);

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
        } else if (deleteMemberErrorMessage) {
            setErrorMessage(deleteMemberErrorMessage);
        } else if (deletePermissionErrorMessage) {
            setErrorMessage(deletePermissionErrorMessage);
        }
    }, [
        putErrorMessage,
        postErrorMessage,
        getErrorMessage,
        deleteMemberErrorMessage,
        deletePermissionErrorMessage
    ]);

    const [snackbarVisible, setSnackbarVisible] = useState(false);

    useEffect(() => {
        if (putResult) {
            setSnackbarVisible(!!putResult);
        } else if (postResult) {
            setSnackbarVisible(!!postResult);
        } else if (deletePermissionResult) {
            setSnackbarVisible(!!deletePermissionResult);
        } else if (deleteMemberResult) {
            setSnackbarVisible(!!deleteMemberResult);
        }
    }, [deleteMemberResult, deletePermissionResult, id, postResult, putResult]);

    const handleActiveChange = (_, newValue) => {
        setGroup({ ...group, active: newValue });
    };

    const handleNameFieldChange = (_, newValue) => {
        setGroup({ ...group, name: newValue });
    };

    const groupMembers = group?.members?.map(member => {
        const employeeMember = employees?.items?.find(e => member?.memberUri === e?.href);

        const addedByEmployee = employees?.items?.find(e => member?.addedByUri === e?.href);
        return {
            employeeMemberId: employeeMember?.id,
            employeeMember,
            addedByEmployeeId: addedByEmployee?.id,
            addedByEmployee,
            groupMemberId: member?.id,
            dateAdded: member?.dateAdded
        };
    });

    const groupPermissions = group?.permissions?.map(permission => {
        const addedByEmployee = employees?.items?.find(e => permission?.grantedByUri === e?.href);

        return {
            id: permission?.id,
            privilege: permission?.privilege,
            addedByEmployeeId: addedByEmployee?.id ? addedByEmployee?.id : null,
            addedByEmployee,
            dateGranted: permission?.dateGranted
        };
    });

    const employeeColumns = [
        {
            field: 'id',
            headerName: 'ID',
            width: 75
        },
        {
            field: 'employeeMemberId',
            headerName: 'Employee Name',
            width: 200,
            valueGetter: params => params.row.employeeMember?.fullName || 'N/A'
        },
        {
            field: 'addedByEmployee',
            headerName: 'Added By',
            width: 200,
            valueGetter: params => params.row.addedByEmployee?.fullName || 'N/A'
        },
        {
            field: 'dateAdded',
            headerName: 'Date Granted',
            width: 150,
            valueGetter: ({ value }) => value && moment(value).format('DD MMM YYYY')
        },
        {
            field: 'delete',
            headerName: ' ',
            width: 50,
            renderCell: params => (
                <Tooltip title="Delete">
                    <IconButton
                        aria-label="delete"
                        size="small"
                        onClick={() => {
                            deleteMemberSend(params.row.groupMemberId);
                            setGroup(prevGroup => ({
                                ...prevGroup,
                                members: prevGroup.members.filter(
                                    member => member.id !== params.row.groupMemberId
                                )
                            }));
                        }}
                    >
                        <DeleteIcon fontSize="inherit" />
                    </IconButton>
                </Tooltip>
            )
        }
    ];

    const privilegeColumns = [
        {
            field: 'id',
            headerName: 'ID',
            width: 75
        },
        {
            field: 'privilege',
            headerName: 'Privilege Name',
            width: 350
        },
        {
            field: 'addedByEmployeeId',
            headerName: 'Added By',
            width: 200,
            valueGetter: params => params.row.addedByEmployee?.fullName || 'N/A'
        },
        {
            field: 'dateGranted',
            headerName: 'Date Granted',
            width: 150,
            valueGetter: ({ value }) => value && moment(value).format('DD MMM YYYY')
        },
        {
            field: 'delete',
            headerName: ' ',
            width: 50,
            renderCell: params => (
                <Tooltip title="Delete">
                    <IconButton
                        aria-label="delete"
                        size="small"
                        onClick={() => {
                            deletePermissionSend(params.row.id);
                            setGroup(prevGroup => ({
                                ...prevGroup,
                                permissions: prevGroup.permissions.filter(
                                    p => p.id !== params.row.Id
                                )
                            }));
                        }}
                    >
                        <DeleteIcon fontSize="inherit" />
                    </IconButton>
                </Tooltip>
            )
        }
    ];

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                <Typography variant="h4">{creating ? `Create a Group` : `Edit Group`}</Typography>
            </Grid>
            <Grid item xs={12}>
                {(isGetLoading ||
                    isPutLoading ||
                    isDeleteMemberLoading ||
                    isDeletePermissionLoading ||
                    isEmployeesLoading ||
                    isPostLoading) && <Loading />}
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

                {console.log(snackbarVisible)}

                <SnackbarMessage
                    visible={snackbarVisible}
                    onClose={() => setSnackbarVisible(false)}
                    message={
                        deletePermissionResult || deleteMemberResult
                            ? 'Delete Successful'
                            : 'Save Successful'
                    }
                />

                <Box mt={3} />

                {!creating && groupMembers && group && (
                    <>
                        <Grid item xs={12}>
                            <Typography variant="h5">Group Members</Typography>

                            {groupMembers?.length === 0 ? (
                                <Typography color="primary">
                                    {group.name} does not contain any members
                                </Typography>
                            ) : (
                                <Grid item xs={12}>
                                    <DataGrid
                                        rows={
                                            groupMembers?.map(e => ({
                                                ...e,
                                                id: e?.groupMemberId
                                            })) || []
                                        }
                                        columns={employeeColumns}
                                        density="comfortable"
                                        rowHeight={34}
                                        disableMultipleSelection
                                        hideFooter
                                    />
                                </Grid>
                            )}
                        </Grid>

                        <Box mt={3} />

                        <Grid item xs={12}>
                            <Typography variant="h5">Permissions</Typography>
                        </Grid>

                        {group.permissions?.length === 0 ? (
                            <Typography color="primary">
                                {group.name} does not have any permissions
                            </Typography>
                        ) : (
                            <Grid item xs={12}>
                                <DataGrid
                                    rows={
                                        groupPermissions?.map(e => ({
                                            ...e,
                                            id: e?.id
                                        })) || []
                                    }
                                    columns={privilegeColumns}
                                    density="comfortable"
                                    rowHeight={34}
                                    disableMultipleSelection
                                    hideFooter
                                />
                            </Grid>
                        )}
                    </>
                )}
            </Grid>
        </Page>
    );
}

Group.propTypes = { creating: PropTypes.bool };
Group.defaultProps = { creating: false };

export default Group;
