import React from 'react';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';

import config from '../config';
import history from '../history';
import Page from './Page';

function CreatePermission() {
    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typography variant="h4">Create a new Permission</Typography>
                </Grid>
            </Grid>
        </Page>
    );
}

export default CreatePermission;
