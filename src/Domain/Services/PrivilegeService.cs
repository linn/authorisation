using System;

namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Claims;
    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Authorisation.Domain.Groups;
    using Linn.Authorisation.Domain.Permissions;
    using Linn.Common.Persistence;

    public class PrivilegeService : IPrivilegeService
    {
        private readonly IRepository<Privilege, int> privilegeRepository;

        public PrivilegeService(IRepository<Privilege, int> privilegeRepository)
        {
            this.privilegeRepository = privilegeRepository;
        }

        public IEnumerable<Privilege> GetPrivilegesForPermission(IEnumerable<string> userPrivileges = null)
        {
            if (userPrivileges == null || !userPrivileges.Any())
            {
                throw new LackingPermissionException("You do not have any permissions");
            }

            var adminDepartments = userPrivileges
                .Where(p => p.ToLower().Contains("admin")) 
                .Select(p => p.Split('.')[0])  
                .Distinct()
                .ToList();

            if (!adminDepartments.Any())
            {
                throw new LackingPermissionException("You are not an super user of any department");
            }

            var resultList = this.privilegeRepository.FilterBy(p =>
                adminDepartments.Any(dept => p.Name.StartsWith(dept + "."))).ToList();

            return resultList;
        }


        public Privilege GetPrivilegeById(int privilegeId, IEnumerable<string> userPrivileges = null)
        {
            if (userPrivileges == null || !userPrivileges.Any())
            {
                throw new LackingPermissionException("You do not have any permissions");
            }

            var isSuperUser = userPrivileges.Any(p => p.Equals("authorisation.superuser", StringComparison.OrdinalIgnoreCase));

            var adminDepartments = userPrivileges
                .Where(p => p.ToLower().Contains("admin"))
                .Select(p => p.Split('.')[0]) 
                .Distinct()
                .ToList();

            if (!isSuperUser && !adminDepartments.Any())
            {
                throw new LackingPermissionException("You are not an admin of any department, nor a superuser.");
            }

            return this.privilegeRepository.FindBy(p =>
                p.Id == privilegeId && (isSuperUser || adminDepartments.Any(dept => p.Name.StartsWith(dept + "."))));
        }

    }
}