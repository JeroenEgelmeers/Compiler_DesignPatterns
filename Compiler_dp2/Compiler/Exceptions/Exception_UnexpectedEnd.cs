using System;
using System.Runtime.Serialization;

namespace Compiler_dp2.Compiler
{
    [Serializable]
    internal class Exception_UnexpectedEnd : Exception
    {
        public Exception_UnexpectedEnd()
        {
        }

        public Exception_UnexpectedEnd(string message) : base(message)
        {
        }

        public Exception_UnexpectedEnd(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected Exception_UnexpectedEnd(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}