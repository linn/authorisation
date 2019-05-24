namespace Linn.Authorisation.Service.Modules
{
    using Linn.Authorisation.Facade;
    using Linn.Authorisation.Service.Models;

    using Nancy;

    public sealed class AuthorisationModule : NancyModule
    {
        private readonly IAuthorisationService authorisationService;

        public AuthorisationModule(IAuthorisationService authorisationService)
        {
            this.authorisationService = authorisationService;

            this.Get("/privileges/{who*}", parameters => this.GetPrivilegesForMember(parameters.who));
        }

        private object GetPrivilegesForMember(string who)
        {
            var result = this.authorisationService.GetPrivilegesForMember(who);
            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
