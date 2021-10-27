import React from 'react';
import { Link } from 'react-router-dom';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/styles';
import { Title } from '@linn-it/linn-form-components-library';
import Mypage from './myPageWidth';

const useStyles = makeStyles({
    root: {
        margin: '40px',
        padding: '40px'
    },
    rightMargin: {
        marginRight: '20px'
    }
});

function App() {
    const classes = useStyles();
    return (
        <Mypage>
            <Paper className={classes.root}>
                <Title text="Authorisation" />
                <Link to="../authorisation/viewprivileges">
                    <Button type="button" variant="outlined" className={classes.rightMargin}>
                        View Privileges
                    </Button>
                </Link>
                <Link to="../authorisation/groups">
                    <Button type="button" variant="outlined">
                        View Groups
                    </Button>
                </Link>
                <Link to="../authorisation/viewpermissions">
                <Button type="button" variant="outlined" className={classes.rightMargin}>
                        View Permissions
                    </Button>
                </Link>
            </Paper>
        </Mypage>
    );
}

export default App;
