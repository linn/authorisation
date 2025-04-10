using System;

namespace Linn.Authorisation.Domain.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.Security.Claims;
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
            if (userPrivileges.Contains("authorisation.super-user"))
            {
                return this.privilegeRepository.FindAll();
            }

            if (userPrivileges == null || !userPrivileges.Any())
            {
                throw new LackingPermissionException("You do not have any permissions");
            }

            var adminDepartments = userPrivileges
                .Where(p => p.ToLower().Contains("super-user"))
                .Select(p => p.Split('.')[0])
                .Distinct()
                .ToList();

            if (!adminDepartments.Any())
            {
                throw new LackingPermissionException("You are not an super user of any department");
            }

            var resultList = this.privilegeRepository
                .FilterBy(p => true)
                .AsEnumerable()
                .Where(p => adminDepartments.Contains(p.Name.Split('.')[0]))
                .ToList();


            return resultList;
        }

        public Privilege GetPrivilegeById(int privilegeId, IEnumerable<string> userPrivileges = null)
        {
            if (userPrivileges.Contains("authorisation.super-user"))
            {
                return this.privilegeRepository.FindById(privilegeId); ;
            }

            if (userPrivileges == null || !userPrivileges.Any())
            {
                throw new LackingPermissionException("You do not have any permissions");
            }

            var adminDepartments = userPrivileges
                .Where(p => p.ToLower().Contains("super-user"))
                .Select(p => p.Split('.')[0])
                .Distinct()
                .ToList();

            if (!adminDepartments.Any())
            {
                throw new LackingPermissionException("You are not an super user of any department");
            }

            var result = this.privilegeRepository
                .FindById(privilegeId);

            if (adminDepartments.Contains(result.Name.Split(".")[0]))
            {
                return result;
            }

            throw new LackingPermissionException("You do not have the permission to access this privilege");
        }

    }
}