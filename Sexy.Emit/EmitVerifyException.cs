using System;

namespace Sexy.Emit
{
    public class EmitVerifyException : Exception
    {
        public EmitVerifyException()
        {
        }

        public EmitVerifyException(string message) : base(message)
        {
        }

        public EmitVerifyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
