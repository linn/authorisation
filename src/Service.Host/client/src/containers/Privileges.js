import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import Privileges from '../components/Privileges';
import {
    getPrivileges,
    getNewPrivilegeForCreation,
    getPrivilegesLoading,
    getAllUsers,
    getSelectedUser,
    getShouldShowCreate,
    getPrivilegesForAssignment,
    getCurrentUser,
    getPermissionMessageVisibility,
    getPermissionMessage
} from '../selectors/privilegeSelectors';
import {
    fetchPrivileges,
    createPrivilege,
    updateNewPrivilege,
    fetchUsers,
    selectUser,
    fetchPrivilegesForUser,
    fetchPrivilegesForAssignment,
    createPermission,
    setPrivilegeMessageVisible,
    deletePermission
} from '../actions/privilegeActions';

const mapStateToProps = state => ({
    privileges: getPrivileges(state),
    newprivilege: getNewPrivilegeForCreation(state),
    loading: getPrivilegesLoading(state),
    users: getAllUsers(state),
    selectedUser: getSelectedUser(state),
    showCreate: getShouldShowCreate(state),
    privilegesForAssignment: getPrivilegesForAssignment(state),
    currentUserUri: getCurrentUser(state),
    showPrivilegeMessage: getPermissionMessageVisibility(state),
    permissionMessage: getPermissionMessage(state)
});

const mapDispatchToProps = {
    getAllPrivileges: fetchPrivileges,
    getPrivilegesForUser: fetchPrivilegesForUser,
    getPrivilegesForAssignment: fetchPrivilegesForAssignment,
    getUsers: fetchUsers,
    createPrivilege,
    updateNewPrivilege,
    selectUser,
    createPermission,
    setPrivilegeMessageVisible,
    deletePermission
};

export default withRouter(
    connect(
        mapStateToProps,
        mapDispatchToProps
    )(Privileges)
);
