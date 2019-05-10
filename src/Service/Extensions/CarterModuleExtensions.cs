namespace Linn.Authorisation.Service.Extensions
{
    using Carter;

    public static class CarterModuleExtensions
    {
        public static void RequiresEmployeeClaim(this CarterModule module) => module.RequiresClaims(c => c.Type == "employee");
    }
}
