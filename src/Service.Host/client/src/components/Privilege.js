import React, { useEffect, Fragment } from 'react';
import { Link } from 'react-router-dom';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Switch from '@material-ui/core/Switch';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import { withStyles } from '@material-ui/core/styles';
import PropTypes from 'prop-types';
import {
    Loading,
    Title,
    InputField,
    getSelfHref,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Mypage from './myPageWidth';

const styles = () => ({
    root: {
        margin: '40px',
        padding: '40px'
    }
});

function ViewPrivilege({
    classes,
    id,
    fetchPrivilege,
    updatePrivilegeName,
    togglePrivilegeStatus,
    savePrivilege,
    privilege,
    loading,
    showUpdatedMessage,
    setUpdatedMessageVisible,
    enableSave,
    usersWithPermission,
    fetchUsersForPrivilege,
    fetchUsers,
    allUsers
}) {
    useEffect(() => {
        fetchPrivilege(id);
        fetchUsersForPrivilege(id);
        fetchUsers();
    }, [fetchPrivilege, fetchUsersForPrivilege, fetchUsers, id]);

    const handleSaveClick = () => {
        savePrivilege(privilege.name, privilege.active, getSelfHref(privilege));
    };

    const handleUpdatePrivilegeName = (propertyName, newValue) => {
        updatePrivilegeName(newValue);
    };
    const handleTogglePrivilegeStatus = () => {
        togglePrivilegeStatus();
    };

    const getEmployeeName = uri => {
        if (allUsers) {
            const employee = allUsers.find(x => x.href === uri);
            if (employee) return employee.fullName;
        }
        return 'Employee not found';
    };

    const createTable = (membersList, isGroup) => {
        const table = [];
        for (let i = 0; i < membersList.length; i += 4) {
            const children = [];
            for (let j = 0; j < 4; j += 1) {
                if (membersList[i + j]) {
                    children.push(
                        <td>
                            {isGroup ? (
                                <li key={membersList[i + j].groupName}>
                                    {membersList[i + j].groupName}
                                </li>
                            ) : (
                                <li key={membersList[i + j].granteeUri}>
                                    {getEmployeeName(membersList[i + j].granteeUri)}
                                </li>
                            )}
                        </td>
                    );
                } else {
                    break;
                }
            }
            table.push(<tr>{children}</tr>);
        }
        return table;
    };

    const privilegeName = privilege.name;
    const privilegeActive = privilege.active || false;

    const individualPermissions = usersWithPermission
        ? usersWithPermission.filter(x => x.granteeUri)
        : [];
    const groupPermissions = usersWithPermission
        ? usersWithPermission.filter(x => x.groupName)
        : [];

    let elementsToDisplay;

    if (loading) {
        elementsToDisplay = <Loading />;
    } else {
        elementsToDisplay = (
            <Fragment>
                <Grid item xs={12}>
                    <Fragment>
                        <Grid item xs={8}>
                            <InputField
                                fullWidth
                                disabled={false}
                                value={privilegeName}
                                label="Privilege"
                                maxLength={100}
                                propertyName="name"
                                onChange={handleUpdatePrivilegeName}
                            />
                        </Grid>
                        <Grid item xs={8}>
                            <FormControlLabel
                                control={
                                    <Switch
                                        checked={privilegeActive}
                                        onChange={handleTogglePrivilegeStatus}
                                        color="primary"
                                    />
                                }
                                label="Active Status"
                                labelPlacement="start"
                            />
                        </Grid>
                        <Grid item xs={8}>
                            {usersWithPermission && (
                                <Fragment>
                                    <h4>Groups With permission:</h4>
                                    <ol style={{ maxHeight: '200px', overflowY: 'scroll' }}>
                                        <table style={{ width: '100%' }}>
                                            {createTable(groupPermissions, true)}
                                        </table>
                                    </ol>
                                    <h4>Users With permission:</h4>
                                    <ol style={{ maxHeight: '200px', overflowY: 'scroll' }}>
                                        <table style={{ width: '100%' }}>
                                            {createTable(individualPermissions, false)}
                                        </table>
                                    </ol>
                                </Fragment>
                            )}
                        </Grid>
                    </Fragment>
                </Grid>
                <br />
                <Button
                    type="button"
                    variant="outlined"
                    disabled={!enableSave}
                    onClick={handleSaveClick}
                >
                    Save
                </Button>
            </Fragment>
        );
    }

    return (
        <Mypage>
            <Link to="../viewprivileges">
                <Button type="button" variant="outlined">
                    Back to all privileges
                </Button>
            </Link>
            <Paper className={classes.root}>
                <Title text="View/Edit Privilege" />
                {elementsToDisplay}
            </Paper>
            <SnackbarMessage
                visible={showUpdatedMessage}
                onClose={() => setUpdatedMessageVisible(false)}
                message="Save Successful"
            />
        </Mypage>
    );
}

ViewPrivilege.propTypes = {
    classes: PropTypes.shape({}),
    id: PropTypes.string.isRequired,
    fetchPrivilege: PropTypes.func.isRequired,
    updatePrivilegeName: PropTypes.func.isRequired,
    togglePrivilegeStatus: PropTypes.func.isRequired,
    savePrivilege: PropTypes.func.isRequired,
    privilege: PropTypes.shape({
        name: PropTypes.string,
        Id: PropTypes.number,
        active: PropTypes.bool
    }),
    loading: PropTypes.bool.isRequired,
    showUpdatedMessage: PropTypes.bool.isRequired,
    setUpdatedMessageVisible: PropTypes.func.isRequired,
    enableSave: PropTypes.bool.isRequired,
    usersWithPermission: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
    fetchUsersForPrivilege: PropTypes.func.isRequired,
    fetchUsers: PropTypes.func.isRequired,
    allUsers: PropTypes.arrayOf(PropTypes.shape({})).isRequired
};

ViewPrivilege.defaultProps = {
    classes: {},
    privilege: null
};
export default withStyles(styles)(ViewPrivilege);
