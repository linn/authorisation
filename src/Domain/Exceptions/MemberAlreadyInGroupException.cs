namespace Linn.Authorisation.Domain.Exceptions
{
    using System;
    using Common.Domain.Exceptions;

    public class MemberAlreadyInGroupException : DomainException
    {
        public MemberAlreadyInGroupException(string message) : base(message)
        {
        }

        public MemberAlreadyInGroupException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
