import React, { useState } from 'react';

import Typography from '@mui/material/Typography';
import { Loading, Dropdown } from '@linn-it/linn-form-components-library';
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import Snackbar from '@mui/material/Snackbar';
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
        itemTypes.privileges.url
    );
    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );

    const { send, isPostLoading, postResult } = usePost(
        itemTypes.permissions.url,
        null,
        {
            Privilegeid: privilegeInput,
            GranteeUri: `/employees/${employeeInput}`
        },
        true
    );

    const spinningWheel = () => {
        if (privilegesLoading || isEmployeesLoading || isPostLoading) {
            return <Loading />;
        }
        return <div />;
    };

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
                {spinningWheel()}
                <Typography variant="h4">Create a new Permission</Typography>
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

            <Snackbar
                open={!!postResult?.privilegeId}
                autoHideDuration={5000}
                message="Save Successful"
            />
        </Page>
    );
}

export default CreateIndividualPermission;
