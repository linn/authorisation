import { useState, useEffect } from 'react';

function useInitialise(url, id) {
    const [isGetLoading, setIsGetLoading] = useState(false);
    const [serverError, setServerError] = useState(null);
    const [data, setData] = useState(null);

    useEffect(() => {
        setIsGetLoading(true);
        setData(null);
        setServerError(null);
        const requestParameters = {
            method: 'GET',
            headers: {
                accept: 'application/json'
            }
        };
        console.log(id);
        console.log(url);
        console.log(requestParameters);
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
    }, [url, id]);
    return { isGetLoading, serverError, data };
}

export default useInitialise;
