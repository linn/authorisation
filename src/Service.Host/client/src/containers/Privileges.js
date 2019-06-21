import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import Privileges from '../components/Privileges';
import { getPrivileges } from '../selectors/privilegeSelectors';

import { fetchPrivileges } from '../actions/privilegeActions';

const mapStateToProps = state => ({
    privileges: getPrivileges(state)
});

const mapDispatchToProps = dispatch => ({
    initialise: () => dispatch(fetchPrivileges())
});

export default withRouter(
    connect(
        mapStateToProps,
        mapDispatchToProps
    )(Privileges)
);
