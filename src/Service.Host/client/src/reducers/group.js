import * as actionTypes from '../actions/actionTypes';
//define the initial state
const initialState = { data: {}, loading: false };

//define a reducer with an initialized state action
function Group(state = initialState, action) {
    switch (action.type) {
        case actionTypes.REQUEST_GROUP:
        case actionTypes.REQUEST_CREATE_GROUP_PERMISSION:
        case actionTypes.REQUEST_SAVE_GROUP:
        case actionTypes.REQUEST_POTENTIAL_GROUP_PRIVILEGES:
        case actionTypes.REQUEST_CREATE_INDIVIDUAL_MEMBER:
        case actionTypes.REQUEST_DELETE_GROUP_PERMISSION:
        case actionTypes.REQUEST_DELETE_MEMBER:
            return { ...state, loading: true };
        case actionTypes.RECEIVE_GROUP:
            return { ...state, loading: false, enableSave: false, ...action.payload };
        case actionTypes.RECEIVE_PRIVILEGES_FOR_GROUP:
            return { ...state, privileges: action.payload.data };
        case actionTypes.RECEIVE_UPDATED_GROUP:
            return {
                ...state,
                enableSave: false,
                loading: false,
                groupMessageVisibility: true,
                groupMessage: 'Save Successful',
                ...action.payload
            };
        case actionTypes.UPDATE_GROUP_NAME: {
            return {
                ...state,
                data: { ...state.data, name: action.data },
                enableSave: true
            };
        }
        case actionTypes.TOGGLE_GROUP_STATUS: {
            return {
                ...state,
                data: { ...state.data, active: !state.data.active },
                enableSave: true
            };
        }
        case actionTypes.SET_UPDATE_MESSAGE_VISIBILITY: {
            return {
                ...state,
                groupMessageVisibility: action.data
            };
        }
        case actionTypes.RECEIVE_CREATE_GROUP_PERMISSION: {
            return {
                ...state,
                privileges: [...state.privileges, action.payload.data],
                loading: false,
                groupMessageVisibility: true,
                groupMessage: 'Permission created'
            };
        }
        case actionTypes.RECEIVE_CREATE_INDIVIDUAL_MEMBER: {
            return {
                ...state,
                loading: false,
                groupMessageVisibility: true,
                groupMessage: 'Member added',
                data: { ...state.data, members: action.payload.data.members }
            };
        }
        case actionTypes.RECEIVE_POTENTIAL_GROUP_PRIVILEGES: {
            return {
                ...state,
                potentialPrivileges: action.payload.data,
                loading: false
            };
        }
        case actionTypes.RECEIVE_DELETE_GROUP_PERMISSION: {
            return {
                ...state,
                privileges: state.privileges.filter(
                    i => i.privilege !== action.payload.data.privilege
                ),
                loading: false,
                groupMessageVisibility: true,
                groupMessage: 'Permission removed'
            };
        }
        case actionTypes.RECEIVE_DELETE_MEMBER: {
            return {
                ...state,
                data: { ...state.data, members: action.payload.data.members },
                loading: false,
                groupMessageVisibility: true,
                groupMessage: 'Member removed'
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

export default Group;
