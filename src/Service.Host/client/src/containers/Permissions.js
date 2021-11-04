import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import Permissions from '../components/Permissions';
import {
    getPermissions,
    getNewPermissionForCreation,
    getPermissionsLoading,
    getSelectedUser,
    getCurrentUser,
    getPrivilegesForAssignment,
    getNewPrivilegeForCreation,
    getShouldShowCreate
} from '../selectors/PermissionsSelectors';
import {
    fetchPermissions,
    updateNewPermission,
    selectUser,
    fetchPermissionsForUser,
    createPermission,
    deletePermission,
    fetchPrivilegesForAssignment
} from '../actions/PermissionsActions';
import getAllUsers from '../selectors/usersSelectors';
import fetchUsers from '../actions/userActions';

const mapStateToProps = state => ({
    permissions: getPermissions(state),
    newprivilege: getNewPrivilegeForCreation(state),
    loading: getPermissionsLoading(state),
    users: getAllUsers(state),
    selectedUser: getSelectedUser(state),
    showCreate: getShouldShowCreate(state),
    privilegesForAssignment: getPrivilegesForAssignment(state),
    currentUserUri: getCurrentUser(state)
});

const mapDispatchToProps = {
    getAllPermissions: fetchPermissions,
    getPermissionsForUser: fetchPermissionsForUser,
    getPrivilegesForAssignment: fetchPrivilegesForAssignment,
    getUsers: fetchUsers,
    createPermission,
    updateNewPermission,
    selectUser,
    deletePermission
};

export default withRouter(
    connect(
        mapStateToProps,
        mapDispatchToProps
    )(Permissions)
);
