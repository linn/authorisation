export const getPrivileges = state => {
    const { privileges } = state;
    return privileges.data;
};

export const getNewPrivilegeForCreation = state => {
    const { privileges } = state;
    const { newPrivilege } = privileges;
    return newPrivilege || null;
};

export const getPrivilegesForAssignment = state => {
    const { privileges } = state;
    const { privilegesForAssignment } = privileges;
    return privilegesForAssignment || [{ name: 'No Privileges Available' }];
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

export const getPermissionMessageVisibility = state => {
    const { privileges } = state;
    const { permissionMessageVisibility } = privileges;
    return permissionMessageVisibility || false;
};

export const getPermissionMessage = state => {
    const { privileges } = state;
    const { permissionMessage } = privileges;
    return permissionMessage;
};

export const getSaveEnabled = state => {
    const { privilege } = state;
    const { enableSave } = privilege;
    return enableSave || false;
};

export const getPermissionsForPrivilege = state => {
    const { privilege } = state;
    const { permissions } = privilege;
    if (permissions) {
        return permissions;
    }
    return [];
};

export const getSelectedUser = state => {
    const { privileges } = state;
    const { selectedUser } = privileges;
    if (selectedUser) {
        return selectedUser;
    }
    return '-1';
};

export const getShouldShowCreate = state => {
    const { privileges } = state;
    const { selectedUser } = privileges;
    return selectedUser == null || selectedUser == -1;
};

export const getCurrentUser = state => {
    const { oidc } = state;
    const { user } = oidc;
    const { profile } = user;
    const { employee } = profile;
    return employee;
};
