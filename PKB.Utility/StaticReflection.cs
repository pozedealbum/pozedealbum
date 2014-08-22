using System;
using System.Linq.Expressions;
using System.Reflection;

namespace PKB.Utility
{
    public static class StaticReflection
    {
        public static MemberInfo GetMember<T>(
            this T instance,
            Expression<Func<T, object>> expression)
        {
            return GetMember(expression);
        }


        public static MemberInfo GetMember<T>(
            Expression<Func<T, object>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException(
                    "The expression cannot be null.");
            }

            return GetMember(expression.Body);
        }

        public static MemberInfo GetMember<T>(
            this T instance,
            Expression<Action<T>> expression)
        {
            return GetMember(expression);
        }

        public static MemberInfo GetMember<T>(
            Expression<Action<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException(
                    "The expression cannot be null.");
            }

            return GetMember(expression.Body);
        }

        private static MemberInfo GetMember(
            Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentException(
                    "The expression cannot be null.");
            }

            if (expression is MemberExpression)
            {
                // Reference type property or field
                var memberExpression =
                    (MemberExpression)expression;
                return memberExpression.Member;
            }

            if (expression is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression =
                    (MethodCallExpression)expression;
                return methodCallExpression.Method;
            }

            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMember(unaryExpression);
            }

            throw new ArgumentException("Invalid expression");
        }

        private static MemberInfo GetMember(
            UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MethodCallExpression)
            {
                var methodExpression =
                    (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method;
            }

            return ((MemberExpression)unaryExpression.Operand)
                .Member;
        }
    }
}
