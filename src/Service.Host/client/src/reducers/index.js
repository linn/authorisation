import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import privileges from './privileges';
import privilege from './privilege';

const rootReducer = combineReducers({
    oidc,
    privileges,
    privilege
});

export default rootReducer;
