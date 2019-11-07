const getAllUsers = state => {
    const { users } = state;
    const { allUsers } = users;
    return allUsers;
};

export default getAllUsers;
