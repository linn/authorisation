import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Paper, Button, Table, TableHead, TableBody, TableCell, TableRow } from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles';
import PropTypes from 'prop-types';
import { Loading, Title } from '@linn-it/linn-form-components-library';

const styles = () => ({
    root: {
        margin: '40px',
        padding: '40px'
    }
});

const ViewPrivileges = ({ classes, initialise, privileges }) => {
    useEffect(() => {
        initialise();
    }, [initialise]);

    return privileges ? (
        <div>
            <Paper className={classes.root}>
                <Title text="All Privileges" />
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Privilege</TableCell>
                            <TableCell align="right">Active status</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {privileges.map(privilege => (
                            <TableRow key={privilege.name}>
                                <TableCell component="th" scope="row">
                                    <Link to={privilege.links[0].href.slice(1)}>
                                        {privilege.name}
                                    </Link>
                                </TableCell>
                                <TableCell align="right">{privilege.active.toString()}</TableCell>
                            </TableRow>
                        ))}
                        <TableRow key="New privilege">
                            <TableCell component="th" scope="row">
                                <input type="text" placeholder="new privilege" />
                            </TableCell>
                            <TableCell align="right">
                                <input type="checkbox" />
                                <Button spacing type="button" variant="outlined">
                                    Create
                                </Button>
                            </TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
                <br />
                <Link to="../authorisation">
                    <Button spacing type="button" variant="outlined">
                        Home
                    </Button>
                </Link>
            </Paper>
        </div>
    ) : (
        <Loading />
    );
};

ViewPrivileges.propTypes = {
    classes: PropTypes.shape({}),
    initialise: PropTypes.func.isRequired,
    privileges: PropTypes.arrayOf(
        PropTypes.shape({
            name: PropTypes.string,
            Id: PropTypes.number,
            active: PropTypes.bool
        })
    )
};

ViewPrivileges.defaultProps = {
    classes: {},
    privileges: null
};

export default withStyles(styles)(ViewPrivileges);
