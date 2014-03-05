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

            if (!pass)
                throw new ValidationFailedException(null, exception);

            return target;
        }

        public static Target<TargetType> Fails<TargetType>(this Target<TargetType> target, Predicate<TargetType> validation, string message = null)
        {
            bool pass = false;
            Exception exception = null;

            try
            {
                pass = !validation(target.TypeSafeValue);
            }
            catch (Exception e)
            {
                pass = false;
                exception = e;
            }

            if (!pass)
                throw new ValidationFailedException(null, exception);

            return target;
        }
    }
}
