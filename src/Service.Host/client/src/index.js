import React from 'react';
import { createRoot } from 'react-dom/client';
import { SnackbarProvider } from 'notistack';
import { ThemeProvider, StyledEngineProvider } from '@mui/material/styles';
import { linnTheme } from '@linn-it/linn-form-components-library';
import { AuthProvider } from 'react-oidc-context';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import Root from './components/Root';
import 'typeface-roboto';
import config from './config';

const container = document.getElementById('root');
const root = createRoot(container);

// const host = window.location.origin;

const oidcConfig = {
    authority: config.authorityUri,
    client_id: 'app2',
    // client_secret: 'EWxIhT8Geozd2Ng0rAEhwYjS0DS8fpjO=',
    response_type: 'code',
    scope: 'openid profile email associations',
    redirect_uri: `http://localhost:3000/authorisation`,
    post_logout_redirect_uri: 'https://app-sys.linn.co.uk/authentication/Account/Logout',
    onSigninCallback: () => {
        window.location = 'http://localhost:3000/authorisation';
    }
    // userStore: new WebStorageStateStore({ store: window.localStorage })
};

const render = Component => {
    root.render(
        <AuthProvider {...oidcConfig}>
            <StyledEngineProvider injectFirst>
                <ThemeProvider theme={linnTheme}>
                    <SnackbarProvider dense maxSnack={5}>
                        <LocalizationProvider dateAdapter={AdapterMoment} locale="en-GB">
                            <Component />
                        </LocalizationProvider>
                    </SnackbarProvider>
                </ThemeProvider>
            </StyledEngineProvider>
        </AuthProvider>
    );
};

document.body.style.margin = '0';

render(Root);
