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
    TextField,
    List,
    ListItem,
    ListItemGraphic,
    ListItemText
} from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles';
import PropTypes from 'prop-types';
import { Loading, Title, getHref, Dropdown } from '@linn-it/linn-form-components-library';
import SelectInput from '@material-ui/core/Select/SelectInput';

const styles = () => ({
    root: {
        margin: '40px',
        padding: '40px'
    }
});

const backgroundImage = id => {
    backgroundImage: `url(http://app.linn.co.uk/images/staff/${id}.jpg)`;
};

// const hoverStyle = {
//     'box-shadow': '0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19)'
// };

const ViewPrivileges = ({
    classes,
    initialise,
    createPrivilege,
    updateNewPrivilege,
    selectUser,
    privileges,
    newprivilege,
    loading,
    users,
    selectedUser,
    showCreate
}) => {
    useEffect(() => {
        initialise();
    }, [initialise]);

    let initialName = '';
    if (newprivilege) {
        initialName = newprivilege.name;
    }

    const handleNameChange = e => {
        updateNewPrivilege(e.target.value);
    };
    const dispatchcreatePrivilege = () => createPrivilege(initialName);

    const changeUser = e => {
        console.info(e.target.value);
        selectUser(e.target.value);
    };

    return (
        <div>
            <Link to="../authorisation">
                <Button type="button" variant="outlined">
                    Home
                </Button>
            </Link>

            <Paper className={classes.root}>
                <Title text="All Privileges" />

                {/*'../images/staff/33067.jpg'*/
                /* <Dropdown align="right" items={users} helpText="Filter by specific user" />*/}
                <div>
                    <select
                        selected={selectedUser}
                        onChange={changeUser}
                        style={{
                            height: '75px',
                            width: '66.66667%',
                            display: 'inline-block',
                            fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
                            borderColor: 'rgba(0, 0, 0, 0.23)',
                            borderRadius: '5px',
                            cursor: 'pointer'
                        }}
                    >
                        <option value={-1}>All Users</option>
                        {users.map(user => (
                            //TODO replace hardcoded app.linn image url with ../images etc
                            <option
                                value={user.id}
                                style={{
                                    backgroundImage: `url(http://app.linn.co.uk/images/staff/${user.id}.jpg)`
                                }}
                            >
                                {user.firstName} {user.lastName}
                            </option>
                        ))}
                    </select>
                    <div
                        style={{
                            height: '70px',
                            maxWidth: '100px',
                            borderRadius: '10px',
                            overflow: 'hidden',
                            display: 'inline-block',
                            position: 'relative',
                            left: '-120px',
                            top: '30px'
                        }}
                    >
                        <img
                            style={{
                                height: '100%'
                            }}
                            src={`http://app.linn.co.uk/images/staff/${selectedUser}.jpg`}
                        />
                    </div>
                </div>

                {/* <List>
                    <ListItem>
                        <ListItemText primaryText="All Users" />
                    </ListItem>
                    {users.map(user => (
                        <ListItem>
                            <img
                                src={`http://app.linn.co.uk/images/staff/${user.id}.jpg`}
                                style={imageStyle.dropdown}
                            />
                            <span>{user.firstName} {user.lastName}</span>
                            <span> {user.emailAddress} </span>
                        </ListItem>
                    ))}
                </List> */}
                {loading ? (
                    <Loading />
                ) : (
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Privilege</TableCell>
                                <TableCell align="right">Active status</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {privileges.map(privilege => (
                                <TableRow
                                    key={privilege.name}
                                    component={Link}
                                    to={getHref(privilege, 'self').slice(1)}
                                >
                                    <TableCell component="th" scope="row">
                                        {privilege.name}
                                    </TableCell>
                                    <TableCell align="right">
                                        {privilege.active.toString()}
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
                                            id="newPrivilegeStatus"
                                        >
                                            Create
                                        </Button>
                                    </TableCell>
                                </TableRow>
                            ) : (
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
                                            id="newPrivilegeStatus"
                                        >
                                            Give to user
                                        </Button>
                                    </TableCell>
                                </TableRow>
                            )}
                        </TableBody>
                    </Table>
                )}
            </Paper>
        </div>
    );
};

ViewPrivileges.propTypes = {
    classes: PropTypes.shape({}),
    initialise: PropTypes.func.isRequired,
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
    )
};

ViewPrivileges.defaultProps = {
    classes: {},
    privileges: null,
    newprivilege: { name: '', active: false },
    users: [{ displayText: 'All users', id: -1 }, { displayText: 'test user', id: 12 }]
};

export default withStyles(styles)(ViewPrivileges);
