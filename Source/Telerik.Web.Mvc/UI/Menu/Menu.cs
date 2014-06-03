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
    using Telerik.Web.Mvc.Resources;

    public class Menu : ViewComponentBase, INavigationItemContainer<MenuItem>, IEffectEnabled
    {
        private readonly IList<IEffect> defaultEffects = new List<IEffect> { new SlideAnimation() };

        private readonly IMenuRendererFactory rendererFactory;

        public Menu(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, IUrlGenerator urlGenerator, INavigationItemAuthorization authorization, IMenuRendererFactory factory) : base(viewContext, clientSideObjectWriterFactory)
        {
            Guard.IsNotNull(urlGenerator, "urlGenerator");
            Guard.IsNotNull(authorization, "authorization");
            Guard.IsNotNull(factory, "factory");

            UrlGenerator = urlGenerator;
            Authorization = authorization;
            rendererFactory = factory;

            ClientEvents = new MenuClientEvents();

            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.menu.js" });

            Effects = new List<IEffect>(defaultEffects.Count);

            defaultEffects.Each(el => Effects.Add(el));

            Items = new LinkedObjectCollection<MenuItem>(null);

            SelectedIndex = -1;
            HighlightPath = true;
        }

        public bool OpenOnClick
        {
            get;
            set;
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

        public MenuOrientation Orientation
        {
            get;
            set;
        }

        public MenuClientEvents ClientEvents
        {
            get;
            private set;
        }

        public string Theme
        {
            get;
            set;
        }

        public IList<IEffect> Effects
        {
            get;
            private set;
        }

        public IList<MenuItem> Items
        {
            get;
            private set;
        }

        public Action<MenuItem> ItemAction
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
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "tMenu", writer);

            objectWriter.Start()
                        .Append("orientation", Orientation, MenuOrientation.Horizontal);

            if (!defaultEffects.SequenceEqual(Effects))
            {
                var effectSerialization = new List<string>();

                Effects.Each(e => effectSerialization.Add(e.Serialize()));

                objectWriter.Append("effects:[{0}]".FormatWith(String.Join(",", effectSerialization.ToArray())));
            }

            if (OpenOnClick)
            {
                objectWriter.Append("openOnClick", true);
            }

            objectWriter.Append("onOpen", ClientEvents.OnOpen);
            objectWriter.Append("onClose", ClientEvents.OnClose);
            objectWriter.Append("onSelect", ClientEvents.OnSelect);
            objectWriter.Append("onLoad", ClientEvents.OnLoad);

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            Guard.IsNotNull(writer, "writer");

            if (!Items.IsEmpty())
            {
                IMenuRenderer renderer = rendererFactory.Create(this, writer);

                int itemIndex = 0;
                bool isPathHighlighted = false;

                if (SelectedIndex != -1 && Items.Count < SelectedIndex)
                {
                    throw new ArgumentOutOfRangeException(TextResource.IndexOutOfRange);
                }

                renderer.MenuStart();

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

                    WriteItem(item, renderer);
                });

                renderer.MenuEnd();
            }

            base.WriteHtml(writer);
        }

        private void WriteItem(MenuItem item, IMenuRenderer renderer)
        {
            if (item.Visible && item.IsAccessible(Authorization, ViewContext))
            {
                if (ItemAction != null)
                {
                    ItemAction(item);
                }

                item.Url = item.GenerateUrl(ViewContext, UrlGenerator) ?? "#";

                renderer.ItemStart(item);

                renderer.Link(item);

                WriteContent(item, renderer);

                renderer.ItemEnd();
            }
        }

        private void WriteContent(MenuItem item, IMenuRenderer renderer)
        {
            if (item.Content != null)
            {
                item.HtmlAttributes.Clear();
                renderer.GroupStart();
                renderer.ItemStart(item);
                renderer.WriteContent(item);
                renderer.ItemEnd();
                renderer.GroupEnd();
            }
            else if (!item.Items.IsEmpty() && item.Items.IsAccessible(Authorization, ViewContext))
            {
                renderer.GroupStart();

                item.Items.Each(subitem => WriteItem(subitem, renderer));

                renderer.GroupEnd();
            }
        }
    }
}