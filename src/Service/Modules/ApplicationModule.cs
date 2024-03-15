namespace Linn.Authorisation.Service.Modules
{
    using System.Threading.Tasks;

    using Linn.Authorisation.Service.Models;
    using Linn.Common.Service.Core;
    using Linn.Common.Service.Core.Extensions;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class ApplicationModule : IModule
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/", this.Redirect);
            app.MapGet("/authorisation", this.GetApp);
            app.MapGet("/authorisation/redirect", this.Redirect);
        }

        private async Task Redirect(HttpRequest req, HttpResponse res)
        {
            await res.Negotiate(new ViewResponse { ViewName = "Index.cshtml" });
        }

        private async Task GetApp(HttpRequest req, HttpResponse res)
        {
            await res.Negotiate(new ViewResponse { ViewName = "Index.cshtml" });
        }
    }
}
