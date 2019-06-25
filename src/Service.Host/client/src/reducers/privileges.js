﻿import * as actionTypes from '../actions/actionTypes';
//define the initial state
const initialState = { data: [], loading: false };

//define a reducer with an initialized state action
function Privileges(state = initialState, action) {
    switch (action.type) {
        case actionTypes.RECEIVE_PRIVILEGES:
            return { ...state, ...action.payload, newPrivilege: { active: false, name: '' } };
        case actionTypes.UPDATE_NEW_PRIVILEGE_NAME: {
            return { ...state, newPrivilege: { active: true, name: action.data } };
        }
        default:
            return state;
    }
}

export default Privileges;
