import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Typography from '@mui/material/Typography';
import {
    Loading,
    InputField,
    OnOffSwitch,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';
import config from '../config';
import usePut from '../hooks/usePut';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import Page from './Page';

function Privilege() {
    const { id } = useParams();

    const { data, isGetLoading } = useInitialise(itemTypes.privileges.url, id, true);
    const [privilege, setPrivilege] = useState();

    const { send, isPutLoading, errorMessage, putResult } = usePut(
        itemTypes.privileges.url,
        id,
        {
            name: privilege?.name,
            active: privilege?.active,
            ...privilege
        },
        true
    );

    const [snackbarVisible, setSnackbarVisible] = useState(false);

    useEffect(() => {
        setSnackbarVisible(!!putResult);
    }, [putResult]);

    useEffect(() => {
        if (data) {
            setPrivilege(data);
        }
    }, [data]);

    const handleActiveChange = (_, newValue) => {
        setPrivilege({ ...privilege, active: newValue });
    };

    const handleNameFieldChange = (_, newValue) => {
        setPrivilege({ ...privilege, name: newValue });
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                {(isGetLoading || isPutLoading) && <Loading />}
            </Grid>
            <Grid item xs={12}>
                <Typography variant="h4">Edit Privilege</Typography>
            </Grid>
            <Grid item xs={6}>
                <InputField
                    propertyName="inputValue"
                    label="Name"
                    value={privilege?.name}
                    onChange={handleNameFieldChange}
                    fullWidth
                />
                <Typography color="black">
                    Inactive
                    <OnOffSwitch
                        value={privilege?.active}
                        onChange={handleActiveChange}
                        inputProps={{ 'aria-label': 'Switch demo' }}
                        defaultchecked={data?.active}
                    />
                    Active
                </Typography>
                <Button
                    variant="contained"
                    disabled={data?.name === privilege?.name && data?.active === privilege?.active}
                    onClick={() => {
                        send();
                    }}
                >
                    Save
                </Button>

                {errorMessage && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={errorMessage} />
                    </Grid>
                )}

                <SnackbarMessage
                    visible={snackbarVisible}
                    onClose={() => setSnackbarVisible(false)}
                    message="Save Successful"
                />
            </Grid>
        </Page>
    );
}

export default Privilege;
