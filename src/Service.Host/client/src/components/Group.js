import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Typography from '@mui/material/Typography';
import {
    Loading,
    InputField,
    OnOffSwitch,
    ErrorCard,
    SnackbarMessage
} from '@linn-it/linn-form-components-library';
import PropTypes from 'prop-types';
import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import config from '../config';
import usePut from '../hooks/usePut';
import usePost from '../hooks/usePost';
import useDelete from '../hooks/useDelete';
import history from '../history';
import useInitialise from '../hooks/useInitialise';
import itemTypes from '../itemTypes';
import Page from './Page';

function Group({ creating }) {
    const { id } = useParams();
    const { data, isGetLoading } = useInitialise(itemTypes.groups.url, id, true);
    const { data: employees, isGetLoading: isEmployeesLoading } = useInitialise(
        itemTypes.employees.url
    );
    const [group, setGroup] = useState();
    const [errorMessage, setErrorMessage] = useState();

    const {
        send: putSend,
        isPutLoading,
        errorMessage: putErrorMessage,
        putResult
    } = usePut(
        itemTypes.groups.url,
        id,
        {
            name: group?.name,
            active: group?.active,
            ...group
        },
        true
    );

    const {
        send: deleteSend,
        isLoading: isDeleteLoading,
        errorMessage: deleteErrorMessage,
        deleteResult
    } = useDelete(itemTypes.members.url, true);

    const {
        send: postSend,
        isLoading: isPostLoading,
        errorMessage: postErrorMessage,
        postResult
    } = usePost(itemTypes.groups.url, null, group, true);

    useEffect(() => {
        if (!creating && data) {
            setGroup(data);
        }
    }, [data, creating]);

    useEffect(() => {
        if (putErrorMessage) {
            setErrorMessage(putErrorMessage);
        } else if (postErrorMessage) {
            setErrorMessage(postErrorMessage);
        } else if (deleteErrorMessage) {
            setErrorMessage(deleteErrorMessage);
        }
    }, [putErrorMessage, postErrorMessage, deleteErrorMessage]);

    const [snackbar, setSnackbar] = useState(false);

    useEffect(() => {
        if (putResult) {
            setSnackbar({ result: !!putResult, message: 'Save Successful' });
        } else if (postResult) {
            setSnackbar({ result: !!postResult, message: 'Save Successful' });
        } else if (deleteResult) {
            setSnackbar({ result: !!deleteResult, message: 'Delete Successful' });
        }
    }, [deleteResult, postResult, putResult]);

    const handleActiveChange = (_, newValue) => {
        setGroup({ ...group, active: newValue });
    };

    const handleNameFieldChange = (_, newValue) => {
        setGroup({ ...group, name: newValue });
    };

    const renderListItem = member => {
        const employee = employees?.items.find(i => member.memberUri === i.href);

        if (!employee) {
            return (
                <ListItem key={member.memberUri}>
                    <Typography color="primary">{`${member.memberUri} - Employee not found`}</Typography>
                </ListItem>
            );
        }

        return (
            <ListItem key={employee.href}>
                <Grid container direction="row" alignItems="center" spacing={2}>
                    <Grid item xs={9}>
                        <Typography color="primary">{employee?.fullName}</Typography>
                    </Grid>
                    <Grid item xs={3}>
                        <Button
                            variant="outlined"
                            onClick={() => {
                                deleteSend(member.id);
                            }}
                        >
                            Delete
                        </Button>
                    </Grid>
                </Grid>
            </ListItem>
        );
    };

    return (
        <Page homeUrl={config.appRoot} history={history}>
            <Grid item xs={12}>
                <Typography variant="h4">{creating ? `Create a Group` : `Edit Group`}</Typography>
            </Grid>
            <Grid item xs={12}>
                {(isGetLoading ||
                    isPutLoading ||
                    isDeleteLoading ||
                    isEmployeesLoading ||
                    isPostLoading) && <Loading />}
            </Grid>
            <Grid item xs={6}>
                <InputField
                    propertyName="inputValue"
                    label="Name"
                    value={group?.name}
                    onChange={handleNameFieldChange}
                    fullWidth
                />
                <Typography color="black">
                    Inactive
                    <OnOffSwitch
                        value={group?.active}
                        onChange={handleActiveChange}
                        inputProps={{ 'aria-label': 'Switch demo' }}
                        defaultchecked={data?.active}
                        propertyName="OnOffSwitch"
                    />
                    Active
                </Typography>
                <Button
                    variant="contained"
                    disabled={
                        data?.name === group?.name && data?.active === group?.active && !group?.name
                    }
                    onClick={creating ? postSend : putSend}
                >
                    Save
                </Button>
                {errorMessage && (
                    <Grid item xs={12}>
                        <ErrorCard errorMessage={errorMessage} />
                    </Grid>
                )}

                <SnackbarMessage
                    visible={snackbar.result}
                    onClose={() => setSnackbar({ result: false, message: '' })}
                    message={deleteResult ? 'Delete Successful' : 'Save Successful'}
                />

                {!creating && (
                    <Grid>
                        <Typography variant="h5">Group Members</Typography>

                        <List>{group?.members?.map(renderListItem)}</List>
                    </Grid>
                )}
            </Grid>
        </Page>
    );
}

Group.propTypes = { creating: PropTypes.bool };
Group.defaultProps = { creating: false };

export default Group;
