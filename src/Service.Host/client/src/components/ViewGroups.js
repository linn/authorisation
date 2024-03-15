import React, { useEffect, useState } from 'react';
import Typography from '@mui/material/Typography';
import { Link } from 'react-router-dom';
import { Loading } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import Page from './Page';

function ViewGroups() {
    const [groups, setGroups] = useState([]);

    const { data, isLoading } = useInitialise(itemTypes.groups.url);

    useEffect(() => {
        if (data) {
            setGroups(data);
        }
    }, [data]);

    const spinningWheel = () => {
        if (isLoading) {
            return <Loading />;
        }
        return <div />;
    };

    const renderPrivilege = group => (
        <ListItem component={Link} to={`/authorisation/group/${group.id}`}>
            <Typography color="primary">
                {group?.active ? `${group.name} - ACTIVE` : `${group.name} - INACTIVE`}
            </Typography>
        </ListItem>
    );

    groups.sort((a, b) => {
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

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typography variant="h4">Groups</Typography>
                </Grid>
                <Grid item xs={12}>
                    {spinningWheel()}
                </Grid>
                <Grid item xs={12}>
                    <List>{groups.map(renderPrivilege)}</List>
                </Grid>
                <Grid item xs={6}>
                    <ListItem component={Link} to="/authorisation/groups/create">
                        <Typography color="primary">Create a Group</Typography>
                    </ListItem>
                </Grid>
            </Grid>
        </Page>
    );
}

export default ViewGroups;
