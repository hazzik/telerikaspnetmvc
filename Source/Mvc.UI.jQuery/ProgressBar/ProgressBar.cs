// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    using System.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.UI;
    using System.Web.UI;

    /// <summary>
    /// Displays a progress bar in an ASP.NET MVC view.
    /// </summary>
    public class ProgressBar : jQueryViewComponentBase
    {
        private int? progressValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class.
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <param name="clientSideObjectWriterFactory">The client side object writer factory.</param>
        public ProgressBar(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory) : base(viewContext, clientSideObjectWriterFactory)
        {
            UpdateElements = new List<string>();
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
        {
            [DebuggerStepThrough]
            get
            {
                return progressValue.GetValueOrDefault();
            }

            [DebuggerStepThrough]
            set
            {
                if ((value < 0) || (value > 100))
                {
                    throw new ArgumentOutOfRangeException("value", Resources.TextResource.ValueShouldBeBetweenZeroToHundred);
                }

                progressValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the update elements to display the current value.
        /// </summary>
        /// <value>The update elements.</value>
        public IList<string> UpdateElements
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the on change.
        /// </summary>
        /// <value>The on change.</value>
        public Action OnChange
        {
            get;
            set;
        }

        /// <summary>
        /// Writes the initialization script.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public override void WriteInitializationScript(TextWriter writer)
        {
            string id = Id;

            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(id, "progressbar", writer);

            objectWriter.Start()
                        .Append("value", GetValue())
                        .Append("change", OnChange)
                        .Complete();

            StringBuilder scriptBuilder = new StringBuilder();

            if (!UpdateElements.IsNullOrEmpty())
            {
                StringBuilder eventBuilder = new StringBuilder();
                StringBuilder updateBuilder = new StringBuilder();

                foreach (string selector in UpdateElements)
                {
                    if (!string.IsNullOrEmpty(selector))
                    {
                        string script = "jQuery('{0}').text(jQuery('#{1}').progressbar('option','value'));".FormatWith(selector, id);

                        eventBuilder.Append(script);
                        updateBuilder.Append(script);
                    }
                }

                if (eventBuilder.Length > 0)
                {
                    scriptBuilder.AppendLine();
                    scriptBuilder.Append("jQuery('#{0}')".FormatWith(id));
                    scriptBuilder.Append(".bind('progressbarchange',function(e,ui){");
                    scriptBuilder.Append(eventBuilder);
                    scriptBuilder.Append("});");
                }

                if (updateBuilder.Length > 0)
                {
                    scriptBuilder.AppendLine();
                    scriptBuilder.Append(updateBuilder);
                }
            }

            writer.Write(scriptBuilder.ToString());

            base.WriteInitializationScript(writer);
        }

        // Marked as internal for unit test
        internal int? GetValue()
        {
            int? value = progressValue;

            if (!value.HasValue)
            {
                object viewDataValue = ViewContext.ViewData.Eval(Name);

                if (viewDataValue != null)
                {
                    value = (int?)viewDataValue;
                }
            }

            return value;
        }

        /// <summary>
        /// Writes the HTML.
        /// </summary>
        protected override void WriteHtml(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(Theme))
            {
                writer.Write("<div class=\"{0}\">".FormatWith(Theme));
            }

            HtmlAttributes.Merge("id", Id, false);
            writer.Write("<div{0}></div>".FormatWith(HtmlAttributes.ToAttributeString()));

            if (!string.IsNullOrEmpty(Theme))
            {
                writer.Write("</div>");
            }
        }
    }
}