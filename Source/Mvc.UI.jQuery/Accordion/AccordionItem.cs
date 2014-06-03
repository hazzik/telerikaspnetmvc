// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Web.Routing;

    using Telerik.Web.Mvc.Infrastructure;

    /// <summary>
    /// Represents a accordion item displayed in the Accordion control.
    /// </summary>
    public class AccordionItem
    {
        private string text;
        private Action content;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccordionItem"/> class.
        /// </summary>
        public AccordionItem()
        {
            HtmlAttributes = new RouteValueDictionary();
            ContentHtmlAttributes = new RouteValueDictionary();
        }

        /// <summary>
        /// Gets or sets the header HTML attributes.
        /// </summary>
        /// <value>The header HTML attributes.</value>
        public IDictionary<string, object> HtmlAttributes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the header text to display.
        /// </summary>
        /// <value>The header text.</value>
        public string Text
        {
            [DebuggerStepThrough]
            get
            {
                return text;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                text = value;
            }
        }

        /// <summary>
        /// Gets or sets the content HTML attributes.
        /// </summary>
        /// <value>The content HTML attributes.</value>
        public IDictionary<string, object> ContentHtmlAttributes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public Action Content
        {
            [DebuggerStepThrough]
            get
            {
                return content;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNull(value, "value");

                content = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AccordionItem"/> is selected.
        /// </summary>
        /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
        public bool Selected
        {
            get;
            set;
        }
    }
}