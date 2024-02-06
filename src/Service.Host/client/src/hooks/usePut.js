import { useState } from 'react';
import { useSelector } from 'react-redux';
import { getAccessToken } from '../selectors/getAccessToken';

function usePut(url, id, data, requiresAuth = false) {
    const [isLoading, setIsLoading] = useState(false);
    const [serverError, setServerError] = useState(null);
    const [postResult, setPostResult] = useState(null);

    // for now, until we decide whether we want to keep redux handling oidc state
    const token = useSelector(state => getAccessToken(state));

    const send = () => {
        setIsLoading(true);
        setPostResult(null);
        setServerError(null);

        const headers = {
            accept: 'application/json',
            'Content-Type': 'application/json'
        };
        const requestParameters = {
            method: 'PUT',
            body: JSON.stringify(data),
            headers: requiresAuth ? { ...headers, Authorization: `Bearer ${token}` } : headers
        };
        fetch(id ? `${url}/${id}` : url, requestParameters)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(json => {
                setPostResult(json);
                setIsLoading(false);
            })
            .catch(error => {
                setServerError(error);
                setIsLoading(false);
            });
    };

    return { send, isLoading, serverError, postResult };
}

export default usePut;
