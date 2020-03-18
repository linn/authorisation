namespace Linn.Production.Facade.ResourceBuilders
{
    using System;
    using System.Collections.Generic;
    using Linn.Authorisation.Domain.Exceptions;
    using Linn.Common.Facade;
    using Linn.Common.Resources;

    public class ErrorResourceBuilder : IResourceBuilder<Error>
    {
        public ErrorResource Build(Error error)
        {
            return new ErrorResource { Errors = new List<string> { error.Message } };
        }

        public string GetLocation(Error error)
        {
            throw new NotImplementedException();
        }

        object IResourceBuilder<Error>.Build(Error error) => this.Build(error);
    }
}
