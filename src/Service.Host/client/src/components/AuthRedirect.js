import React from 'react';
import Page from './Page';
import config from '../config';
import history from '../history';

function AuthRedirect() {
    history.push(sessionStorage.getItem('auth:redirect'));
    sessionStorage.removeItem('auth:redirect');

    return <div />;
}

export default AuthRedirect;
