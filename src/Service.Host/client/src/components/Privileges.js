import React, { useEffect, useState } from 'react';
import Typography from '@mui/material/Typography';
import { Link } from 'react-router-dom';
import {
    Loading,
    utilities,
    CreateButton,
    PermissionIndicator
} from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import Page from './Page';

function Privileges() {
    const [privilegesWithView, setPrivilegesWithView] = useState([]);

    const { data, isGetLoading } = useInitialise(itemTypes.privileges.url);

    useEffect(() => {
        if (data) {
            const withView = data?.filter(privilege => utilities.getHref(privilege, 'view'));
            setPrivilegesWithView(withView);
        }
    }, [data]);

    const renderPrivilege = privilege => (
        <ListItem component={Link} to={`/authorisation/privileges/${privilege?.id}`}>
            <Typography color="primary">
                {privilege?.active ? `${privilege.name} - ACTIVE` : `${privilege.name} - INACTIVE`}
            </Typography>
        </ListItem>
    );

    privilegesWithView?.sort((a, b) => {
        const fa = a?.name.toLowerCase();
        const fb = b?.name.toLowerCase();

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
                    <Typography variant="h4">Privileges</Typography>
                </Grid>
                <Grid item xs={4}>
                    <CreateButton createUrl="/authorisation/privileges/create" />
                </Grid>
                <Grid item xs={1}>
                    <PermissionIndicator
                        hasPermission={privilegesWithView?.length > 0}
                        hasPermissionMessage="You can only view these privileges"
                        noPermissionMessage="You do not have permission to view any privileges"
                    />
                </Grid>
                <Grid item xs={12}>
                    {isGetLoading && <Loading />}
                </Grid>
                <Grid item xs={12}>
                    <List>{privilegesWithView?.map(renderPrivilege)}</List>
                </Grid>
            </Grid>
        </Page>
    );
}

export default Privileges;
