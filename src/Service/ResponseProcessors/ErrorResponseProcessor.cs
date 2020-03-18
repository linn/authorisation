namespace Linn.Authorisation.Service.ResponseProcessors
{
    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Common.Nancy.Facade;

    public class ErrorResponseProcessor : JsonResponseProcessor<Error>
    {
        public ErrorResponseProcessor(IResourceBuilder<Error> resourceBuilder)
            : base(resourceBuilder, "error", 1)
        {
        }
    }
}