import React, { Component } from 'react';

const styles = {
    outer: {
        textAlign: 'center',
        margin: '20px',
        height: '40px'
    },
    loading: {
        backgroundColor: '#88cbf0',
        height: '100%',
        width: '6px',
        animation: 'stretchdelay 1.2s infinite ease-in-out',
        display: 'inline-block'
    }
};

export class Loading extends Component {
    render() {
        return (
            <div style={styles.outer} >
                <div style={{ ...styles.loading }}></div>{' '}
                <div style={{ ...styles.loading, animationDelay: '-1.0s' }}></div>{' '}
                <div style={{ ...styles.loading, animationDelay: '-0.9s' }}></div>{' '}
                <div style={{ ...styles.loading, animationDelay: '-0.8s' }}></div>{' '}
                <div style={{ ...styles.loading, animationDelay: '-0.7s' }}></div>{' '}
                <div style={{ ...styles.loading, animationDelay: '-0.6s' }}></div>
            </div>
        );
    }
}