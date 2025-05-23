﻿import React from 'react';
import Typography from '@mui/material/Typography';
import List from '@mui/material/List';
import { Link } from 'react-router-dom';
import ListItem from '@mui/material/ListItem';
import Page from './Page';
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
                <ListItem component={Link} to="/authorisation/privileges/create">
                    <Typography color="primary">Create a Privilege</Typography>
                </ListItem>
                <ListItem component={Link} to="/authorisation/create-individual-permission">
                    <Typography color="primary">Create Individual Permission</Typography>
                </ListItem>
                <ListItem component={Link} to="/authorisation/create-group-permission">
                    <Typography color="primary">Create Group Permission</Typography>
                </ListItem>
                <ListItem component={Link} to="/authorisation/view-permission-users">
                    <Typography color="primary">View all Employees with a Permission</Typography>
                </ListItem>
                <ListItem component={Link} to="/authorisation/view-individual-permission">
                    <Typography color="primary">View Employee&apos;s Permission</Typography>
                </ListItem>
                <ListItem component={Link} to="/authorisation/groups">
                    <Typography color="primary">Groups</Typography>
                </ListItem>
                <ListItem component={Link} to="/authorisation/groups/create">
                    <Typography color="primary">Create a Group</Typography>
                </ListItem>
                <ListItem component={Link} to="/authorisation/groups/add-individual-member">
                    <Typography color="primary">Add a Member to a Group</Typography>
                </ListItem>
            </List>
        </Page>
    );
}

export default App;
