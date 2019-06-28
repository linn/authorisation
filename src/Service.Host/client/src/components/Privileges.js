import React, { useEffect } from 'react';
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
import { Loading, Title, getHref, Dropdown } from '@linn-it/linn-form-components-library';
import SelectInput from '@material-ui/core/Select/SelectInput';

const styles = () => ({
    root: {
        margin: '40px',
        padding: '40px'
    }
});

// const hoverStyle = {
//     'box-shadow': '0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19)'
// };

const ViewPrivileges = ({
    classes,
    initialise,
    createPrivilege,
    updateNewPrivilege,
    privileges,
    newprivilege,
    loading,
    users
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

    return (
        <div>
            <Link to="../authorisation">
                <Button type="button" variant="outlined">
                    Home
                </Button>
            </Link>

            <Paper className={classes.root}>
                <Title text="All Privileges" />

                <Dropdown align="right" items={users} helpText="Filter by specific user" />

                {/* <label id="userSelectLabel"> View Privileges By user </label>

                <select>
                    {users.map(user => (
                        <option value={user.name}>{user.name}</option>
                    ))}
                </select> */}

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
