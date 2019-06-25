import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Paper, Button, Table, TableHead, TableBody, TableCell, TableRow } from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles';
import PropTypes from 'prop-types';
import { Loading, Title } from '@linn-it/linn-form-components-library';
import { create } from 'jss';
// import { Save } from '@material-ui/icons';

const styles = () => ({
    root: {
        margin: '40px',
        padding: '40px'
    }
});

const ViewPrivileges = ({
    classes,
    initialise,
    createPrivilege,
    updateNewPrivilegeText,
    privileges,
    newprivilege
}) => {
    useEffect(() => {
        initialise();
    }, [initialise]);

    let initialName = '';
    let initialStatus = false;
    if (newprivilege) {
        initialName = newprivilege.name;
        initialStatus = newprivilege.active;
    }

    const handleNameChange = e => {
        updateNewPrivilegeText(e.target.value);
    };
    const dispatchcreatePrivilege = () => createPrivilege(initialName);

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
                                <input
                                    type="text"
                                    placeholder="new privilege"
                                    value={initialName}
                                    id="newPrivilegeName"
                                    onChange={handleNameChange}
                                />
                            </TableCell>
                            <TableCell align="right">
                                <input type="checkbox" checked={initialStatus} />
                                <Button
                                    spacing
                                    type="button"
                                    variant="outlined"
                                    onClick={dispatchcreatePrivilege}
                                    id="newPrivilegeStatus"
                                >
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
    createPrivilege: PropTypes.func.isRequired,
    updateNewPrivilegeText: PropTypes.func.isRequired,
    privileges: PropTypes.arrayOf(
        PropTypes.shape({
            name: PropTypes.string,
            Id: PropTypes.number,
            active: PropTypes.bool
        })
    ),
    newprivilege: PropTypes.shape({
        name: PropTypes.string,
        active: PropTypes.bool
    })
};

ViewPrivileges.defaultProps = {
    classes: {},
    privileges: null,
    newprivilege: { name: '', active: false }
};

export default withStyles(styles)(ViewPrivileges);
