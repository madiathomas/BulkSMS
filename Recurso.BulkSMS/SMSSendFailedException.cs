using System;
using System.Runtime.Serialization;

namespace Recurso.BulkSMS
{
    [Serializable]
    public class SMSSendFailedException : Exception
    {
        private Exception errorException;

        public SMSSendFailedException()
        {
        }

        public SMSSendFailedException(Exception errorException)
        {
            this.errorException = errorException;
        }

        public SMSSendFailedException(string message) : base(message)
        {
        }

        public SMSSendFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SMSSendFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}