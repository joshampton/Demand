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
        private readonly string targetName;
        private readonly string validation;

        public string TargetName { get { return this.targetName; } }
        public string Validation { get { return this.validation; } }

        public ValidationFailedException(string targetName, string validation, Exception innerException = null) : base(null, innerException)
        {
            if (string.IsNullOrEmpty(targetName))
                throw new ArgumentException("targetName cannot be null or empty", "targetName");

            if (string.IsNullOrEmpty(validation))
                throw new ArgumentException("validation cannot be null or empty", "validation");

            this.targetName = targetName;
            this.validation = validation;
        }

        protected ValidationFailedException(
          SerializationInfo info,
          StreamingContext context)
            : base(info, context) 
        {
            this.targetName = info.GetString("targetName");
            this.validation = info.GetString("validation");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            info.AddValue("targetName", targetName, typeof(string));
            info.AddValue("validation", validation, typeof(string));

            base.GetObjectData(info, context);
        }
    }
}
