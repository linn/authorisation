import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import Group from '../components/Group';
import {
    getGroup,
    getGroupLoading,
    getUpdatedMessageVisibility,
    getSaveEnabled,
    getGroupPrivileges,
    getGroupMembers,
    getAllUsers,
    getGroupPotentialPrivileges,
    getCurrentUser,
    getGroupMessage
} from '../selectors/groupSelectors';
import {
    fetchGroup,
    updateGroupName,
    saveGroup,
    toggleGroupStatus,
    setUpdatedMessageVisible,
    fetchPrivilegesForGroup,
    fetchPotentialPrivileges,
    fetchUsers,
    createPermission,
    createNewIndividualMember,
    deletePermission
} from '../actions/groupActions';

const mapStateToProps = (state, { match }) => ({
    id: match.params.id,
    group: getGroup(state),
    loading: getGroupLoading(state),
    showUpdatedMessage: getUpdatedMessageVisibility(state),
    enableSave: getSaveEnabled(state),
    privileges: getGroupPrivileges(state),
    potentialPrivileges: getGroupPotentialPrivileges(state),
    members: getGroupMembers(state),
    users: getAllUsers(state),
    currentUserUri: getCurrentUser(state),
    groupMessage: getGroupMessage(state)
});

const mapDispatchToProps = {
    fetchGroup,
    updateGroupName,
    toggleGroupStatus,
    saveGroup,
    setUpdatedMessageVisible,
    fetchPrivilegesForGroup,
    fetchUsers,
    fetchPotentialPrivileges,
    createPermission,
    createNewIndividualMember,
    deletePermission
};

export default withRouter(
    connect(
        mapStateToProps,
        mapDispatchToProps
    )(Group)
);
