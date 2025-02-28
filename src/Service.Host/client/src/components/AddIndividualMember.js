import React, { useState, useEffect } from 'react';
import { useAuth } from 'react-oidc-context';
import Typography from '@mui/material/Typography';
import {
    Loading,
    Dropdown,
    ErrorCard,
    utilities,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import usePost from '../hooks/usePost';
import useGet from '../hooks/useGet';
import itemTypes from '../itemTypes';
import Page from './Page';

function AddIndividualMember() {
    const [employeeInput, setEmployeeInput] = useState('');
    const [groupInput, setGroupInput] = useState('');
    const [groupsWithView, setGroupsWithView] = useState([]);

    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );

    const {
        send: fetchGroups,
        isLoading: isGroupsLoading,
        result: groups
    } = useGet(itemTypes.groups.url, true);

    const auth = useAuth();
    const token = auth.user?.access_token;

    const [hasFetched, setHasFetched] = useState(false);

    if (!hasFetched && token) {
        fetchGroups();
        setHasFetched(true);
    }

    useEffect(() => {
        if (groups) {
            const withView = groups.filter(group => utilities.getHref(group, 'view'));
            setGroupsWithView(withView);
        }
    }, [groups]);

    const { send, isPostLoading, errorMessage, postResult } = usePost(
        itemTypes.members.url,
        null,
        {
            memberUri: `/employees/${employeeInput}`,
            GroupId: groupInput
        },
        true
    );

    const [snackbarVisible, setSnackbarVisible] = useState(false);

    useEffect(() => {
        setSnackbarVisible(!!postResult);
    }, [postResult]);

    groupsWithView?.sort((a, b) => {
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
                <Typography variant="h4">Add an Employee to a Group</Typography>
            </Grid>
            <Grid item xs={12}>
                {(isGroupsLoading || isEmployeesLoading || isPostLoading) && <Loading />}
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
                    items={groupsWithView?.map(group => ({
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

            <Grid item xs={12}>
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

                <SnackbarMessage
                    visible={snackbarVisible}
                    onClose={() => setSnackbarVisible(false)}
                    message="Save Successful"
                />
            </Grid>
        </Page>
    );
}

export default AddIndividualMember;
