import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';

const rootReducer = combineReducers({
    oidc
});

export default rootReducer;