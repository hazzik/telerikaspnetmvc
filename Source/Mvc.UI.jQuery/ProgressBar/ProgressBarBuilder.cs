// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Class used by the HTML helpers to build HTML tags for progress bar.
    /// </summary>
    public class ProgressBarBuilder : ViewComponentBuilderBase<ProgressBar, ProgressBarBuilder>, IHideObjectMembers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBarBuilder"/> class.
        /// </summary>
        /// <param name="component">The component.</param>
        public ProgressBarBuilder(ProgressBar component) : base(component)
        {
        }

        /// <summary>
        /// The value of the progressbar.
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <returns></returns>
        public virtual ProgressBarBuilder Value(int theValue)
        {
            Component.Value = theValue;

            return this;
        }

        /// <summary>
        /// If set, the current progress numeric value will be displayed in the specified elements
        /// </summary>
        /// <param name="selectors">The selectors.</param>
        /// <returns></returns>
        public virtual ProgressBarBuilder UpdateElements(params string[] selectors)
        {
            Guard.IsNotNullOrEmpty(selectors, "selectors");

            Component.UpdateElements.Clear();
            Component.UpdateElements.AddRange(selectors);

            return this;
        }

        /// <summary>
        /// This event is triggered every time the progress changes.
        /// </summary>
        /// <param name="javaScript">The java script.</param>
        /// <returns></returns>
        public virtual ProgressBarBuilder OnChange(Action javaScript)
        {
            Component.OnChange = javaScript;

            return this;
        }

        /// <summary>
        /// Specify the name of the theme apply to the progress bar.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual ProgressBarBuilder Theme(string name)
        {
            Component.Theme = name;

            return this;
        }
    }
}