import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import Privilege from '../components/Privilege';
import { getPrivilege } from '../selectors/privilegeSelectors';
import {
    fetchPrivilege,
    updatePrivilegeName,
    savePrivilege,
    togglePrivilegeStatus
} from '../actions/privilegeActions';

const getId = ownProps => ownProps.match.params.id;

const mapStateToProps = (state, { match }) => ({
    id: match.params.id,
    privilege: getPrivilege(state)
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
        savePrivilege: (name, active) => {
            dispatch(savePrivilege(name, active));
        }
    };
};

export default withRouter(
    connect(
        mapStateToProps,
        mapDispatchToProps
    )(Privilege)
);
