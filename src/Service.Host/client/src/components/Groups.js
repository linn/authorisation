import React, { useState } from 'react';
import Typography from '@mui/material/Typography';
import { useNavigate } from 'react-router-dom';
import { Loading, OnOffSwitch, utilities } from '@linn-it/linn-form-components-library';
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import { DataGrid } from '@mui/x-data-grid';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import Page from './Page';

function Groups() {
    const navigate = useNavigate();
    const [activeOnly, setActiveOnly] = useState(true);

    const { data, isGetLoading } = useInitialise(itemTypes.groups.url, null, true);

    const activeGroups = data?.filter(h => h.active === true);

    function sorting(a, b) {
        const fa = a.name?.toLowerCase() || '';
        const fb = b.name?.toLowerCase() || '';
        return fa.localeCompare(fb);
    }

    const sortedGroupsInfo = data ? [...data].sort(sorting) : [];
    const sortedActiveGroupsInfo = activeGroups ? [...activeGroups].sort(sorting) : [];

    const columns = [
        { field: 'id', headerName: 'ID', width: 100 },
        { field: 'name', headerName: 'Name', width: 225 },
        { field: 'active', headerName: 'Active', width: 225 }
    ];

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={8}>
                    <Typography variant="h4">Groups</Typography>
                </Grid>
                <Grid item xs={2}>
                    <Button
                        variant="outlined"
                        onClick={() => navigate(`/authorisation/groups/add-individual-member`)}
                    >
                        Add Member to Group
                    </Button>
                </Grid>
                <Grid item xs={2}>
                    <Button
                        variant="contained"
                        onClick={() => navigate(`/authorisation/groups/create`)}
                    >
                        Create Group
                    </Button>
                </Grid>
                <Grid item xs={12}>
                    {isGetLoading && <Loading />}
                </Grid>
                {data?.length > 0 && (
                    <Grid item xs={12}>
                        <Typography color="black">
                            All
                            <OnOffSwitch
                                value={activeOnly}
                                onChange={() => setActiveOnly(currentValue => !currentValue)}
                                propertyName="dateInvalid"
                            />
                            Active Only
                        </Typography>
                        <DataGrid
                            rows={activeOnly ? sortedActiveGroupsInfo : sortedGroupsInfo}
                            getRowId={row => row?.id}
                            columns={columns}
                            editMode="cell"
                            onRowClick={clicked => {
                                navigate(utilities.getSelfHref(clicked.row));
                            }}
                            autoHeight
                            columnBuffer={8}
                            density="comfortable"
                            rowHeight={34}
                            loading={isGetLoading}
                        />
                    </Grid>
                )}
            </Grid>
        </Page>
    );
}

export default Groups;
