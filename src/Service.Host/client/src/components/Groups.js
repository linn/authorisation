import React, { useEffect, useState } from 'react';
import Typography from '@mui/material/Typography';
import { Link, useNavigate } from 'react-router-dom';
import { Loading } from '@linn-it/linn-form-components-library';
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import Page from './Page';

function Groups() {
    const navigate = useNavigate();

    const [groups, setGroups] = useState([]);
    const { data, isGetLoading } = useInitialise(itemTypes.groups.url, null, true);

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
                <Grid item xs={2}>
                    <Button
                        variant="outlined"
                        onClick={() => navigate(`/authorisation/groups/add-individual-member`)}
                    >
                        Add Member to Group
                    </Button>
                </Grid>
                <Grid item xs={2}>
                    <Button
                        variant="contained"
                        onClick={() => navigate(`/authorisation/groups/create`)}
                    >
                        Create Group
                    </Button>
                </Grid>
                <Grid item xs={12}>
                    {isGetLoading && <Loading />}
                </Grid>
                <Grid item xs={12}>
                    <List>{groups.map(renderPrivilege)}</List>
                </Grid>
            </Grid>
        </Page>
    );
}

export default Groups;
