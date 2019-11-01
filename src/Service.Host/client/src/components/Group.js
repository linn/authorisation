import React, { useEffect, Fragment, useState } from 'react';
import { Link } from 'react-router-dom';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Switch from '@material-ui/core/Switch';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import { makeStyles } from '@material-ui/styles';
import PropTypes from 'prop-types';
import {
    Loading,
    Title,
    InputField,
    getHref,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import Mypage from './myPageWidth';

const useStyles = makeStyles({
    root: {
        margin: '40px',
        padding: '40px'
    },
    thinPrivilegesSelectList: {
        height: '36px',
        width: '300px',
        display: 'inline-block',
        borderColor: 'rgba(0, 0, 0, 0.23)',
        borderRadius: '5px',
        cursor: 'pointer',
        marginRight: `20px`
    },
    bottomPadding: {
        paddingBottom: '20px'
    }
});

const ViewGroup = ({
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

    const classes = useStyles();

    const dispatchcreatePermission = () => {
        createPermission(privilegeToAssign, group.name, currentUserUri);
    };

    const dispatchCreateIndividualMember = () => {
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
        if (members && users) {
            const employee = users.find(x => x.href === uri);
            if (employee) return employee.fullName;
        }
        return 'Employee not found';
    };

    const setPrivilegeForAssignment = e => {
        setPrivilegeToAssign(e.target.value);
    };

    const groupName = group.name;
    const groupActive = group.active || false;

    const editGroupSection = (
        <div className={classes.bottomPadding}>
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
        </div>
    );

    let privilegesElements;

    if (loading) {
        privilegesElements = <Loading />;
    } else {
        privilegesElements = (
            <div className={classes.bottomPadding}>
                {privileges && (
                    <div>
                        <ol style={{ maxHeight: '200px', overflowY: 'scroll' }}>
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
                )}
            </div>
        );
    }

    const createTable = membersList => {
        const table = [];
        for (let i = 0; i < membersList.length; i += 4) {
            const children = [];
            for (let j = 0; j < 4; j += 1) {
                if (membersList[i + j]) {
                    children.push(
                        <td>
                            <li key={membersList[i + j].name}>
                                {getEmployeeName(membersList[i + j].memberUri)}
                            </li>
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

    let membersElements;

    if (loading) {
        membersElements = <Loading />;
    } else {
        membersElements = (
            <div>
                {members && users && (
                    <div>
                        <ol style={{ maxHeight: '200px', overflowY: 'scroll' }}>
                            <table style={{ width: '100%' }}>{createTable(members)}</table>
                        </ol>

                        <select
                            value={selectedUser}
                            defaultValue={-1}
                            onChange={changeUser}
                            className={classes.thinPrivilegesSelectList}
                        >
                            <option value={-1} key="-1">
                                All Users
                            </option>
                            {users.map(user => (
                                <option value={user.id} key={user.id}>
                                    {user.fullName}
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
                    </div>
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
                {editGroupSection}

                <h4>{`Privileges assigned to '${group.name}'`}</h4>
                {privilegesElements}

                <h4>{`Members of '${group.name}'`}</h4>
                {membersElements}
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
    ),
    potentialPrivileges: PropTypes.arrayOf(
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
    ).isRequired,
    fetchPotentialPrivileges: PropTypes.func.isRequired,
    users: PropTypes.arrayOf(
        PropTypes.shape({
            name: PropTypes.number,
            fullName: PropTypes.string
        })
    ).isRequired,
    currentUserUri: PropTypes.string.isRequired,
    createPermission: PropTypes.func.isRequired,
    groupMessage: PropTypes.string.isRequired,
    createNewIndividualMember: PropTypes.func.isRequired
};

ViewGroup.defaultProps = {
    group: null,
    privileges: null,
    members: null
};
export default ViewGroup;
