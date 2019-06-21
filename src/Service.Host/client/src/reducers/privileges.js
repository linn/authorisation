import * as actionTypes from '../actions/actionTypes';
//define the initial state
const initialState = { data: [], loading: false };

//define a reducer with an initialized state action
function Privileges(state = initialState, action) {
    switch (action.type) {
        case actionTypes.RECEIVE_PRIVILEGES:
            return { ...state, ...action.payload };
        default:
            return state;
    }
}

export default Privileges;
