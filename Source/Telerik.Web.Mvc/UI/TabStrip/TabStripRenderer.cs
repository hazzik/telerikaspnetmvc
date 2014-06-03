// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Reflection;
    using System.Web.UI;

    using Extensions;
    using Infrastructure;

    public class TabStripRenderer : ITabStripRenderer
    {
        private readonly IActionMethodCache actionMethodCache;
        private readonly NavigationItemImageRenderer<TabStripItem> imageRenderer;

        public TabStripRenderer(TabStrip tabStrip, HtmlTextWriter writer, IActionMethodCache actionMethodCache)
        {
            TabStrip = tabStrip;
            Writer = writer;

            this.actionMethodCache = actionMethodCache;
            imageRenderer = new NavigationItemImageRenderer<TabStripItem>(writer, tabStrip.ViewContext);
        }

        protected TabStrip TabStrip
        {
            get;
            private set;
        }

        protected HtmlTextWriter Writer
        {
            get;
            private set;
        }
       
        public void TabStripStart()
        {
            TabStrip.HtmlAttributes.Merge("id", TabStrip.Id, false);
            TabStrip.PrependCssClasses(new[] { UIPrimitives.Widget, "t-tabstrip", UIPrimitives.Header });

            Writer.AddAttributes(TabStrip.HtmlAttributes);
            Writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        public void TabStripEnd()
        {
            Writer.RenderEndTag();
        }

        public void ItemsStart()
        {
            Writer.AddAttribute(HtmlTextWriterAttribute.Class, UIPrimitives.ResetStyle);
            Writer.RenderBeginTag(HtmlTextWriterTag.Ul);
        }

        public void ItemsEnd()
        {
            Writer.RenderEndTag();
        }

        public void ItemContent(TabStripItem item)
        {
            item.AppendCssClass(UIPrimitives.Item);

            if (item.Selected)
            {
                item.AppendCssClass(UIPrimitives.ActiveState);
            }
            else if (!item.Enabled)
            {
                item.AppendCssClasses(new [] { UIPrimitives.DefaultState,  UIPrimitives.DisabledState });
            }
            else
            {
                item.AppendCssClass(UIPrimitives.DefaultState);
            }

            Writer.AddAttributes(item.HtmlAttributes);
            Writer.RenderBeginTag(HtmlTextWriterTag.Li);

            if (item.Enabled)
            {
                string url = TabStrip.GetItemUrl(item, TabStrip.ViewContext, TabStrip.UrlGenerator);
                Writer.AddAttribute(HtmlTextWriterAttribute.Href, url, true);
            }

            Writer.AddAttribute(HtmlTextWriterAttribute.Class, UIPrimitives.Link);
            Writer.RenderBeginTag(HtmlTextWriterTag.A);

            imageRenderer.WriteSprite(item);
            imageRenderer.WriteImage(item);

            Writer.WriteEncodedText(GetText(item));
            Writer.RenderEndTag();

            Writer.RenderEndTag();
        }

        public void TabContent(TabStripItem item)
        {
            if (item.Visible)
            {
                bool hasContentUrl = !string.IsNullOrEmpty(item.ContentUrl);

                if (hasContentUrl || item.Content != null)
                {
                    item.AppendContentCssClass(UIPrimitives.Content);
                    item.ContentHtmlAttributes.Merge("id", GetTabContentId(item), true);

                    if (item.Selected)
                    {
                        item.AppendContentCssClass(UIPrimitives.ActiveState);
                        item.ContentHtmlAttributes.AddStyleAttribute("display", "block");
                    }

                    Writer.AddAttributes(item.ContentHtmlAttributes);
                    Writer.RenderBeginTag(HtmlTextWriterTag.Div);

                    if (item.Content != null)
                    {
                        item.Content();
                    }

                    Writer.RenderEndTag();
                }
            }
        }

        private string GetTabContentId(TabStripItem item)
        {
            return item.ContentHtmlAttributes.ContainsKey("id") ?
                    item.ContentHtmlAttributes["id"].ToString() :
                    string.Concat(TabStrip.Id, "-", (TabStrip.Items.IndexOf(item) + 1).ToString(Culture.Invariant));
        }

        private string GetText(NavigationItem<TabStripItem> item)
        {
            string text = item.Text;

            if (string.IsNullOrEmpty(text) && ((!string.IsNullOrEmpty(item.ControllerName) && !string.IsNullOrEmpty(item.ActionName))))
            {
                foreach(MethodInfo method in actionMethodCache.GetActionMethods(TabStrip.ViewContext.RequestContext, item.ControllerName, item.ActionName))
                {
                    if (method != null)
                    {
                        string displayName = method.GetDisplayName();

                        if (!string.IsNullOrEmpty(displayName))
                        {
                            text = displayName;
                            break;
                        }
                    }
                }
            }

            return text;
        }
    }
}