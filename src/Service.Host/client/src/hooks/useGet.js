import { useState } from 'react';
import { useAuth } from 'react-oidc-context';

function useGet(url, requiresAuth = false) {
    const [isLoading, setIsLoading] = useState(false);
    const [serverError, setServerError] = useState(null);
    const [result, setResult] = useState(null);

    let token = '';

    const auth = useAuth();
    if (requiresAuth) {
        token = auth.user?.access_token;
    }

    const send = (id, queryString) => {
        setIsLoading(true);
        setResult(null);
        setServerError(null);

        const headers = {
            accept: 'application/json'
        };
        const requestParameters = {
            method: 'GET',
            headers: requiresAuth ? { ...headers, Authorization: `Bearer ${token}` } : headers
        };
        fetch(id ? `${url}/${id}${queryString}}` : `${url}${queryString}`, requestParameters)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(json => {
                setResult(json);
                setIsLoading(false);
            })
            .catch(error => {
                setServerError(error);
                setIsLoading(false);
            });
    };

    return { send, isLoading, serverError, result };
}

export default useGet;
