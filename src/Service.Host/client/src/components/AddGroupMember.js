import React, { useState } from 'react';

import Typography from '@mui/material/Typography';
import { Loading, Dropdown } from '@linn-it/linn-form-components-library';
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';

import Page from './Page';

function AddGroupMember() {
    const [addingGroupInput, setAddingGroupInput] = useState('');
    const [addedToGroupInput, setAddedToGroupInput] = useState('');

    const { data: groups, isGetLoading: isGroupsLoading } = useInitialise(itemTypes.groupData.url);

    const spinningWheel = () => {
        if (isGroupsLoading) {
            return <Loading />;
        }
        return <div />;
    };

    groups?.sort((a, b) => {
        const fa = a.name.toLowerCase();
        const fb = b.name.toLowerCase();

        if (fa < fb) {
            return -1;
        }
        if (fa > fb) {
            return 1;
        }
        return 0;
    });

    const handleAddingGroupDropDownChange = (propertyName, newValue) => {
        setAddingGroupInput(newValue);
    };

    const handleAddedToGroupDropDownChange = (propertyName, newValue) => {
        setAddedToGroupInput(newValue);
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                {spinningWheel()}
                <Typography variant="h4">Add a Group to a Group</Typography>
            </Grid>

            <Grid item xs={4}>
                <Dropdown
                    propertyName="Adding to Group choice"
                    items={groups?.map(group => ({
                        id: group.id,
                        displayText: `${group.name}`
                    }))}
                    required
                    label="Choose a Group to add into another Group"
                    fullWidth
                    onChange={handleAddingGroupDropDownChange}
                    value={addingGroupInput}
                />
            </Grid>
            <Grid item xs={4}>
                <Dropdown
                    propertyName="employee choice"
                    items={groups?.map(group => ({
                        id: group.id,
                        displayText: `${group.name}`
                    }))}
                    required
                    label="Choose a Group"
                    fullWidth
                    onChange={handleAddedToGroupDropDownChange}
                    value={addedToGroupInput}
                />
            </Grid>

            <Grid item xs={6}>
                <Button
                    disabled={
                        addingGroupInput === '' ||
                        addedToGroupInput === '' ||
                        addingGroupInput === addedToGroupInput
                    }
                    variant="contained"
                    onClick={() => {
                        //send();
                    }}
                >
                    Save
                </Button>
            </Grid>
        </Page>
    );
}

export default AddGroupMember;
