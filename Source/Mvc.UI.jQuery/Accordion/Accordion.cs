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
    using System.Web.Mvc;
    using System.Web.UI;

    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Displays an accordion in an ASP.NET MVC view.
    /// </summary>
    public class Accordion : jQueryViewComponentBase, IAccordionItemContainer
    {
        private bool autoHeight;
        private bool clearStyle;
        private bool collapsibleContent;
        private bool noActiveSelection;

        /// <summary>
        /// Initializes a new instance of the <see cref="Accordion"/> class.
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <param name="clientSideObjectWriterFactory">The client side object writer factory.</param>
        public Accordion(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory) : base(viewContext, clientSideObjectWriterFactory)
        {
            Items = new List<AccordionItem>();
            autoHeight = true;
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        public IList<AccordionItem> Items
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the name of the animation.
        /// </summary>
        /// <value>The name of the animation.</value>
        public string AnimationName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether auto height.
        /// </summary>
        /// <value><c>true</c> if auto height; otherwise, <c>false</c>.</value>
        public bool AutoHeight
        {
            [DebuggerStepThrough]
            get
            {
                return autoHeight;
            }

            [DebuggerStepThrough]
            set
            {
                autoHeight = value;

                if (!autoHeight)
                {
                    clearStyle = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether clear style.
        /// </summary>
        /// <value><c>true</c> if clear style; otherwise, <c>false</c>.</value>
        public bool ClearStyle
        {
            [DebuggerStepThrough]
            get
            {
                return clearStyle;
            }

            [DebuggerStepThrough]
            set
            {
                clearStyle = value;

                if (clearStyle)
                {
                    autoHeight = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the open on.
        /// </summary>
        /// <value>The open on.</value>
        public string OpenOn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether no pane will be active.
        /// </summary>
        /// <value><c>true</c> if [no active selection]; otherwise, <c>false</c>.</value>
        public bool NoActiveSelection
        {
            [DebuggerStepThrough]
            get
            {
                return noActiveSelection;
            }

            [DebuggerStepThrough]
            set
            {
                noActiveSelection = value;

                if (noActiveSelection)
                {
                    collapsibleContent = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether collapsible content.
        /// </summary>
        /// <value><c>true</c> if collapsible content; otherwise, <c>false</c>.</value>
        public bool CollapsibleContent
        {
            [DebuggerStepThrough]
            get
            {
                return collapsibleContent;
            }

            [DebuggerStepThrough]
            set
            {
                collapsibleContent = value;

                if (!collapsibleContent)
                {
                    noActiveSelection = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [fill space].
        /// </summary>
        /// <value><c>true</c> if [fill space]; otherwise, <c>false</c>.</value>
        public bool FillSpace
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the header icon.
        /// </summary>
        /// <value>The header icon.</value>
        public string Icon
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the header selected icon.
        /// </summary>
        /// <value>The header selected icon.</value>
        public string SelectedIcon
        {
            get;
            set;
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
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "accordion", writer);

            objectWriter.Start()
                        .Append("animated", AnimationName)
                        .Append("autoHeight", AutoHeight, true)
                        .Append("clearStyle", ClearStyle, false)
                        .Append("collapsible", CollapsibleContent, false)
                        .Append("event", OpenOn)
                        .Append("fillSpace", FillSpace, false);

            if (NoActiveSelection)
            {
                objectWriter.Append("active", false, true);
            }
            else
            {
                int selectedIndex = Items.IndexOf(GetSelectedItem());

                objectWriter.Append("active", selectedIndex, 0);
            }

            if (!string.IsNullOrEmpty(Icon) || !string.IsNullOrEmpty(SelectedIcon))
            {
                if (!string.IsNullOrEmpty(Icon) && !string.IsNullOrEmpty(SelectedIcon))
                {
                    objectWriter.Append("icons:{'header':'" + Icon + "','headerSelected':'" + SelectedIcon + "'}");
                }
                else if (!string.IsNullOrEmpty(Icon))
                {
                    objectWriter.Append("icons:{'header':'" + Icon + "'}");
                }
                else if (!string.IsNullOrEmpty(SelectedIcon))
                {
                    objectWriter.Append("icons:{'headerSelected':'" + SelectedIcon + "'}");
                }
            }

            objectWriter.Append("change", OnChange)
                        .Complete();

            base.WriteInitializationScript(writer);
        }

        /// <summary>
        /// Writes the HTML.
        /// </summary>
        protected override void WriteHtml(HtmlTextWriter writer)
        {
            AccordionItem selectedItem = GetSelectedItem();

            if (!string.IsNullOrEmpty(Theme))
            {
                writer.Write("<div class=\"{0}\">".FormatWith(Theme));
            }

            HtmlAttributes.Merge("id", Id, false);
            HtmlAttributes.AppendInValue("class", " ", "ui-accordion ui-widget ui-helper-reset");
            writer.Write("<div{0}>".FormatWith(HtmlAttributes.ToAttributeString()));

            foreach (AccordionItem item in Items)
            {
                item.HtmlAttributes.AppendInValue("class", " ", "ui-accordion-header ui-helper-reset ui-state-default ");
                item.ContentHtmlAttributes.AppendInValue("class", " ", "ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom");

                if ((!NoActiveSelection) && (item == selectedItem))
                {
                    item.ContentHtmlAttributes.AppendInValue("class", " ", "ui-accordion-content-active");
                }
                else
                {
                    item.HtmlAttributes.AppendInValue("class", " ", "ui-corner-all");
                }

                writer.Write("<h3{0}><a href=\"#\">{1}</a></h3>".FormatWith(item.HtmlAttributes.ToAttributeString(), item.Text));

                item.ContentHtmlAttributes.AppendInValue("style", ";", (item == selectedItem) ? "display:block" : "display:none");

                writer.Write("<div{0}>".FormatWith(item.ContentHtmlAttributes.ToAttributeString()));
                item.Content();
                writer.Write("</div>");
            }

            writer.Write("</div>");

            if (!string.IsNullOrEmpty(Theme))
            {
                writer.Write("</div>");
            }
        }

        private AccordionItem GetSelectedItem()
        {
            AccordionItem selectedItem = null;

            for (int i = Items.Count - 1; i > -1; i--)
            {
                if (Items[i].Selected)
                {
                    selectedItem = Items[i];
                    break;
                }
            }

            return selectedItem ?? ((Items.Count > 0) ? Items[0] : null);
        }
    }
}