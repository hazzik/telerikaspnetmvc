// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;
    using System.Web.Routing;
    using System.Reflection;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// Represents a column in the <see cref="Grid&lt;T&gt;"/> component
    /// </summary>
    /// <typeparam name="T">The type of the data item</typeparam>
    public class GridColumn<T> where T : class
    {
        private static readonly Regex NameExpression = new Regex("([A-Z]+(?=$|[A-Z][a-z])|[A-Z]?[a-z]+)", RegexOptions.Compiled);

        /// <summary>
        /// Initializes a new instance of the <see cref="GridColumn&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="value">The property to which the column is bound to.</param>
        public GridColumn(Expression<Func<T, object>> value) : this()
        {
            Guard.IsNotNull(value, "value");

            Sortable = true;
            Filterable = true;
            Encoded = true;

            Name = value.MemberWithoutInstance();
            DataType = TypeFromMemberExpression(value.ToMemberExpression());
            Title = !string.IsNullOrEmpty(Name) ? NameExpression.Replace(value.LastMemberName(), "$1 ").Trim() : null;
            Value = value.Compile();

            string title;
            string format;

            GetTitleAndFormat(value, out title, out format);

            Title = title;
            Format = format;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GridColumn&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="template">The action which defines how the column will render.</param>
        public GridColumn(Action<T> template) : this()
        {
            Guard.IsNotNull(template, "value");

            Template = template;
        }

        private GridColumn()
        {
            HeaderHtmlAttributes = new RouteValueDictionary();
            HtmlAttributes = new RouteValueDictionary();

            Visible = true;
        }

        /// <summary>
        /// Gets type of the property to which the column is bound to.
        /// </summary>
        public Type DataType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a function which returns the value of the property to which the column is bound to.
        /// </summary>
        public Func<T, object> Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the template of the column.
        /// </summary>
        public Action<T> Template
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the title of the column.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the width of the column.
        /// </summary>
        /// <value>The width.</value>
        public string Width
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the header HTML attributes.
        /// </summary>
        /// <value>The header HTML attributes.</value>
        public IDictionary<string, object> HeaderHtmlAttributes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the display format for the data.
        /// </summary>
        /// <value>The format.</value>
        public string Format
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GridColumn&lt;T&gt;"/> is sortable.
        /// </summary>
        /// <value><c>true</c> if sortable; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool Sortable
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GridColumn&lt;T&gt;"/> is filterable.
        /// </summary>
        /// <value><c>true</c> if filterable; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool Filterable
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GridColumn&lt;T&gt;"/> is encoded.
        /// </summary>
        /// <value><c>true</c> if encoded; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool Encoded
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GridColumn&lt;T&gt;"/> is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool Visible
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the HTML attributes of the cell rendered for the column
        /// </summary>
        /// <value>The HTML attributes.</value>
        public IDictionary<string, object> HtmlAttributes
        {
            get;
            private set;
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

        private static void GetTitleAndFormat(Expression<Func<T, object>> expression, out string title, out string format)
        {
            title = null;
            format = null;

            MemberExpression memberExpression = expression.ToMemberExpression();

            if (memberExpression != null)
            {
                MemberInfo member = memberExpression.Member;

                if ((member.MemberType == MemberTypes.Property) || (member.MemberType == MemberTypes.Field))
                {
                    title = member.GetDisplayName();
                    format = member.GetFormat();
                }

                if (string.IsNullOrEmpty(title))
                {
                    title = NameExpression.Replace(memberExpression.Member.Name, " $1").Trim();
                }
            }
        }
    }
}
