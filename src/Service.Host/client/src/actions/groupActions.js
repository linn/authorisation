import { RSAA } from 'redux-api-middleware';
import config from '../config';
import * as actionTypes from './actionTypes';

export const fetchGroups = () => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/groups`,
        method: 'GET',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_GROUPS,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_GROUPS,
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

export const fetchGroup = id => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/groups/${id}`,
        method: 'GET',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_GROUP,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_GROUP,
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

export const fetchPrivilegesForGroup = groupId => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/groups/${groupId}/permissions`,
        method: 'GET',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_PRIVILEGES_FOR_GROUP,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_PRIVILEGES_FOR_GROUP,
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

export const fetchMembersForGroup = groupId => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/groups/${groupId}/members`,
        method: 'GET',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_MEMBERS_FOR_GROUP,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_MEMBERS_FOR_GROUP,
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
    [RSAA]: {
        //todo change back to ${config.appRoot}
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

export const createGroup = (name, active) => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/groups`,
        method: 'POST',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name, active }),
        types: [
            {
                type: actionTypes.REQUEST_CREATE_GROUP,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_NEW_GROUP,
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

export const updateNewGroup = name => ({
    type: actionTypes.UPDATE_NEW_PRIVILEGE_NAME,
    data: name
});

export const selectUser = id => ({
    type: actionTypes.UPDATE_SELECTED_USER,
    data: id
});

export const updateGroupName = name => ({
    type: actionTypes.UPDATE_GROUP_NAME,
    data: name
});

export const toggleGroupStatus = () => ({
    type: actionTypes.TOGGLE_GROUP_STATUS
});

export const saveGroup = (name, active, uri) => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/${uri}`,
        method: 'PUT',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name, active }),
        types: [
            {
                type: actionTypes.REQUEST_SAVE_GROUP,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_UPDATED_GROUP,
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

export const removeGroup = uri => ({
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
                type: actionTypes.REQUEST_DELETE_GROUP,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_GROUP_DELETED,
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
