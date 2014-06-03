// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Web.Mvc;

    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Displays a theme switcher in an ASP.NET MVC view.
    /// </summary>
    public class ThemeSwitcher : ViewComponentBase
    {
        private int? height;
        private int? width;
        private int? buttonHeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewComponentBase"/> class.
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <param name="clientSideObjectWriterFactory">The client side object writer factory.</param>
        public ThemeSwitcher(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory) : base(viewContext, clientSideObjectWriterFactory)
        {
            CloseOnSelect = true;
            ScriptFileNames.Add("themeswitchertool.js");
        }

        /// <summary>
        /// Gets or sets the initial theme.
        /// </summary>
        /// <value>The initial theme.</value>
        public string InitialTheme
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height
        {
            [DebuggerStepThrough]
            get
            {
                return height.GetValueOrDefault();
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotZeroOrNegative(value, "value");

                height = value;
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width
        {
            [DebuggerStepThrough]
            get
            {
                return width.GetValueOrDefault();
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotZeroOrNegative(value, "value");

                width = value;
            }
        }

        /// <summary>
        /// Gets or sets the initial text.
        /// </summary>
        /// <value>The initial text.</value>
        public string InitialText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the button pre text.
        /// </summary>
        /// <value>The button pre text.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "PreText", Justification = "To align with the same parameter name of client side jQuery object.")]
        public string ButtonPreText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether close on select.
        /// </summary>
        /// <value><c>true</c> if close on select; otherwise, <c>false</c>.</value>
        public bool CloseOnSelect
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height of the button.
        /// </summary>
        /// <value>The height of the button.</value>
        public int ButtonHeight
        {
            [DebuggerStepThrough]
            get
            {
                return buttonHeight.GetValueOrDefault();
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotZeroOrNegative(value, "value");

                buttonHeight = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the cookie.
        /// </summary>
        /// <value>The name of the cookie.</value>
        public string CookieName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on open.
        /// </summary>
        /// <value>The on open.</value>
        public Action OnOpen
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on select.
        /// </summary>
        /// <value>The on select.</value>
        public Action OnSelect
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on close.
        /// </summary>
        /// <value>The on close.</value>
        public Action OnClose
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
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "themeswitcher", writer);

            objectWriter.Start()
                        .Append("loadTheme", GetTheme())
                        .Append("height", height)
                        .Append("width", width)
                        .Append("initialText", InitialText)
                        .Append("buttonPreText", ButtonPreText)
                        .Append("closeOnSelect", CloseOnSelect, true)
                        .Append("buttonHeight", buttonHeight)
                        .Append("cookieName", CookieName)
                        .Append("onOpen", OnOpen)
                        .Append("onSelect", OnSelect)
                        .Append("onClose", OnClose)
                        .Complete();

            base.WriteInitializationScript(writer);
        }

        // Marked as internal for unit test
        internal string GetTheme()
        {
            string theme = InitialTheme;

            if (string.IsNullOrEmpty(theme))
            {
                object viewDataTheme = ViewContext.ViewData.Eval(Name);

                if (viewDataTheme != null)
                {
                    theme = (string)viewDataTheme;
                }
            }

            return theme;
        }

        /// <summary>
        /// Writes the HTML.
        /// </summary>
        protected override void WriteHtml()
        {
            TextWriter writer = ViewContext.HttpContext.Response.Output;

            HtmlAttributes.Merge("id", Id, false);
            writer.Write("<div{0}></div>".FormatWith(HtmlAttributes.ToAttributeString()));

            base.WriteHtml();
        }
    }
}