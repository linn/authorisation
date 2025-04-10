import React, { useState, useEffect } from 'react';

import Typography from '@mui/material/Typography';
import {
    Loading,
    Dropdown,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import usePost from '../hooks/usePost';
import Page from './Page';

function CreateIndividualPermission() {
    const [privilegeInput, setPrivilegeInput] = useState('');
    const [employeeInput, setEmployeeInput] = useState('');

    const { data: privileges, isGetLoading: privilegesLoading } = useInitialise(
        itemTypes.privileges.url,
        null,
        true
    );
    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );

    const { send, isPostLoading, errorMessage, postResult } = usePost(
        itemTypes.permissions.url,
        null,
        {
            Privilegeid: privilegeInput,
            GranteeUri: `/employees/${employeeInput}`
        },
        true
    );

    const [snackbarVisible, setSnackbarVisible] = useState(false);

    useEffect(() => {
        setSnackbarVisible(!!postResult);
    }, [postResult]);

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

    const handlePrivilegeDropDownChange = (propertyName, newValue) => {
        setPrivilegeInput(newValue);
    };

    const handleEmployeeDropDownChange = (propertyName, newValue) => {
        setEmployeeInput(newValue);
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                <Typography variant="h4">Create a new Individual Permission</Typography>
            </Grid>
            <Grid item xs={12}>
                {(privilegesLoading || isEmployeesLoading || isPostLoading) && <Loading />}
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
                    onChange={handlePrivilegeDropDownChange}
                    value={privilegeInput}
                />
            </Grid>
            <Grid item xs={4}>
                <Dropdown
                    propertyName="employee choice"
                    items={employees?.items.map(employee => ({
                        id: employee.id,
                        displayText: `${employee?.firstName} ${employee?.lastName}`
                    }))}
                    required
                    label="Choose an Employee"
                    fullWidth
                    onChange={handleEmployeeDropDownChange}
                    value={employeeInput}
                />
            </Grid>

            <Grid item xs={6}>
                <Button
                    disabled={privilegeInput === '' || employeeInput === ''}
                    variant="contained"
                    onClick={() => {
                        send();
                    }}
                >
                    Save
                </Button>
            </Grid>

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
        </Page>
    );
}

export default CreateIndividualPermission;
