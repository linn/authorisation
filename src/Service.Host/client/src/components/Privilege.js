import React from 'react';
import { Page } from '@linn-it/linn-form-components-library';
import { useParams } from 'react-router-dom';
import Grid from '@mui/material/Grid';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise'; // will want to use this hook again to do the data fetching

function Privilege() {
    // below is how you determine the id of the privilege in question if the browser is at location /authorisation/privileges/<id>
    const { id } = useParams();
    const endpoint = `${config.appRoot}/authorisation/privileges/${id}`;

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}></Grid>
        </Page>
    );
}

export default Privilege;
