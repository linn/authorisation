import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import Privileges from '../components/Privileges';
import { getPrivileges, getNewPrivilegeForCreation } from '../selectors/privilegeSelectors';

import { fetchPrivileges, createPrivilege, updateNewPrivilege } from '../actions/privilegeActions';

const mapStateToProps = state => ({
    privileges: getPrivileges(state),
    newprivilege: getNewPrivilegeForCreation(state)
});

const mapDispatchToProps = dispatch => ({
    initialise: () => dispatch(fetchPrivileges()),
    createPrivilege: name => {
        dispatch(createPrivilege(name, false));
    },
    updateNewPrivilege: name => {
        dispatch(updateNewPrivilege(name));
    }
});

export default withRouter(
    connect(
        mapStateToProps,
        mapDispatchToProps
    )(Privileges)
);
