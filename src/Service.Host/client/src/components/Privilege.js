import React, { useEffect, useState } from 'react';
import { Page, Loading } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import useInitialise from '../hooks/useInitialise';
import config from '../config';
import history from '../history';

function Id() {
    const [privilege, setPrivilege] = useState([]);
    const endpoint = `${config.appRoot}/authorisation/privileges`;
    const { data, isLoading } = useInitialise(endpoint);

    useEffect(() => {
        if (data) {
            data.array.forEach(dataInfo => {
                if (dataInfo.id === desiredId) {
                    setPrivilege(dataInfo);
                }
            });
        }
    }, [data]);

    const spinningWheel = () => {
        if (isLoading) {
            return <Loading />;
        }
        return <div />;
    };

    function DisplayActive() {
        if (privilege.active) return 'Active';

        return `Unactive`;
    }

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={6}>
                    <Typography variant="h2">{privilege.name}</Typography>
                    <p>jfjfjf</p>
                </Grid>
                <Grid item xs={12}>
                    {spinningWheel()}
                </Grid>
                <Grid item xs={12}>
                    <Typography variant="p">{DisplayActive}</Typography>
                </Grid>
            </Grid>
        </Page>
    );
}

export default Id;
