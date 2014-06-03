﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web.Mvc;
    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.Infrastructure.Implementation;
    using Telerik.Web.Mvc.Resources;
    using Telerik.Web.Mvc.UI.Html;

    public class GridBoundColumn<TModel, TValue> : GridColumnBase<TModel>, IGridBoundColumn, IGridTemplateColumn<TModel> where TModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridBoundColumn{T}"/> class.
        /// </summary>
        /// <param name="value">The property to which the column is bound to.</param>
        public GridBoundColumn(Grid<TModel> grid, Expression<Func<TModel, TValue>> expression)
            : base(grid)
        {
            Guard.IsNotNull(expression, "expression");

            if (
#if MVC3
!typeof(TModel).IsDynamicObject() &&
#endif
 !typeof(TModel).IsDataRow() && !(typeof(TModel).IsCompatibleWith(typeof(ICustomTypeDescriptor))) && !expression.IsBindable())
            {
                throw new InvalidOperationException(TextResource.MemberExpressionRequired);
            }

            Expression = expression;
            Member = expression.MemberWithoutInstance();
            MemberType = TypeFromMemberExpression(expression.ToMemberExpression());
            Value = expression.Compile();
            Aggregates = new List<AggregateFunction>();
            GroupFooterTemplate = new HtmlTemplate<GridAggregateResult>();
            GroupHeaderTemplate = new HtmlTemplate<GridGroupAggregateResult>();

#if MVC2 || MVC3
            if (
#if MVC3
!typeof(TModel).IsDynamicObject() &&
#endif
 !typeof(TModel).IsDataRow() && !(typeof(TModel).IsCompatibleWith(typeof(ICustomTypeDescriptor))))
            {
                Metadata = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>());
                MemberType = Metadata.ModelType;
                Title = Metadata.DisplayName;
                Format = Metadata.DisplayFormatString;
                Visible = Metadata.ShowForDisplay;
                ReadOnly = Metadata.IsReadOnly;
            }
#endif
            if (string.IsNullOrEmpty(Title))
            {
                Title = Member.AsTitle();
            }
        }

        /// <summary>
        /// Gets type of the property to which the column is bound to.
        /// </summary>
        public Type MemberType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this column is groupable.
        /// </summary>
        /// <value><c>true</c> if groupable; otherwise, <c>false</c>.</value>
        public bool Groupable
        {
            get
            {
                return Settings.Groupable;
            }
            set
            {
                Settings.Groupable = value;
            }
        }

        /// <summary>
        /// Gets the name of the column
        /// </summary>
        [Obsolete("Use the Member property instead")]
        public string Name
        {
            get
            {
                return Member;
            }
            set
            {
                Member = value;
            }
        }

#if MVC2 || MVC3

        public bool ReadOnly
        {
            get
            {
                return Settings.ReadOnly;
            }
            set
            {
                Settings.ReadOnly = value;
            }
        }

        public ModelMetadata Metadata
        {
            get;
            private set;
        }

        public string EditorTemplateName
        {
            get;
            set;
        }

