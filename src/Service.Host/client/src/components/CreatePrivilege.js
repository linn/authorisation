import React, { useEffect, useState } from 'react';
import { Page, Loading, InputField } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import Snackbar from '@mui/material/Snackbar';

import IconButton from '@mui/material/IconButton';
import CloseIcon from '@mui/icons-material/Close';

import useInitialise from '../hooks/useInitialise';
import usePost from '../hooks/usePost';
import config from '../config';
import history from '../history';

function CreatePrivilege() {
    const [inputValue, setInputValue] = useState('');
    const [privileges, setPrivileges] = useState([]);
    const [open, setOpen] = React.useState(false);
    const endpoint = `${config.appRoot}/authorisation/privileges`;

    const { data } = useInitialise(endpoint);
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
    useEffect(() => {
        if (data) {
            setPrivileges(data);
        }
    }, [data]);

    function isNameValid(name) {
        let valid = true;
        privileges.forEach(
            privilege => {
                if (name === privilege.name) {
                    valid = false;
                }
            },
            [valid]
        );
        return valid;
    }

    function createNewPrivilege(name) {
        if (isNameValid(name)) {
            //POST to backend to get add the new privilege
            send();
        }
    }

    const handleFieldChange = (propertyName, newValue) => {
        console.log(propertyName);
        setInputValue(newValue);
    };

    const handleClose = (event, reason) => {
        if (reason === 'clickaway') {
            return;
        }

        setOpen(false);
    };

    const action = (
        <fragment>
            <Button color="secondary" size="small" onClick={handleClose}>
                CLOSE
            </Button>
            <IconButton size="small" aria-label="close" color="inherit" onClick={handleClose}>
                <CloseIcon fontSize="small" />
            </IconButton>
        </fragment>
    );

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typography variant="h4">Create a new Privilege</Typography>
                    <InputField
                        propertyName="inputValue"
                        label="name"
                        value={inputValue}
                        onChange={handleFieldChange}
                        fullWidth
                    />

                    <Button
                        variant="contained"
                        onClick={() => {
                            createNewPrivilege(inputValue.trim());
                            setOpen(true);
                        }}
                    >
                        Save
                    </Button>
                </Grid>
                <Grid>
                    <Snackbar
                        open={open}
                        autoHideDuration={5000}
                        onClose={handleClose}
                        message={`Save successful - ${postResult}`}
                        action={action}
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
