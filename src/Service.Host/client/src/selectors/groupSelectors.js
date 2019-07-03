export const getGroups = state => {
    const { groups } = state;
    return groups.data;
};
export const getGroupsLoading = state => {
    const { groups } = state;
    const { loading } = groups;
    return loading || false;
};

export const getNewGroupForCreation = state => {
    const { groups } = state;
    const { newGroup } = groups;
    return newGroup || null;
};

export const getGroup = state => {
    const { group } = state;
    return group.data;
};

export const getGroupLoading = state => {
    const { group } = state;
    const { loading } = group;
    return loading || false;
};

export const getUpdatedMessageVisibility = state => {
    const { group } = state;
    const { updatedMessageVisibility } = group;
    return updatedMessageVisibility || false;
};

export const getSaveEnabled = state => {
    const { group } = state;
    const { enableSave } = group;
    return enableSave || false;
};

export const getUsers = state => {
    const { groups } = state;
    const { users } = groups;
    if (users) {
        return users.data.items;
    }
    return [];
};

export const getSelectedUser = state => {
    const { groups } = state;
    const { selectedUser } = groups;
    if (selectedUser) {
        return selectedUser;
    }
    return -1;
};

export const getShouldShowCreate = state => {
    const { groups } = state;
    const { selectedUser } = groups;
    return selectedUser === -1;
};
