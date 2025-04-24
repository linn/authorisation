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
        const fetchData = async () => {
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

            try {
                if (!requiresAuth || token) {
                    const response = await fetch(id ? `${url}/${id}` : url, requestParameters);

                    if (!response.ok) {
                        const errorText = await response.text();
                        throw new Error(errorText || 'Network response was not ok');
                    }

                    const json = await response.json();
                    setData(json);
                }
            } catch (error) {
                setServerError(error.message || 'An error occurred');
            } finally {
                setIsGetLoading(false);
            }
        };

        fetchData();
    }, [url, id, token, requiresAuth]);

    return { isGetLoading, serverError, data };
}

export default useInitialise;
