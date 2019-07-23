import * as actionTypes from '../actions/actionTypes';
//define the initial state
const initialState = { data: [], loading: false };

function Groups(state = initialState, action) {
    switch (action.type) {
        case actionTypes.REQUEST_GROUPS:
            return { ...state, loading: true };
        case actionTypes.RECEIVE_GROUPS:
            return {
                ...state,
                ...action.payload,
                newGroupName: '',
                loading: false
            };
        case actionTypes.UPDATE_NEW_GROUP_NAME: {
            return { ...state, newGroupName: action.data };
        }
        default:
            return state;
    }
}

export default Groups;
