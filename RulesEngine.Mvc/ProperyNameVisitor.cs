using System.Linq.Expressions;

namespace SpearSoft.RulesEngine.Mvc
{
    internal class ProperyNameVisitor : ExpressionVisitor
    {
        string _propertyName = null;
        ParameterExpression _p;
        
        /// <summary>
        /// Gets PropertyName
        /// </summary>
        public string PropertyName
        {
            get { return _propertyName; }
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            if (node.Parameters.Count == 1) _p = node.Parameters[0];
            return base.VisitLambda<T>(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression == _p 
                && (node.Member.MemberType == System.Reflection.MemberTypes.Field || node.Member.MemberType == System.Reflection.MemberTypes.Property
                ))
            {
                _propertyName = node.Member.Name;
            }
            return base.VisitMember(node);
        }
    }
}
