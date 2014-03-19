using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Validation.Core
{
    public sealed class ValidationExpressionVisitor : DynamicExpressionVisitor
    {
        private readonly Stack<string> members;

        public string Expression
        {
            get
            {
                return string.Join(".", this.members.ToArray());
            }
        }

        public ValidationExpressionVisitor()
        {
            this.members = new Stack<string>();
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            members.Push(node.Member.Name);

            return base.VisitMember(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var parameters = node.Method.GetParameters()
                .Select(p => string.Format("{0} {1}", p.ParameterType.Name, p.Name))
                .ToArray();

            var paramString = string.Join(", ", parameters);

            var genericParameters = node.Method.GetGenericArguments()
                .Select(t => t.Name)
                .ToArray();

            var genericParamString = genericParameters.Length == 0 ? null : string.Format("<{0}>", string.Join(",", genericParameters));

            members.Push(string.Format("{0}{1}({2})", node.Method.Name, genericParamString, paramString));

            return base.VisitMethodCall(node);
        }
    }
}
