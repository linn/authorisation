﻿import { createStore, applyMiddleware, compose } from 'redux';
import { apiMiddleware as api } from 'redux-api-middleware';
import thunkMiddleware from 'redux-thunk';
import reducer from './reducers';
import authorization from './middleware/authorization';
import itemCreated from './middleware/itemCreated';

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const middleware = [authorization, api, itemCreated, thunkMiddleware];

const configureStore = initialState => {
    const enhancers = composeEnhancers(applyMiddleware(...middleware));
    const store = createStore(reducer, initialState, enhancers);

    return store;
};

export default configureStore;
