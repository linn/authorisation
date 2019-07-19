import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Paper, Button, Table, TableHead, TableBody, TableCell, TableRow } from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles';
import PropTypes from 'prop-types';
import { Loading, Title, getHref } from '@linn-it/linn-form-components-library';

const styles = () => ({
    root: {
        margin: '40px',
        padding: '40px'
    }
});

const ViewGroups = ({ classes, fetchGroups, groups, loading }) => {
    useEffect(() => {
        fetchGroups();
    }, [fetchGroups]);

    return (
        <div
            style={{
                width: '66.66667%',
                margin: '0 auto'
            }}
        >
            <Link to="../authorisation">
                <Button type="button" variant="outlined">
                    Home
                </Button>
            </Link>

            <Paper className={classes.root}>
                <Title text="Groups" style={{ textAlign: 'center' }} />

                {loading ? (
                    <Loading />
                ) : (
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Group</TableCell>
                                <TableCell align="right">Active status</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {groups.map(group => (
                                <TableRow
                                    key={group.name}
                                    component={Link}
                                    to={getHref(group, 'self')}
                                >
                                    <TableCell component="th" scope="row">
                                        {group.name}
                                    </TableCell>
                                    <TableCell align="right">{group.active.toString()}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                )}
            </Paper>
        </div>
    );
};

ViewGroups.propTypes = {
    classes: PropTypes.shape({}),
    fetchGroups: PropTypes.func.isRequired,
    groups: PropTypes.arrayOf(
        PropTypes.shape({
            name: PropTypes.string,
            Id: PropTypes.number,
            active: PropTypes.bool
        })
    ),
    loading: PropTypes.bool.isRequired
};

ViewGroups.defaultProps = {
    classes: {},
    groups: null
};

export default withStyles(styles)(ViewGroups);
