using System;
using System.Runtime.Serialization;

namespace Compiler_dp2.Tokenizer
{
    [Serializable]
    internal class Exception_UnableToPopStack : Exception
    {
        public Exception_UnableToPopStack()
        {
        }

        public Exception_UnableToPopStack(string message) : base(message)
        {
        }

        public Exception_UnableToPopStack(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected Exception_UnableToPopStack(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}