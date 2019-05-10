namespace Linn.Authorisation.Service.Modules
{
    using Linn.Authorisation.Service.Models;
    using Carter;
    using Carter.Response;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using System.Threading.Tasks;

    public sealed class HomeModule : CarterModule
    {
        public HomeModule()
        {
            //this.Get("/", args => new RedirectResponse("/authorisation"));
            this.Get("/authorisation", this.GetApp);

            this.Get("/authorisation/signin-oidc-client", this.GetApp);
            this.Get("/authorisation/signin-oidc-silent", this.GetSilentRenew);
        }

        private async Task GetApp(HttpRequest req, HttpResponse res, RouteData routeData)
        {
            await res.Negotiate(new ViewResponse { ViewName = "Index.html" });
        }

        private async Task GetSilentRenew(HttpRequest req, HttpResponse res, RouteData routeData)
        {
            await res.Negotiate(new ViewResponse { ViewName = "SilentRenew.html" });
        }
    }
}