namespace Linn.Authorisation.Domain.Exceptions
{
    using System;
    using Common.Domain.Exceptions;

    public class DuplicateGroupNameException : DomainException
    {
        public DuplicateGroupNameException(string message) : base(message)
        {
        }

        public DuplicateGroupNameException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}