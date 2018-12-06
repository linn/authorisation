namespace Linn.Authorisation.Service.Modules
{
    using Linn.Authorisation.Service.Models;

    using Nancy;
    using Nancy.Responses;

    public sealed class HomeModule : NancyModule
    {
        public HomeModule()
        {
            this.Get("/", args => new RedirectResponse("/authorisation"));
            this.Get("/authorisation", _ => this.GetApp());

            this.Get("/authorisation/signin-oidc-client", _ => this.GetApp());
            this.Get("/authorisation/signin-oidc-silent", _ => this.SilentRenew());
        }

        private object SilentRenew()
        {
            return this.Negotiate.WithView("silent-renew");
        }

        private object GetApp()
        {
            return this.Negotiate.WithModel(ApplicationSettings.Get()).WithView("Index");
        }
    }
}