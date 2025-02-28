import React, { useEffect, useState } from 'react';
import { useAuth } from 'react-oidc-context';
import Typography from '@mui/material/Typography';
import { Link, useNavigate } from 'react-router-dom';
import {
    Loading,
    CreateButton,
    utilities,
    PermissionIndicator
} from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import config from '../config';
import history from '../history';
import useGet from '../hooks/useGet';
import itemTypes from '../itemTypes';
import Page from './Page';

function ViewGroups() {
    const navigate = useNavigate();
    const [groupswithView, setGroupsWithView] = useState([]);

    const { send: fetchGroups, isLoading, result: groups } = useGet(itemTypes.groups.url, true);

    const auth = useAuth();
    const token = auth.user?.access_token;

    const [hasFetched, setHasFetched] = useState(false);

    const hasViewPermission = utilities.getHref(groups?.[0], 'view');
    const hasCreatePermission = utilities.getHref(groups?.[0], 'create');

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

    const renderPrivilege = group => (
        <ListItem component={Link} to={`/authorisation/groups/${group.id}`}>
            <Typography color="primary">
                {group?.active ? `${group.name} - ACTIVE` : `${group.name} - INACTIVE`}
            </Typography>
        </ListItem>
    );

    groupswithView?.sort((a, b) => {
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
                <Grid item xs={7}>
                    <Typography variant="h4">Groups</Typography>
                </Grid>
                <Grid item xs={1}>
                    <CreateButton
                        disabled={!hasCreatePermission}
                        createUrl="/authorisation/groups/create"
                    />
                </Grid>
                <Grid item xs={3}>
                    <Button
                        variant="contained"
                        disabled={!hasCreatePermission}
                        onClick={() => navigate('/authorisation/groups/add-individual-member')}
                    >
                        Add Member to Group
                    </Button>
                </Grid>
                <Grid item xs={1}>
                    <PermissionIndicator
                        hasPermission={!!hasViewPermission}
                        hasPermissionMessage="You can only view these groups"
                        noPermissionMessage="You do not have permission to view any groups"
                    />
                </Grid>
                <Grid item xs={12}>
                    {isLoading && <Loading />}
                </Grid>
                <Grid item xs={12}>
                    <List>{groupswithView.map(renderPrivilege)}</List>
                </Grid>
            </Grid>
        </Page>
    );
}

export default ViewGroups;
