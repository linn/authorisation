import * as actionTypes from '../actions/actionTypes';
//define the initial state
const initialState = { data: {}, loading: false };

//define a reducer with an initialized state action
function Group(state = initialState, action) {
    switch (action.type) {
        case actionTypes.REQUEST_GROUP:
            return { ...state, loading: true };
        case actionTypes.REQUEST_SAVE_GROUP: {
            return { ...state, loading: true };
        }
        case actionTypes.RECEIVE_GROUP:
            return { ...state, loading: false, enableSave: false, ...action.payload };
        case actionTypes.RECEIVE_UPDATED_GROUP:
            return { ...state, loading: false, updatedMessageVisibility: true, ...action.payload };
        case actionTypes.UPDATE_GROUP_NAME: {
            return {
                ...state.data,
                data: { ...state.data, name: action.data, enableSave: true }
            };
        }
        case actionTypes.TOGGLE_GROUP_STATUS: {
            return {
                ...state.data,
                data: { ...state.data, active: !state.data.active, enableSave: true }
            };
        }
        case actionTypes.SET_UPDATE_MESSAGE_VISIBILITY: {
            return {
                ...state,
                updatedMessageVisibility: action.data
            };
        }
        default:
            return state;
    }
}

export default Group;
