import React from 'react';
import { CallbackComponent } from 'redux-oidc';
import userManager from '../helpers/userManager';

class Callback extends React.Component {
    render() {
        // TODO: handle error case appropriately
        return (
            <CallbackComponent
                userManager={userManager}
                successCallback={this.props.onSuccess}
                errorCallback={err => console.error(err)}
            >
            </CallbackComponent>
        );
    }
}

export default Callback;
