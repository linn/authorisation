export const getPermissions = state => {
    const { permissions } = state;
    console.log(permissions.data.privilege);
    return permissions.data;
};

export const getPermission = state => {
    const { permissions } = state;
    console.log(permissions.data);
    return permissions.data;
};

export const getPermissionLoading = state => {
    const { Permission } = state;
    const { loading } = permission;
    return loading || false;
};

export const getPermissionsLoading = state => {
    const { permissions } = state;
    const { loading } = permissions;
    return loading || false;
};

export const getSaveEnabled = state => {
    const { permission } = state;
    const { enableSave } = permission;
    return enableSave || false;
};

export const getSelectedUser = state => {
    const { permissions } = state;
    const { selectedUser } = permissions;
    if (selectedUser) {
        return selectedUser;
    }
    return '-1';
};

export const getCurrentUser = state => {
    const { oidc } = state;
    const { user } = oidc;
    const { profile } = user;
    const { employee } = profile;
    return employee;
};

export const getShouldShowCreate = state => {
    const { privileges } = state;
    const { selectedUser } = privileges;
    return selectedUser == null || selectedUser == -1;
};

export const getPrivilegesForAssignment = state => {
    const { privileges } = state;
    const { privilegesForAssignment } = privileges;
    return privilegesForAssignment || [{ name: 'No Privileges Available' }];
};

export const getNewPrivilegeForCreation = state => {
    const { privileges } = state;
    const { newPrivilege } = privileges;
    return newPrivilege || null;
};