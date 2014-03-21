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
        private readonly List<Expression> arguments;

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
            this.arguments = new List<Expression>();
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
            this.arguments.AddRange(node.Arguments);

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

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (!node.Type.Name.Contains("<>") && !this.arguments.Contains(node))
                members.Push(string.Format("{0}", node.Value));
            return base.VisitConstant(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            return base.Visit(node.Body);
        }
    }
}
