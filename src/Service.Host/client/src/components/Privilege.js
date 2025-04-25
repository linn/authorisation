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
import PropTypes from 'prop-types';
import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';
import config from '../config';
import usePut from '../hooks/usePut';
import usePost from '../hooks/usePost';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import Page from './Page';

function Privilege({ creating }) {
    const { id } = useParams();

    const { data, isGetLoading } = useInitialise(itemTypes.privileges.url, id, true);
    const [privilege, setPrivilege] = useState();
    const [errorMessage, setErrorMessage] = useState();

    const {
        send: putSend,
        isPutLoading,
        errorMessage: putErrorMessage,
        putResult
    } = usePut(
        itemTypes.privileges.url,
        id,
        {
            name: privilege?.name,
            active: privilege?.active,
            ...privilege
        },
        true
    );

    const {
        send: postSend,
        isLoading: isPostLoading,
        errorMessage: postErrorMessage,
        postResult
    } = usePost(itemTypes.privileges.url, null, privilege, true);

    const [snackbarVisible, setSnackbarVisible] = useState(false);

    useEffect(() => {
        if (putErrorMessage) {
            setErrorMessage(putErrorMessage);
        } else if (postErrorMessage) {
            setErrorMessage(postErrorMessage);
        }
    }, [putErrorMessage, postErrorMessage]);

    useEffect(() => {
        if (putResult) {
            setSnackbarVisible(!!putResult);
        } else if (postResult) {
            setSnackbarVisible(!!postResult);
        }
    }, [postResult, putResult]);

    useEffect(() => {
        if (!creating && data) {
            setPrivilege(data);
        }
    }, [data, creating]);

    const handleActiveChange = (_, newValue) => {
        setPrivilege({ ...privilege, active: newValue });
    };

    const handleNameFieldChange = (_, newValue) => {
        setPrivilege({ ...privilege, name: newValue });
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                {(isGetLoading || isPutLoading || isPostLoading) && <Loading />}
            </Grid>
            <Grid item xs={12}>
                <Typography variant="h4">
                    {creating ? `Create Privilege` : `Edit Privilege`}
                </Typography>
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
                    onClick={creating ? postSend : putSend}
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

Privilege.propTypes = { creating: PropTypes.bool };
Privilege.defaultProps = { creating: false };

export default Privilege;
