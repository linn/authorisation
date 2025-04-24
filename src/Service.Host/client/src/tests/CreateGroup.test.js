/**
 * @jest-environment jsdom
 */
import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent } from '@testing-library/react';
import usePost from '../hooks/usePost';
import render from '../test-utils';
import config from '../config';
import Group from '../components/Group';

jest.mock('../hooks/usePost');

const postMock = jest.fn();

describe('When loading ', () => {
    beforeEach(() => {
        jest.clearAllMocks();
        usePost.mockImplementation(() => ({ isLoading: true }));
    });

    test('renders loading spinner', () => {
        const { getByRole } = render(<Group creating />);
        expect(getByRole('progressbar')).toBeInTheDocument();
    });
});

describe('When no name input ', () => {
    beforeEach(() => {
        jest.clearAllMocks();
        usePost.mockImplementation(() => ({ send: postMock }));
    });

    test('Upload button should be disabled...', () => {
        const { getByRole } = render(<Group creating />);

        const button = getByRole('button', { name: 'Save' });
        expect(button).toBeDisabled();
    });
});

describe('When name Input ', () => {
    beforeEach(() => {
        jest.clearAllMocks();

        usePost.mockImplementation(() => ({ send: postMock }));
    });

    test('Should post when save button clicked...', () => {
        const { getByLabelText, getByRole } = render(<Group creating />);
        const input = getByLabelText('Name');

        fireEvent.change(input, { target: { value: 'new.name' } });
        expect(usePost).toBeCalledWith(
            `${config.appRoot}/authorisation/groups`,
            null,
            expect.objectContaining({ name: 'new.name' }),
            true
        );

        const button = getByRole('button', { name: 'Save' });
        fireEvent.click(button);
        expect(postMock).toHaveBeenCalledTimes(1);
    });
});

describe('When Result ', () => {
    beforeEach(() => {
        jest.clearAllMocks();
        usePost.mockImplementation(() => ({
            isLoading: false,
            postResult: { id: 1, name: 'new.name' }
        }));
    });

    test('Renders success message', () => {
        const { getByText } = render(<Group creating />);
        expect(getByText('Save Successful')).toBeInTheDocument();
    });
});
