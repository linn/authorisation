import {
    reducers as sharedLibraryReducers,
    fetchErrorReducer
} from '@linn-it/linn-form-components-library';
import { combineReducers } from 'redux';
import { reducer as oidc } from 'redux-oidc';
import * as itemTypes from '../itemTypes';

const errors = fetchErrorReducer({ ...itemTypes });

const rootReducer = history =>
    combineReducers({
        oidc,
        ...sharedLibraryReducers,
        errors
    });

export default rootReducer;
