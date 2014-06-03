// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using System.Collections;
    using System.Linq.Expressions;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Creates series for the <see cref="Chart{TModel}" />.
    /// </summary>
    /// <typeparam name="TModel">The type of the data item to which the chart is bound to</typeparam>
    public class ChartSeriesFactory<TModel> : IHideObjectMembers
        where TModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartSeriesFactory{TModel}"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ChartSeriesFactory(Chart<TModel> container)
        {
            Guard.IsNotNull(container, "container");

            Container = container;
        }

        /// <summary>
        /// The parent Chart
        /// </summary>
        public Chart<TModel> Container
        {
            get;
            private set;
        }

        /// <summary>
        /// Defines bound bar series.
        /// </summary>
        /// <param name="expression">
        /// The expression used to extract the series value from the chart model
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Bar<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            Guard.IsNotNull(expression, "expression");

            ChartBarSeries<TModel, TValue> barSeries = new ChartBarSeries<TModel, TValue>(Container, expression);

            Container.Series.Add(barSeries);

            return new ChartBarSeriesBuilder<TModel>(barSeries);
        }

        /// <summary>
        /// Defines bound bar series.
        /// </summary>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Bar(string memberName)
        {
            return Bar(null, memberName);
        }

        /// <summary>
        /// Defines bound bar series.
        /// </summary>
        /// <param name="memberType">
        /// The type of the value member.
        /// </param>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Bar(Type memberType, string memberName)
        {
            const bool liftMemberAccess = false;

            var lambdaExpression = ExpressionBuilder.Lambda<TModel>(memberType, memberName, liftMemberAccess);

#if MVC3
            if (typeof(TModel).IsDynamicObject() && memberType != null && lambdaExpression.Body.Type.GetNonNullableType() != memberType.GetNonNullableType())
            {
                lambdaExpression = Expression.Lambda(Expression.Convert(lambdaExpression.Body, memberType), lambdaExpression.Parameters);
            }
#endif

            var seriesType = typeof(ChartBarSeries<,>).MakeGenericType(new[] { typeof(TModel), lambdaExpression.Body.Type });

            var constructor = seriesType.GetConstructor(new[] { Container.GetType(), lambdaExpression.GetType() });

            var series = (IChartBarSeries)constructor.Invoke(new object[] { Container, lambdaExpression });

            series.Member = memberName;

            if (!series.Name.HasValue())
            {
                series.Name = memberName.AsTitle();
            }

            Container.Series.Add((ChartSeriesBase<TModel>)series);

            return new ChartBarSeriesBuilder<TModel>(series);
        }

        /// <summary>
        /// Defines bar series bound to inline data.
        /// </summary>
        /// <param name="data">
        /// The data to bind to.
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Bar(IEnumerable data)
        {
            Guard.IsNotNull(data, "data");

            ChartBarSeries<TModel, object> barSeries = new ChartBarSeries<TModel, object>(Container, data);

            Container.Series.Add(barSeries);

            return new ChartBarSeriesBuilder<TModel>(barSeries);
        }

        /// <summary>
        /// Defines bound column series.
        /// </summary>
        /// <param name="expression">
        /// The expression used to extract the series value from the chart model
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Column<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var builder = Bar(expression);
            builder.Series.Orientation = ChartBarSeriesOrientation.Vertical;

            return builder;
        }

        /// <summary>
        /// Defines bound bar series.
        /// </summary>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Column(string memberName)
        {
            return Column(null, memberName);
        }

        /// <summary>
        /// Defines bound bar series.
        /// </summary>
        /// <param name="memberType">
        /// The type of the value member.
        /// </param>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Column(Type memberType, string memberName)
        {
            var builder = Bar(memberType, memberName);
            builder.Series.Orientation = ChartBarSeriesOrientation.Vertical;

            return builder;
        }

        /// <summary>
        /// Defines bar series bound to inline data.
        /// </summary>
        /// <param name="data">
        /// The data to bind to
        /// </param>
        public virtual ChartBarSeriesBuilder<TModel> Column(IEnumerable data)
        {
            var builder = Bar(data);
            builder.Series.Orientation = ChartBarSeriesOrientation.Vertical;

            return builder;
        }

        /// <summary>
        /// Defines bound line series.
        /// </summary>
        /// <param name="expression">
        /// The expression used to extract the series value from the chart model
        /// </param>
        public virtual ChartLineSeriesBuilder<TModel> Line<TValue>(Expression<Func<TModel, TValue>> expression)
        {
            Guard.IsNotNull(expression, "expression");

            ChartLineSeries<TModel, TValue> LineSeries = new ChartLineSeries<TModel, TValue>(Container, expression);

            Container.Series.Add(LineSeries);

            return new ChartLineSeriesBuilder<TModel>(LineSeries);
        }

        /// <summary>
        /// Defines bound line series.
        /// </summary>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartLineSeriesBuilder<TModel> Line(string memberName)
        {
            return Line(null, memberName);
        }

        /// <summary>
        /// Defines bound line series.
        /// </summary>
        /// <param name="memberType">
        /// The type of the value member.
        /// </param>
        /// <param name="memberName">
        /// The name of the value member.
        /// </param>
        public virtual ChartLineSeriesBuilder<TModel> Line(Type memberType, string memberName)
        {
            const bool liftMemberAccess = false;

            var lambdaExpression = ExpressionBuilder.Lambda<TModel>(memberType, memberName, liftMemberAccess);

#if MVC3
            if (typeof(TModel).IsDynamicObject() && memberType != null && lambdaExpression.Body.Type.GetNonNullableType() != memberType.GetNonNullableType())
            {
                lambdaExpression = Expression.Lambda(Expression.Convert(lambdaExpression.Body, memberType), lambdaExpression.Parameters);
            }
#endif

            var seriesType = typeof(ChartLineSeries<,>).MakeGenericType(new[] { typeof(TModel), lambdaExpression.Body.Type });

            var constructor = seriesType.GetConstructor(new[] { Container.GetType(), lambdaExpression.GetType() });

            var series = (IChartLineSeries)constructor.Invoke(new object[] { Container, lambdaExpression });

            series.Member = memberName;

            if (!series.Name.HasValue())
            {
                series.Name = memberName.AsTitle();
            }

            Container.Series.Add((ChartSeriesBase<TModel>)series);

            return new ChartLineSeriesBuilder<TModel>(series);
        }

        /// <summary>
        /// Defines line series bound to inline data.
        /// </summary>
        /// <param name="data">
        /// The data to bind to
        /// </param>
        public virtual ChartLineSeriesBuilder<TModel> Line(IEnumerable data)
        {
            var builder = Line(data);

            return builder;
        }
    }
}