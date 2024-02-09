import React, { useState } from 'react';

import Typography from '@mui/material/Typography';
import { Loading, Dropdown } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import config from '../config';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import Page from './Page';

function CreatePermission() {
    console.log(useInitialise(itemTypes.employees.url));

    const [dropDownOption, setDropDownOPtion] = useState('Choose a Privilege');

    const { data: privileges, isGetLoading: privilegesLoading } = useInitialise(
        itemTypes.privileges.url
    );
    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );

    const spinningWheel = () => {
        if (privilegesLoading || isEmployeesLoading) {
            return <Loading />;
        }
        return <div />;
    };

    // function displayPrivilegesInDropDown () {
    //     privileges.forEach(privilege => {
    //         id: {privilege.id}
    //         displayText: {privilege?.name}
    //         })
    //     };
    // };

    const handleDropDownOptionChange = (propertyName, newValue) => {
        console.log(newValue);
        setDropDownOPtion(newValue);
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                {spinningWheel()}

                <Typography variant="h4">Create a new Permission</Typography>
            </Grid>

            <Grid item xs={6}>
                <Dropdown
                    propertyName="privilege choice"
                    items={privileges?.map(privilege => ({
                        id: privilege.id,
                        displayText: privilege?.name
                    }))}
                    required
                    label="Choose a Privilege"
                    fullWidth
                    value="Choose a Privilege"
                    onChange={handleDropDownOptionChange}
                />
            </Grid>
            <Grid item xs={6}>
                <Dropdown
                    propertyName="privilege choice"
                    items={employees?.items?.map(employee => ({
                        id: employee.id,
                        displayText: `${employee?.firstName} ${employee?.lastName}`
                    }))}
                    required
                    label="Choose an Employee"
                    fullWidth
                    value="Choose an  Employee"
                    onChange={handleDropDownOptionChange}
                />
            </Grid>
            <Grid container spacing={20}>
                <Grid item xs={12} />
            </Grid>
        </Page>
    );
}

export default CreatePermission;
