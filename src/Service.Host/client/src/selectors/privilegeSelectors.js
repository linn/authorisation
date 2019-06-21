export const getPrivileges = state => {
    const { privileges } = state;
    return privileges.data;
};

export const getPrivilege = state => {
    const { privilege } = state;
    return privilege.data;
};
