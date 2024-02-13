import React, { useState } from 'react';

import Typography from '@mui/material/Typography';
import { Loading, Dropdown } from '@linn-it/linn-form-components-library';
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import Snackbar from '@mui/material/Snackbar';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import usePost from '../hooks/usePost';
import Page from './Page';

function CreatePermission() {
    const [, setDropDownOption] = useState('Choose a Privilege');
    const [privilegeInput, setPrivilegeInput] = useState('');
    const [fullName, setFullName] = useState('');
    const [employeeInput, setEmployeeInput] = useState('');

    const { data: privileges, isGetLoading: privilegesLoading } = useInitialise(
        itemTypes.privileges.url
    );
    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );
    const { data: permissions, isGetLoading: isPermissionsLoading } = useInitialise(
        itemTypes.permissions.url,
        employeeInput
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
        if (privilegesLoading || isEmployeesLoading || isPostLoading || isPermissionsLoading) {
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

    const renderEmployeesPermission = permission => (
        <ListItem>
            <Typography color="primary">{permission.privilege}</Typography>
        </ListItem>
    );
    const handlePrivilegeDropDownChange = (propertyName, newValue) => {
        setDropDownOption(newValue);
        setPrivilegeInput(newValue);
    };

    const handleEmployeeDropDownChange = (propertyName, newValue) => {
        setDropDownOption(newValue);
        setEmployeeInput(newValue);
    };

    function handleButtonClick() {
        send();
    }

    const getFullName = () => {
        console.log(employeeInput);
        employees.forEach(employee => {
            console.log(employee.id);
            if (employeeInput === employee.id) {
                console.log('match');
                setFullName(`${employee?.firstName} ${employee?.lastName}`);
            }
        });
    };

    // const displayUserPermissions = () => {
    //     if (employeeInput) {
    //         if (permissions !== '[]') {
    //             return (
    //                 <Grid item xs={12}>
    //                     <Typography color="black" variant="h5">
    //                         {employeeInput
    //                             ? `Current Permissions available for ${employeeInput.firstName}`
    //                             : ''}
    //                     </Typography>
    //                     <List>{permissions?.map(renderEmployeesPermission)}</List>
    //                 </Grid>
    //             );
    //         }
    //         return (
    //             <Grid item xs={12}>
    //                 <Typography color="black" variant="h5">
    //                     {employeeInput
    //                         ? `${employeeInput.firstName} has no permissions currently`
    //                         : ''}
    //                 </Typography>
    //             </Grid>
    //         );
    //     }
    //     return null;
    // };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                {spinningWheel()}
                {console.log(permissions)};
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
                    items={employees?.items?.map(employee => ({
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

            <Grid container spacing={20}>
                <Grid item xs={12} />
            </Grid>

            <Grid item xs={6}>
                <Button
                    disabled={privilegeInput === '' || employeeInput === ''}
                    variant="contained"
                    onClick={() => {
                        handleButtonClick();
                    }}
                >
                    Save
                </Button>
            </Grid>
            <Grid>
                <Snackbar
                    open={!!postResult?.id}
                    autoHideDuration={5000}
                    message="Save Successful"
                />
                <Grid item xs={12}>
                    {spinningWheel()}
                </Grid>
            </Grid>
            <Grid container spacing={20}>
                <Grid item xs={12} />
            </Grid>
            <Grid item xs={12}>
                {getFullName}
                <Typography color="black" variant="h5">
                    {employeeInput ? `Current Permissions available for ${fullName}` : ''}
                </Typography>
                <List>{permissions?.map(renderEmployeesPermission)}</List>
            </Grid>
            {/* <Grid item xs={12}>
                {displayUserPermissions}
            </Grid> */}
        </Page>
    );
}

export default CreatePermission;
