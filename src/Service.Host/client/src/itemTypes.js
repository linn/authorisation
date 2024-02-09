import config from './config';

const itemTypes = {
    privileges: { url: `${config.appRoot}/authorisation/privileges` },
    employees: { url: `${config.proxyRoot}/employees?currentEmployees=true` }
};

export default itemTypes;
