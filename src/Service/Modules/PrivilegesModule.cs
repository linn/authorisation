namespace Linn.Authorisation.Service.Modules
{
    using Linn.Authorisation.Facade;
    using Linn.Authorisation.Service.Models;

    using Nancy;

    public sealed class PrivilegesModule : NancyModule
    {
        private readonly IAuthorisationService authorisationService;

        public PrivilegesModule(IAuthorisationService authorisationService)
        {
            this.authorisationService = authorisationService;

            this.Get("/privileges/{who}", parameters => this.GetPrivileges(parameters.who));
        }

        private object GetPrivileges(string who)
        {
            var result = this.authorisationService.GetPrivileges(who);
            return this.Negotiate.WithModel(result).WithMediaRangeModel("text/html", ApplicationSettings.Get)
                .WithView("Index");
        }
    }
}
