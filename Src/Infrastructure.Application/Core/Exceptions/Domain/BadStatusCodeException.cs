using System;
using System.Runtime.Serialization;

namespace Infrastructure.Application.Core.Exceptions.Domain
{
    [Serializable]
    public class BadStatusCodeException : Exception
    {
        public BadStatusCodeException()
        {
        }

        public BadStatusCodeException(string message) : base(message)
        {
        }

        public BadStatusCodeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected BadStatusCodeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}