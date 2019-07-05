import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import Privileges from '../components/Privileges';
import {
    getPrivileges,
    getNewPrivilegeForCreation,
    getPrivilegesLoading,
    getUsers,
    getSelectedUser,
    getShouldShowCreate,
    getPrivilegesForAssignment
} from '../selectors/privilegeSelectors';
import {
    fetchPrivileges,
    createPrivilege,
    updateNewPrivilege,
    fetchUsers,
    selectUser,
    fetchPrivilegesForUser,
    fetchPrivilegesForAssignment
} from '../actions/privilegeActions';

const mapStateToProps = state => ({
    privileges: getPrivileges(state),
    newprivilege: getNewPrivilegeForCreation(state),
    loading: getPrivilegesLoading(state),
    users: getUsers(state),
    selectedUser: getSelectedUser(state),
    showCreate: getShouldShowCreate(state),
    privilegesForAssignment: getPrivilegesForAssignment(state)
});

const mapDispatchToProps = dispatch => ({
    initialise: id => {
        if (id != -1) {
            dispatch(fetchPrivilegesForUser(id));
            dispatch(fetchPrivilegesForAssignment());
        } else {
            dispatch(fetchPrivileges());
        }
        dispatch(fetchUsers());
    },
    createPrivilege: name => {
        dispatch(createPrivilege(name, false));
    },
    updateNewPrivilege: name => {
        dispatch(updateNewPrivilege(name));
    },
    selectUser: id => {
        dispatch(selectUser(id));
        if (id != -1) {
            dispatch(fetchPrivilegesForUser(id));
            dispatch(fetchPrivilegesForAssignment());
        } else {
            dispatch(fetchPrivileges());
        }
    }
});

export default withRouter(
    connect(
        mapStateToProps,
        mapDispatchToProps
    )(Privileges)
);
