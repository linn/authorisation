import config from './config';

const itemTypes = {
    privileges: { url: `${config.appRoot}/authorisation/privileges` },
    employees: { url: `${config.proxyRoot}/employees` },
    permissions: { url: `${config.appRoot}/authorisation/permissions` },
    groups: { url: `${config.appRoot}/authorisation/groups` },
    members: { url: `${config.appRoot}/authorisation/members` }
};

export default itemTypes;
