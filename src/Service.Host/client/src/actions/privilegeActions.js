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
export const createPrivilege = (name, active) => ({
    [RSAA]: {
        endpoint: `${config.appRoot}/authorisation/privileges?Name=${name}&Active=${active}`,
        method: 'POST',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json'
        },
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

// export function updateNewPrivilegeName(name) {
//     return { type: actionTypes.UPDATE_NEW_PRIVILEGE_NAME, data: name };
// }

export const updateNewPrivilegeName = name => ({
    type: actionTypes.UPDATE_NEW_PRIVILEGE_NAME,
    data: name
});

// export const editPrivilege = id => ({
//     [RSAA]: {
//         endpoint: `${config.appRoot}/authorisation/privileges/`,
//         method: 'GET',
//         options: { requiresAuth: false },
//         headers: {
//             Accept: 'application/json'
//         },
//         body: { name: 'IainTest', active: true },
//         types: [
//             {
//                 type: actionTypes.REQUEST_PRIVILEGE,
//                 payload: {}
//             },
//             {
//                 type: actionTypes.RECEIVE_PRIVILEGE,
//                 payload: async (action, state, res) => ({ data: await res.json() })
//             },
//             {
//                 type: actionTypes.FETCH_ERROR,
//                 payload: (action, state, res) =>
//                     res ? `Report - ${res.status} ${res.statusText}` : `Network request failed`
//             }
//         ]
//     }
// });
