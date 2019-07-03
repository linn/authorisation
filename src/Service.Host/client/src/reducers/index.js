import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import privileges from './privileges';
import privilege from './privilege';
import groups from './groups';
import group from './group';
const rootReducer = combineReducers({
    oidc,
    privileges,
    privilege,
    groups,
    group
});

export default rootReducer;
