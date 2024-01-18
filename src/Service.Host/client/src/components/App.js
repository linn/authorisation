import React from 'react';
import Typography from '@mui/material/Typography';
import { Page } from '@linn-it/linn-form-components-library';
import List from '@mui/material/List';
import { Link } from 'react-router-dom';

import ListItem from '@mui/material/ListItem';

import config from '../config';
import history from '../history';

function App() {
    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Typography variant="h6">Authorisation</Typography>
            <List>
                <ListItem component={Link} to="/authorisation/privileges">
                    <Typography color="primary">Privileges</Typography>
                </ListItem>
            </List>
        </Page>
    );
}

export default App;
