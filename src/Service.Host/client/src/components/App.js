import React from 'react';
import { Link } from 'react-router-dom';
import { withStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import PropTypes from 'prop-types';
import { Title } from '@linn-it/linn-form-components-library';
import Mypage from './myPageWidth';

const styles = () => ({
    root: {
        margin: '40px',
        padding: '40px'
    }
});

function App({ classes }) {
    return (
        <Mypage>
            <Paper className={classes.root}>
                <Title text="Authorisation" />
                <Link to="../authorisation/viewprivileges">
                    <Button type="button" variant="outlined">
                        View Privileges
                    </Button>
                </Link>
                <Link to="../authorisation/groups">
                    <Button type="button" variant="outlined">
                        View Groups
                    </Button>
                </Link>
            </Paper>
        </Mypage>
    );
}

App.propTypes = {
    classes: PropTypes.shape({})
};

App.defaultProps = {
    classes: {}
};

export default withStyles(styles)(App);
