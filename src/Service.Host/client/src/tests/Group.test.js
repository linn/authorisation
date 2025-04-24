/**
 * @jest-environment jsdom
 */
import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { useParams } from 'react-router-dom';
import { fireEvent } from '@testing-library/react';

import useInitialise from '../hooks/useInitialise';
import render from '../test-utils';
import Group from '../components/Group';
import usePut from '../hooks/usePut';
import config from '../config';

jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
    useParams: jest.fn()
}));
useParams.mockImplementation(() => ({ id: 1 }));

jest.mock('../hooks/useInitialise');
const ACTIVE_GROUP = { name: 'a.group', active: true, id: 1 };
const INACTIVE_GROUP = { name: 'a.group', active: false, id: 1 };

jest.mock(`../hooks/usePut`);
const sendPut = jest.fn();
usePut.mockImplementation(() => ({ send: sendPut }));

describe('When Active Group ', () => {
    beforeEach(() => {
        useInitialise.mockImplementation(() => ({ data: ACTIVE_GROUP }));
    });

    test('fetches data', () => {
        render(<Group />);
        expect(useInitialise).toBeCalledWith(`${config.appRoot}/authorisation/groups`, 1, true);
    });

    test('renders group', () => {
        const { getByDisplayValue, getByRole } = render(<Group />);
        expect(getByDisplayValue('a.group')).toBeInTheDocument();
        expect(getByRole('checkbox')).toBeChecked();
    });

    test('does not render loading spinner', () => {
        const { queryByRole } = render(<Group />);
        expect(queryByRole('progressbar')).not.toBeInTheDocument();
    });
});

describe('When Inactive Group ', () => {
    beforeEach(() => {
        useInitialise.mockImplementation(() => ({ data: INACTIVE_GROUP }));
    });

    test('fetches data', () => {
        render(<Group />);
        expect(useInitialise).toBeCalledWith(`${config.appRoot}/authorisation/groups`, 1, true);
    });

    test('renders group', () => {
        const { getByDisplayValue, getByRole } = render(<Group />);
        expect(getByDisplayValue('a.group')).toBeInTheDocument();
        expect(getByRole('checkbox')).not.toBeChecked();
    });

    test('does not render loading spinner', () => {
        const { queryByRole } = render(<Group />);
        expect(queryByRole('progressbar')).not.toBeInTheDocument();
    });
});

describe('When loading ', () => {
    beforeEach(() => {
        useInitialise.mockImplementation(() => ({ isGetLoading: true }));
    });

    test('renders loading spinner', () => {
        const { getByRole } = render(<Group />);
        expect(getByRole('progressbar')).toBeInTheDocument();
    });
});

describe('When updating Group ', () => {
    beforeEach(() => {
        useInitialise.mockImplementation(() => ({ data: ACTIVE_GROUP }));
    });

    test('makes PUT request with updated data when fields changed and save clicked', () => {
        const { getByLabelText, getByText } = render(<Group />);
        const input = getByLabelText('Name');
        expect(input).toBeInTheDocument();

        fireEvent.change(input, { target: { value: 'testPut123' } });

        const button = getByText('Save');
        expect(button).toBeInTheDocument();
        fireEvent.click(button);

        expect(sendPut).toHaveBeenCalled();
    });
});

describe('When update succeeds ', () => {
    beforeEach(() => {
        usePut.mockImplementation(() => ({ send: sendPut, putResult: { id: 1 } }));
    });

    test('renders success message', () => {
        const { getByText } = render(<Group />);
        expect(getByText('Save Successful')).toBeInTheDocument();
    });
});
