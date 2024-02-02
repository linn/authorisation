import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Typography from '@mui/material/Typography';
import { Page, Loading, InputField } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import Switch from '@mui/material/Switch';
import Button from '@mui/material/Button';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise'; // will want to use this hook again to do the data fetching

function Privilege() {
    // below is how you determine the id of the privilege in question if the browser is at location /authorisation/privileges/<id>
    const { id } = useParams();
    const endpoint = `${config.appRoot}/authorisation/privileges/${id}`;

    const { data, isLoading } = useInitialise(endpoint);
    const [privilege, setPrivilege] = useState();
    const [editingButtonValue, setEditingButtonValue] = useState(false);
    const [inputNameValue, setInputNameValue] = useState('');
    const [inputActiveValue, setInputActiveValue] = useState(false);

    useEffect(() => {
        if (data) {
            setPrivilege(data);
        }
    }, [data]);

    const spinningWheel = () => {
        if (isLoading) {
            return <Loading />;
        }
        return <div />;
    };

    const handleChange = () => {
        setEditingButtonValue(!editingButtonValue);
        setInputNameValue(privilege.name);
        setInputActiveValue(privilege.active);
    };
    const handleActiveChange = () => {
        setInputActiveValue(!inputActiveValue);
    };

    const handleNameFieldChange = (_, newValue) => {
        setInputNameValue(newValue);
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {spinningWheel()}
                </Grid>
                <Grid item xs={12}>
                    <Typography color="black">
                        {privilege?.name} {privilege?.active ? ' - ACTIVE' : '- INACTIVE'}
                    </Typography>

                    <br />
                    <Typography color="black">
                        Edit:
                        <Switch
                            inputProps={{ 'aria-label': 'Switch demo' }}
                            onChange={handleChange}
                        />
                    </Typography>
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        propertyName="inputValue"
                        label="Name"
                        value={inputNameValue}
                        onChange={handleNameFieldChange}
                        fullWidth
                        disabled={editingButtonValue === false}
                    />
                    <Typography color="black">
                        Inactive{' '}
                        <Switch
                            defaultc
                            value={inputActiveValue}
                            onChange={handleActiveChange}
                            inputProps={{ 'aria-label': 'Switch demo' }}
                            disabled={editingButtonValue === false}
                        />
                        Active
                    </Typography>
                    <Button
                        variant="contained"
                        //</Grid></Grid>disabled={!editingButtonValue && (inputValue === privilege.name) === true}
                        disabled={
                            editingButtonValue === false ||
                            (inputNameValue === privilege?.name &&
                                inputActiveValue === privilege?.active)
                        }
                    >
                        Save
                    </Button>
                </Grid>
            </Grid>
        </Page>
    );
}

export default Privilege;
