import { getSelfHref } from '@linn-it/linn-form-components-library';
import history from '../history';

export default () => next => action => {
    const result = next(action);
    if (action.type === 'RECEIVE_NEW_PRIVILEGE') {
        history.push(getSelfHref(action.payload.data).slice(1));
    } else if (action.type.startsWith('RECEIVE_NEW')) {
        history.push(getSelfHref(action.payload.data));
    }
    return result;
};
