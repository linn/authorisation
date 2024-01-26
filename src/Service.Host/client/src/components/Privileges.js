import React, { useEffect, useState } from 'react';
import Typography from '@mui/material/Typography';
import { Page, Loading } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import List from '@mui/material/List';
import { Link } from 'react-router-dom';
import ListItem from '@mui/material/ListItem';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import usePost from '../hooks/usePost';

function Privileges() {
    const [privileges, setPrivileges] = useState([]);
    const endpoint = `${config.appRoot}/authorisation/privileges`;

    // note that this function also returns a loading boolean to tell you whether or not the request is loading
    const { data, isLoading } = useInitialise(endpoint);

    const postBody = { name: 'something.new' };
    const { send } = usePost(endpoint, postBody, true);

    // any time data changes, load it into our local privileges state
    useEffect(() => {
        if (data) {
            setPrivileges(data);
        }
    }, [data]);

    const spinningWheel = () => {
        if (isLoading) {
            return <Loading />;
        }
        return <div />;
    };

    const renderPrivilege = privilege => {
        if (privilege.active === true) {
            return (
                <ListItem component={Link} to="/authorisation/Id" key={privilege.id}>
                    <Typography color="black">{privilege.name} - ACTIVE</Typography>
                </ListItem>
            );
        }
        return (
            <ListItem key={privilege.id}>
                <Typography color="black">{privilege.name} - INACTIVE</Typography>
            </ListItem>
        );
    };

    privileges.sort((a, b) => {
        const fa = a.name.toLowerCase();
        const fb = b.name.toLowerCase();

        if (fa < fb) {
            console.log(`fa ${fa} is less than fb ${fb}`);
            return -1;
        }
        if (fa > fb) {
            console.log(`fa ${fa} is greater than fb ${fb}`);
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
                <Grid item xs={12}>
                    {spinningWheel()}
                </Grid>
                <Grid item xs={12}>
                    <List>{privileges.map(renderPrivilege)}</List>
                </Grid>
            </Grid>
        </Page>
    );
}

export default Privileges;
