// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System;
    using System.IO;
    using System.Web.Mvc;

    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Displays a message box in an ASP.NET MVC view.
    /// </summary>
    public class MessageBox : jQueryViewComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBox"/> class.
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <param name="clientSideObjectWriterFactory">The client side object writer factory.</param>
        public MessageBox(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory) : base(viewContext, clientSideObjectWriterFactory)
        {
        }

        /// <summary>
        /// The HTML content of message.
        /// </summary>
        /// <value>The content.</value>
        public Action Content
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the message box.
        /// </summary>
        /// <value>The type of the message.</value>
        public MessageBoxType MessageType
        {
            get;
            set;
        }

        /// <summary>
        /// Writes the HTML.
        /// </summary>
        protected override void WriteHtml()
        {
            TextWriter writer = ViewContext.HttpContext.Response.Output;

            if (!string.IsNullOrEmpty(Theme))
            {
                writer.Write("<div class=\"{0}\">".FormatWith(Theme));
            }

            HtmlAttributes.AppendInValue("class", " ", "ui-widget");
            HtmlAttributes.Merge("id", Id, false);
            writer.Write("<div{0}>".FormatWith(HtmlAttributes.ToAttributeString()));

            string cssClass = MessageType == MessageBoxType.Error ? "ui-state-error" : "ui-state-highlight";

            writer.Write("<div style=\"padding:0 0.7em;margin-top:20px;\" class=\"{0} ui-corner-all\">".FormatWith(cssClass));
            writer.Write("<p>");

            string icon = MessageType == MessageBoxType.Error ? "ui-icon-alert" : "ui-icon-info";

            writer.Write("<span style=\"float:left;margin-right:0.3em;\" class=\"ui-icon {0}\"></span>".FormatWith(icon));
            Content();

            writer.Write("</p>");
            writer.Write("</div>");

            writer.Write("</div>");

            if (!string.IsNullOrEmpty(Theme))
            {
                writer.Write("</div>");
            }

            base.WriteHtml();
        }
    }
}