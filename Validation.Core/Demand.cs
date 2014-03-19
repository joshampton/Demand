using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Validation.Core
{
    public static class Demand
    {
        public static Target<TargetType> That<TargetType>(string name, TargetType value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be null or empty", "name");

            return new Target<TargetType>(name, value);
        }

        public static Target<TargetType> That<TargetType>(Expression<Func<TargetType>> targetSelector)
        {
            var visitor = new ValidationExpressionVisitor();
            visitor.Visit(targetSelector);

            var compiled = targetSelector.Compile();
            var value = compiled.Invoke();

            return new Target<TargetType>(visitor.Expression, value);
        }
    }
}
