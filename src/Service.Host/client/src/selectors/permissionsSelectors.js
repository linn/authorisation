export const getPermissions = state => {
    const { permissions } = state;
    console.log(permissions.data.permission);
    return permissions.data;
};

export const getNewPermissionForCreation = state => {
    const { permissions } = state;
    const { newPermission } = permissions;
    return newPermission || null;
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
