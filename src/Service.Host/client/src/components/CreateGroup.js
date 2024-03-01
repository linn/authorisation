import React, { useState } from 'react';
import { Loading, InputField } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import Snackbar from '@mui/material/Snackbar';
import usePost from '../hooks/usePost';
import config from '../config';
import history from '../history';
import itemTypes from '../itemTypes';
import Page from './Page';

function CreatePrivilege() {
    const [inputValue, setInputValue] = useState('');

    const { send, isLoading, postResult } = usePost(
        itemTypes.groups.url,
        null,
        { name: inputValue },
        true
    );

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
                    <Typography variant="h4">Create a new Group</Typography>
                    <InputField
                        propertyName="inputValue"
                        label="Name"
                        value={inputValue}
                        onChange={handleFieldChange}
                        fullWidth
                    />

                    <Button variant="contained" onClick={send} disabled={!inputValue}>
                        Save
                    </Button>
                </Grid>
                <Grid>
                    <Snackbar
                        open={!!postResult?.id}
                        autoHideDuration={5000}
                        message="Save Successful"
                    />
                    <Grid item xs={12}>
                        {spinningWheel()}
                    </Grid>
                </Grid>
            </Grid>
        </Page>
    );
}

export default CreatePrivilege;
