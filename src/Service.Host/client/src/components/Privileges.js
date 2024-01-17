import React, { useEffect } from 'react';
import Typography from '@mui/material/Typography';
import { Page } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';

function Privileges() {
    const endpoint = 'http://localhost:51799/authorisation/privileges';
    // use this new hook to fetch data once when this component is first rendered
    const { data } = useInitialise(endpoint);

    // any time data changes, load it into our locall privileges state
    useEffect(() => {
        if (data) {
            // something like setPrivileges(data);
        }
    }, [data]);

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typography variant="h1">{privileges}</Typography>
                </Grid>
            </Grid>
        </Page>
    );
}

export default Privileges;
