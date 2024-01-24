import React, { useState } from 'react';
import { Page, Loading, InputField, SnackbarMessage } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import Privileges from './Privileges';
import usePost from '../hooks/usePost';
import config from '../config';
import history from '../history';

function CreatePrivilege() {
    const [inputValue, setInputValue] = useState([]);
    const [result, setResult] = useState([]);
    const endpoint = `${config.appRoot}/authorisation/privileges`;
    const { send, isLoading, postResult } = usePost(
        endpoint,
        {
            // post body here
            name: inputValue
        },
        true
    );

    const spinningWheel = () => {
        if (isLoading) {
            return <Loading />;
        }
        return <div />;
    };

    function isNameValid(name) {
        let valid = true;
        Privileges.forEach(
            privilege => {
                if (name === privilege.name) {
                    setResult({
                        message: 'Save Unsuccessful - Privilege witht the same name already exists',
                        showMessage: false
                    });
                    valid = false;
                }
            },
            [valid]
        );
        setResult({
            message: 'Save Successful',
            showMessage: true
        });
        return valid;
    }

    function createNewPrivilege(name) {
        if (isNameValid(name)) {
            //POST to backend to get add the new privilege
            send();
        }
    }

    // function displayMessage(name) {
    //     if (isNameValid(name)) {
    //         return <Typography>{result.message}</Typography>;
    //     }
    //     return <Typography>Name is not valid</Typography>;
    // }
    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={6}>
                    <InputField
                        propertyName="inputValue"
                        label="Name"
                        value={inputValue}
                        onChange={e => setInputValue(e.target.value)}
                        fullWidth
                    />
                </Grid>
                <Grid>
                    <Button
                        variant="contained"
                        onClick={() => {
                            createNewPrivilege(inputValue.trim());
                        }}
                    >
                        Save
                    </Button>
                    <Grid item xs={12}>
                        {spinningWheel()}
                    </Grid>
                </Grid>
            </Grid>
        </Page>
    );
}

export default CreatePrivilege;
