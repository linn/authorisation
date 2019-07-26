import React, { useEffect, Fragment, useState } from 'react';
import { Link } from 'react-router-dom';
import { Paper, Button, Grid, Switch } from '@material-ui/core';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import { withStyles } from '@material-ui/core/styles';
import PropTypes from 'prop-types';
import {
    Loading,
    Title,
    InputField,
    getHref,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Mypage from './myPageWidth';

const styles = () => ({
    root: {
        margin: '40px',
        padding: '40px'
    }
});

const ViewGroup = ({
    classes,
    fetchGroup,
    updateGroupName,
    toggleGroupStatus,
    saveGroup,
    id,
    group,
    loading,
    showUpdatedMessage,
    setUpdatedMessageVisible,
    enableSave,
    fetchPrivilegesForGroup,
    fetchUsers,
    privileges,
    potentialPrivileges,
    fetchPotentialPrivileges,
    members,
    users,
    currentUserUri,
    createPermission,
    groupMessage,
    createNewIndividualMember
}) => {
    useEffect(() => {
        fetchGroup(id);
        fetchPrivilegesForGroup(id);
        fetchPotentialPrivileges();
        fetchUsers();
    }, [fetchGroup, fetchPrivilegesForGroup, fetchPotentialPrivileges, fetchUsers, id]);

    const [privilegeToAssign, setPrivilegeToAssign] = useState({});

    const [selectedUser, selectUser] = useState({});

    const dispatchcreatePermission = () => {
        createPermission(privilegeToAssign, group.name, currentUserUri);
    };

    const dispatchCreateIndividualMember = () => {
        console.log(`current user passed through is ${currentUserUri}`);
        createNewIndividualMember(selectedUser, id, currentUserUri);
    };

    const handleSaveClick = () => {
        saveGroup(group.name, group.active, getHref(group, 'self'));
    };

    const handleUpdateGroupName = (propertyName, newValue) => {
        updateGroupName(newValue);
    };
    const handleToggleGroupStatus = () => {
        toggleGroupStatus();
    };

    const changeUser = e => {
        selectUser(e.target.value);
    };

    const getEmployeeName = uri => {
        // let a = [];
        if (members && users) {
            // const individualMembers = members.filter(x => x.memberUri.includes('employees'));
            // console.info(individualMembers);
            // for (let i = 0; i < individualMembers.length; i++) {
            //     a.push({
            //         uri: individualMembers[i].memberUri,
            //         name:
            //     });
            const employee = users.find(x => x.href === uri);
            if (employee) return employee.fullName;
            //}
        }
        return 'Employee not found';
    };

    const setPrivilegeForAssignment = e => {
        setPrivilegeToAssign(e.target.value);
    };

    const groupName = group.name;
    const groupActive = group.active || false;

    let elementsToDisplay;

    if (loading) {
        elementsToDisplay = <Loading />;
    } else {
        elementsToDisplay = (
            <div>
                <Grid container spacing={24}>
                    <Grid item xs={12}>
                        <Fragment>
                            <div>
                                <Grid item xs={8}>
                                    <InputField
                                        fullWidth
                                        disabled={false}
                                        value={groupName}
                                        label="Group"
                                        maxLength={100}
                                        propertyName="name"
                                        onChange={handleUpdateGroupName}
                                    />
                                </Grid>
                                <Grid item xs={8}>
                                    <FormControlLabel
                                        control={
                                            <Switch
                                                checked={groupActive}
                                                onChange={handleToggleGroupStatus}
                                                color="primary"
                                            />
                                        }
                                        label="Active Status"
                                        labelPlacement="start"
                                    />
                                </Grid>
                            </div>
                        </Fragment>
                    </Grid>
                </Grid>
                <Button
                    type="button"
                    variant="outlined"
                    disabled={!enableSave}
                    onClick={handleSaveClick}
                >
                    Save
                </Button>

                {privileges ? (
                    <div>
                        <span>Privileges:</span>
                        <ol>
                            {privileges.map(p => (
                                <li key={p.privilege}>
                                    {p.privilege} granted by {getEmployeeName(p.grantedByUri)} on{' '}
                                    {p.dateGranted}
                                </li>
                            ))}
                        </ol>
                        <select
                            value={privilegeToAssign}
                            onChange={setPrivilegeForAssignment}
                            className={classes.thinPrivilegesSelectList}
                        >
                            <option value="-1" key="-1">
                                Select Privilege
                            </option>
                            {potentialPrivileges ? (
                                potentialPrivileges.map(p => (
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
                        <Button
                            type="button"
                            variant="outlined"
                            onClick={dispatchcreatePermission}
                            id="createNewPermission"
                        >
                            Assign to group
                        </Button>
                    </div>
                ) : (
                    <div />
                )}

                {members && users ? (
                    <div>
                        <span>Members:</span>
                        <ol>
                            {members.map(m => (
                                <li key={m.name}>{getEmployeeName(m.memberUri)}</li>
                            ))}
                        </ol>

                        <select
                            value={selectedUser}
                            defaultValue={-1}
                            onChange={changeUser}
                            //className={classes.privilegesSelectList}
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
                        <Button
                            type="button"
                            variant="outlined"
                            onClick={dispatchCreateIndividualMember}
                            id="AddNewMember"
                        >
                            Add to group
                        </Button>
                        {/* <div className={classes.employeeImageContainer}>{image}</div> */}
                    </div>
                ) : (
                    <div />
                )}
            </div>
        );
    }

    return (
        <Mypage>
            <Link to="../groups">
                <Button type="button" variant="outlined">
                    Back to all groups
                </Button>
            </Link>
            <Paper className={classes.root}>
                <Title text="View/Edit Group" />
                {elementsToDisplay}
            </Paper>
            <SnackbarMessage
                visible={showUpdatedMessage}
                onClose={() => setUpdatedMessageVisible(false)}
                message={groupMessage}
            />
        </Mypage>
    );
};

ViewGroup.propTypes = {
    classes: PropTypes.shape({}),
    fetchGroup: PropTypes.func.isRequired,
    updateGroupName: PropTypes.func.isRequired,
    toggleGroupStatus: PropTypes.func.isRequired,
    saveGroup: PropTypes.func.isRequired,
    group: PropTypes.shape({
        name: PropTypes.string,
        Id: PropTypes.number,
        active: PropTypes.bool
    }),
    loading: PropTypes.bool.isRequired,
    showUpdatedMessage: PropTypes.bool.isRequired,
    setUpdatedMessageVisible: PropTypes.func.isRequired,
    enableSave: PropTypes.bool.isRequired,
    id: PropTypes.number.isRequired,
    fetchPrivilegesForGroup: PropTypes.func.isRequired,
    privileges: PropTypes.arrayOf(
        PropTypes.shape({
            dateGranted: PropTypes.string,
            grantedByUri: PropTypes.string,
            id: PropTypes.number,
            privileges: PropTypes.shape({
                name: PropTypes.string,
                Id: PropTypes.number,
                active: PropTypes.bool
            })
        })
    ),
    fetchUsers: PropTypes.func.isRequired,
    members: PropTypes.arrayOf(
        PropTypes.shape({
            name: PropTypes.string,
            uri: PropTypes.string
        })
    )
};

ViewGroup.defaultProps = {
    classes: {},
    group: null,
    privileges: null,
    members: null
};
export default withStyles(styles)(ViewGroup);
