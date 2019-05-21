namespace Linn.Authorisation.Facade
{
    using System.Collections.Generic;
    using Domain.Groups;

    using Linn.Authorisation.Domain.Services;
    using Linn.Common.Facade;

    public class GroupFacadeService : IGroupFacadeService
    {
        private readonly IGroupService groupService;

        public GroupFacadeService(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        public IResult<IEnumerable<Group>> GetGroupMemberships(string who)
        {
            var groups = this.groupService.GetGroupMemberships(who);
            return new SuccessResult<IEnumerable<Group>>(groups);
        }

    }
}