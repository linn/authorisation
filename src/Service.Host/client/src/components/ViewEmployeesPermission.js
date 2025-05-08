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
import useDelete from '../hooks/useDelete';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import useGet from '../hooks/useGet';
import Page from './Page';

function ViewEmployeesPermission() {
    const [employeeInput, setEmployeeInput] = useState('');
    const [snackbarVisible, setSnackbarVisible] = useState(false);

    const {
        send,
        isLoading: isGetLoading,
        result: permissionsInfo
    } = useGet(itemTypes.permissions.url, true);

    const {
        send: deleteSend,
        isLoading: isDeleteLoading,
        deleteResult
    } = useDelete(itemTypes.permissions.url, true);

    const { data: currentEmployees, isGetLoading: isCurrentEmployeesLoading } = useInitialise(
        `${itemTypes.employees.url}?currentEmployees=true`
    );

    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );

    useEffect(() => {
        setSnackbarVisible(!!deleteResult);
    }, [deleteResult]);

    const permissions = permissionsInfo?.map(permission => {
        const addedByEmployee = employees?.items?.find(e => permission?.grantedByUri === e?.href);
        return {
            id: permission?.id,
            addedByEmployee: addedByEmployee?.fullName,
            privilege: permission?.privilege,
            groupName: permission?.groupName,
            dateGranted: permission?.dateGranted
        };
    });

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
            valueGetter: params => params.row.addedByEmployee || 'N/A'
        },
        {
            field: 'dateGranted',
            headerName: 'Date Granted',
            width: 150,
            valueGetter: ({ value }) => value && moment(value).format('DD MMM YYYY')
        },
        {
            field: 'Group',
            headerName: 'Group',
            width: 200,
            valueGetter: params => params.row.groupName || 'N/A'
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
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typography variant="h4">View an Employee&apos;s Permissions</Typography>
                </Grid>
                <Grid item xs={12}>
                    {(isEmployeesLoading ||
                        isGetLoading ||
                        isCurrentEmployeesLoading ||
                        isDeleteLoading) && <Loading />}
                </Grid>
                <Grid item xs={12}>
                    <Dropdown
                        propertyName="employee choice"
                        items={currentEmployees?.items?.map(employee => ({
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

                {permissions && (
                    <Grid item xs={12}>
                        <DataGrid
                            rows={
                                permissions?.map(e => ({
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

                <SnackbarMessage
                    visible={snackbarVisible}
                    onClose={() => setSnackbarVisible(false)}
                    message="Delete Successful"
                />
            </Grid>
        </Page>
    );
}

export default ViewEmployeesPermission;
