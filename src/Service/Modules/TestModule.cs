namespace Linn.Authorisation.Service.Modules
{
    using System;

    public class TestModule : Carter.CarterModule
    {
        public TestModule()
        {
            this.Get("/authorisation/test", this.Test);
        }

        private async System.Threading.Tasks.Task Test(Microsoft.AspNetCore.Http.HttpRequest req, Microsoft.AspNetCore.Http.HttpResponse res, Microsoft.AspNetCore.Routing.RouteData routeData)
        {
            throw new Exception("Will this be logged");
        }
    }
}