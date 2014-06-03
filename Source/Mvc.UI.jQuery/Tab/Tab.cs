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

    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;
    using System.Web.UI;

    /// <summary>
    /// Displays a tab in an ASP.NET MVC view.
    /// </summary>
    public class Tab : jQueryViewComponentBase, ITabItemContainer
    {
        private string animationOpacity;
        private int animationDuration;

        private string openAnimationOpacity;
        private int openAnimationDuration;

        private string closeAnimationOpacity;
        private int closeAnimationDuration;

        private int rotationDurationInMilliseconds;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tab"/> class.
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <param name="clientSideObjectWriterFactory">The client side object writer factory.</param>
        public Tab(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory) : base(viewContext, clientSideObjectWriterFactory)
        {
            Items = new List<TabItem>();
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        public IList<TabItem> Items
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the animation opacity.
        /// </summary>
        /// <value>The animation opacity.</value>
        public string AnimationOpacity
        {
            [DebuggerStepThrough]
            get
            {
                return animationOpacity;
            }

            [DebuggerStepThrough]
            set
            {
                if (IsOpenAnimationSet)
                {
                    throw new InvalidOperationException(Resources.TextResource.CannotSetAnimationOpacityWhenOpenAnimationOpacityIsAlreadySet);
                }

                if (IsCloseAnimationSet)
                {
                    throw new InvalidOperationException(Resources.TextResource.CannotSetAnimationOpacityWhenCloseAnimationOpacityIsAlreadySet);
                }

                Guard.IsNotNullOrEmpty(value, "value");

                animationOpacity = value;
            }
        }

        /// <summary>
        /// Gets or sets the duration of the animation.
        /// </summary>
        /// <value>The duration of the animation.</value>
        public int AnimationDuration
        {
            [DebuggerStepThrough]
            get
            {
                return animationDuration;
            }

            [DebuggerStepThrough]
            set
            {
                if (IsOpenAnimationSet)
                {
                    throw new InvalidOperationException(Resources.TextResource.CannotSetAnimationDurationWhenOpenAnimationOpacityIsAlreadySet);
                }

                if (IsCloseAnimationSet)
                {
                    throw new InvalidOperationException(Resources.TextResource.CannotSetAnimationDurationWhenCloseAnimationOpacityIsAlreadySet);
                }

                Guard.IsNotZeroOrNegative(value, "value");

                animationDuration = value;
            }
        }

        /// <summary>
        /// Gets or sets the open animation opacity.
        /// </summary>
        /// <value>The open animation opacity.</value>
        public string OpenAnimationOpacity
        {
            [DebuggerStepThrough]
            get
            {
                return openAnimationOpacity;
            }

            [DebuggerStepThrough]
            set
            {
                if (IsAnimationSet)
                {
                    throw new InvalidOperationException(Resources.TextResource.CannotSetOpenAnimationOpacityWhenAnimationOpacityIsAlreadySet);
                }

                Guard.IsNotNullOrEmpty(value, "value");

                openAnimationOpacity = value;
            }
        }

        /// <summary>
        /// Gets or sets the duration of the open animation.
        /// </summary>
        /// <value>The duration of the open animation.</value>
        public int OpenAnimationDuration
        {
            [DebuggerStepThrough]
            get
            {
                return openAnimationDuration;
            }

            [DebuggerStepThrough]
            set
            {
                if (IsAnimationSet)
                {
                    throw new InvalidOperationException(Resources.TextResource.CannotSetOpenAnimationDurationWhenAnimationOpacityIsAlreadySet);
                }

                Guard.IsNotZeroOrNegative(value, "value");

                openAnimationDuration = value;
            }
        }

        /// <summary>
        /// Gets or sets the close animation opacity.
        /// </summary>
        /// <value>The close animation opacity.</value>
        public string CloseAnimationOpacity
        {
            [DebuggerStepThrough]
            get
            {
                return closeAnimationOpacity;
            }

            [DebuggerStepThrough]
            set
            {
                if (IsAnimationSet)
                {
                    throw new InvalidOperationException(Resources.TextResource.CannotSetCloseAnimationOpacityWhenAnimationOpacityIsAlreadySet);
                }

                Guard.IsNotNullOrEmpty(value, "value");

                closeAnimationOpacity = value;
            }
        }

        /// <summary>
        /// Gets or sets the duration of the close animation.
        /// </summary>
        /// <value>The duration of the close animation.</value>
        public int CloseAnimationDuration
        {
            [DebuggerStepThrough]
            get
            {
                return closeAnimationDuration;
            }

            [DebuggerStepThrough]
            set
            {
                if (IsAnimationSet)
                {
                    throw new InvalidOperationException(Resources.TextResource.CannotSetCloseAnimationDurationWhenAnimationOpacityIsAlreadySet);
                }

                Guard.IsNotZeroOrNegative(value, "value");

                closeAnimationDuration = value;
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
        /// Gets or sets a value indicating whether collapsible content.
        /// </summary>
        /// <value><c>true</c> if collapsible content otherwise, <c>false</c>.</value>
        public bool CollapsibleContent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether cache ajax response.
        /// </summary>
        /// <value><c>true</c> if cache ajax response otherwise, <c>false</c>.</value>
        public bool CacheAjaxResponse
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the spinner text.
        /// </summary>
        /// <value>The spinner text.</value>
        public string SpinnerText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the rotation duration in milliseconds.
        /// </summary>
        /// <value>The rotation duration in milliseconds.</value>
        public int RotationDurationInMilliseconds
        {
            [DebuggerStepThrough]
            get
            {
                return rotationDurationInMilliseconds;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotZeroOrNegative(value, "value");

                rotationDurationInMilliseconds = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether rotation continue.
        /// </summary>
        /// <value><c>true</c> if [rotation continue]; otherwise, <c>false</c>.</value>
        public bool RotationContinue
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
        /// Gets or sets the on show.
        /// </summary>
        /// <value>The on show.</value>
        public Action OnShow
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on add.
        /// </summary>
        /// <value>The on add.</value>
        public Action OnAdd
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on remove.
        /// </summary>
        /// <value>The on remove.</value>
        public Action OnRemove
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on enable.
        /// </summary>
        /// <value>The on enable.</value>
        public Action OnEnable
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on disable.
        /// </summary>
        /// <value>The on disable.</value>
        public Action OnDisable
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the on load.
        /// </summary>
        /// <value>The on load.</value>
        public Action OnLoad
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is animation set.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is animation set; otherwise, <c>false</c>.
        /// </value>
        private bool IsAnimationSet
        {
            [DebuggerStepThrough]
            get
            {
                return !string.IsNullOrEmpty(AnimationOpacity);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is open animation set.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is open animation set; otherwise, <c>false</c>.
        /// </value>
        private bool IsOpenAnimationSet
        {
            [DebuggerStepThrough]
            get
            {
                return !string.IsNullOrEmpty(OpenAnimationOpacity);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is close animation set.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is close animation set; otherwise, <c>false</c>.
        /// </value>
        private bool IsCloseAnimationSet
        {
            [DebuggerStepThrough]
            get
            {
                return !string.IsNullOrEmpty(CloseAnimationOpacity);
            }
        }

        /// <summary>
        /// Writes the initialization script.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public override void WriteInitializationScript(TextWriter writer)
        {
            string id = Id;

            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(id, "tabs", writer);

            objectWriter.Start()
                        .Append("event", OpenOn)
                        .Append("collapsible", CollapsibleContent, false)
                        .Append("cache", CacheAjaxResponse, false)
                        .Append("spinner", SpinnerText);

            int selectedIndex = 0;
            IList<int> disabledIndexes = new List<int>();

            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Selected)
                {
                    selectedIndex = i;
                }

                if (Items[i].Disabled)
                {
                    disabledIndexes.Add(i);
                }
            }

            objectWriter.Append("selected", selectedIndex, 0)
                        .Append("disabled", disabledIndexes);

            string animationSettings = BuildAnimationSettings();

            if (!string.IsNullOrEmpty(animationSettings))
            {
                objectWriter.Append("fx:{0}".FormatWith(animationSettings));
            }

            objectWriter.Append("select", OnSelect)
                        .Append("show", OnShow)
                        .Append("add", OnAdd)
                        .Append("remove", OnRemove)
                        .Append("enable", OnEnable)
                        .Append("disable", OnDisable)
                        .Append("load", OnLoad)
                        .Complete();

            if (RotationDurationInMilliseconds > 0)
            {
                writer.WriteLine("jQuery('#{0}').tabs('rotate',{1},{2});".FormatWith(id, RotationDurationInMilliseconds, RotationContinue.ToString(Culture.Invariant).ToLower(Culture.Invariant)));
            }

            base.WriteInitializationScript(writer);
        }

        // Marked as internal for unit test
        internal string BuildAnimationSettings()
        {
            Func<string, int, string> format = (opacity, duration) => "{" + "opacity:'{0}', duration:{1}".FormatWith(opacity, AnimationDurationConverter.ToString(duration)) + "}";

            if (IsAnimationSet || IsOpenAnimationSet || IsCloseAnimationSet)
            {
                if (IsAnimationSet)
                {
                    return format(AnimationOpacity, AnimationDuration);
                }

                if (IsOpenAnimationSet && IsCloseAnimationSet)
                {
                    return "[{0}, {1}]".FormatWith(format(OpenAnimationOpacity, OpenAnimationDuration), format(CloseAnimationOpacity, CloseAnimationDuration));
                }

                if (!IsCloseAnimationSet)
                {
                    return "[{0}, null]".FormatWith(format(OpenAnimationOpacity, OpenAnimationDuration));
                }

                if (!IsOpenAnimationSet)
                {
                    return "[null, {0}]".FormatWith(format(CloseAnimationOpacity, CloseAnimationDuration));
                }
            }

            return null;
        }

        /// <summary>
        /// Writes the HTML.
        /// </summary>
        protected override void WriteHtml(HtmlTextWriter writer)
        {
            Func<TabItem, string> getContentId = tabItem => tabItem.ContentHtmlAttributes.ContainsKey("id") ?
                                                            tabItem.ContentHtmlAttributes["id"].ToString() :
                                                            string.Concat(Id, "-", (Items.IndexOf(tabItem) + 1).ToString(Culture.Invariant));

            if (!string.IsNullOrEmpty(Theme))
            {
                writer.Write("<div class=\"{0}\">".FormatWith(Theme));
            }

            HtmlAttributes.Merge("id", Id, false);
            HtmlAttributes.AppendInValue("class", " ", "ui-tabs ui-widget ui-widget-content ui-corner-all");
            writer.Write("<div{0}>".FormatWith(HtmlAttributes.ToAttributeString()));

            writer.Write("<ul class=\"ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all\">");

            Dictionary<TabItem, string> contentIds = new Dictionary<TabItem, string>();
            TabItem selectedItem = GetSelectedItem();

            foreach (TabItem item in Items)
            {
                item.HtmlAttributes.AppendInValue("class", " ", "ui-state-default ui-corner-top");

                if (item == selectedItem)
                {
                    item.HtmlAttributes.AppendInValue("class", " ", "ui-tabs-selected ui-state-active");
                }
                else if (item.Disabled)
                {
                    item.HtmlAttributes.AppendInValue("class", " ", "ui-state-disabled");
                }

                string contentId = string.IsNullOrEmpty(item.LoadContentFromUrl) ? getContentId(item) : string.Empty;
                string href = string.IsNullOrEmpty(item.LoadContentFromUrl) ? "#" + contentId : item.LoadContentFromUrl;

                writer.Write("<li{0}><a href=\"{1}\">{2}</a></li>".FormatWith(item.HtmlAttributes.ToAttributeString(), href, item.Text));

                contentIds.Add(item, contentId);
            }

            writer.Write("</ul>");

            foreach (TabItem item in Items)
            {
                if (string.IsNullOrEmpty(item.LoadContentFromUrl))
                {
                    item.ContentHtmlAttributes.Merge("id", contentIds[item], false);
                    item.ContentHtmlAttributes.AppendInValue("class", " ", "ui-tabs-panel ui-widget-content ui-corner-bottom");

                    if (item == selectedItem)
                    {
                        item.ContentHtmlAttributes.AppendInValue("class", " ", "ui-tabs-hide");
                    }

                    writer.Write("<div{0}>".FormatWith(item.ContentHtmlAttributes.ToAttributeString()));
                    item.Content();
                    writer.Write("</div>");
                }
            }

            writer.Write("</div>");

            if (!string.IsNullOrEmpty(Theme))
            {
                writer.Write("</div>");
            }
        }

        private TabItem GetSelectedItem()
        {
            TabItem selectedItem = null;

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