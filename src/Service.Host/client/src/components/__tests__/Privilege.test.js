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
        const { getByDisplayValue, getByRole } = render(<Privilege />);
        expect(getByDisplayValue('a.privilege')).toBeInTheDocument();
        expect(getByRole('checkbox')).toBeChecked();
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
        const { getByDisplayValue, getByRole } = render(<Privilege />);
        expect(getByDisplayValue('a.privilege')).toBeInTheDocument();
        expect(getByRole('checkbox')).not.toBeChecked();
    });

    test('does not render loading spinner', () => {
        const { queryByRole } = render(<Privilege />);
        expect(queryByRole('progressbar')).not.toBeInTheDocument();
    });
});

describe('When loading ', () => {
    beforeEach(() => {
        useInitialise.mockImplementation(() => ({ isGetLoading: true }));
    });

    test('renders loading spinner', () => {
        const { getByRole } = render(<Privilege />);
        expect(getByRole('progressbar')).toBeInTheDocument();
    });
});

describe('When updating Privilege ', () => {
    beforeEach(() => {
        useInitialise.mockImplementation(() => ({ data: ACTIVE_PRIVILEGE }));
    });

    test('makes PUT request with updated data when fields changed and save clicked', () => {
        // before doing anything we will need to mock out usePut in a similar way to how useInitialise is mocked
        // up at the top of this file
        
        // then, the general recipe for the test will be:
        // 1. imulate input to the input field to change it to new.name
        // 2. simulate click on switch to switch it from active = false
        // 3. simulate click on save button
        // 4. expect usePut to be called with correct parameters
        // 5. expect send() to be called
    });
});
