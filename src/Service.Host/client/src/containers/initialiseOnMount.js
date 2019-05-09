import React, { Component } from 'react';

const initialiseOnMount = ComposedComponent => class extends Component {

    componentDidMount() {
        const { initialise } = this.props;
        if (initialise) {
            initialise(this.props);
        }
    }

    render() {
        return <ComposedComponent {...this.props} />;
    }
};

export default initialiseOnMount;