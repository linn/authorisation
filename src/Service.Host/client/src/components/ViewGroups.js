import React, { useEffect, useState } from 'react';
import Typography from '@mui/material/Typography';
import { Link, useNavigate } from 'react-router-dom';
import { Loading, CreateButton } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import Page from './Page';

function ViewGroups() {
    const navigate = useNavigate();
    const [groups, setGroups] = useState([]);

    const { data, isLoading } = useInitialise(itemTypes.groups.url);

    useEffect(() => {
        if (data) {
            setGroups(data);
        }
    }, [data]);

    const renderPrivilege = group => (
        <ListItem component={Link} to={`/authorisation/groups/${group.id}`}>
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
                <Grid item xs={8}>
                    <Typography variant="h4">Groups</Typography>
                </Grid>
                <Grid item xs={1}>
                    <CreateButton createUrl="/authorisation/groups/create" />
                </Grid>
                <Grid item xs={3}>
                    <Button
                        variant="contained"
                        onClick={() => navigate('/authorisation/groups/add-individual-member')}
                    >
                        Add Member to Group
                    </Button>
                </Grid>
                <Grid item xs={12}>
                    {isLoading && <Loading />}
                </Grid>
                <Grid item xs={12}>
                    <List>{groups.map(renderPrivilege)}</List>
                </Grid>
            </Grid>
        </Page>
    );
}

export default ViewGroups;
