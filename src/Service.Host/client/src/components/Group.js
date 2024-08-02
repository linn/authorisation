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
import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import config from '../config';
import usePut from '../hooks/usePut';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import Page from './Page';

function Group() {
    const { id } = useParams();
    const { data, isGetLoading } = useInitialise(itemTypes.groupData.url, id);
    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );
    const [group, setGroup] = useState();

    const { send, isPutLoading, errorMessage, putResult } = usePut(
        itemTypes.groups.url,

        id,
        {
            name: group?.name,
            active: group?.active,
            ...group
        },
        true
    );

    useEffect(() => {
        if (data) {
            setGroup(data);
        }
    }, [data]);

    const [snackbarVisible, setSnackbarVisible] = useState(false);

    useEffect(() => {
        setSnackbarVisible(!!putResult);
    }, [putResult]);

    const handleActiveChange = (_, newValue) => {
        setGroup({ ...group, active: newValue });
    };

    const handleNameFieldChange = (_, newValue) => {
        setGroup({ ...group, name: newValue });
    };

    const getMembers = member => {
        const employee = employees?.items.find(i => member.memberUri === i.href);

        if (!employees?.items.find(i => member.memberUri === i.href)) {
            return (
                <ListItem key={employee.href}>
                    <Typography color="primary">{`${employee.href} - Employee not found`}</Typography>
                </ListItem>
            );
        }
        return (
            <ListItem key={employee.href}>
                <Typography color="primary">{`${employee.firstName} ${employee.lastName}`}</Typography>
            </ListItem>
        );
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                <Typography variant="h4">Edit Group</Typography>
            </Grid>
            <Grid item xs={12}>
                {(isGetLoading || isPutLoading || isEmployeesLoading) && <Loading />}
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
                    disabled={data?.name === group?.name && data?.active === group?.active}
                    onClick={send}
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

                <Grid item xs={12}>
                    <Typography variant="h5">Group Members</Typography>

                    <List>{group?.members?.map(getMembers)}</List>
                </Grid>
            </Grid>
        </Page>
    );
}

export default Group;
