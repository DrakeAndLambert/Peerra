using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;

namespace DrakeLambert.Peerra.WebApi.Infrastructure
{
    [Serializable]
    internal class CreateUserException : Exception
    {
        public CreateUserException(IdentityResult createUserResult) : this(createUserResult.Errors.Select(error => $"Code: {error.Code}; Description: {error.Description}.").Aggregate((error1, error2) => error1 + "\n" + error2))
        { }

        public CreateUserException(string message) : base(message)
        { }

        public CreateUserException(string message, Exception innerException) : base(message, innerException)
        { }

        protected CreateUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}