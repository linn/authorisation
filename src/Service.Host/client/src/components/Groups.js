﻿import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import Table from '@material-ui/core/Table';
import TableHead from '@material-ui/core/TableHead';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import TextField from '@material-ui/core/TextField';
import { withStyles } from '@material-ui/core/styles';
import PropTypes from 'prop-types';
import { Loading, Title, getSelfHref } from '@linn-it/linn-form-components-library';
import Mypage from './myPageWidth';

const styles = () => ({
    root: {
        margin: '40px',
        padding: '40px'
    }
});

function ViewGroups({
    classes,
    fetchGroups,
    groups,
    loading,
    createGroup,
    updateNewGroup,
    newGroupName
}) {
    useEffect(() => {
        fetchGroups();
    }, [fetchGroups]);

    let initialName = '';
    if (newGroupName) {
        initialName = newGroupName;
    }
    const handleNameChange = e => {
        updateNewGroup(e.target.value);
    };
    const dispatchcreategroup = () => createGroup(initialName, false);

    return (
        <Mypage>
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
                                <TableRow key={group.name} component={Link} to={getSelfHref(group)}>
                                    <TableCell component="th" scope="row">
                                        {group.name}
                                    </TableCell>
                                    <TableCell align="right">{group.active.toString()}</TableCell>
                                </TableRow>
                            ))}

                            <TableRow key="New Group">
                                <TableCell component="th" scope="row">
                                    <TextField
                                        placeholder="new group"
                                        value={initialName}
                                        id="newPrivilegeName"
                                        onChange={handleNameChange}
                                        label="New Group"
                                    />
                                </TableCell>
                                <TableCell align="right">
                                    <Button
                                        type="button"
                                        variant="outlined"
                                        onClick={dispatchcreategroup}
                                        id="createNewGroupButton"
                                    >
                                        Create
                                    </Button>
                                </TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                )}
            </Paper>
        </Mypage>
    );
}

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
    loading: PropTypes.bool.isRequired,
    createGroup: PropTypes.func.isRequired,
    updateNewGroup: PropTypes.func.isRequired,
    newGroupName: PropTypes.string.isRequired
};

ViewGroups.defaultProps = {
    classes: {},
    groups: null
};

export default withStyles(styles)(ViewGroups);
