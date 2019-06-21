import React from 'react';
import { Link } from 'react-router-dom';
import { withStyles } from '@material-ui/core/styles';
import { List, ListItem, Paper, Typography } from '@material-ui/core';
import PropTypes from 'prop-types';
import Privilege from '../components/Privilege';

const styles = () => ({});

const PrivilegeList = ({ privileges }) => (
    <ul>
        {
            privileges.map(privilege =>
                <Privilege privilege={privilege} />
        )
        }       
    </ul>
);

PrivilegeList.defaultProps = {
    privileges: [{ name: "default", id: 1, active: true }],
};

export default (PrivilegeList);

