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

    public class TabStrip : ViewComponentBase, INavigationItemContainer<TabStripItem>, IEffectEnabled
    {
        private readonly IList<IEffect> defaultEffects = new List<IEffect>{ new SlideAnimation() };

        private readonly ITabStripRendererFactory rendererFactory;

        public TabStrip(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, IUrlGenerator urlGenerator, INavigationItemAuthorization authorization, ITabStripRendererFactory rendererFactory) : base(viewContext, clientSideObjectWriterFactory)
        {
            Guard.IsNotNull(urlGenerator, "urlGenerator");
            Guard.IsNotNull(authorization, "authorization");
            Guard.IsNotNull(rendererFactory, "rendererFactory");

            this.rendererFactory = rendererFactory;

            UrlGenerator = urlGenerator;
            Authorization = authorization;

            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.tabstrip.js" });

            Effects = new List<IEffect>(defaultEffects.Count);

            defaultEffects.Each(el => Effects.Add(el));

            ClientEvents = new TabStripClientEvents();

            Items = new List<TabStripItem>();
            SelectedIndex = -1;
            HighlightPath = true;
        }

        public IUrlGenerator UrlGenerator
        {
            get;
            private set;
        }

        public INavigationItemAuthorization Authorization
        {
            get;
            private set;
        }

        public TabStripClientEvents ClientEvents
        {
            get;
            private set;
        }

        public IList<IEffect> Effects
        {
            get;
            private set;
        }

        public IList<TabStripItem> Items
        {
            get;
            private set;
        }

        public Action<TabStripItem> ItemAction
        {
            get;
            set;
        }

        public int SelectedIndex
        {
            get;
            set;
        }

        public bool HighlightPath
        {
            get;
            set;
        }

        public override void WriteInitializationScript(TextWriter writer)
        {
            string id = Id;

            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(id, "tTabStrip", writer);

            objectWriter.Start();

            if (!defaultEffects.SequenceEqual(Effects))
            {
                var effectSerialization = new List<string>();

                Effects.Each(e => effectSerialization.Add(e.Serialize()));

                objectWriter.Append("effects:[{0}]".FormatWith(String.Join(",", effectSerialization.ToArray())));
            }

            objectWriter.Append("onSelect", ClientEvents.OnSelect);
            objectWriter.Append("onLoad", ClientEvents.OnLoad);
            objectWriter.Append("onError", ClientEvents.OnError);

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            Guard.IsNotNull(writer, "writer");

            if (!Items.IsEmpty())
            {
                ITabStripRenderer renderer = rendererFactory.Create(this, writer);

                int itemIndex = 0;
                bool isPathHighlighted = false;

                renderer.TabStripStart();

                renderer.ItemsStart();

                if (HighlightPath)
                {
                    isPathHighlighted = this.HighlightItem(ViewContext);
                }

                Items.Each(item =>
                {
                    if (!isPathHighlighted)
                    {
                        if (itemIndex == this.SelectedIndex)
                        {
                            item.Selected = true;
                        }
                        itemIndex++;
                    }
                    RenderItem(item, renderer);
                });

                renderer.ItemsEnd();

                Items.Each(renderer.TabContent);

                renderer.TabStripEnd();
            }
            base.WriteHtml(writer);
        }

        private void RenderItem(TabStripItem item, ITabStripRenderer renderer)
        {
            if (item.Visible && item.IsAccessible(Authorization, ViewContext))
            {
                if (ItemAction != null)
                {
                    ItemAction(item);
                }

                renderer.ItemContent(item);
            }
        }
    }
}