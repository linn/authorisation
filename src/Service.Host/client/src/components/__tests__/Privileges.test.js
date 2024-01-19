/**
 * @jest-environment jsdom
 */
import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import useInitialise from '../../hooks/useInitialise';
import render from '../../test-utils';
import Privileges from '../Privileges';
import config from '../../config';

jest.mock('../../hooks/useInitialise');

describe('When Privileges ', () => {
    const MOCK_PRIVILEGES = [
        { name: 'a.privilege', active: true },
        { name: 'd.privilege', active: true },
        { name: 'c.privilege', active: false },
        { name: 'b.privilege', active: true }
    ];
    beforeEach(() => {
        useInitialise.mockImplementation(() => ({ data: MOCK_PRIVILEGES }));
    });

    test('fetches data', () => {
        render(<Privileges />);
        expect(useInitialise).toBeCalledWith(`${config.appRoot}/authorisation/privileges`);
    });

    test('renders all privileges', () => {
        const { getByText } = render(<Privileges />);
        expect(getByText('a.privilege - ACTIVE')).toBeInTheDocument();
        expect(getByText('d.privilege - ACTIVE')).toBeInTheDocument();
        expect(getByText('c.privilege - INACTIVE')).toBeInTheDocument();
        expect(getByText('b.privilege - ACTIVE')).toBeInTheDocument();
    });

    test('orders privileges alphabetically by name', () => {
        const { getAllByRole } = render(<Privileges />);
        const listItems = getAllByRole('listitem');
        expect(listItems[0].children[0].innerHTML).toBe('a.privilege - ACTIVE');
        expect(listItems[1].children[0].innerHTML).toBe('b.privilege - ACTIVE');
        expect(listItems[2].children[0].innerHTML).toBe('c.privilege - INACTIVE');
        expect(listItems[3].children[0].innerHTML).toBe('d.privilege - ACTIVE');
    });

    test('does not render loading spinner', () => {
        const { queryByRole } = render(<Privileges />);
        expect(queryByRole('progressbar')).not.toBeInTheDocument();
    });
});

describe('When loading ', () => {
    beforeEach(() => {
        useInitialise.mockImplementation(() => ({ isLoading: true }));
    });

    test('renders loading spinner', () => {
        const { getByRole } = render(<Privileges />);
        expect(getByRole('progressbar')).toBeInTheDocument();
    });
});
