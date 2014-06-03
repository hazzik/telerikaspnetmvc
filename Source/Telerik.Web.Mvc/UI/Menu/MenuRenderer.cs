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

    public class MenuRenderer : IMenuRenderer
    {
        private readonly IActionMethodCache actionMethodCache;
        private readonly NavigationItemImageRenderer<MenuItem> imageRenderer;

        public MenuRenderer(Menu menu, HtmlTextWriter htmlWriter, IActionMethodCache actionMethodCache)
        {
            Guard.IsNotNull(menu, "menu");
            Guard.IsNotNull(htmlWriter, "htmlWriter");
            Guard.IsNotNull(actionMethodCache, "actionMethodCache");

            Menu = menu;
            Writer = htmlWriter;
            UrlGenerator = new UrlGenerator();

            this.actionMethodCache = actionMethodCache;

            imageRenderer = new NavigationItemImageRenderer<MenuItem>(htmlWriter, menu.ViewContext);
        }

        protected UrlGenerator UrlGenerator
        {
            get;
            private set;
        }

        protected Menu Menu
        {
            get;
            private set;
        }

        protected HtmlTextWriter Writer
        {
            get;
            private set;
        }

        public void MenuStart()
        {
            Menu.HtmlAttributes.Merge("id", Menu.Id, false);

            Menu.PrependCssClasses(new[] { UIPrimitives.Widget, UIPrimitives.ResetStyle, UIPrimitives.Header, "t-menu"});

            if (Menu.Orientation == MenuOrientation.Vertical)
            {
                Menu.AppendCssClass("t-menu-vertical");
            }

            Writer.AddAttributes(Menu.HtmlAttributes);
            Writer.RenderBeginTag(HtmlTextWriterTag.Ul);
        }

        public void MenuEnd()
        {
            Writer.RenderEndTag();
        }

        public void ItemStart(MenuItem item)
        {
            if (!item.Enabled)
            {
                item.PrependCssClass(UIPrimitives.DisabledState);
            }
            else
            {
                if (item.Selected)
                {
                    item.PrependCssClass(UIPrimitives.SelectedState);
                }
                else
                {
                    item.PrependCssClass(UIPrimitives.DefaultState);
                }
            }

            item.PrependCssClass(UIPrimitives.Item);

            Writer.AddAttributes(item.HtmlAttributes);
            Writer.RenderBeginTag(HtmlTextWriterTag.Li);
        }

        public void ItemEnd()
        {
            Writer.RenderEndTag();
        }

        public void Link(MenuItem item)
        {
            string url = Menu.GetItemUrl(item, Menu.ViewContext, Menu.UrlGenerator);
            Writer.AddAttribute(HtmlTextWriterAttribute.Href, System.Web.HttpUtility.HtmlAttributeEncode(url));
            Writer.AddAttribute(HtmlTextWriterAttribute.Class, UIPrimitives.Link);
            Writer.RenderBeginTag(HtmlTextWriterTag.A);

            imageRenderer.WriteSprite(item);
            imageRenderer.WriteImage(item);

            Writer.Write(GetText(item));

            if (item.Items.Count > 0 || item.Content != null)
            {
                string iconClass = "t-arrow-next";

                if (Menu.Orientation == MenuOrientation.Horizontal && item.Parent == null)
                {
                    iconClass = "t-arrow-down";
                }

                Writer.AddAttribute(HtmlTextWriterAttribute.Class, UIPrimitives.Icon + " " + iconClass);
                Writer.RenderBeginTag(HtmlTextWriterTag.Span);
                Writer.RenderEndTag();
            }

            Writer.RenderEndTag();
        }

        public void GroupStart()
        {
            Writer.AddAttribute(HtmlTextWriterAttribute.Class, UIPrimitives.Group);
            Writer.RenderBeginTag(HtmlTextWriterTag.Ul);
        }

        public void GroupEnd()
        {
            Writer.RenderEndTag();
        }

        public void WriteContent(MenuItem item)
        {
            if (item.Content != null)
            {
                item.AppendContentCssClass(UIPrimitives.Content);
                item.ContentHtmlAttributes.Merge("id", GetItemContentId(item), true);

                Writer.AddAttributes(item.ContentHtmlAttributes);
                Writer.RenderBeginTag(HtmlTextWriterTag.Div);

                if (item.Content != null)
                {
                    item.Content();
                }

                Writer.RenderEndTag();
            }
        }

        private object GetItemContentId(MenuItem item)
        {
            return item.ContentHtmlAttributes.ContainsKey("id") ?
                   item.ContentHtmlAttributes["id"].ToString() :
                   string.Concat(Menu.Id, "-", (Menu.Items.IndexOf(item) + 1).ToString(Culture.Invariant));
        }

        private string GetText(NavigationItem<MenuItem> item)
        {
            string text = item.Text;

            if (string.IsNullOrEmpty(text) && ((!string.IsNullOrEmpty(item.ControllerName) && !string.IsNullOrEmpty(item.ActionName))))
            {
                foreach (MethodInfo method in actionMethodCache.GetActionMethods(Menu.ViewContext.RequestContext, item.ControllerName, item.ActionName))
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