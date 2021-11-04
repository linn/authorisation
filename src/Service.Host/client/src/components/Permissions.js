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
import Typography from '@material-ui/core/Typography';
import Grid from '@material-ui/core/Grid';
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
    permissionsSelectList: {
        height: '75px',
        width: '300px',
        display: 'inline-block',
        fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
        borderColor: 'rgba(0, 0, 0, 0.23)',
        borderRadius: '5px',
        cursor: 'pointer'
    },
    thinPermissionsSelectList: {
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

function ViewPermissions({
    getAllPermissions,
    getPermissionsForUser,
    getPrivilegesForAssignment,
    getUsers,
    updateNewPermission,
    createPermission,
    selectUser,
    permissions,
    newprivilege,
    loading,
    users,
    selectedUser,
    showCreate,
    privilegesForAssignment,
    currentUserUri,
    deletePermission
}) {
    useEffect(() => {
        getPermissionsForUser(selectedUser);
        getPrivilegesForAssignment(selectedUser);
        getUsers();
    }, [
        getAllPermissions,
        getPermissionsForUser,
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
        updateNewPermission(e.target.value);
    };
	const dispatchcreatePrivilege = () => createPrivilege(initialName);	   
    const changeUser = e => {
        selectUser(e.target.value);
    };
    
    const [privilegeToAssign, setPrivilegeToAssign] = useState({});
    const [dialogOpen, setDialogOpen] = useState(false);
    const [deletePermissionName, setDeletePermissionName] = useState({});

    const [deletePermissionUri, setDeletePermissionUri] = useState({});
    const dispatchcreatePermission = () => {
        createPermission(privilegeToAssign, selectedUser, currentUserUri);
    };
    const setPrivilegeForAssignment = e => {
        setPrivilegeToAssign(e.target.value);
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

            <Paper className={classes.root}>
                <Title text="Permissions" className={classes.centerText} />
                <div className={classes.bottomMargin}>
                    <span className={classes.dropDownLabel}>View permissions for:</span>
                    <select
                        defaultValue={selectedUser}
                        selected={selectedUser}
                        onChange={changeUser}
                        className={classes.permissionsSelectList}
                    >
                        <option value={-1} key="-1">
                            All Users
                        </option>
                        {users.map(user => (
                            <option value={user.id} key={user.id}>
                                {user.firstName} {user.lastName}
                            </option>
                        ))}
                    </select>
                    <div className={classes.employeeImageContainer}>{image}</div>
                </div>

                {loading ? (
                    <Loading />
                ) : (
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Permission</TableCell>
                                <TableCell>Granted By Uri</TableCell>
                                <TableCell>Type</TableCell>
                                <TableCell align="right">Action</TableCell>
                                <TableCell align="right" />
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {permissions.map(permission => (
                                <TableRow key={permission.privilege}>
                                    <TableCell component="th" scope="row">
                                        {permission.privilege}
                                    </TableCell>
                                    <TableCell component="th" scope="row">
                                        {permission.grantedByUri}
                                    </TableCell>
                                    {permission.groupName ? (
                                    <TableCell component="th" scope="row">
                                        Group Permission : {permission.groupName}
                                    </TableCell>
                                    ):
                                    (
                                        <TableCell component="th" scope="row">
                                            Individual Permission
                                        </TableCell>
                                    )}
                                        <TableCell align="right">
                                        {
                                            <Button
                                                onClick={e =>
                                                    deletePermission(permission.privilege, selectedUser, currentUserUri)
                                                }
                                            >
                                                X
                                            </Button>
                                        }
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
                                            className={classes.thinPermissionsSelectList}
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
        </Mypage>
    );
}

ViewPermissions.propTypes = {
    getAllPermissions: PropTypes.func.isRequired,
    getPermissionsForUser: PropTypes.func.isRequired,
    getPrivilegesForAssignment: PropTypes.func.isRequired,
    getUsers: PropTypes.func.isRequired,
    createPermission: PropTypes.func.isRequired,
    updateNewPermission: PropTypes.func.isRequired,
    permissions: PropTypes.arrayOf(
        PropTypes.shape({
            privilege: PropTypes.string,
            grantedByUri: PropTypes.string,
            granteeByUri: PropTypes.string,
            groupName: PropTypes.string,
            dateGranted: PropTypes.string
        })
    ),
    newPermission: PropTypes.shape({
        privilege: PropTypes.string,
        grantedByUri: PropTypes.string,
        granteeByUri: PropTypes.string,
        groupName: PropTypes.string,
        dateGranted: PropTypes.string
    }),
    loading: PropTypes.bool.isRequired,
    users: PropTypes.arrayOf(
        PropTypes.shape({ displayText: PropTypes.string, id: PropTypes.number })
    ),
    selectUser: PropTypes.func.isRequired,
    selectedUser: PropTypes.string.isRequired, 
    createPermission: PropTypes.func.isRequired,
    currentUserUri: PropTypes.string.isRequired,
    deletePermission: PropTypes.func.isRequired
};

ViewPermissions.defaultProps = {
    permissions: null,
    newPermission: null,
    users: [{ displayText: 'No users to display', id: -5 }]
};

export default ViewPermissions;
