import React, { useEffect, Fragment } from 'react';
import { Link } from 'react-router-dom';
import {
    Paper,
    Button,
    Table,
    TableHead,
    TableBody,
    TableCell,
    TableRow,
    TextField
} from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles';
import PropTypes from 'prop-types';
import { Loading, Title, getHref } from '@linn-it/linn-form-components-library';

const styles = () => ({
    root: {
        margin: '40px',
        padding: '40px'
    }
});

const ViewGroups = ({ classes, initialise, groups, loading }) => {
    useEffect(() => {
        initialise();
    }, [initialise]);

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
                <div style={{ marginBottom: '10px' }}>
                    <span
                        style={{
                            fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
                            marginRight: '20px'
                        }}
                    >
                        View
                    </span>
                </div>

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
                                    to={`groups/${group.id}`}
                                >
                                    <TableCell component="th" scope="row">
                                        {group.name}
                                    </TableCell>
                                    <TableCell align="right">{group.active.toString()}</TableCell>
                                </TableRow>
                            ))}
                            {/* <TableRow key="New group">
                                <TableCell component="th" scope="row">
                                    <TextField
                                        placeholder="new group"
                                        value={initialName}
                                        id="newGroupName"
                                        onChange={handleNameChange}
                                        label="New Group"
                                    />
                                </TableCell>
                                <TableCell align="right">
                                    <Button
                                        type="button"
                                        variant="outlined"
                                        onClick={dispatchcreateGroup}
                                        id="newGroupStatus"
                                    >
                                        Create
                                    </Button>
                                </TableCell>
                            </TableRow> */}
                        </TableBody>
                    </Table>
                )}
            </Paper>
        </div>
    );
};

ViewGroups.propTypes = {
    classes: PropTypes.shape({}),
    initialise: PropTypes.func.isRequired,
    // createGroup: PropTypes.func.isRequired,
    // updateNewGroup: PropTypes.func.isRequired,
    groups: PropTypes.arrayOf(
        PropTypes.shape({
            name: PropTypes.string,
            Id: PropTypes.number,
            active: PropTypes.bool
        })
    ),
    // newgroup: PropTypes.shape({
    //     name: PropTypes.string,
    //     active: PropTypes.bool
    // }),
    loading: PropTypes.bool.isRequired,
    // users: PropTypes.arrayOf(
    //     PropTypes.shape({ displayText: PropTypes.string, id: PropTypes.number })
    // ),
    // selectUser: PropTypes.func.isRequired,
    // selectedUser: PropTypes.string.isRequired,
    // showCreate: PropTypes.bool.isRequired
};

ViewGroups.defaultProps = {
    classes: {},
    groups: null
    // newgroup: { name: '', active: false },
    // users: [{ displayText: 'All users', id: -1 }, { displayText: 'test user', id: 12 }]
};

export default withStyles(styles)(ViewGroups);
