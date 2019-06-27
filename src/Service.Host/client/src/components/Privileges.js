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
import { Loading, Title } from '@linn-it/linn-form-components-library';

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
    loading
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
                                    to={privilege.links[0].href.slice(1)}
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
                ;
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
    loading: PropTypes.bool.isRequired
};

ViewPrivileges.defaultProps = {
    classes: {},
    privileges: null,
    newprivilege: { name: '', active: false }
};

export default withStyles(styles)(ViewPrivileges);
