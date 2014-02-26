using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validation.Core
{
    public static class CoreExtensions
    {
        public static Target<TargetType> Passes<TargetType>(this Target<TargetType> target, Predicate<TargetType> validation, string message = null)
        {
            bool pass = false;
            Exception exception = null;

            try
            {
                pass = validation(target.TypeSafeValue);
            }
            catch(Exception e)
            {
                pass = false;
                exception = e;
            }

            var result = new ValidationResult(pass, message, exception);

            target.AddValidationResult(result);

            return target;
        }
    }
}
