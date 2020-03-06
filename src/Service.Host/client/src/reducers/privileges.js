import * as actionTypes from '../actions/actionTypes';

const initialState = { data: [], loading: false };

function Privileges(state = initialState, action) {
    switch (action.type) {
        case actionTypes.REQUEST_PRIVILEGES:
        case actionTypes.REQUEST_PRIVILEGES_FOR_USER:
        case actionTypes.REQUEST_PRIVILEGES_FOR_ASSIGNMENT:
        case actionTypes.REQUEST_CREATE_PERMISSION:
        case actionTypes.REQUEST_DELETE_PERMISSION:
        case actionTypes.REQUEST_DELETE_PRIVILEGE:
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
        case actionTypes.UPDATE_SELECTED_USER: {
            return { ...state, selectedUser: action.data, enableSave: false };
        }
        case actionTypes.RECEIVE_CREATE_PERMISSION: {
            const permission = state.privilegesForAssignment.find(
                obj => obj.name == action.payload.data.privilege
            );
            return {
                ...state,
                data: [...state.data, permission],
                loading: false,
                permissionMessageVisibility: true,
                permissionMessage: 'Permission created'
            };
        }
        case actionTypes.SET_PERMISSION_MESSAGE_VISIBILITY: {
            return {
                ...state,
                permissionMessageVisibility: action.data
            };
        }
        case actionTypes.RECEIVE_DELETE_PERMISSION: {
            return {
                ...state,
                data: state.data.filter(i => i.name !== action.payload.data.privilege),
                loading: false,
                permissionMessageVisibility: true,
                permissionMessage: 'Permission removed'
            };
        }

        case actionTypes.RECEIVE_DELETE_PRIVILEGE: {
            return {
                ...state,
                data: state.data.filter(i => i.name !== action.payload.data.name),
                loading: false,
                permissionMessageVisibility: true,
                permissionMessage: `Privilege "${action.payload.data.name}" removed`
            };
        }

        default:
            return state;
    }
}

export default Privileges;
