import React from 'react';

const Mypage = props => {
    return (
        <div
            style={{
                width: '66.66667%',
                margin: '0 auto',
                fontFamily: 'Roboto, Helvetica, Arial, sans-serif'
            }}
        >
            {props.children}
        </div>
    );
};

// export const roundedEdges = {
//   borderRadius: "5px";

export default Mypage;
