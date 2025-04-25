/**
 * @jest-environment jsdom
 */
import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import useInitialise from '../hooks/useInitialise';
import render from '../test-utils';
import Groups from '../components/Groups';
import config from '../config';

jest.mock('../hooks/useInitialise');

describe('When groups ', () => {
    const MOCK_GROUPS = [
        { name: 'a.group', active: true, id: 1 },
        { name: 'd.group', active: true, id: 2 },
        { name: 'c.group', active: false, id: 3 },
        { name: 'b.group', active: true, id: 4 }
    ];
    beforeEach(() => {
        useInitialise.mockImplementation(() => ({ data: MOCK_GROUPS }));
    });

    test('fetches data', () => {
        render(<Groups />);
        expect(useInitialise).toBeCalledWith(`${config.appRoot}/authorisation/groups`, null, true);
    });

    test('renders all groups', () => {
        const { getByText } = render(<Groups />);
        expect(getByText('a.group - ACTIVE')).toBeInTheDocument();
        expect(getByText('d.group - ACTIVE')).toBeInTheDocument();
        expect(getByText('c.group - INACTIVE')).toBeInTheDocument();
        expect(getByText('b.group - ACTIVE')).toBeInTheDocument();
    });

    test('orders groups alphabetically by name, rendering them as links', () => {
        const { getAllByRole } = render(<Groups />);
        const listItems = getAllByRole('link');
        expect(listItems[1].href).toContain('authorisation/groups/1');
        expect(listItems[2].href).toContain('authorisation/groups/4');
        expect(listItems[3].href).toContain('authorisation/groups/3');
        expect(listItems[4].href).toContain('authorisation/groups/2');
    });

    test('does not render loading spinner', () => {
        const { queryByRole } = render(<Groups />);
        expect(queryByRole('progressbar')).not.toBeInTheDocument();
    });
});

describe('When loading ', () => {
    beforeEach(() => {
        useInitialise.mockImplementation(() => ({ isGetLoading: true }));
    });

    test('renders loading spinner', () => {
        const { getByRole } = render(<Groups />);
        expect(getByRole('progressbar')).toBeInTheDocument();
    });
});
