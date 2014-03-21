using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Validation.Core
{
    public static class CoreExtensions
    {
        public static Target<TargetType> Passes<TargetType>(this Target<TargetType> target, Expression<Predicate<TargetType>> validation, string message = null)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            if (validation == null)
                throw new ArgumentNullException("validation");

            bool pass = false;
            Exception exception = null;
            string validationText = validation.Visit();

            try
            {
                var compiledValidation = validation.Compile();
                pass = compiledValidation(target.TypeSafeValue);
            }
            catch(Exception e)
            {
                pass = false;
                exception = e;
            }

            if (!pass)
                throw new ValidationFailedException(target.Name, validationText, null, exception);

            return target;
        }

        public static Target<TargetType> Fails<TargetType>(this Target<TargetType> target, Expression<Predicate<TargetType>> validation, string message = null)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            if (validation == null)
                throw new ArgumentNullException("validation");

            bool pass = false;
            Exception exception = null;
            string validationText = validation.Visit();

            try
            {
                var compiledValidation = validation.Compile();
                pass = !compiledValidation(target.TypeSafeValue);
            }
            catch (Exception e)
            {
                pass = false;
                exception = e;
            }

            if (!pass)
                throw new ValidationFailedException(target.Name, validationText, null, exception);

            return target;
        }

        public static string Visit(this Expression target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            var visitor = new ValidationExpressionVisitor();
            visitor.Visit(target);

            return visitor.Expression;
        }
    }
}
