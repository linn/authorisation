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
    const [privileges, setPrivileges] = useState([]);

    const { data, isGetLoading } = useInitialise(itemTypes.privileges.url);

    const hasViewPermission = utilities.getHref(data?.[0], 'view');

    useEffect(() => {
        if (data) {
            setPrivileges(data);
        }
    }, [data]);

    const renderPrivilege = privilege => (
        <ListItem component={Link} to={`/authorisation/privileges/${privilege.id}`}>
            <Typography color="primary">
                {privilege?.active ? `${privilege.name} - ACTIVE` : `${privilege.name} - INACTIVE`}
            </Typography>
        </ListItem>
    );

    privileges?.sort((a, b) => {
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
                <Grid item xs={12}>
                    <Typography variant="h4">Privileges</Typography>
                </Grid>
                <Grid item xs={4}>
                    <CreateButton createUrl="/authorisation/privileges/create" />
                </Grid>
                <Grid item xs={1}>
                    <PermissionIndicator
                        hasPermission={hasViewPermission}
                        hasPermissionMessage="You have view cashbook permissions"
                        noPermissionMessage="You do not have view cashbook permissions"
                    />
                </Grid>
                <Grid item xs={12}>
                    {isGetLoading && <Loading />}
                </Grid>
                <Grid item xs={12}>
                    <List>{privileges.map(renderPrivilege)}</List>
                </Grid>
            </Grid>
        </Page>
    );
}

export default Privileges;
