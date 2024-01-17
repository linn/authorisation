import React from 'react';

import Typography from '@mui/material/Typography';
import { Page } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import config from '../config';
import history from '../history';

function Privileges() {
    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typography variant="h1">Privileges</Typography>
                </Grid>
            </Grid>
        </Page>
    );
}

export default Privileges;
