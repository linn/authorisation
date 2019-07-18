import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import Groups from '../components/Groups';
import { getGroups, getGroupsLoading } from '../selectors/groupSelectors';
import { fetchGroups } from '../actions/groupActions';

const mapStateToProps = state => ({
    groups: getGroups(state),
    loading: getGroupsLoading(state)
});

const mapDispatchToProps = dispatch => ({
    initialise: () => {
        dispatch(fetchGroups());
    }
});

export default withRouter(
    connect(
        mapStateToProps,
        mapDispatchToProps
    )(Groups)
);
