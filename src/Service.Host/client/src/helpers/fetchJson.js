import es6promise from 'es6-promise';
import fetch from 'isomorphic-fetch';

es6promise.polyfill();

async function checkStatus(response) {
    if (response.ok) {
        return response;
    } else {
        let error = new Error(response.statusText);
        try {
            const jsonError = await response.json();
            if (jsonError && jsonError.errorMessage) {
                error = new Error(jsonError.errorMessage);
            }
        } catch (e) {
            error = new Error(response.statusText);
            error.response = response;
            throw error;
        }

        error.response = response;
        throw error;
    }
}

const defaultFetchOptions = {
    headers: {},
    authenticated: false
}

export const fetchJson = async (url, options = defaultFetchOptions) => {

    let headers = {
        'Accept': 'application/json',
        ...options.headers
    };

    if (options.authenticated && options.accessToken) {
        headers = { ...headers, 'Authorization': `Bearer ${options.accessToken}` };
    }
    try {
        let response = await fetch(url,
            {
                method: 'GET',
                headers: headers,
                credentials: 'same-origin'
            });

        response = await checkStatus(response);

        return await response.json();

    } catch (e) {
        const er = new Error(e.message);
        er.url = url;
        throw er;
    }
}

export const postJson = async (url, body, options = defaultFetchOptions) => {

    let headers = {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        ...options.headers
    };

    if (options.authenticated && options.accessToken) {
        headers = { ...headers, 'Authorization': `Bearer ${options.accessToken}` };
    }
    try {
        let response;

        response = await fetch(url,
            {
                method: 'POST',
                headers: headers,
                body: body && typeof body !== 'string' ? JSON.stringify(body) : '',
                credentials: 'same-origin'
            });

        if (response.ok && response.status === 204) {
            return null;
        }

        response = await checkStatus(response);

        if (!options.dontExpectResponse) {
            return await response.json();
        }
    } catch (e) {
        const er = new Error(e.message);
        er.url = url;
        throw er;
    }
}

export const putJson = async (url, body, options = defaultFetchOptions) => {

    let headers = {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        ...options.headers
    };

    if (options.authenticated && options.accessToken) {
        headers = { ...headers, 'Authorization': `Bearer ${options.accessToken}` };
    }
    try {
        let response;

        response = await fetch(url,
            {
                method: 'PUT',
                headers: headers,
                body: body && typeof body !== 'string' ? JSON.stringify(body) : '',
                credentials: 'same-origin'
            });

        if (response.ok && response.status === 204) {
            return null;
        }

        response = await checkStatus(response);

        if (!options.dontExpectResponse) {
            return await response.json();
        }
    } catch (e) {
        const er = new Error(e.message);
        er.url = url;
        throw er;
    }
}

export const deleteJson = async (url, options = defaultFetchOptions) => {

    let headers = {
        'Accept': 'application/json',
        ...options.headers
    };

    if (options.authenticated && options.accessToken) {
        headers = { ...headers, 'Authorization': `Bearer ${options.accessToken}` };
    }
    try {
        let response = await fetch(url,
            {
                method: 'DELETE',
                headers: headers,
                credentials: 'same-origin'
            });

        response = await checkStatus(response);

        return await response.json();

    } catch (e) {
        const er = new Error(e.message);
        er.url = url;
        throw er;
    }
}