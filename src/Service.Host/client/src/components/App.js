import React from 'react';
import Typography from '@mui/material/Typography';
import { Page } from '@linn-it/linn-form-components-library';
import List from '@mui/material/List';
import { Link } from 'react-router-dom';
import { useAuth } from 'react-oidc-context';

import ListItem from '@mui/material/ListItem';

import config from '../config';
import history from '../history';

function App() {
    const auth = useAuth();

    const authShit = () => {
        console.log(auth);
        switch (auth.activeNavigator) {
            case 'signinSilent':
                return <div>Signing you in...</div>;
            case 'signoutRedirect':
                return <div>Signing you out...</div>;
        }

        if (auth.isLoading) {
            return <div>Loading...</div>;
        }

        if (auth.error) {
            return <div>Oops... {auth.error.message}</div>;
        }

        if (auth.isAuthenticated) {
            return (
                <div>
                    Hello {auth.user?.profile.sub}{' '}
                    <button onClick={() => void auth.removeUser()}>Log out</button>
                </div>
            );
        }
        return <button onClick={() => void auth.signinRedirect()}>Log in</button>;
    };
    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Typography variant="h6">Authorisation</Typography>
            {authShit()}
            {/* <List>
                <ListItem component={Link} to="/authorisation/privileges">
                    <Typography color="primary">Privileges</Typography>
                </ListItem>
                <ListItem component={Link} to="/authorisation/privileges/create">
                    <Typography color="primary">Create Privileges</Typography>
                </ListItem>
            </List> */}
        </Page>
    );
}

export default App;
