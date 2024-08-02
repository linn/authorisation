namespace Linn.Authorisation.Domain.Exceptions
{
    using System;
    using Common.Domain.Exceptions;

    public class CannotAddGroupToItselfException : DomainException
    {
        public CannotAddGroupToItselfException(string message) : base(message)
        {
        }

        public CannotAddGroupToItselfException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}