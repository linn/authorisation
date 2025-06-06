/**
 * @jest-environment jsdom
 */
import React from 'react';
import '@testing-library/jest-dom/extend-expect';
import { fireEvent } from '@testing-library/react';
import usePost from '../hooks/usePost';
import render from '../test-utils';
import config from '../config';
import Privilege from '../components/Privilege';

jest.mock('../hooks/usePost');

const postMock = jest.fn();

describe('When loading ', () => {
    beforeEach(() => {
        jest.clearAllMocks();
        usePost.mockImplementation(() => ({ isLoading: true }));
    });

    test('renders loading spinner', () => {
        const { getByRole } = render(<Privilege creating />);
        expect(getByRole('progressbar')).toBeInTheDocument();
    });
});

describe('When no name input ', () => {
    beforeEach(() => {
        jest.clearAllMocks();
        usePost.mockImplementation(() => ({ send: postMock }));
    });

    test('Upload button should be disabled...', () => {
        const { getByRole } = render(<Privilege creating />);

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
        const { getByLabelText, getByRole } = render(<Privilege creating />);
        const input = getByLabelText('Name');

        fireEvent.change(input, { target: { value: 'new.name' } });
        expect(usePost).toBeCalledWith(
            `${config.appRoot}/authorisation/privileges`,
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
        const { getByText } = render(<Privilege creating />);
        expect(getByText('Save Successful')).toBeInTheDocument();
    });
});
