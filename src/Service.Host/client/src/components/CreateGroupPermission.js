import React, { useState } from 'react';

import Typography from '@mui/material/Typography';
import { Loading, Dropdown, InputField } from '@linn-it/linn-form-components-library';
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import Snackbar from '@mui/material/Snackbar';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import usePost from '../hooks/usePost';
import Page from './Page';

function CreateGroupPermission() {
    const [privilegeInput, setPrivilegeInput] = useState('');
    const [groupValue, setGroupValue] = useState('');

    const { data: privileges, isGetLoading: privilegesLoading } = useInitialise(
        itemTypes.privileges.url
    );

    const { send, isPostLoading, postResult } = usePost(
        itemTypes.permissions.url,
        null,
        {
            Privilegeid: privilegeInput,
            GranteeGroupId: groupValue
        },
        true
    );

    const spinningWheel = () => {
        if (privilegesLoading || isPostLoading) {
            return <Loading />;
        }
        return <div />;
    };

    privileges?.sort((a, b) => {
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

    const handlePrivilegeDropDownChange = (propertyName, newValue) => {
        setPrivilegeInput(newValue);
    };

    const handleGroupChange = (propertyName, newValue) => {
        setGroupValue(newValue);
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                {spinningWheel()}
                <Typography variant="h4">Create a new Permission</Typography>
            </Grid>

            <Grid item xs={4}>
                <Dropdown
                    propertyName="privilege choice"
                    items={privileges?.map(privilege => ({
                        id: privilege.id,
                        displayText: privilege?.name
                    }))}
                    required
                    label="Choose a Privilege"
                    fullWidth
                    onChange={handlePrivilegeDropDownChange}
                    value={privilegeInput}
                />
            </Grid>
            <Grid item xs={4}>
                <InputField
                    propertyName="groupValue"
                    label="Choose a Group"
                    value={groupValue}
                    onChange={handleGroupChange}
                    required
                    fullWidth
                />
            </Grid>

            <Grid item xs={6}>
                <Button
                    disabled={privilegeInput === '' || groupValue === ''}
                    variant="contained"
                    onClick={send}
                >
                    Save
                </Button>
            </Grid>
            <Snackbar
                open={!!postResult?.granteeGroupId}
                autoHideDuration={5000}
                message="Save Successful"
            />
        </Page>
    );
}

export default CreateGroupPermission;
