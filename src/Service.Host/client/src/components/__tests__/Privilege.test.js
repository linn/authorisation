/**
 * @jest-environment jsdom
 */
import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { useParams } from 'react-router-dom';

import useInitialise from '../../hooks/useInitialise';
import render from '../../test-utils';
import Privilege from '../Privilege';
import config from '../../config';

jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
    useParams: jest.fn()
}));
useParams.mockImplementation(() => ({ id: 1 }));

jest.mock('../../hooks/useInitialise');
const ACTIVE_PRIVILEGE = { name: 'a.privilege', active: true, id: 1 };
const INACTIVE_PRIVILEGE = { name: 'a.privilege', active: false, id: 1 };

describe('When Active Privilege ', () => {
    beforeEach(() => {
        useInitialise.mockImplementation(() => ({ data: ACTIVE_PRIVILEGE }));
    });

    test('fetches data', () => {
        render(<Privilege />);
        expect(useInitialise).toBeCalledWith(`${config.appRoot}/authorisation/privileges`, 1);
    });

    test('renders privilege', () => {
        const { getByText } = render(<Privilege />);
        expect(getByText('a.privilege - ACTIVE')).toBeInTheDocument();
    });

    test('does not render loading spinner', () => {
        const { queryByRole } = render(<Privilege />);
        expect(queryByRole('progressbar')).not.toBeInTheDocument();
    });
});

describe('When Inactive Privilege ', () => {
    beforeEach(() => {
        useInitialise.mockImplementation(() => ({ data: INACTIVE_PRIVILEGE }));
    });

    test('fetches data', () => {
        render(<Privilege />);
        expect(useInitialise).toBeCalledWith(`${config.appRoot}/authorisation/privileges`, 1);
    });

    test('renders privilege', () => {
        const { getByText } = render(<Privilege />);
        expect(getByText('a.privilege - INACTIVE')).toBeInTheDocument();
    });

    test('does not render loading spinner', () => {
        const { queryByRole } = render(<Privilege />);
        expect(queryByRole('progressbar')).not.toBeInTheDocument();
    });
});

describe('When loading ', () => {
    beforeEach(() => {
        useInitialise.mockImplementation(() => ({ isLoading: true }));
    });

    test('renders loading spinner', () => {
        const { getByRole } = render(<Privilege />);
        expect(getByRole('progressbar')).toBeInTheDocument();
    });
});
