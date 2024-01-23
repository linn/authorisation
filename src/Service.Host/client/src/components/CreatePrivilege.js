import React, { useState } from 'react';
import { Page, Loading, InputField, SnackbarMessage } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import usePost from '../hooks/usePost';
import config from '../config';
import history from '../history';

function CreatePrivilege() {
    const endpoint = `${config.appRoot}/authorisation/privileges`;
    const { send, isLoading, postResult } = usePost(endpoint, { name: newName }, true);

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typography variant="h4">Create Privilege</Typography>
                </Grid>
            </Grid>
        </Page>
    );
}

export default CreatePrivilege;
