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

export const getGroupPrivileges = state => {
    const { group } = state;
    const { privileges } = group;
    return privileges;
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

export const getGroupMembers = state => {
    const { group } = state;
    const { data } = group;
    const { members } = data;
    const { users } = group;

    let a = [];
    if (members && users) {
        const individualMembers = members.filter(x => x.memberUri.includes('employees'));
        console.info(individualMembers);
        for (let i = 0; i < individualMembers.length; i++) {
            a.push({
                uri: individualMembers[i].memberUri,
                name: users.find(x => x.href === individualMembers[i].memberUri).fullName
            });
        }
    }
    return a;
};
