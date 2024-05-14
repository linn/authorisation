import React, { useState } from 'react';

import Typography from '@mui/material/Typography';
import { Loading, Dropdown, ErrorCard } from '@linn-it/linn-form-components-library';
import Button from '@mui/material/Button';
import Snackbar from '@mui/material/Snackbar';
import Grid from '@mui/material/Grid';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import usePut from '../hooks/usePut';
import itemTypes from '../itemTypes';

import Page from './Page';

function AddIndividualMember() {
    const [employeeInput, setEmployeeInput] = useState('');
    const [groupInput, setGroupInput] = useState('');

    const { data: members, isGetLoading: isMembersLoading } = useInitialise(itemTypes.members.url);
    const { data: groups, isGetLoading: isGroupsLoading } = useInitialise(itemTypes.groupData.url);
    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );

    const { send, isPutLoading, errorMessage, putResult } = usePut(
        itemTypes.members.url,
        groupInput,
        {
            memberUri: employeeInput.memberUri,
            GroupId: groupInput.id,
            ...members
        },
        true
    );

    const spinningWheel = () => {
        if (isGroupsLoading || isEmployeesLoading || isPutLoading || isMembersLoading) {
            return <Loading />;
        }
        return <div />;
    };

    groups?.sort((a, b) => {
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

    const handleEmployeeDropDownChange = (propertyName, newValue) => {
        setEmployeeInput(newValue);
    };

    const handleGroupDropDownChange = (propertyName, newValue) => {
        setGroupInput(newValue);
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                {spinningWheel()}
                <Typography variant="h4">Add an Employee to a Group</Typography>
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

            <Grid item xs={4}>
                <Dropdown
                    propertyName="Group choice"
                    items={groups?.map(group => ({
                        id: group.id,
                        displayText: group?.name
                    }))}
                    required
                    label="Choose a Group"
                    fullWidth
                    onChange={handleGroupDropDownChange}
                    value={groupInput}
                />
            </Grid>

            <Grid item xs={6}>
                <Button
                    disabled={employeeInput === '' || groupInput === ''}
                    variant="contained"
                    onClick={() => {
                        send();
                    }}
                >
                    Save
                </Button>

                {errorMessage && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={errorMessage} />
                    </Grid>
                )}

                <Snackbar
                    open={!!putResult?.id}
                    autoHideDuration={5000}
                    message="Save Successful"
                />
            </Grid>
        </Page>
    );
}

export default AddIndividualMember;
