using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Validation.Core
{
    [Serializable]
    public class ValidationFailedException : Exception
    {
        public ValidationFailedException() { }
        public ValidationFailedException(string message) : base(message) { }
        public ValidationFailedException(string message, Exception inner) : base(message, inner) { }
        protected ValidationFailedException(
          SerializationInfo info,
          StreamingContext context)
            : base(info, context) { }
    }
}
