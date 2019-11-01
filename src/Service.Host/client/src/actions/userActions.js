import { RSAA } from 'redux-api-middleware';
import config from '../config';
import * as actionTypes from './actionTypes';

const fetchUsers = () => ({
    [RSAA]: {
        //${config.appRoot} can be changed to https://app-sys.linn.co.uk to see names locally
        endpoint: `${config.appRoot}/employees?currentEmployees=true`,
        method: 'GET',
        options: { requiresAuth: false },
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        },
        types: [
            {
                type: actionTypes.REQUEST_ALL_USERS,
                payload: {}
            },
            {
                type: actionTypes.RECEIVE_ALL_USERS,
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

export default fetchUsers;
