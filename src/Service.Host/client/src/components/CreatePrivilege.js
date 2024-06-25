import React, { useState } from 'react';
import {
    Loading,
    InputField,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import usePost from '../hooks/usePost';
import config from '../config';
import history from '../history';
import itemTypes from '../itemTypes';
import Page from './Page';

function CreatePrivilege() {
    const [inputValue, setInputValue] = useState('');
    const { send, isLoading, errorMessage, postResult } = usePost(
        itemTypes.privileges.url,
        null,
        { name: inputValue },
        true
    );
    const [snackbarVisible, setSnackbarVisible] = useState(!!postResult?.id);
    console.log(snackbarVisible);

    const spinningWheel = () => {
        if (isLoading) {
            return <Loading />;
        }
        return <div />;
    };

    const handleFieldChange = (propertyName, newValue) => {
        setInputValue(newValue);
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typography variant="h4">Create a new Privilege</Typography>
                    <InputField
                        propertyName="inputValue"
                        label="Name"
                        value={inputValue}
                        onChange={handleFieldChange}
                        fullWidth
                    />

                    <Button
                        variant="contained"
                        onClick={() => {
                            send();
                        }}
                        disabled={!inputValue}
                    >
                        Save
                    </Button>

                    {errorMessage && (
                        <Grid item xs={12}>
                            <ErrorCard errorMessage={errorMessage} />
                        </Grid>
                    )}
                </Grid>
                <Grid>
                    <Grid item xs={12}>
                        {spinningWheel()}
                    </Grid>
                    <SnackbarMessage
                        open={!!postResult?.id}
                        onClose={() => setSnackbarVisible(false)}
                        message="Save Successful"
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

export default CreatePrivilege;
