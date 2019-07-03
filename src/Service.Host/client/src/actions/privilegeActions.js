import { RSAA } from 'redux-api-middleware';
import config from '../config';
import * as actionTypes from './actionTypes';

export const fetchPrivileges = () => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/privileges/all`,
        method: 'GET',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_PRIVILEGES,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_PRIVILEGES,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) =>
                    res ? `Report - ${res.status} ${res.statusText}` : `Network request failed`
            }
        ]
    }
});

export const fetchPrivilege = id => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/privileges/${id}`,
        method: 'GET',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_PRIVILEGE,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_PRIVILEGE,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) =>
                    res ? `Report - ${res.status} ${res.statusText}` : `Network request failed`
            }
        ]
    }
});

export const fetchPrivilegesForUser = userId => ({
    [RSAA]: {
        //TODO set back to ${config.appRoot}
        endpoint: `https://app.linn.co.uk/authorisation/privileges?Who=/employees/${userId}`,
        method: 'GET',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_PRIVILEGES_FOR_USER,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_PRIVILEGES_FOR_USER,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) =>
                    res ? `Report - ${res.status} ${res.statusText}` : `Network request failed`
            }
        ]
    }
});

export const fetchUsers = () => ({
    // TODO: remove app.linn and use config.appRoot again for end point
    //   endpoint: `${config.appRoot}/authorisation/employees?currentEmployees=true`,
    [RSAA]: {
        endpoint: `https://app.linn.co.uk/employees?currentEmployees=true`,
        method: 'GET',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_USERS,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_USERS,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) =>
                    res ? `Report - ${res.status} ${res.statusText}` : `Network request failed`
            }
        ]
    }
});

export const createPrivilege = (name, active) => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/privileges`,
        method: 'POST',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name, active }),
        types: [
            {
                type: actionTypes.REQUEST_CREATE_PRIVILEGE,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_NEW_PRIVILEGE,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) =>
                    res ? `Report - ${res.status} ${res.statusText}` : `Network request failed`
            }
        ]
    }
});

export const updateNewPrivilege = name => ({
    type: actionTypes.UPDATE_NEW_PRIVILEGE_NAME,
    data: name
});

export const selectUser = id => ({
    type: actionTypes.UPDATE_SELECTED_USER,
    data: id
});

export const updatePrivilegeName = name => ({
    type: actionTypes.UPDATE_PRIVILEGE_NAME,
    data: name
});

export const togglePrivilegeStatus = () => ({
    type: actionTypes.TOGGLE_PRIVILEGE_STATUS
});

export const savePrivilege = (name, active, uri) => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation${uri}`,
        method: 'PUT',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name, active }),
        types: [
            {
                type: actionTypes.REQUEST_SAVE_PRIVILEGE,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_UPDATED_PRIVILEGE,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) =>
                    res ? `Report - ${res.status} ${res.statusText}` : `Network request failed`
            }
        ]
    }
});

export const removePrivilege = uri => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation${uri}`,
        method: 'PUT',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_DELETE_PRIVILEGE,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_PRIVILEGE_DELETED,
                payload: async (action, state, res) => ({ data: await res.json() })
            },
            {
                type: actionTypes.FETCH_ERROR,
                payload: (action, state, res) =>
                    res ? `Report - ${res.status} ${res.statusText}` : `Network request failed`
            }
        ]
    }
});

export const setUpdatedMessageVisible = visible => ({
    type: actionTypes.SET_UPDATE_MESSAGE_VISIBILITY,
    data: visible
});
