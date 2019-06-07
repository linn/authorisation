namespace Linn.Authorisation.Facade
{
    using Common.Facade;
    using Domain.Groups;
    using Resources;

    public interface IGroupService : IFacadeService<Group, int, GroupResource, GroupResource>
    {
        IResult<Group> AddGroupMember(int groupId, GroupMemberResource resource);

        IResult<Group> RemoveGroupMember(int groupId, int memberId);
    }
}