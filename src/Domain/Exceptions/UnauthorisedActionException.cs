﻿namespace Linn.Authorisation.Domain.Exceptions
{
    using System;

    using Linn.Common.Domain.Exceptions;

    public class UnauthorisedActionException : DomainException
    {
        public UnauthorisedActionException(string message)
            : base(message)
        {
        }

        public UnauthorisedActionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
