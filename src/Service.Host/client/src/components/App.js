import React from 'react';
import { Link } from 'react-router-dom';
import { withStyles } from '@material-ui/core/styles';
import { Paper, Button } from '@material-ui/core';
import PropTypes from 'prop-types';
import { Title } from '@linn-it/linn-form-components-library';

const styles = () => ({
    root: {
        margin: '40px',
        padding: '40px'
    }
});

const App = ({ classes }) => (
    <Paper className={classes.root}>
        <Title text="Authorisation" />
        <Link to="../authorisation/viewprivileges">
            <Button type="button" variant="outlined">
                View Privileges
            </Button>
        </Link>
    </Paper>
);

App.propTypes = {
    classes: PropTypes.shape({})
};

App.defaultProps = {
    classes: {}
};

export default withStyles(styles)(App);
