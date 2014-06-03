﻿using Telerik.Web.Mvc.Infrastructure;
// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI;

    using Extensions;
    using Infrastructure;
    using Resources;

    public class PanelBar : ViewComponentBase, INavigationItemComponent<PanelBarItem>, IEffectEnabled
    {
        private readonly IList<IEffect> defaultEffects = new List<IEffect> { new PropertyAnimation(PropertyAnimationType.Height) };

        private readonly IPanelBarHtmlBuilderFactory builderFactory;

        internal bool isBoundToSiteMap;
        internal bool isPathHighlighted;
        internal bool isExpanded;

        public PanelBar(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, IUrlGenerator urlGenerator, INavigationItemAuthorization authorization, IPanelBarHtmlBuilderFactory rendererFactory)
            : base(viewContext, clientSideObjectWriterFactory)
        {
            Guard.IsNotNull(urlGenerator, "urlGenerator");
            Guard.IsNotNull(authorization, "authorization");

            Authorization = authorization;
            UrlGenerator = urlGenerator;

            this.builderFactory = rendererFactory;

            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.panelbar.js" });

            ClientEvents = new PanelBarClientEvents();

            this.Effects = new Effects();
            defaultEffects.Each(el => Effects.Container.Add(el));

            ExpandMode = PanelBarExpandMode.Multiple;
            HighlightPath = true;

            Items = new LinkedObjectCollection<PanelBarItem>(null);

            SelectedIndex = -1;
        }

        public INavigationItemAuthorization Authorization
        {
            get;
            private set;
        }

        public IUrlGenerator UrlGenerator
        {
            get;
            private set;
        }

        public PanelBarClientEvents ClientEvents
        {
            get;
            private set;
        }

        public string Theme
        {
            get;
            set;
        }

        public Action<PanelBarItem> ItemAction
        {
            get;
            set;
        }

        public bool HighlightPath
        {
            get;
            set;
        }

        public PanelBarExpandMode ExpandMode
        {
            get;
            set;
        }

        public bool ExpandAll
        {
            get;
            set;
        }

        public int SelectedIndex
        {
            get;
            set;
        }

        public Effects Effects
        {
            get;
            set;
        }

        public IList<PanelBarItem> Items
        {
            get;
            private set;
        }

        public override void WriteInitializationScript(TextWriter writer)
        {
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "tPanelBar", writer);

            objectWriter.Start();

            if (!defaultEffects.SequenceEqual(Effects.Container))
            {
                objectWriter.Serialize("effects", Effects);
            }

            objectWriter.Append("onExpand", ClientEvents.OnExpand);
            objectWriter.Append("onCollapse", ClientEvents.OnCollapse);
            objectWriter.Append("onSelect", ClientEvents.OnSelect);
            objectWriter.Append("onLoad", ClientEvents.OnLoad);
            objectWriter.Append("onError", ClientEvents.OnError);

            objectWriter.Append("expandMode", (int) ExpandMode);

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            Guard.IsNotNull(writer, "writer");

            if (!Items.IsEmpty())
            {
                if (SelectedIndex != -1 && Items.Count < SelectedIndex)
                {
                    throw new ArgumentOutOfRangeException(TextResource.IndexOutOfRange);
                }

                int itemIndex = 0;

                IPanelBarHtmlBuilder builder = builderFactory.Create(this);

                IHtmlNode panelbarTag = builder.PanelBarTag();

                //this loop is required because of SelectedIndex feature.
                if (HighlightPath)
                {
                    Items.Each(HighlightSelectedItem);
                }

                this.Items.Each(item =>
                {
                    if (item.Enabled)
                    {
                        PrepareItem(item, itemIndex);
                    }

                    itemIndex++;

                    WriteItem(item, panelbarTag, builder);
                });

                panelbarTag.WriteTo(writer);
            }
            base.WriteHtml(writer);
        }

        private void WriteItem(PanelBarItem item, IHtmlNode parentTag, IPanelBarHtmlBuilder builder)
        {
            if (ItemAction != null)
            {
                ItemAction(item);
            }

            if (item.Visible)
            {
                if (!isBoundToSiteMap || item.IsAccessible(Authorization, ViewContext))
                {
                    IHtmlNode itemTag = builder.ItemTag(item).AppendTo(parentTag);

                    builder.ItemInnerTag(item).AppendTo(itemTag);

                    if (item.Content != null || !string.IsNullOrEmpty(item.ContentUrl))
                    {
                        builder.ItemContentTag(item).AppendTo(itemTag);
                    }
                    else if (!item.Items.IsEmpty() && item.Items.IsAccessible(Authorization, ViewContext))
                    {
                        IHtmlNode ul = builder.ChildrenTag(item).AppendTo(itemTag);

                        item.Items.Each(child => WriteItem(child, ul, builder));
                    }
                }
            }
        }

        private void HighlightSelectedItem(PanelBarItem item)
        {
            if (item.Enabled)
            {
                string controllerName = ViewContext.RouteData.Values["controller"] as string ?? string.Empty;
                string actionName = ViewContext.RouteData.Values["action"] as string ?? string.Empty;

                var urlHelper = new UrlHelper(ViewContext.RequestContext);
                var panelBarItemUrl = item.GenerateUrl(ViewContext, UrlGenerator);
                var currentUrl = urlHelper.Action(actionName, controllerName);

                if (!currentUrl.IsNullOrEmpty() && panelBarItemUrl.IsCaseInsensitiveEqual(currentUrl))
                {
                    item.Selected = true;
                    isPathHighlighted = true;

                    PanelBarItem tmpItem = item.Parent;
                    while (tmpItem != null)
                    {
                        tmpItem.Expanded = true;
                        tmpItem = tmpItem.Parent;
                    }
                }
                item.Items.Each(HighlightSelectedItem);
            }
        }

        private void PrepareItem(PanelBarItem item, int itemIndex) 
        {
            if (!this.isPathHighlighted)
            {
                if (itemIndex == this.SelectedIndex)
                {
                    item.Selected = true;

                    if (!item.Items.IsEmpty() || item.Content != null || !string.IsNullOrEmpty(item.ContentUrl))
                        item.Expanded = true;
                }
            }

            if (ExpandMode == PanelBarExpandMode.Single)
            {
                if (item.Expanded && !isExpanded)
                {
                    isExpanded = true;
                }
                else
                {
                    if (item.Parent != null && item.Parent.Expanded)
                        item.Expanded = true;
                    else
                        item.Expanded = false;
                }
            }
            else
            {
                if (ExpandAll)
                {
                    item.Expanded = true;
                }
            }
        }
    }
}
