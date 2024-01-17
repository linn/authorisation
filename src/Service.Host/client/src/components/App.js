import React from 'react';
import Typography from '@mui/material/Typography';
import { Page } from '@linn-it/linn-form-components-library';
import List from '@mui/material/List';
import { Link } from 'react-router-dom';

import ListItem from '@mui/material/ListItem';

import config from '../config';
import history from '../history';
import Privileges from './Priviliges';

function App() {
    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Typography variant="h6">Authorisation</Typography>
            <List>
                <ListItem component={Link} to="/authorisation/example-component">
                    <Typography color="primary">Example Link</Typography>
                </ListItem>
                <ListItem component={Link} to="/authorisation/example-component">
                    <Typography color="primary">Example Link 2</Typography>
                </ListItem>
                <ListItem component={Link} to="/authorisation/example-component">
                    <Typography color="primary">Example Link 3</Typography>
                </ListItem>
                <ListItem component={Link} to="/authorisation/example-component">
                    <Typography color="primary">{privileges}</Typography>
                </ListItem>
                <ListItem component={Link} to="/authorisation/example-component">
                    <Typography color="primary">Example Link 5</Typography>
                </ListItem>
            </List>

            <Privileges privileges ={privileges}/>
        </Page>
    );
}

export default App;
