import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Typography from '@mui/material/Typography';
import { Page, Loading } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise'; // will want to use this hook again to do the data fetching

function Privilege() {
    // below is how you determine the id of the privilege in question if the browser is at location /authorisation/privileges/<id>
    const { id } = useParams();
    const endpoint = `${config.appRoot}/authorisation/privileges/${id}`;

    const { data, isLoading } = useInitialise(endpoint);
    const [privilege, setPrivilege] = useState([]);

    useEffect(() => {
        if (data) {
            setPrivilege(data);
        }
    }, [data]);

    const spinningWheel = () => {
        if (isLoading) {
            return <Loading />;
        }
        return <div />;
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {spinningWheel()}
                </Grid>
                <Grid item xs={12}>
                    <List>
                        <ListItem component={Typography}>
                            <Typography color="black">
                                {privilege.name} {privilege.active ? ' - ACTIVE' : '- INACTIVE'}
                            </Typography>
                        </ListItem>
                    </List>
                </Grid>
            </Grid>
        </Page>
    );
}

export default Privilege;
