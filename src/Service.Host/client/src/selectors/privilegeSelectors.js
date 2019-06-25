export const getPrivileges = state => {
    const { privileges } = state;
    return privileges.data;
};

export const getNewPrivilegeForCreation = state => {
    const { privileges } = state;
    const { newPrivilege } = privileges;
    return newPrivilege || null;
};

export const getPrivilege = state => {
    const { privilege } = state;
    return privilege.data;
};
