import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import Privilege from '../components/Privilege';
import {
    getPrivilege,
    getPrivilegeLoading,
    getUpdatedMessageVisibility
} from '../selectors/privilegeSelectors';
import {
    fetchPrivilege,
    updatePrivilegeName,
    savePrivilege,
    togglePrivilegeStatus,
    setUpdatedMessageVisible
} from '../actions/privilegeActions';

const getId = ownProps => ownProps.match.params.id;

const mapStateToProps = (state, { match }) => ({
    id: match.params.id,
    privilege: getPrivilege(state),
    loading: getPrivilegeLoading(state),
    showUpdatedMessage: getUpdatedMessageVisibility(state)
});

const mapDispatchToProps = (dispatch, ownProps) => {
    return {
        initialise: () => dispatch(fetchPrivilege(getId(ownProps))),
        updatePrivilegeName: name => {
            dispatch(updatePrivilegeName(name));
        },
        togglePrivilegeStatus: () => {
            dispatch(togglePrivilegeStatus());
        },
        savePrivilege: (name, active, uri) => {
            dispatch(savePrivilege(name, active, uri));
        },
        setUpdatedMessageVisible: visible => {
            dispatch(setUpdatedMessageVisible(visible));
        }
    };
};

export default withRouter(
    connect(
        mapStateToProps,
        mapDispatchToProps
    )(Privilege)
);
