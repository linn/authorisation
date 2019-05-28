namespace Linn.Authorisation.Service.Modules
{
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

            this.Get("/privileges", _ => this.GetPrivilegesForMember());
        }

        private object GetPrivilegesForMember()
        {
            var resource = this.Bind<AuthorisationRequestResource>();
            var result = this.authorisationService.GetPrivilegesForMember(resource.Who);
            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
