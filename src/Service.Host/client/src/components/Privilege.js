import React, { useEffect, Fragment } from 'react';
import { Link } from 'react-router-dom';
import {
    Paper,
    Button,
    Grid,
    Switch,
    Table,
    TableHead,
    TableBody,
    TableCell,
    TableRow
} from '@material-ui/core';
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
import SubTitle from './SubTitle';

const styles = () => ({
    root: {
        margin: '40px',
        padding: '40px'
    }
});

const ViewPrivilege = ({
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
    //employeesWithPrivilege,
    fetchUsers
}) => {
    useEffect(() => {
        fetchPrivilege(id);
      //  fetchEmployeesWithPrivilege(id),
      fetchUsers();
    }, [fetchPrivilege, fetchUsers, id]);

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

    // const membersElements = (
    //     <div className={classes.bottomPadding}>
    //         <SubTitle>Employees with privilege '{privilege.name}'</SubTitle>
    //         {employeesWithPrivilege && (
    //             <div>
    //                 <div className={classes.paddedScrollableDiv}>
    //                     <Table>
    //                         <TableHead>
    //                             <TableRow>
    //                                 <TableCell>Employee</TableCell>
    //                                 <TableCell align="right">Granted permission by</TableCell>
    //                                 <TableCell align="right">Date granted</TableCell>
    //                                 <TableCell align="right" />
    //                             </TableRow>
    //                         </TableHead>
    //                         <TableBody>
    //                             {employeesWithPrivilege.map(employee => (
    //                                 <TableRow key={employee.name}>
    //                                     {/* link to privilege */}

    //                                     <TableCell component="th" scope="row">
    //                                        {/* {getEmployeeName(p.grantedByUri)} */}
    //                                         {employee.name}{' '}
    //                                     </TableCell>

    //                                     <TableCell align="right">
    //                                       {/* {getEmployeeName(p.grantedByUri)} */}
    //                                       {employee.name}{' '}
    //                                     </TableCell>
    //                                     <TableCell align="right">{p.dateGranted} </TableCell>
    //                                     <TableCell align="right">
    //                                         {/* <Button
    //                                             onClick={() => deleteThisPermission(p.privilege)}
    //                                         >
    //                                             Revoke privilege
    //                                         </Button> */}
    //                                     </TableCell>
    //                                 </TableRow>
    //                             ))}
    //                         </TableBody>
    //                     </Table>
    //                 </div>
    //                 <select
    //                     value={privilegeToAssign}
    //                     onChange={setPrivilegeForAssignment}
    //                     className={classes.thinPrivilegesSelectList}
    //                 >
    //                     <option value="-1" key="-1">
    //                         Select Privilege
    //                     </option>
    //                     {potentialPrivileges ? (
    //                         potentialPrivileges.map(p => (
    //                             <option value={p.name} key={p.name}>
    //                                 {p.name}
    //                             </option>
    //                         ))
    //                     ) : (
    //                         <option value="no privileges to assign" key="-1">
    //                             no privileges to assign
    //                         </option>
    //                     )}
    //                 </select>
    //                 <Button
    //                     type="button"
    //                     variant="outlined"
    //                     onClick={dispatchcreatePermission}
    //                     id="createNewPermission"
    //                 >
    //                     Assign to group
    //                 </Button>
    //             </div>
    //         )}
    //     </div>
    // );

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
};

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
    enableSave: PropTypes.bool.isRequired
};

ViewPrivilege.defaultProps = {
    classes: {},
    privilege: null
};
export default withStyles(styles)(ViewPrivilege);
