namespace Linn.Authorisation.Service.Host.Modules
{
    using Carter;
    using Carter.Request;
    using Carter.Response;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public sealed class HealthCheckModule : CarterModule
    {
        public HealthCheckModule()
        {
            this.Get("/healthcheck", this.GetHealthcheck);
        }

        private async Task GetHealthcheck(HttpRequest req, HttpResponse res, RouteData routeData)
        {
            res.StatusCode = 200;

            await res.Negotiate(null);
        }
    }
}
