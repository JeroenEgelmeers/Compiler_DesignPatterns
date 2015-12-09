using System;
using System.Runtime.Serialization;

namespace Compiler_dp2.Compiler
{
    [Serializable]
    internal class Exception_FactoryFailed : Exception
    {
        public Exception_FactoryFailed()
        {
        }

        public Exception_FactoryFailed(string message) : base(message)
        {
        }

        public Exception_FactoryFailed(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected Exception_FactoryFailed(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}