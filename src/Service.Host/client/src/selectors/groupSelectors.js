export const getGroups = state => {
    const { groups } = state;
    return groups.data;
};
export const getGroupsLoading = state => {
    const { groups } = state;
    const { loading } = groups;
    return loading || false;
};
