using System;
using System.Runtime.Serialization;

namespace Compiler_dp2.Tokenizer
{
    [Serializable]
    internal class Exception_StackNotEmpty : Exception
    {
        public Exception_StackNotEmpty()
        {
        }

        public Exception_StackNotEmpty(string message) : base(message)
        {
        }

        public Exception_StackNotEmpty(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected Exception_StackNotEmpty(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}