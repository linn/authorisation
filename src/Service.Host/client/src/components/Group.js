import React, { useEffect, Fragment } from 'react';
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
    members
}) => {
    useEffect(() => {
        fetchGroup(id);
        fetchPrivilegesForGroup(id);
        fetchUsers();
    }, [fetchGroup, fetchPrivilegesForGroup, fetchUsers, id]);

    const handleSaveClick = () => {
        saveGroup(group.name, group.active, getHref(group, 'self'));
    };

    const handleUpdateGroupName = (propertyName, newValue) => {
        updateGroupName(newValue);
    };
    const handleToggleGroupStatus = () => {
        toggleGroupStatus();
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
                        <span>Privileges</span>
                        <ol>
                            {privileges.map(p => (
                                <li key={p.name}>{p.name}</li>
                            ))}
                        </ol>
                    </div>
                ) : (
                    <div />
                )}

                {members ? (
                    <div>
                        <span>Membaz</span>
                        <ol>
                            {members.map(m => (
                                <li key={m.name}>{m.name}</li>
                            ))}
                        </ol>
                    </div>
                ) : (
                    <div />
                )}
            </div>
        );
    }

    return (
        <div>
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
                message="Save Successful"
            />
        </div>
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
            name: PropTypes.string,
            Id: PropTypes.number,
            active: PropTypes.bool
        })
    ),
    fetchUsers: PropTypes.func.isRequired
};

ViewGroup.defaultProps = {
    classes: {},
    group: null,
    privileges: null
};
export default withStyles(styles)(ViewGroup);
