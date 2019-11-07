import * as actionTypes from '../actions/actionTypes';

const initialState = { data: {}, loading: false };

function Privilege(state = initialState, action) {
    switch (action.type) {
        case actionTypes.REQUEST_PRIVILEGE:
            return { ...state, loading: true };
        case actionTypes.REQUEST_USERS_FOR_PRIVILEGE:
            return { ...state, loading: true };
        case actionTypes.REQUEST_SAVE_PRIVILEGE: {
            return { ...state, loading: true };
        }
        case actionTypes.RECEIVE_PRIVILEGE:
            return { ...state, loading: false, enableSave: false, ...action.payload };
        case actionTypes.RECEIVE_USERS_FOR_PRIVILEGE:
            return {
                ...state,
                loading: false,
                enableSave: false,
                permissions: action.payload.data
            };
        case actionTypes.RECEIVE_UPDATED_PRIVILEGE:
            return {
                ...state,
                loading: false,
                enableSave: false,
                updatedMessageVisibility: true,
                ...action.payload
            };
        case actionTypes.UPDATE_PRIVILEGE_NAME: {
            return {
                ...state,
                data: { ...state.data, name: action.data },
                enableSave: true
            };
        }
        case actionTypes.TOGGLE_PRIVILEGE_STATUS: {
            return {
                ...state,
                data: { ...state.data, active: !state.data.active },
                enableSave: true
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

export default Privilege;
