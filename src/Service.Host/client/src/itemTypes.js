import config from './config';

const itemTypes = {
    privileges: { url: `${config.appRoot}/authorisation/privileges` },
    employees: { url: `${config.proxyRoot}/employees?currentEmployees=true` },
    permissions: { url: `${config.appRoot}/authorisation/permissions` },
    groups: { url: `${config.appRoot}/authorisation/groups` }
};

export default itemTypes;
