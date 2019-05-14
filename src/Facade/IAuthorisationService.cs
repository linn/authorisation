namespace Linn.Authorisation.Facade
{
    using Common.Facade;
    using Domain;

    public interface IAuthorisationService
    {
        IResult<System.Collections.Generic.IEnumerable<Domain.Claim>> GetClaims(string who);
    }

    public class AuthorisationService : IAuthorisationService
    {
        private Common.Persistence.IRepository<Role, int> RoleRepository;

        public IResult<System.Collections.Generic.IEnumerable<Domain.Claim>> GetClaims(string who)
        {
            return null;
            // var roles = this.RoleRepository.FilterBy();
        }
    }
}
