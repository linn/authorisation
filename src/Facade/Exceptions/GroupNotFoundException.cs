namespace Linn.Authorisation.Facade.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class GroupNotFoundException : DomainException
    {
        public GroupNotFoundException(string message)
            : base(message)
        {
        }

        public GroupNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}