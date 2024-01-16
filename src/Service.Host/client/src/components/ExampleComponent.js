import React from 'react';
import Typography from '@mui/material/Typography';
import { Page } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import { Link } from 'react-router-dom';

import config from '../config';
import history from '../history';

function ExampleComponent() {
    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typography variant="h6">Example Component / Grid Layout demo</Typography>
                </Grid>
                <Grid item xs={12}>
                    This is just an example component. You can see that by importing the Page
                    component and making it your outermost element, that you get the nav menu and
                    breadcrumb buttons above for free.
                </Grid>
                <Grid item xs={12} style={{ backgroundColor: 'pink' }}>
                    You can use the grid to lay out your page elements in a sensible, consistent
                    way. The grid consists of 12 columns, of which I should span all 12. I have a
                    pink background. Each of the below elements span just one column. You can see
                    that 12 of them fill the entire page
                </Grid>
                <Grid item xs={12} style={{ backgroundColor: 'pink' }}>
                    Each of the below elements span just one column. You can see that 12 of them
                    fill the entire page
                </Grid>
                <Grid item xs={1} style={{ backgroundColor: 'violet' }}>
                    col
                </Grid>
                <Grid item xs={1}>
                    col
                </Grid>
                <Grid item xs={1} style={{ backgroundColor: 'violet' }}>
                    col
                </Grid>
                <Grid item xs={1}>
                    col
                </Grid>
                <Grid item xs={1} style={{ backgroundColor: 'violet' }}>
                    col
                </Grid>
                <Grid item xs={1}>
                    col
                </Grid>
                <Grid item xs={1} style={{ backgroundColor: 'violet' }}>
                    col
                </Grid>
                <Grid item xs={1}>
                    col
                </Grid>
                <Grid item xs={1} style={{ backgroundColor: 'violet' }}>
                    col
                </Grid>
                <Grid item xs={1}>
                    col
                </Grid>
                <Grid item xs={1} style={{ backgroundColor: 'violet' }}>
                    col
                </Grid>
                <Grid item xs={1}>
                    col
                </Grid>

                <Grid item xs={6} style={{ backgroundColor: 'grey' }}>
                    I should span 6, i.e. half the page. I have a pink background
                </Grid>
                <Grid item xs={6} style={{ backgroundColor: 'purple' }}>
                    I should span 6 too, and have a purple background
                </Grid>

                <Grid item xs={3} style={{ backgroundColor: 'blue' }}>
                    I should span 3, i.e. a quarter of the page and have a blue background
                </Grid>

                <Grid item xs={9} style={{ backgroundColor: 'olive' }}>
                    I fill up the remaining 9 columns with an olive green background
                </Grid>

                <Grid item xs={2}>
                    <Link to="/authorisation">Back Home</Link>
                </Grid>
            </Grid>
        </Page>
    );
}

export default ExampleComponent;
