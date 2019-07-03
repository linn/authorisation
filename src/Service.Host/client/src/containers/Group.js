import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import Group from '../components/Group';
import {
    getGroup,
    getGroupLoading,
    getUpdatedMessageVisibility,
    getSaveEnabled
} from '../selectors/groupSelectors';
import {
    fetchGroup,
    updateGroupName,
    saveGroup,
    toggleGroupStatus,
    setUpdatedMessageVisible
} from '../actions/groupActions';

const getId = ownProps => ownProps.match.params.id;

const mapStateToProps = (state, { match }) => ({
    id: match.params.id,
    group: getGroup(state),
    loading: getGroupLoading(state),
    showUpdatedMessage: getUpdatedMessageVisibility(state),
    enableSave: getSaveEnabled(state)
});

const mapDispatchToProps = (dispatch, ownProps) => {
    return {
        initialise: () => dispatch(fetchGroup(getId(ownProps))),
        updateGroupName: name => {
            dispatch(updateGroupName(name));
        },
        toggleGroupStatus: () => {
            dispatch(toggleGroupStatus());
        },
        saveGroup: (name, active, uri) => {
            dispatch(saveGroup(name, active, uri));
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
    )(Group)
);
