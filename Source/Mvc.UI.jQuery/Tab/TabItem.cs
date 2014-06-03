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

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Infrastructure;

    /// <summary>
    /// Represents a tab item displayed in the tab control.
    /// </summary>
    public class TabItem : IHideObjectMembers
    {
        private string text;
        private string loadContentFromUrl;
        private Action content;
        private bool selected;
        private bool disabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabItem"/> class.
        /// </summary>
        public TabItem()
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
        /// Gets or sets the header text.
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
        /// Gets or sets the load content from URL.
        /// </summary>
        /// <value>The load content from URL.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Its take a URI as a string parameter.")]
        public string LoadContentFromUrl
        {
            [DebuggerStepThrough]
            get
            {
                return loadContentFromUrl;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNullOrEmpty(value, "value");

                loadContentFromUrl = value;
                ContentHtmlAttributes.Clear();
                content = null;
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
                loadContentFromUrl = null;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TabItem"/> is selected.
        /// </summary>
        /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
        public bool Selected
        {
            [DebuggerStepThrough]
            get
            {
                return selected;
            }

            [DebuggerStepThrough]
            set
            {
                selected = value;

                if (selected)
                {
                    disabled = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TabItem"/> is disabled.
        /// </summary>
        /// <value><c>true</c> if disabled; otherwise, <c>false</c>.</value>
        public bool Disabled
        {
            [DebuggerStepThrough]
            get
            {
                return disabled;
            }

            [DebuggerStepThrough]
            set
            {
                disabled = value;

                if (disabled)
                {
                    selected = false;
                }
            }
        }
    }
}