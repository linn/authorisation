import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Typography from '@mui/material/Typography';
import { Page, Loading, InputField, OnOffSwitch } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Snackbar from '@mui/material/Snackbar';
import Button from '@mui/material/Button';
import config from '../config';
import usePut from '../hooks/usePut';
import history from '../history';
import useInitialise from '../hooks/useInitialise';

import itemTypes from '../itemTypes';

function Privilege() {
    // below is how you determine the id of the privilege in question if the browser is at location /authorisation/privileges/<id>
    const { id } = useParams();

    const { data, isGetLoading } = useInitialise(itemTypes.privileges.url, id);
    const [privilege, setPrivilege] = useState();

    const { send, isPutLoading, putResult } = usePut(
        itemTypes.privileges.url,
        id,
        {
            name: privilege?.name,
            active: privilege?.active,
            ...privilege
        },
        true
    );

    useEffect(() => {
        if (data) {
            setPrivilege(data);
        }
    }, [data]);

    const spinningWheel = () => {
        if (isGetLoading || isPutLoading) {
            return <Loading />;
        }
        return <div />;
    };

    const handleActiveChange = (_, newValue) => {
        setPrivilege({ ...privilege, active: newValue });
    };

    const handleNameFieldChange = (_, newValue) => {
        setPrivilege({ ...privilege, name: newValue });
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {spinningWheel()}
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
                        disabled={
                            data?.name === privilege?.name && data?.active === privilege?.active
                        }
                        onClick={() => {
                            send();
                        }}
                    >
                        Save
                    </Button>

                    <Snackbar
                        open={!!putResult?.id}
                        autoHideDuration={5000}
                        message="Save Successful"
                    />
                </Grid>
            </Grid>
        </Page>
    );
}

export default Privilege;
