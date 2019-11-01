export const getGroups = state => {
    const { groups } = state;
    return groups.data;
};
export const getGroupsLoading = state => {
    const { groups } = state;
    const { loading } = groups;
    return loading || false;
};

export const getNewGroupName = state => {
    const { groups } = state;
    const { newGroupName } = groups;
    return newGroupName || '';
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

export const getGroupPrivileges = state => {
    const { group } = state;
    const { privileges } = group;
    return privileges;
};

export const getGroupPotentialPrivileges = state => {
    const { group } = state;
    const { potentialPrivileges } = group;
    return potentialPrivileges;
};

export const getGroupLoading = state => {
    const { group } = state;
    const { loading } = group;
    return loading || false;
};

export const getUpdatedMessageVisibility = state => {
    const { group } = state;
    const { groupMessageVisibility } = group;
    return groupMessageVisibility || false;
};

export const getGroupMessage = state => {
    const { group } = state;
    const { groupMessage } = group;
    return groupMessage || false;
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

export const getGroupMembers = state => {
    const { group } = state;
    const { data } = group;
    const { members } = data;

    return members ? members.filter(x => x.memberUri.includes('employees')) : [];
};

export const getCurrentUser = state => {
    const { oidc } = state;
    const { user } = oidc;
    const { profile } = user;
    const { employee } = profile;
    return employee;
};
