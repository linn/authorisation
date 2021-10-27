import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import Permissions from '../components/Permissions';
import {
    getPermissions,
    getNewPermissionForCreation,
    getPermissionsLoading,
    getSelectedUser,
    getCurrentUser
} from '../selectors/PermissionsSelectors';
import {
    fetchPermissions,
    updateNewPermission,
    selectUser,
    fetchPermissionsForUser,
    createPermission,
    deletePermission
} from '../actions/PermissionsActions';
import getAllUsers from '../selectors/usersSelectors';
import fetchUsers from '../actions/userActions';

const mapStateToProps = state => ({
    permissions: getPermissions(state),
    newPermission: getNewPermissionForCreation(state),
    loading: getPermissionsLoading(state),
    users: getAllUsers(state),
    selectedUser: getSelectedUser(state),
    currentUserUri: getCurrentUser(state)
});

const mapDispatchToProps = {
    getAllPermissions: fetchPermissions,
    getPermissionsForUser: fetchPermissionsForUser,
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
