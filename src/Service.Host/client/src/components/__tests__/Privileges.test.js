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
        { name: 'a.privilege', active: true, id: 1 },
        { name: 'd.privilege', active: true, id: 2 },
        { name: 'c.privilege', active: false, id: 3 },
        { name: 'b.privilege', active: true, id: 4 }
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

    test('orders privileges alphabetically by name, rendering them as links', () => {
        const { getAllByRole } = render(<Privileges />);
        const listItems = getAllByRole('link');
        expect(listItems[1].href).toContain('authorisation/privileges/1');
        expect(listItems[2].href).toContain('authorisation/privileges/4');
        expect(listItems[3].href).toContain('authorisation/privileges/3');
        expect(listItems[4].href).toContain('authorisation/privileges/2');
    });

    test('does not render loading spinner', () => {
        const { queryByRole } = render(<Privileges />);
        expect(queryByRole('progressbar')).not.toBeInTheDocument();
    });
});

describe('When loading ', () => {
    beforeEach(() => {
        useInitialise.mockImplementation(() => ({ isGetLoading: true }));
    });

    test('renders loading spinner', () => {
        const { getByRole } = render(<Privileges />);
        expect(getByRole('progressbar')).toBeInTheDocument();
    });
});
