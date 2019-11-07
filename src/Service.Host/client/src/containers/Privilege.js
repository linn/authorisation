import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import Privilege from '../components/Privilege';
import {
    getPrivilege,
    getPrivilegeLoading,
    getUpdatedMessageVisibility,
    getSaveEnabled,
    getPermissionsForPrivilege
} from '../selectors/privilegeSelectors';
import {
    fetchPrivilege,
    updatePrivilegeName,
    savePrivilege,
    togglePrivilegeStatus,
    setUpdatedMessageVisible,
    fetchUsersForPrivilege
} from '../actions/privilegeActions';
import getAllUsers from '../selectors/usersSelectors';
import fetchUsers from '../actions/userActions';

const mapStateToProps = (state, { match }) => ({
    id: match.params.id,
    privilege: getPrivilege(state),
    loading: getPrivilegeLoading(state),
    showUpdatedMessage: getUpdatedMessageVisibility(state),
    enableSave: getSaveEnabled(state),
    usersWithPermission: getPermissionsForPrivilege(state),
    allUsers: getAllUsers(state)
});

const mapDispatchToProps = {
    fetchPrivilege,
    updatePrivilegeName,
    togglePrivilegeStatus,
    savePrivilege,
    setUpdatedMessageVisible,
    fetchUsersForPrivilege,
    fetchUsers
};

export default withRouter(
    connect(
        mapStateToProps,
        mapDispatchToProps
    )(Privilege)
);
