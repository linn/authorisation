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
            app.MapGet("/authorisation/view-permission-users", this.GetApp);
            app.MapGet("/authorisation/create-individual-permission", this.GetApp);
            app.MapGet("/authorisation/create-group-permission", this.GetApp);
            app.MapGet("/authorisation/view-permission-users", this.GetApp);
            app.MapGet("/authorisation/view-individual-permission", this.GetApp);
            app.MapGet("/authorisation/groups", this.GetApp);
            app.MapGet("/authorisation/groups/create", this.GetApp);
            app.MapGet("/authorisation/groups/add-individual-member", this.GetApp);
        }

        private Task Redirect(HttpRequest req, HttpResponse res)
        {
            res.Redirect("/authorisation");
            return Task.CompletedTask;
        }

        private async Task GetApp(HttpRequest req, HttpResponse res)
        {
            await res.Negotiate(new ViewResponse { ViewName = "Index.cshtml" });
        }
    }
}

