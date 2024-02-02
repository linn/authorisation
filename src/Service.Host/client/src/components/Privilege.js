import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Typography from '@mui/material/Typography';
import { Page, Loading, InputField, OnOffSwitch } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
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
    };
    const handleActiveChange = event => {
        setPrivilege({ ...privilege, active: event.target.checked });
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
                <Grid item xs={12}>
                    <Typography color="black">
                        Current Values: {data?.name} {data?.active ? ' - ACTIVE' : '- INACTIVE'}
                    </Typography>

                    <br />
                    <Typography color="black">
                        Edit:
                        <OnOffSwitch
                            inputProps={{ 'aria-label': 'Switch demo' }}
                            onChange={handleChange}
                        />
                    </Typography>
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        propertyName="inputValue"
                        label="Name"
                        value={privilege?.name}
                        onChange={handleNameFieldChange}
                        fullWidth
                        disabled={editingButtonValue === false}
                    />
                    <Typography color="black">
                        Inactive{' '}
                        <OnOffSwitch
                            value={privilege?.active}
                            onChange={handleActiveChange()}
                            inputProps={{ 'aria-label': 'Switch demo' }}
                            disabled={editingButtonValue === false}
                            defaultchecked={data?.active}
                        />
                        Active
                    </Typography>
                    <Button
                        variant="contained"
                        disabled={
                            editingButtonValue === false ||
                            (data?.name === privilege?.name && data?.active === privilege?.active)
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
