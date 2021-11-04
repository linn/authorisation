import { RSAA } from 'redux-api-middleware';
import config from '../config';
import * as actionTypes from './actionTypes';

export const fetchPermissions = () => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/Permissions/all`,
        method: 'GET',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_PERMISSIONS,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_PERMISSIONS,
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

export const fetchPermission = id => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/Permissions/${id}`,
        method: 'GET',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_PERMISSION,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_PERMISSION,
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

export const fetchPermissionsForUser = userId => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/permissions?granteeUri=/employees/${userId}`,
        method: 'GET',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_PERMISSIONS_FOR_USER,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_PERMISSIONS_FOR_USER,
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

export const fetchUsersForPermission = permissionId => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/permissions/${permissionId}`,
        method: 'GET',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_USERS_FOR_PERMISSION,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_USERS_FOR_PERMISSION,
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

export const createPrivilege = name => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/privileges`,
        method: 'POST',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name }),
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

export const createPermission = (privilege, user, currentUserUri) => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/permissions`,
        method: 'POST',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            privilege: privilege,
            GrantedByUri: currentUserUri,
            GranteeUri: `/employees/${user}`
        }),
        types: [
            {
                type: actionTypes.REQUEST_CREATE_PERMISSION,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_CREATE_PERMISSION,
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

export const deletePermission = (privilegeName, user, currentUserUri) => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/permissions`,
        method: 'DELETE',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            privilege: privilegeName,
            GrantedByUri: currentUserUri,
            GranteeUri: `/employees/${user}`
        }),
        types: [
            {
                type: actionTypes.REQUEST_DELETE_PERMISSION,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_DELETE_PERMISSION,
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

export const savePermission = (name, active, uri) => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation${uri}`,
        method: 'PUT',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name, active }),
        types: [
            {
                type: actionTypes.REQUEST_SAVE_PERMISSION,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_UPDATED_PERMISSION,
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

export const setPermissionMessageVisible = visible => ({
    type: actionTypes.SET_PERMISSION_MESSAGE_VISIBILITY,
    data: visible
});

export const fetchPrivilegesForAssignment = () => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/privileges/all`,
        method: 'GET',
        options: { requiresAuth: true },
        headers: {
            Accept: 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_PRIVILEGES_FOR_ASSIGNMENT,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_PRIVILEGES_FOR_ASSIGNMENT,
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
