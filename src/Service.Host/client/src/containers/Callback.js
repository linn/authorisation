import { connect } from 'react-redux';
import Callback from '../components/Callback';
import { loadUser } from 'redux-oidc';
import history from '../history';

function mapDispatchToProps(dispatch) {
    return {
        onSuccess: user => {
            history.push(user.state.redirect);
        }
    };
}

export default connect(null, mapDispatchToProps)(Callback);
