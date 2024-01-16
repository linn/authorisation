import React, { useState } from 'react';

// these are components from other libraries that we are making use of in this demo
import Typography from '@mui/material/Typography';
import { Page, InputField } from '@linn-it/linn-form-components-library';
import Grid from '@mui/material/Grid';
import { Link } from 'react-router-dom';
import Button from '@mui/material/Button';

import config from '../config';
import history from '../history';

function ExampleComponent() {
    const [inputValue, setInputValue] = useState('default value');

    // the onChange prop of the InputField below expects a function that takes the following two params
    // so just define that function here for clarity
    const handleFieldChange = (propertyName, newValue) => {
        console.log(propertyName);
        setInputValue(newValue);
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typography variant="h1">Example Component / Grid Layout demo</Typography>
                </Grid>
                <Grid item xs={12}>
                    <Typography>
                        This is just an example component. You can see that by importing the Page
                        component above and making it your outermost element, you will render the
                        nav menu and breadcrumb buttons above. Note that all free text is rendered
                        inside a Typography element to give it a consistent font / style
                    </Typography>
                </Grid>
                <Grid item xs={12} style={{ backgroundColor: 'pink' }}>
                    <Typography>
                        You can use the grid to arrange your page elements in a sensible, consistent
                        layout. This grid consists of 12 columns, of which I should span all 12. I
                        have a pink background.
                    </Typography>
                </Grid>
                <Grid item xs={12} style={{ backgroundColor: 'pink' }}>
                    <Typography>
                        Each of the below elements span just one column. You can see that 12 of them
                        fill the entire page
                    </Typography>
                </Grid>
                <Grid item xs={1} style={{ backgroundColor: 'violet' }}>
                    <Typography>col</Typography>
                </Grid>
                <Grid item xs={1}>
                    <Typography>col</Typography>
                </Grid>
                <Grid item xs={1} style={{ backgroundColor: 'violet' }}>
                    <Typography>col</Typography>
                </Grid>
                <Grid item xs={1}>
                    <Typography>col</Typography>
                </Grid>
                <Grid item xs={1} style={{ backgroundColor: 'violet' }}>
                    <Typography>col</Typography>
                </Grid>
                <Grid item xs={1}>
                    <Typography>col</Typography>
                </Grid>
                <Grid item xs={1} style={{ backgroundColor: 'violet' }}>
                    <Typography>col</Typography>
                </Grid>
                <Grid item xs={1}>
                    <Typography>col</Typography>
                </Grid>
                <Grid item xs={1} style={{ backgroundColor: 'violet' }}>
                    <Typography>col</Typography>
                </Grid>
                <Grid item xs={1}>
                    <Typography>col</Typography>
                </Grid>
                <Grid item xs={1} style={{ backgroundColor: 'violet' }}>
                    <Typography>col</Typography>
                </Grid>
                <Grid item xs={1}>
                    <Typography>col</Typography>
                </Grid>

                <Grid item xs={6} style={{ backgroundColor: 'grey' }}>
                    <Typography>
                        I should span 6, i.e. half the page. I have a pink background
                    </Typography>
                </Grid>
                <Grid item xs={6} style={{ backgroundColor: 'purple' }}>
                    <Typography>I should span 6 too, and have a purple background</Typography>
                </Grid>

                <Grid item xs={3} style={{ backgroundColor: 'blue' }}>
                    <Typography>
                        I should span 3, i.e. a quarter of the page and have a blue background
                    </Typography>
                </Grid>

                <Grid item xs={9} style={{ backgroundColor: 'olive' }} />

                <Grid item xs={6}>
                    <Typography>
                        To the right is an example of our InputField component taken from our
                        components library, that corresponds to the inputValue react state variable
                    </Typography>
                </Grid>
                <Grid item xs={6}>
                    <InputField
                        propertyName="inputValue"
                        label="Enter some text and you should see me update"
                        value={inputValue}
                        onChange={handleFieldChange}
                        fullWidth
                    />
                </Grid>

                <Grid item xs={6}>
                    <Typography>
                        To the right is an example button taken straight from the material-ui
                        library. It just shows an alert when clicked.
                    </Typography>
                </Grid>
                <Grid item xs={6}>
                    <Button
                        variant="contained"
                        onClick={() => {
                            alert(`The text currently entered is ${inputValue}`);
                        }}
                    >
                        Click me
                    </Button>
                </Grid>
                <Grid item xs={12}>
                    <Typography>
                        Below is an example router link, which will direct the browser back home.
                    </Typography>
                </Grid>
                <Grid item xs={2}>
                    <Typography>
                        <Link to="/authorisation">Back Home</Link>
                    </Typography>
                </Grid>
            </Grid>
        </Page>
    );
}

export default ExampleComponent;
