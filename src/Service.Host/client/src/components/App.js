import React from 'react';
import { Link } from 'react-router-dom';
import { withStyles } from '@material-ui/core/styles';
import { List, ListItem, Paper, Typography } from '@material-ui/core';
import PropTypes from 'prop-types';

const styles = () => ({
    root: {
        margin: '40px',
        padding: '40px'
    }
});

const App = ({ classes }) => (
    <Paper className={classes.root}>
        <Typography variant="h6">Authorisation</Typography>
      
    </Paper>
);

App.propTypes = {
    classes: PropTypes.shape({})
};

App.defaultProps = {
    classes: {}
};

export default withStyles(styles)(App);
