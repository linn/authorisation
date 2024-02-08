import React, { useEffect, useState } from 'react';
import { Loading } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import Snackbar from '@mui/material/Snackbar';
import usePost from '../hooks/usePost';
import config from '../config';
import history from '../history';
import itemTypes from '../itemTypes';
import Page from './Page';
import useInitialise from '../hooks/useInitialise';
//import Privilege from './Privilege';

function CreatePermission() {
    const [dropDownOption, setDropDownOPtion] = useState('Choose a Privilege');
    //const [privileges, setPrivileges] = useState([]);
    const { data, isGetLoading } = useInitialise(itemTypes.privileges.url);

    useEffect(() => {
        if (data) {
            //setPrivileges(data);
        }
    }, [data]);

    const { send, isPostLoading, postResult } = usePost(
        itemTypes.permission.url,
        null,
        {
            //Input Values
        },
        true
    );

    const spinningWheel = () => {
        if (isGetLoading || isPostLoading) {
            return <Loading />;
        }
        return <div />;
    };

    const handleDropDownOptionChange = event => {
        setDropDownOPtion(event.target.value);
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typography variant="h4">Create a new Permission</Typography>
                    <select
                        id="dropdown"
                        value={dropDownOption}
                        onChange={handleDropDownOptionChange}
                    >
                        {/* privileges.foreach((privilege) ={'>'} {
                            <option value = {privileges.id}>{privilege.name}</option>
                            
                        ) */}
                    </select>

                    <Button
                        variant="contained"
                        onClick={() => {
                            send();
                        }}
                        disabled={dropDownOption === 'Choose a Privilege'}
                    >
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

export default CreatePermission;
