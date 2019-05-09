import React from 'react';
import { CallbackComponent } from 'redux-oidc';
import userManager from '../helpers/userManager';
import { Loading } from './common/Loading';

class Callback extends React.Component {
    render() {
        // TODO: handle error case appropriately
        return (
            <CallbackComponent
                userManager={userManager}
                successCallback={this.props.onSuccess}
                errorCallback={err => console.error(err)}
            >
                <Loading />
            </CallbackComponent>
        );
    }
}

export default Callback;
