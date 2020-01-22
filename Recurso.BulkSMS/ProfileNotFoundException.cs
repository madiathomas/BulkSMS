using System;
using System.Runtime.Serialization;

namespace Recurso.BulkSMS
{
    [Serializable]
    public class ProfileNotFoundException : Exception
    {
        private Exception errorException;

        public ProfileNotFoundException()
        {
        }

        public ProfileNotFoundException(Exception errorException)
        {
            this.errorException = errorException;
        }

        public ProfileNotFoundException(string message) : base(message)
        {
        }

        public ProfileNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProfileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}