import React, { useState, useEffect } from 'react';
import Typography from '@mui/material/Typography';
import { Loading, Dropdown, SnackbarMessage } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import { DataGrid } from '@mui/x-data-grid';
import DeleteIcon from '@mui/icons-material/Delete';
import Tooltip from '@mui/material/Tooltip';
import IconButton from '@mui/material/IconButton';
import moment from 'moment';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import useGet from '../hooks/useGet';
import useDelete from '../hooks/useDelete';
import Page from './Page';

function ViewPermissionUsers() {
    const [privilegeInput, setPrivilegeInput] = useState('');
    const [snackbarVisible, setSnackbarVisible] = useState(false);

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
        result: permissionsInfo
    } = useGet(`${itemTypes.permissions.url}/privilege`, true);

    const {
        send: deleteSend,
        isLoading: isDeleteLoading,
        deleteResult
    } = useDelete(itemTypes.permissions.url, true);

    useEffect(() => {
        setSnackbarVisible(!!deleteResult);
    }, [deleteResult]);

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

    const permissions = permissionsInfo?.map(permission => {
        const addedByEmployee = employees?.items?.find(e => permission?.grantedByUri === e?.href);
        const granteeEmployee = employees?.items?.find(e => permission?.granteeUri === e?.href);

        return {
            id: permission?.id,
            granteeEmployee: granteeEmployee?.fullName,
            addedByEmployee: addedByEmployee?.fullName,
            privilege: permission?.privilege,
            groupName: permission?.groupName,
            dateGranted: permission?.dateGranted
        };
    });

    const permissionColumns = [
        {
            field: 'id',
            headerName: 'ID',
            width: 75
        },
        {
            field: 'granteeEmployee',
            headerName: 'Employee',
            width: 200,
            valueGetter: params => params.row.granteeEmployee || 'N/A'
        },
        {
            field: 'Group',
            headerName: 'Group',
            width: 200,
            valueGetter: params => params.row.groupName || 'N/A'
        },
        {
            field: 'addedByEmployee',
            headerName: 'Added By',
            width: 200,
            valueGetter: params => params.row.addedByEmployee || 'N/A'
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
            renderCell: params =>
                params.row.groupName ? null : (
                    <Tooltip title="Delete">
                        <IconButton
                            aria-label="delete"
                            size="small"
                            onClick={() => {
                                deleteSend(params.row.id);
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
                <Typography variant="h4">View all Employees with a Permission</Typography>
            </Grid>
            <Grid item xs={12}>
                {(isEmployeesLoading || isDeleteLoading || isPrivilegeLoading || isGetLoading) && (
                    <Loading />
                )}
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

            {permissions && (
                <Grid item xs={12}>
                    <DataGrid
                        rows={permissions}
                        getRowId={row => row?.id}
                        columns={permissionColumns}
                        density="comfortable"
                        rowHeight={34}
                        disableMultipleSelection
                        hideFooter
                    />
                </Grid>
            )}
            <Grid item xs={12}>
                <SnackbarMessage
                    visible={snackbarVisible}
                    onClose={() => setSnackbarVisible(false)}
                    message="Delete Successful"
                />
            </Grid>
        </Page>
    );
}

export default ViewPermissionUsers;
