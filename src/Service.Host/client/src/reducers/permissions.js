import * as actionTypes from '../actions/actionTypes';

const initialState = { data: [], loading: false };

function Permissions(state = initialState, action) {
    switch (action.type) {
        case actionTypes.REQUEST_PERMISSIONS:
        case actionTypes.REQUEST_PERMISSIONS_FOR_USER:
        case actionTypes.REQUEST_PERMISSIONS_FOR_ASSIGNMENT:
        case actionTypes.REQUEST_CREATE_PERMISSION:
        case actionTypes.REQUEST_DELETE_PERMISSION:
        case actionTypes.REQUEST_DELETE_PERMISSION:
            return { ...state, loading: true };
        case actionTypes.REQUEST_CREATE_PERMISSION:
            return { ...state, loading: true };
        case actionTypes.RECEIVE_PERMISSIONS:
            return {
                ...state,
                ...action.payload,
                newPermission: { privilege: '', grantedByUri: '', granteeByUri: '', groupName: '', dateGranted: ''  },
                loading: false
            };
        case actionTypes.UPDATE_NEW_PERMISSION_NAME: {
            return { ...state, newPermission: { active: true, name: action.data } };
        }
        case actionTypes.RECEIVE_PERMISSIONS_FOR_USER: {
            return {
                ...state,
                ...action.payload,
                newPermission: { privilege: '', grantedByUri: '', granteeByUri: '', groupName: '', dateGranted: ''  },
                loading: false
            };
        }
        case actionTypes.UPDATE_SELECTED_USER: {
            return { ...state, selectedUser: action.data, enableSave: false };
        }
        case actionTypes.RECEIVE_CREATE_PERMISSION: {
            const permission = state.PermissionsForAssignment.find(
                obj => obj.name == action.payload.data.permission
            );
            return {
                ...state,
                data: [...state.data, permission],
                loading: false,
            };
        }
        case actionTypes.SET_PERMISSION_MESSAGE_VISIBILITY: {
            return {
                ...state
            };
        }
        case actionTypes.RECEIVE_DELETE_PERMISSION: {
            return {
                ...state,
                data: state.data.filter(i => i.privilege !== action.payload.data.privilege),
                loading: false
            };
        }

        default:
            return state;
    }
}

export default Permissions;
