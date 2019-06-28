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

export const getPrivilegeLoading = state => {
    const { privilege } = state;
    const { loading } = privilege;
    return loading || false;
};

export const getPrivilegesLoading = state => {
    const { privileges } = state;
    const { loading } = privileges;
    return loading || false;
};

export const getUpdatedMessageVisibility = state => {
    const { privilege } = state;
    const { updatedMessageVisibility } = privilege;
    return updatedMessageVisibility || false;
};

export const getSaveEnabled = state => {
    const { privilege } = state;
    const { enableSave } = privilege;
    return enableSave || false;
};

const allUsersObj = { displayText: `All Users`, id: -1 };
export const getUsers = state => {
    const { privileges } = state;
    const { users } = privileges;
    if (users) {
        return users.unshift(allUsersObj);
    }
    return [allUsersObj];
};
