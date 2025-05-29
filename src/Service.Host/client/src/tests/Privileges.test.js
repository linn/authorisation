/**
 * @jest-environment jsdom
 */
import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { waitFor } from '@testing-library/react';
import useInitialise from '../hooks/useInitialise';
import render from '../test-utils';
import Privileges from '../components/Privileges';
import config from '../config';

jest.mock('../hooks/useInitialise');

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
        expect(useInitialise).toBeCalledWith(
            `${config.appRoot}/authorisation/privileges`,
            null,
            true
        );
    });

    test('renders all privileges', () => {
        const { getByText } = render(<Privileges />);
        expect(getByText('a.privilege')).toBeInTheDocument();
        expect(getByText('d.privilege')).toBeInTheDocument();
        expect(getByText('b.privilege')).toBeInTheDocument();
    });

    test('orders privileges alphabetically by name, rendering them as links', async () => {
        const { getAllByRole } = render(<Privileges />);
        await waitFor(() => {
            const cells = getAllByRole('cell');
            const privilegeNames = cells
                .filter(cell => cell.getAttribute('data-field') === 'name')
                .map(cell => cell.textContent);
            expect(privilegeNames).toEqual(['a.privilege', 'b.privilege', 'd.privilege']);
        });
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
