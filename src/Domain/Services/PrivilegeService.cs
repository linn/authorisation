namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Common.Persistence;

    public class PrivilegeService : IPrivilegeService
    {
        private readonly IRepository<Privilege, int> privilegeRepository;

        public PrivilegeService(IRepository<Privilege, int> privilegeRepository)
        {
            this.privilegeRepository = privilegeRepository;
        }

        public IEnumerable<Privilege> GetAllPrivilegesForUser(IEnumerable<string> userPrivileges = null)
        {
            if (userPrivileges.Contains(AuthorisedAction.AuthorisationAuthManager))
            {
                return this.privilegeRepository.FindAll();
            }

            if (userPrivileges == null || !userPrivileges.Any())
            {
                return new List<Privilege>();
            }

            var privilegesUserCanManage = userPrivileges
                .Where(p => p.ToLower().Contains("auth-manager"))
                .Select(p => p.Split('.')[0])
                .Distinct()
                .ToList();

            if (!privilegesUserCanManage.Any())
            {
                return new List<Privilege>();
            }

            var resultList = this.privilegeRepository
                .FindAll()
                .AsEnumerable()
                .Where(p => privilegesUserCanManage.Contains(p.Name.Split('.')[0]))
                .ToList();


            return resultList;
        }

        public Privilege GetPrivilegeById(int privilegeId, IEnumerable<string> userPrivileges = null)
        {
            if (userPrivileges.Contains(AuthorisedAction.AuthorisationAuthManager))
            {
                return this.privilegeRepository.FindById(privilegeId);
            }

            if (userPrivileges == null || !userPrivileges.Any())
            {
                throw new UnauthorisedActionException("You do not have any permissions");
            }

            var privilegesUserCanManage = userPrivileges
                .Where(p => p.ToLower().Contains("auth-manager"))
                .Select(p => p.Split('.')[0])
                .Distinct()
                .ToList();

            var result = this.privilegeRepository
                .FindById(privilegeId);

            if (privilegesUserCanManage.Contains(result.Name.Split(".")[0]))
            {
                return result;
            }

            throw new UnauthorisedActionException("You do not have the permission to access this privilege");
        }
    }
}