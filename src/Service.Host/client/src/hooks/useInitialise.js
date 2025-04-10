import { useState, useEffect } from 'react';
import { useAuth } from 'react-oidc-context';

function useInitialise(url, id, requiresAuth = false) {
    const [isGetLoading, setIsGetLoading] = useState(false);
    const [serverError, setServerError] = useState(null);
    const [data, setData] = useState(null);

    let token = '';

    const auth = useAuth();
    if (requiresAuth) {
        token = auth.user?.access_token;
    }

    useEffect(() => {
        setIsGetLoading(true);
        setData(null);
        setServerError(null);

        const headers = {
            accept: 'application/json'
        };

        const requestParameters = {
            method: 'GET',
            headers: requiresAuth ? { ...headers, Authorization: `Bearer ${token}` } : headers
        };

        if (token || !requiresAuth) {
            fetch(id ? `${url}/${id}` : url, requestParameters)
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    return response.json();
                })
                .then(json => {
                    setData(json);
                    setIsGetLoading(false);
                })
                .catch(error => {
                    setServerError(error);
                    setIsGetLoading(false);
                });
        }
    }, [url, id, token, requiresAuth]);
    return { isGetLoading, serverError, data };
}

export default useInitialise;
