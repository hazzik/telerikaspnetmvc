// (c) Copyright 2002-2009 Telerik 
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

    public class PanelBar : ViewComponentBase, INavigationItemContainer<PanelBarItem>, IEffectEnabled
    {
        private readonly IList<IEffect> defaultEffects = new List<IEffect> { new PropertyAnimation(PropertyAnimationType.Height) };

        private readonly IPanelBarRendererFactory rendererFactory;

        internal bool isBindToSiteMap;

        private bool isExpanded;
        internal bool isPathHighlighted;

        public PanelBar(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, IUrlGenerator urlGenerator, INavigationItemAuthorization authorization, IPanelBarRendererFactory rendererFactory) : base(viewContext, clientSideObjectWriterFactory)
        {
            Guard.IsNotNull(urlGenerator, "urlGenerator");
            Guard.IsNotNull(authorization, "authorization");

            Authorization = authorization;
            UrlGenerator = urlGenerator;

            this.rendererFactory = rendererFactory;

            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.panelbar.js" });

            Effects = new List<IEffect>(defaultEffects.Count);

            ClientEvents = new PanelBarClientEvents();

            defaultEffects.Each(el => Effects.Add(el));

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

        public IList<IEffect> Effects
        {
            get;
            private set;
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

            if (!defaultEffects.SequenceEqual(Effects))
            {
                var effectSerialization = new List<string>();

                Effects.Each(e => effectSerialization.Add(e.Serialize()));

                objectWriter.Append("effects:[{0}]".FormatWith(String.Join(",", effectSerialization.ToArray())));
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
                IPanelBarRenderer renderer = rendererFactory.Create(this, writer);

                renderer.PanelBarStart();

                int itemIndex = 0;
                isExpanded = false;

                if (SelectedIndex != -1 && Items.Count < SelectedIndex)
                {
                    throw new ArgumentOutOfRangeException(TextResource.IndexOutOfRange);
                }

                //this loop is required because of SelectedIndex feature.
                Items.Each(HighlightSelectedItem);

                this.Items.Each(item =>
                {
                    if (!this.isPathHighlighted)
                    {
                        if (itemIndex == this.SelectedIndex)
                        {
                            item.Selected = true;

                            if (!item.Items.IsEmpty())
                                item.Expanded = true;
                        }
                    }
                    itemIndex++;

                    WriteItemDependingOnItsBinding(item, renderer, false);
                });

                renderer.PanelBarEnd();
            }

            base.WriteHtml(writer);
        }

        private void WriteItemDependingOnItsBinding(PanelBarItem item, IPanelBarRenderer renderer, bool isSubItem)
        {
            if (item.Visible)
            {
                if (isBindToSiteMap)
                {
                    if (item.IsAccessible(Authorization, ViewContext))
                    {
                        WriteItem(item, renderer, isSubItem);
                    }
                }
                else
                {
                    WriteItem(item, renderer, isSubItem);
                }
            }
        }

        private void WriteItem(PanelBarItem item, IPanelBarRenderer renderer, bool isSubItem)
        {
            if (ItemAction != null)
            {
                ItemAction(item);
            }

            if (Equals(ExpandMode, PanelBarExpandMode.Single))
            {
                if (item.Expanded && !isExpanded)
                {
                    isExpanded = true;
                }
                else
                {
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

            renderer.ListItemStart(item);

            if (!isSubItem)
            {
                renderer.HeaderItemContent(item);
            }
            else
            {
                renderer.ItemContent(item);
            }

            WriteContent(item, renderer);

            renderer.ListItemEnd();
        }

        private void WriteContent(PanelBarItem item, IPanelBarRenderer renderer)
        {
            if (item.Content != null || !string.IsNullOrEmpty(item.ContentUrl))
            {
                renderer.WriteContent(item);
            }
            else
            {
                if (!item.Items.IsEmpty() && string.IsNullOrEmpty(item.ContentUrl))
                {
                    renderer.ListGroupStart(item);
                    item.Items.Each(subItem => WriteItemDependingOnItsBinding(subItem, renderer, true));
                    renderer.ListGroupEnd();
                }
            }
        }

        private void HighlightSelectedItem(PanelBarItem item)
        {
            if (HighlightPath)
            {
                string controllerName = ViewContext.RouteData.Values["controller"] as string;
                string actionName = ViewContext.RouteData.Values["action"] as string;

                if (!string.IsNullOrEmpty(controllerName) && (string.Equals(controllerName, item.Text) ||
                                                              string.Equals(controllerName, item.ControllerName)))
                {
                    if (!string.IsNullOrEmpty(actionName) && string.Equals(actionName, item.ActionName))
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
                }

                if (!item.Items.IsEmpty())
                {
                    item.Items.Each(HighlightSelectedItem);
                }
            }
        }
    }
}
