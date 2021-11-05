namespace Linn.Authorisation.Domain.Exceptions
{
    using System;
    using Common.Domain.Exceptions;

    public class NoGranteeUriProvidedException : DomainException
    {
        public NoGranteeUriProvidedException(string message) : base(message)
        {
        }

        public NoGranteeUriProvidedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
