using System;
using System.Runtime.Serialization;

namespace Lingva.MVC.Infrastructure.Exceptions
{
    [Serializable()]
    public class LingvaCustomException : Exception
    {
        public LingvaCustomException() { }

        public LingvaCustomException(string message) : base(message) { }

        public LingvaCustomException(string message, Exception inner) : base(message, inner) { }

        protected LingvaCustomException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
