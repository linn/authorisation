import React from 'react';
import Typography from '@material-ui/core/Typography';

const SubTitle = props => {
    return (
        <Typography variant="h5" gutterBottom>
            {props.children}
        </Typography>
    );
};

export default SubTitle;
