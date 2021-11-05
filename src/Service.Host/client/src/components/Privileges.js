import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import Table from '@material-ui/core/Table';
import TableHead from '@material-ui/core/TableHead';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import TextField from '@material-ui/core/TextField';
import { makeStyles } from '@material-ui/styles';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Slide from '@material-ui/core/Slide';
import PropTypes from 'prop-types';
import {
    Loading,
    Title,
    getSelfHref,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import config from '../config';
import Mypage from './myPageWidth';

const Transition = React.forwardRef((props, ref) => <Slide direction="up" ref={ref} {...props} />);

const useStyles = makeStyles({
    root: {
        margin: '40px',
        padding: '40px'
    },
    widthTwoThirds: {
        width: '66.66667%',
        margin: '0 auto'
    },
    bottomMargin: { marginBottom: '10px' },
    dropDownLabel: {
        fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
        marginRight: '20px'
    },
    privilegesSelectList: {
        height: '75px',
        width: '300px',
        display: 'inline-block',
        fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
        borderColor: 'rgba(0, 0, 0, 0.23)',
        borderRadius: '5px',
        cursor: 'pointer'
    },
    thinPrivilegesSelectList: {
        height: '36px',
        width: '300px',
        display: 'inline-block',
        fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
        borderColor: 'rgba(0, 0, 0, 0.23)',
        borderRadius: '5px',
        cursor: 'pointer'
    },
    employeeImageContainer: {
        height: '75px',
        maxWidth: '100px',
        borderRadius: '10px',
        overflow: 'hidden',
        display: 'inline-block',
        position: 'relative',
        left: '20px',
        top: '32px'
    },
    centerText: { textAlign: 'center' }
});

function ViewPrivileges({
    getAllPrivileges,
    getPrivilegesForUser,
    getPrivilegesForAssignment,
    getUsers,
    createPrivilege,
    updateNewPrivilege,
    selectUser,
    privileges,
    newprivilege,
    loading,
    users,
    selectedUser,
    showCreate,
    privilegesForAssignment,
    createPermission,
    currentUserUri,
    deletePermission,
    showPrivilegeMessage,
    setPrivilegeMessageVisible,
    permissionMessage,
    deletePrivilege
}) {
    useEffect(() => {
        if (selectedUser == -1) {
            getAllPrivileges();
        } else {
            getPrivilegesForUser(selectedUser);
            getPrivilegesForAssignment(selectedUser);
        }
        getUsers();
    }, [
        getAllPrivileges,
        getPrivilegesForUser,
        getPrivilegesForAssignment,
        selectedUser,
        getUsers
    ]);

    let initialName = '';
    if (newprivilege) {
        initialName = newprivilege.name;
    }

    const classes = useStyles();

    const showPrivilegesForAssignment = privilegesForAssignment.length || false;

    const handleNameChange = e => {
        updateNewPrivilege(e.target.value);
    };
    const dispatchcreatePrivilege = () => createPrivilege(initialName);

    // const changeUser = e => {
    //     selectUser(e.target.value);
    // };

    const [privilegeToAssign, setPrivilegeToAssign] = useState({});
    const [dialogOpen, setDialogOpen] = useState(false);
    const [deletePrivilegeName, setDeletePrivilegeName] = useState({});
    const [deletePrivilegeUri, setDeletePrivilegeUri] = useState({});

    const dispatchcreatePermission = () => {
        createPermission(privilegeToAssign, selectedUser, currentUserUri);
    };
    const setPrivilegeForAssignment = e => {
        setPrivilegeToAssign(e.target.value);
    };

    const deleteThisPermission = (e, name) => {
        e.preventDefault();
        deletePermission(name, selectedUser, currentUserUri);
    };

    const handlePrivilegeDeleteClick = (e, uri, name) => {
        e.preventDefault();
        setDeletePrivilegeName(name);
        setDeletePrivilegeUri(uri);
        setDialogOpen(true);
    };

    const deleteThisPrivilege = () => {
        deletePrivilege(deletePrivilegeUri);
    };

    let image;

    if (selectedUser !== -1) {
        image = (
            <img
                alt=""
                style={{
                    height: '100%'
                }}
                src={`${config.appRoot}/images/staff/${selectedUser}.jpg`}
            />
        );
    }

    return (
        <Mypage>
            <Link to="../authorisation">
                <Button type="button" variant="outlined">
                    Home
                </Button>
            </Link>
            <Dialog
                data-testid="modal"
                open={dialogOpen}
                className={classes.modal}
                onClose={() => {
                    setDialogOpen(false);
                }}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
                TransitionComponent={Transition}
                fullWidth
            >
                <DialogTitle id="alert-dialog-title">
                    Are you sure you want to delete privilege `{deletePrivilegeName}`?
                </DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-slide-description">
                        This will also delete the associated permissions
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button
                        variant="contained"
                        style={{ float: 'left' }}
                        onClick={() => {
                            deleteThisPrivilege();
                            setDialogOpen(false);
                        }}
                        color="secondary"
                    >
                        Delete
                    </Button>
                    <Button
                        color="default"
                        variant="contained"
                        style={{ float: 'right' }}
                        onClick={() => {
                            setDialogOpen(false);
                        }}
                    >
                        Cancel
                    </Button>
                </DialogActions>
            </Dialog>

            <Paper className={classes.root}>
                <Title text="Privileges" className={classes.centerText} />
                {loading ? (
                    <Loading />
                ) : (
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Privilege</TableCell>
                                <TableCell align="right">Active status</TableCell>
                                <TableCell align="right" />
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {privileges.map(privilege => (
                                <TableRow
                                    key={privilege.name}
                                    component={Link}
                                    to={getSelfHref(privilege).slice(1)}
                                >
                                    <TableCell component="th" scope="row">
                                        {privilege.name}
                                    </TableCell>
                                    <TableCell align="right">
                                        {privilege.active.toString()}
                                    </TableCell>
                                    <TableCell align="right">
                                        {!showCreate && (
                                            <Button
                                                onClick={e =>
                                                    deleteThisPermission(e, privilege.name)
                                                }
                                            >
                                                X
                                            </Button>
                                        )}
                                        {showCreate && (
                                            <Button
                                                onClick={e =>
                                                    handlePrivilegeDeleteClick(
                                                        e,
                                                        getSelfHref(privilege),
                                                        privilege.name
                                                    )
                                                }
                                            >
                                                Delete
                                            </Button>
                                        )}
                                    </TableCell>
                                </TableRow>
                            ))}
                            {showCreate ? (
                                <TableRow key="New privilege">
                                    <TableCell component="th" scope="row">
                                        <TextField
                                            placeholder="new privilege"
                                            value={initialName}
                                            id="newPrivilegeName"
                                            onChange={handleNameChange}
                                            label="New Privilege"
                                        />
                                    </TableCell>
                                    <TableCell align="right">
                                        <Button
                                            type="button"
                                            variant="outlined"
                                            onClick={dispatchcreatePrivilege}
                                            id="createNewPrivilegeButton"
                                        >
                                            Create
                                        </Button>
                                    </TableCell>
                                </TableRow>
                            ) : (
                                <TableRow key="New privilege">
                                    <TableCell component="th" scope="row">
                                        <select
                                            value={privilegeToAssign}
                                            onChange={setPrivilegeForAssignment}
                                            className={classes.thinPrivilegesSelectList}
                                        >
                                            <option value="-1" key="-1">
                                                Select Privilege
                                            </option>
                                            {showPrivilegesForAssignment ? (
                                                privilegesForAssignment.map(p => (
                                                    <option value={p.name} key={p.name}>
                                                        {p.name}
                                                    </option>
                                                ))
                                            ) : (
                                                <option value="no privileges to assign" key="-1">
                                                    no privileges to assign
                                                </option>
                                            )}
                                        </select>
                                    </TableCell>
                                    <TableCell align="right">
                                        <Button
                                            type="button"
                                            variant="outlined"
                                            onClick={dispatchcreatePermission}
                                            id="createNewPermission"
                                        >
                                            Assign to user
                                        </Button>
                                    </TableCell>
                                </TableRow>
                            )}
                        </TableBody>
                    </Table>
                )}
            </Paper>
            <SnackbarMessage
                visible={showPrivilegeMessage}
                onClose={() => setPrivilegeMessageVisible(false)}
                message={permissionMessage}
            />
        </Mypage>
    );
}

ViewPrivileges.propTypes = {
    getAllPrivileges: PropTypes.func.isRequired,
    getPrivilegesForUser: PropTypes.func.isRequired,
    getPrivilegesForAssignment: PropTypes.func.isRequired,
    getUsers: PropTypes.func.isRequired,
    createPrivilege: PropTypes.func.isRequired,
    updateNewPrivilege: PropTypes.func.isRequired,
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
    }),
    loading: PropTypes.bool.isRequired,
    users: PropTypes.arrayOf(
        PropTypes.shape({ displayText: PropTypes.string, id: PropTypes.number })
    ),
    selectUser: PropTypes.func.isRequired,
    selectedUser: PropTypes.string.isRequired,
    showCreate: PropTypes.bool.isRequired,
    privilegesForAssignment: PropTypes.arrayOf(
        PropTypes.shape({
            name: PropTypes.string,
            Id: PropTypes.number,
            active: PropTypes.bool
        })
    ).isRequired,
    createPermission: PropTypes.func.isRequired,
    currentUserUri: PropTypes.string.isRequired,
    deletePermission: PropTypes.func.isRequired,
    showPrivilegeMessage: PropTypes.func.isRequired,
    setPrivilegeMessageVisible: PropTypes.func.isRequired,
    permissionMessage: PropTypes.string.isRequired,
    deletePrivilege: PropTypes.func.isRequired
};

ViewPrivileges.defaultProps = {
    privileges: null,
    newprivilege: null,
    users: [{ displayText: 'No users to display', id: -5 }]
};

export default ViewPrivileges;
