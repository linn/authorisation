import config from './config';

const itemTypes = {
    privileges: { url: `${config.appRoot}/authorisation/privileges` },
    employees: { url: `${config.proxyRoot}/employees?currentEmployees=true` },
    permissions: { url: `${config.proxyRoot}/authorisation/permissions?who=/employees` }
};

export default itemTypes;