#endif

        public string ClientGroupHeaderTemplate
        {
            get;
            set;
        }

        public string ClientGroupFooterTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a function which returns the value of the property to which the column is bound to.
        /// </summary>
        public Func<TModel, TValue> Value
        {
            get;
            private set;
        }

        public Expression<Func<TModel, TValue>> Expression
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GridColumnBase&lt;T&gt;"/> is sortable.
        /// </summary>
        /// <value><c>true</c> if sortable; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool Sortable
        {
            get
            {
                return Settings.Sortable;
            }
            set
            {
                Settings.Sortable = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GridColumnBase&lt;T&gt;"/> is filterable.
        /// </summary>
        /// <value><c>true</c> if filterable; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool Filterable
        {
            get
            {
                return Settings.Filterable;
            }

            set
            {
                Settings.Filterable = value;
            }
        }

        public ICollection<AggregateFunction> Aggregates
        {
            get;
            set;
        }

        public override IGridColumnSerializer CreateSerializer()
        {
            return new GridBoundColumnSerializer(this);
        }

        private static Type TypeFromMemberExpression(MemberExpression memberExpression)
        {
            if (memberExpression == null)
            {
                return null;
            }

            MemberInfo memberInfo = memberExpression.Member;

            if (memberInfo.MemberType == MemberTypes.Property)
            {
                return ((PropertyInfo)memberInfo).PropertyType;
            }

            if (memberInfo.MemberType == MemberTypes.Field)
            {
                return ((FieldInfo)memberInfo).FieldType;
            }

            throw new NotSupportedException();
        }

        public string GetSortUrl()
        {
            IList<SortDescriptor> orderBy = new List<SortDescriptor>(Grid.DataProcessor.SortDescriptors);
            SortDescriptor descriptor = orderBy.SingleOrDefault(c => c.Member.IsCaseInsensitiveEqual(Member));

            ListSortDirection? oldDirection = null;

            if (descriptor != null)
            {
                oldDirection = descriptor.SortDirection;

                ListSortDirection? newDirection = oldDirection.Next();

                if (newDirection == null)
                {
                    orderBy.Remove(descriptor);
                }
                else
                {
                    if (Grid.Sorting.SortMode == GridSortMode.SingleColumn)
                    {
                        orderBy.Clear();
                        orderBy.Add(new SortDescriptor { SortDirection = newDirection.Value, Member = descriptor.Member });
                    }
                    else
                    {
                        orderBy[orderBy.IndexOf(descriptor)] = new SortDescriptor { SortDirection = newDirection.Value, Member = descriptor.Member };
                    }
                }
            }
            else
            {
                if (Grid.Sorting.SortMode == GridSortMode.SingleColumn)
                {
                    orderBy.Clear();
                }

                orderBy.Add(new SortDescriptor { Member = Member, SortDirection = ListSortDirection.Ascending });
            }

            return Grid.UrlBuilder.SelectUrl(GridUrlParameters.OrderBy, GridDescriptorSerializer.Serialize(orderBy));
        }

        public ListSortDirection? SortDirection
        {
            get
            {
                var descriptor = Grid.DataProcessor
                                     .SortDescriptors
                                     .FirstOrDefault(column => column.Member.IsCaseInsensitiveEqual(Member));

                if (descriptor == null)
                {
                    return null;
                }

                return descriptor.SortDirection;
            }
        }

        protected override IGridDataCellBuilder CreateDisplayBuilderCore(IGridHtmlHelper htmlHelper)
        {
            if (Template != null || InlineTemplate != null)
            {
                return base.CreateDisplayBuilderCore(htmlHelper);
            }

#if MVC2 || MVC3
            if (!Format.HasValue() && Encoded && !typeof(TModel).IsDataRow()
#if MVC3

 && !typeof(TModel).IsDynamicObject()
#endif
)
            {
                return new GridDisplayForCellBuilder<TModel, TValue>
                {
                    Expression = Expression,
                    HtmlAttributes = HtmlAttributes,
                    ViewContext = Grid.ViewContext
                };
            }
#endif
            return new GridDataCellBuilder<TModel, TValue>
            {
                Encoded = Encoded,
                Format = Format,
                Value = Value,
                HtmlAttributes = HtmlAttributes
            };
        }

        protected override IGridDataCellBuilder CreateEditBuilderCore(IGridHtmlHelper htmlHelper)
        {
#if MVC2 || MVC3
            if (!ReadOnly)
            {
                return new GridEditorForCellBuilder<TModel, TValue>()
                {
                    Expression = Expression,
                    HtmlAttributes = HtmlAttributes,
                    ViewContext = Grid.ViewContext,
                    TemplateName = EditorTemplateName,
                    Member = Member
                };
            }
#endif
            return CreateDisplayBuilder(htmlHelper);
        }

        protected override IGridDataCellBuilder CreateInsertBuilderCore(IGridHtmlHelper htmlHelper)
        {
            return CreateEditBuilderCore(htmlHelper);
        }

        protected override IGridCellBuilder CreateHeaderBuilderCore()
        {
            IGridCellBuilder builder = null;

            if (Sortable && Grid.Sorting.Enabled)
            {
                builder = new GridSortableHeaderCellBuilder(HeaderHtmlAttributes, GetSortUrl(), SortDirection, Grid.Localization.SortedAsc, Grid.Localization.SortedDesc, AppendHeaderContent);
            }
            else
            {
                builder = base.CreateHeaderBuilderCore();
            }

            if (Filterable && Grid.Filtering.Enabled)
            {
                var filtered = Grid.DataProcessor.FilterDescriptors
                                   .SelectMemberDescriptors()
                                   .Any(filter => filter.Member.IsCaseInsensitiveEqual(Member));

                builder.Decorators.Add(new GridFilterCellDecorator(filtered, Grid.Localization.Filter));
            }

            return builder;
        }

        protected override IGridCellBuilder CreateFooterBuilderCore(IEnumerable<AggregateResult> aggregateResults)
        {
            return new GridFooterCellBuilder(FooterHtmlAttributes, FooterTemplate)
            {
                AggregateResults = CalculateAggregates(aggregateResults)
            };
        }

        protected override IGridCellBuilder CreateGroupFooterBuilderCore(IEnumerable<AggregateResult> aggregateResults)
        {
            return new GridFooterCellBuilder(FooterHtmlAttributes, GroupFooterTemplate)
            {
                AggregateResults = CalculateAggregates(aggregateResults)
            };
        }

        private GridAggregateResult CalculateAggregates(IEnumerable<AggregateResult> aggregateResults)
        {
            return new GridAggregateResult(aggregateResults.Where(r => Aggregates.Any(f => f.FunctionName == r.FunctionName)));
        }

        public HtmlTemplate<GridAggregateResult> GroupFooterTemplate
        {
            get;
            private set;
        }

        public HtmlTemplate<GridGroupAggregateResult> GroupHeaderTemplate
        {
            get;
            private set;
        }
    }
}