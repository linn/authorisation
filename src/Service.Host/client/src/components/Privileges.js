import React, { useEffect, useState } from 'react';
import Typography from '@mui/material/Typography';
import { Page } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';

function Privileges() {
    const [privileges, setPrivileges] = useState([]);
    const endpoint = 'http://localhost:51799/authorisation/privileges';

    // use this new hook to fetch data once when this component is first rendered
    const { data } = useInitialise(endpoint);

    // any time data changes, load it into our local privileges state
    useEffect(() => {
        if (data) {
            setPrivileges(data);
        }
    }, [data]);

    const renderPrivilege = privilege => {
        if (privilege.active === true) {
            return (
                <ListItem>
                    <Typography color="black">{privilege.name} - ACTIVE</Typography>
                </ListItem>
            );
            // eslint-disable-next-line no-else-return
        } else {
            return (
                <ListItem>
                    <Typography color="black">{privilege.name} - INACTIVE</Typography>
                </ListItem>
            );
        }
    };

    privileges.sort((a, b) => {
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
                    <Typography variant="h4">Privileges</Typography>
                    <List>{privileges.map(renderPrivilege)}</List>
                </Grid>
            </Grid>
        </Page>
    );
}

export default Privileges;
