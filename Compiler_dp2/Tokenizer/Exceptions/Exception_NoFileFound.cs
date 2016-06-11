using System;
using System.Runtime.Serialization;

namespace Compiler_dp2.Tokenizer
{
    [Serializable]
    internal class Exception_NoFileFound : Exception
    {
        public Exception_NoFileFound()
        {
        }

        public Exception_NoFileFound(string message) : base(message)
        {
        }

        public Exception_NoFileFound(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected Exception_NoFileFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}