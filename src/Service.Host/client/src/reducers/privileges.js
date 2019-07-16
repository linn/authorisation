import * as actionTypes from '../actions/actionTypes';
//define the initial state
const initialState = { data: [], loading: false };

//define a reducer with an initialized state action
function Privileges(state = initialState, action) {
    switch (action.type) {
        case actionTypes.REQUEST_PRIVILEGES:
        case actionTypes.REQUEST_PRIVILEGES_FOR_USER:
        case actionTypes.REQUEST_USERS:
        case actionTypes.REQUEST_PRIVILEGES_FOR_ASSIGNMENT:
        case actionTypes.REQUEST_CREATE_PERMISSION:
            return { ...state, loading: true };
        case actionTypes.RECEIVE_PRIVILEGES_FOR_ASSIGNMENT: {
            return { ...state, loading: false, privilegesForAssignment: action.payload.data };
        }
        case actionTypes.REQUEST_CREATE_PRIVILEGE:
            return { ...state, loading: true };
        case actionTypes.RECEIVE_PRIVILEGES:
            return {
                ...state,
                ...action.payload,
                newPrivilege: { active: false, name: '' },
                loading: false
            };
        case actionTypes.UPDATE_NEW_PRIVILEGE_NAME: {
            return { ...state, newPrivilege: { active: true, name: action.data } };
        }
        case actionTypes.RECEIVE_PRIVILEGES_FOR_USER: {
            return {
                ...state,
                ...action.payload,
                newPrivilege: { active: false, name: '' },
                loading: false
            };
        }
        case actionTypes.RECEIVE_USERS: {
            return {
                ...state,
                users: action.payload,
                newPrivilege: { active: false, name: '' },
                loading: false
            };
        }
        case actionTypes.UPDATE_SELECTED_USER: {
            return { ...state, selectedUser: action.data, enableSave: false };
        }
        default:
            return state;
    }
}

export default Privileges;
