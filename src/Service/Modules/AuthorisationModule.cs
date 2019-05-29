namespace Linn.Authorisation.Service.Modules
{
    using Domain.Services;
    using Linn.Authorisation.Facade;
    using Linn.Authorisation.Resources;
    using Linn.Authorisation.Service.Models;

    using Nancy;
    using Nancy.ModelBinding;

    public sealed class AuthorisationModule : NancyModule
    {
        private readonly IAuthorisationService authorisationService;

        public AuthorisationModule(IAuthorisationService authorisationService)
        {
            this.authorisationService = authorisationService;

            this.Get("/authorisation/privileges", _ => this.GetPrivileges());
        }

        private object GetPrivileges()
        {
            var resource = this.Bind<AuthorisationRequestResource>();
            var result = this.authorisationService.GetPrivilegesForMember(resource.Who);
            return this.Negotiate.WithModel(result);
        }
    }
}
