﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Fluent
{

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// Defines the fluent interface for configuring command.
    /// </summary>
    /// <typeparam name="TModel">The type of the model</typeparam>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <typeparam name="TBuilder">The type of the builder.</typeparam>
    public abstract class GridActionCommandBuilderBase<TCommand, TBuilder>  : IHideObjectMembers
        where TCommand : GridActionCommandBase
        where TBuilder : GridActionCommandBuilderBase<TCommand, TBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridActionCommandBuilderBase&lt;TModel, TColumn, TBuilder&gt;"/> class.
        /// </summary>
        /// <param name="column">The column.</param>
        public GridActionCommandBuilderBase(TCommand command)
        {
            Command = command;
        }

        /// <summary>
        /// Sets the button type.
        /// </summary>
        /// <param name="type">The button type.</param>
        /// <returns></returns>
        public TBuilder ButtonType(GridButtonType type)
        {
            Guard.IsNotNull(type, "type");

            Command.ButtonType = type;

            return this as TBuilder;
        }

        /// <summary>
        /// Sets the HTML attributes.
        /// </summary>
        /// <param name="attributes">The HTML attributes.</param>
        /// <returns></returns>
        public TBuilder HtmlAttributes(object attributes)
        {
            Command.HtmlAttributes.Merge(attributes);
            
            return this as TBuilder;
        }

        /// <summary>
        /// Sets the image HTML attributes.
        /// </summary>
        /// <param name="attributes">The Image HTML attributes.</param>
        /// <returns></returns>
        public TBuilder ImageHtmlAttributes(object attributes)
        {
            Command.ImageHtmlAttributes.Merge(attributes);
            
            return this as TBuilder;
        }

        protected TCommand Command
        {
            get;
            private set;
        }
    }
}
