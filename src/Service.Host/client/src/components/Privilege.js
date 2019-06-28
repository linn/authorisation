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

const ViewPrivilege = ({
    classes,
    initialise,
    updatePrivilegeName,
    togglePrivilegeStatus,
    savePrivilege,
    privilege,
    loading,
    showUpdatedMessage,
    setUpdatedMessageVisible,
    enableSave
}) => {
    useEffect(() => {
        initialise();
    }, [initialise]);

    const handleSaveClick = () => {
        savePrivilege(privilege.name, privilege.active, getHref(privilege, 'self'));
    };

    const handleUpdatePrivilegeName = (propertyName, newValue) => {
        updatePrivilegeName(newValue);
    };
    const handleTogglePrivilegeStatus = () => {
        togglePrivilegeStatus();
    };

    const privilegeName = privilege.name;
    const privilegeActive = privilege.active || false;

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
    }

    return (
        <div>
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
        </div>
    );
};

ViewPrivilege.propTypes = {
    classes: PropTypes.shape({}),
    initialise: PropTypes.func.isRequired,
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
    enableSave: PropTypes.bool.isRequired
};

ViewPrivilege.defaultProps = {
    classes: {},
    privilege: null
};
export default withStyles(styles)(ViewPrivilege);
