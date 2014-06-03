namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    using Extensions;
    using Infrastructure;

    public class GridColumnGenerator
    {
        private readonly IPropertyCache propertyCache;

        public GridColumnGenerator(IPropertyCache propertyCache)
        {
            Guard.IsNotNull(propertyCache, "propertyCache");

            this.propertyCache = propertyCache;
        }

        public GridColumnGenerator() : this(ServiceLocator.Current.Resolve<IPropertyCache>())
        {
        }

        public IEnumerable<GridColumn<T>> GetColumns<T>() where T : class
        {
            return propertyCache.GetReadOnlyProperties(typeof(T))
                                .Where(property => property.PropertyType.IsEnum || (property.PropertyType != typeof(object) && property.PropertyType.IsPredefinedType()))
                                .Select(property => CreateGetterExpression<T>(property))
                                .Select(expression => new GridColumn<T>(expression));
        }

        private static Expression<Func<T, object>> CreateGetterExpression<T>(PropertyInfo property) where T : class
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "x");
            Expression propertyExpression = Expression.Property(parameterExpression, property);

            if (property.PropertyType.IsValueType)
            {
                propertyExpression = Expression.Convert(propertyExpression, typeof(object));
            }

            return Expression.Lambda<Func<T, object>>(propertyExpression, parameterExpression);
        }
    }
}