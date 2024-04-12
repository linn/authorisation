import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Typography from '@mui/material/Typography';
import { Loading, InputField, OnOffSwitch } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Snackbar from '@mui/material/Snackbar';
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
    const [group, setGroup] = useState();

    const { send, isPutLoading, putResult } = usePut(
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

    const spinningWheel = () => {
        if (isGetLoading || isPutLoading) {
            return <Loading />;
        }
        return <div />;
    };

    const handleActiveChange = (_, newValue) => {
        setGroup({ ...group, active: newValue });
    };

    const handleNameFieldChange = (_, newValue) => {
        setGroup({ ...group, name: newValue });
    };

    const renderMembers = member => (
        <ListItem>
            <Typography color="primary">{member.memberUri}</Typography>
        </ListItem>
    );

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                {spinningWheel()}
            </Grid>
            <Grid item xs={12}>
                <Typography variant="h4">Edit Group</Typography>
            </Grid>
            {console.log(group)}
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
                <Snackbar
                    open={!!putResult?.id}
                    autoHideDuration={5000}
                    message="Save Successful"
                />

                <Grid item xs={12}>
                    <List>{group?.members?.map(renderMembers)}</List>
                </Grid>
            </Grid>
        </Page>
    );
}

export default Group;
