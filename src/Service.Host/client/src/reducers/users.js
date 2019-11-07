import * as actionTypes from '../actions/actionTypes';

const initialState = { data: {}, loading: false };

function Users(state = initialState, action) {
    switch (action.type) {
        case actionTypes.REQUEST_ALL_USERS:
            return { ...state, loading: true };
        case actionTypes.RECEIVE_ALL_USERS: {
            return {
                ...state,
                allUsers: action.payload.data.items
            };
        }
        case actionTypes.FETCH_ERROR: {
            return {
                ...state,
                loading: false
            };
        }
        default:
            return state;
    }
}

export default Users;
