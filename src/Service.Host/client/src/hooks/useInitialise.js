import { useState, useEffect } from 'react';

function useInitialise(url) {
    const [isLoading, setIsLoading] = useState(false);
    const [serverError, setServerError] = useState(null);
    const [data, setData] = useState(null);

    useEffect(() => {
        const requestParameters = {
            method: 'GET', // I think this is the part we were missing!
            headers: {
                accept: 'application/json'
            }
        };
        fetch(url, requestParameters)
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
    }, [url]);
    return { isLoading, serverError, data };
}

export default useInitialise;
