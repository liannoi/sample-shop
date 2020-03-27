using System;
using System.Net;
using System.Runtime.Serialization;

namespace Infrastructure.Application.Core.Exceptions.Domain
{
    [Serializable]
    public class BadRequestException : BadStatusCodeException
    {
        public BadRequestException() : base(HttpStatusCode.BadRequest)
        {
        }

        public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest)
        {
        }

        public BadRequestException(string message, Exception inner) : base(message, inner)
        {
        }

        protected BadRequestException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}