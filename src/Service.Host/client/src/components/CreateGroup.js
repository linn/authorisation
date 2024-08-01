import React, { useState, useEffect } from 'react';
import {
    Loading,
    InputField,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import { Link } from 'react-router-dom';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import ListItem from '@mui/material/ListItem';
import { List } from '@mui/material';
import usePost from '../hooks/usePost';
import config from '../config';
import history from '../history';
import itemTypes from '../itemTypes';
import Page from './Page';

function CreatePrivilege() {
    const [inputValue, setInputValue] = useState('');

    const { send, isLoading, errorMessage, postResult } = usePost(
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

    const [snackbarVisible, setSnackbarVisible] = useState(false);

    useEffect(() => {
        setSnackbarVisible(!!postResult);
    }, [postResult]);

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

                    {errorMessage && (
                        <Grid item xs={12}>
                            <ErrorCard errorMessage={errorMessage} />
                        </Grid>
                    )}
                </Grid>
                <Grid>
                    <SnackbarMessage
                        visible={snackbarVisible}
                        onClose={() => setSnackbarVisible(false)}
                        message="Save Successful"
                    />
                    <Grid item xs={12}>
                        {spinningWheel()}
                    </Grid>
                    <Grid item xs={12}>
                        <List>
                            <ListItem
                                component={Link}
                                to="/authorisation/groups/add-individual-member"
                            >
                                <Typography color="primary">Add a Member to a Group</Typography>
                            </ListItem>
                        </List>
                    </Grid>
                </Grid>
            </Grid>
        </Page>
    );
}

export default CreatePrivilege;
