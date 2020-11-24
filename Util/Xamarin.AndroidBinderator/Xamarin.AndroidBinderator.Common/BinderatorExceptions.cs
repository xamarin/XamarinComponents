using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AndroidBinderator.Common
{
    public class AndroidBinderatorException : Exception
    {
        public AndroidBinderatorException()
        {
        }

        public AndroidBinderatorException(string message) : base(message)
        {
        }

        public AndroidBinderatorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AndroidBinderatorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
