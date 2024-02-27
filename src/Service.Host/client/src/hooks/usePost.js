import { useState } from 'react';
import { useAuth } from 'react-oidc-context';

function usePost(url, id, data, requiresAuth = false) {
    const [isLoading, setIsLoading] = useState(false);
    const [serverError, setServerError] = useState(null);
    const [postResult, setPostResult] = useState(null);

    let token = '';

    const auth = useAuth();
    if (requiresAuth) {
        token = auth.user?.access_token;
    }

    const send = () => {
        setIsLoading(true);
        setPostResult(null);
        setServerError(null);

        const headers = {
            accept: 'application/json',
            'Content-Type': 'application/json'
        };
        const requestParameters = {
            method: 'POST',
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

export default usePost;
