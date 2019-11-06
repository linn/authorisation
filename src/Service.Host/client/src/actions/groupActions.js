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

export const createGroup = name => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/groups`,
        method: 'POST',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name, active: true }),
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
    type: actionTypes.UPDATE_NEW_GROUP_NAME,
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

export const createPermission = (privilege, groupName, currentUserUri) => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/permissions`,
        method: 'POST',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Privilege: privilege,
            GrantedByUri: currentUserUri,
            GroupName: groupName
        }),
        types: [
            {
                type: actionTypes.REQUEST_CREATE_GROUP_PERMISSION,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_CREATE_GROUP_PERMISSION,
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

export const fetchPotentialPrivileges = () => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/privileges/all`,
        method: 'GET',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_POTENTIAL_GROUP_PRIVILEGES,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_POTENTIAL_GROUP_PRIVILEGES,
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

export const createNewIndividualMember = (employeeId, groupId, currentUserUri) => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/groups/${groupId}/members`,
        method: 'POST',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            MemberUri: `/employees/${employeeId}`,
            AddedByUri: currentUserUri
            //GroupName: groupName
        }),
        types: [
            {
                type: actionTypes.REQUEST_CREATE_INDIVIDUAL_MEMBER,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_CREATE_INDIVIDUAL_MEMBER,
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

export const deleteMember = entityUri => ({
    [RSAA]: {
        endpoint: `${config.appRoot}${entityUri}`,
        method: 'DELETE',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_DELETE_MEMBER,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_DELETE_MEMBER,
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

export const deletePermission = (privilege, group, currentUserUri) => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/permissions`,
        method: 'DELETE',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Privilege: privilege,
            GrantedByUri: currentUserUri,
            GroupName: group
        }),
        types: [
            {
                type: actionTypes.REQUEST_DELETE_GROUP_PERMISSION,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_DELETE_GROUP_PERMISSION,
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
