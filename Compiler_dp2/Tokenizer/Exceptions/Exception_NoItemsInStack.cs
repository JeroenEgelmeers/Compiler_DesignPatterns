using System;
using System.Runtime.Serialization;

namespace Compiler_dp2.Tokenizer
{
    [Serializable]
    internal class Exception_NoItemsInStack : Exception
    {
        public Exception_NoItemsInStack()
        {
        }

        public Exception_NoItemsInStack(string message) : base(message)
        {
        }

        public Exception_NoItemsInStack(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected Exception_NoItemsInStack(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}