import { connect } from 'react-redux';
import { loadUser } from 'redux-oidc';
import Callback from '../components/Callback';
import history from '../history';

function mapDispatchToProps(dispatch) {
    return {
        onSuccess: user => {
            history.push(user.state.redirect);
        }
    };
}

export default connect(
    null,
    mapDispatchToProps
)(Callback);
