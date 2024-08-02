namespace Linn.Authorisation.Domain.Exceptions
{
    using System;
    using Common.Domain.Exceptions;

    public class GroupRecursionException : DomainException
    {
        public GroupRecursionException(string message) : base(message)
        {
        }

        public GroupRecursionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}