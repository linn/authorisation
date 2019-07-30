import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import Groups from '../components/Groups';
import { getGroups, getGroupsLoading, getNewGroupName } from '../selectors/groupSelectors';
import { fetchGroups, createGroup, updateNewGroup } from '../actions/groupActions';

const mapStateToProps = state => ({
    groups: getGroups(state),
    loading: getGroupsLoading(state),
    newGroupName: getNewGroupName(state)
});

const mapDispatchToProps = {
    fetchGroups,
    createGroup,
    updateNewGroup
};

export default withRouter(
    connect(
        mapStateToProps,
        mapDispatchToProps
    )(Groups)
);
