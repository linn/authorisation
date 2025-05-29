import React, { useState } from 'react';
import Typography from '@mui/material/Typography';
import { useNavigate } from 'react-router-dom';
import { Loading, OnOffSwitch, utilities } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import { DataGrid } from '@mui/x-data-grid';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import Page from './Page';

function Privileges() {
    const navigate = useNavigate();
    const [activeOnly, setActiveOnly] = useState(true);

    const { data, isGetLoading } = useInitialise(itemTypes.privileges.url, null, true);

    const activePrivileges = data?.filter(h => h.active === true);

    function sorting(a, b) {
        const fa = a.name?.toLowerCase() || '';
        const fb = b.name?.toLowerCase() || '';
        return fa.localeCompare(fb);
    }

    const sortedPrivilegesInfo = data ? [...data].sort(sorting) : [];
    const sortedActivePrivilegesInfo = activePrivileges ? [...activePrivileges].sort(sorting) : [];

    const columns = [
        { field: 'id', headerName: 'ID', width: 100 },
        { field: 'name', headerName: 'Name', width: 400 },
        { field: 'active', headerName: 'Active', width: 225 }
    ];

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={10}>
                    <Typography variant="h4">Privileges</Typography>
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
                            rows={activeOnly ? sortedActivePrivilegesInfo : sortedPrivilegesInfo}
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

export default Privileges;
