import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Typography from '@mui/material/Typography';
import {
    Loading,
    InputField,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';
import config from '../config';
import useDelete from '../hooks/useDelete';
import history from '../history';
import useGet from '../hooks/useGet';
import itemTypes from '../itemTypes';
import Page from './Page';

function Permission() {
    const { id } = useParams();
    const { permission, isGetLoading } = useGet(itemTypes.permissions.url, id);
    const [privilege, setPrivilege] = useState();

    const { send, isPutLoading, errorMessage, putResult } = useDelete(
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
        if (permission) {
            setPrivilege(permission);
        }
    }, [permission]);

    console.log(permission);

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                {(isGetLoading || isPutLoading) && <Loading />}
            </Grid>
            <Grid item xs={12}>
                <Typography variant="h4">Edit Privilege</Typography>
            </Grid>
            <Grid item xs={12}>
                <Grid item xs={6}>
                    <InputField
                        propertyName="inputValue"
                        label="Employee"
                        value={permission?.privilegeId}
                        disabled
                        fullWidth
                    />
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        propertyName="inputValue"
                        label="Privilege"
                        value={permission?.GranteeUri}
                        disabled
                        fullWidth
                    />
                </Grid>
                <Grid item xs={6}>
                    <Button
                        variant="contained"
                        onClick={() => {
                            send();
                        }}
                    >
                        Save
                    </Button>
                    <Button
                        variant="outlined"
                        onClick={() => {
                            send();
                        }}
                    >
                        Delete
                    </Button>
                </Grid>

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

export default Permission;
