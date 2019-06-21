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
    Grid
} from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles';
import PropTypes from 'prop-types';
import { Loading, Title, InputField } from '@linn-it/linn-form-components-library';

const styles = () => ({
    root: {
        margin: '40px',
        padding: '40px'
    }
});

const ViewPrivilege = ({ classes, initialise, privilege }) => {
    useEffect(() => {
        initialise();
    }, [initialise]);

    return (
        <div>
            <Paper className={classes.root}>
                <Title text="View/Edit Privilege" />
                {privilege ? (
                    <Grid container spacing={24}>
                        <Grid item xs={12}>
                            <Fragment>
                                {privilege ? (
                                    <div>
                                        <Grid item xs={8}>
                                            <InputField
                                                fullWidth
                                                disabled={false}
                                                value={privilege.name}
                                                label="Privilege"
                                                maxLength={100}
                                                propertyName="code"
                                            />
                                        </Grid>
                                        <Grid item xs={8}>
                                            <InputField
                                                fullWidth
                                                disabled={false}
                                                value={`${privilege.active}`}
                                                label="Active"
                                                maxLength={5}
                                                propertyName="code"
                                            />
                                        </Grid>
                                    </div>
                                ) : (
                                    <Loading />
                                )}
                            </Fragment>
                        </Grid>
                    </Grid>
                ) : (
                    <Loading />
                )}
                <Link to="../viewprivileges">
                    <Button type="button" variant="outlined">
                        Back to all privileges
                    </Button>
                </Link>
            </Paper>
        </div>
    );
};

ViewPrivilege.propTypes = {
    classes: PropTypes.shape({}),
    initialise: PropTypes.func.isRequired,
    privilege: PropTypes.shape({
        name: PropTypes.string,
        Id: PropTypes.number,
        active: PropTypes.bool
    })
};

ViewPrivilege.defaultProps = {
    classes: {},
    privilege: null
};

export default withStyles(styles)(ViewPrivilege);

{
    /* 
  <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Privilege</TableCell>
                                <TableCell align="right">Active Status</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            <TableRow key={privilege.name}>
                                <TableCell component="th" scope="row">
                                    <span>{privilege.name}</span>
                                </TableCell>
                                <TableCell align="right">
                                    <span>{`${privilege.active}`}</span>
                                </TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
*/
}
