import React, { useState } from 'react';
import { Page, Loading, InputField } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import Snackbar from '@mui/material/Snackbar';

//import useInitialise from '../hooks/useInitialise';
import usePost from '../hooks/usePost';
import config from '../config';
import history from '../history';

function CreatePrivilege() {
    const [inputValue, setInputValue] = useState('');

    const endpoint = `${config.appRoot}/authorisation/privileges`;

    //const { data } = useInitialise(endpoint);
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

    // any time data changes, load it into our local privileges state

    function createNewPrivilege() {
        //POST to backend to get add the new privilege
        send();
    }

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
                            createNewPrivilege(inputValue.trim());
                        }}
                        disabled={!inputValue}
                    >
                        Save
                    </Button>
                </Grid>
                <Grid>
                    <Snackbar
                        open={postResult?.id}
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
