import { useState, useEffect } from 'react';

function useInitialise(url, id) {
    const [isLoading, setIsLoading] = useState(false);
    const [serverError, setServerError] = useState(null);
    const [data, setData] = useState(null);

    useEffect(() => {
        setIsLoading(true);
        setData(null);
        setServerError(null);
        const requestParameters = {
            method: 'GET',
            headers: {
                accept: 'application/json'
            }
        };
        fetch(id ? `${url}/${id}` : url, requestParameters)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(json => {
                setData(json);
                setIsLoading(false);
            })
            .catch(error => {
                setServerError(error);
                setIsLoading(false);
            });
    }, [url, id]);
    return { isLoading, serverError, data };
}

export default useInitialise;
