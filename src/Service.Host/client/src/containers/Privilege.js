import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import Privilege from '../components/Privilege';
import { getPrivilege, getPrivilegeLoading } from '../selectors/privilegeSelectors';
import {
    fetchPrivilege,
    updatePrivilegeName,
    savePrivilege,
    togglePrivilegeStatus
} from '../actions/privilegeActions';

const getId = ownProps => ownProps.match.params.id;

const mapStateToProps = (state, { match }) => ({
    id: match.params.id,
    privilege: getPrivilege(state),
    loading: getPrivilegeLoading(state)
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
        }
    };
};

export default withRouter(
    connect(
        mapStateToProps,
        mapDispatchToProps
    )(Privilege)
);
