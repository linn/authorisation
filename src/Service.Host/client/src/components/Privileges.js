import { React, useState, useEffect } from 'react';

import Typography from '@mui/material/Typography';
import { Page } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import config from '../config';
import history from '../history';

function Privileges() {
    const [privileges, setPrivileges] = useState([]);

    const requestParameters = {
        method: 'GET', // I think this is the part we were missing!
        headers: {
            accept: 'application/json'
        }
    };

    const endpoint = 'https://app.linn.co.uk/authorisation/privileges';

    // this effect will run once the first time this component mounts since the second dependency array parameter is just an empty list
    useEffect(() => {
        fetch(endpoint, requestParameters)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => setPrivileges(data))
            .catch(error => {
                console.error('Fetch error:', error);
            });
    });
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
