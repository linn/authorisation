import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import Privilege from '../components/Privilege';
import {
    getPrivilege,
    getPrivilegeLoading,
    getUpdatedMessageVisibility,
    getSaveEnabled
} from '../selectors/privilegeSelectors';
import {
    fetchPrivilege,
    updatePrivilegeName,
    savePrivilege,
    togglePrivilegeStatus,
    setUpdatedMessageVisible,
    fetchEmployeesWithPrivilege,
    fetchUsers
} from '../actions/privilegeActions';

const mapStateToProps = (state, { match }) => ({
    id: match.params.id,
    privilege: getPrivilege(state),
    loading: getPrivilegeLoading(state),
    showUpdatedMessage: getUpdatedMessageVisibility(state),
    enableSave: getSaveEnabled(state)
});

const mapDispatchToProps = {
    fetchPrivilege,
    updatePrivilegeName,
    togglePrivilegeStatus,
    savePrivilege,
    setUpdatedMessageVisible,
    fetchEmployeesWithPrivilege,
    fetchUsers
};

export default withRouter(
    connect(
        mapStateToProps,
        mapDispatchToProps
    )(Privilege)
);
