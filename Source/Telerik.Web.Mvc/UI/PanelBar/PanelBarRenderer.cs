// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Web.UI;
    using System.Reflection;

    using Extensions;
    using Infrastructure;

    public class PanelBarRenderer : IPanelBarRenderer
    {
        private readonly IActionMethodCache actionMethodCache;
        private readonly NavigationItemImageRenderer<PanelBarItem> imageRenderer;

        public PanelBarRenderer(PanelBar panelBar, HtmlTextWriter writer, IActionMethodCache actionMethodCache)
        {
            Guard.IsNotNull(panelBar, "panelBar");
            Guard.IsNotNull(writer, "writer");
            Guard.IsNotNull(actionMethodCache, "actionMethodCache");

            PanelBar = panelBar;
            Writer = writer;
            this.actionMethodCache = actionMethodCache;

            imageRenderer = new NavigationItemImageRenderer<PanelBarItem>(writer, panelBar.ViewContext);
        }

        protected PanelBar PanelBar
        {
            get;
            private set;
        }

        protected HtmlTextWriter Writer
        {
            get;
            private set;
        }

        public void PanelBarStart()
        {
            PanelBar.HtmlAttributes.Merge("id", PanelBar.Id, false);
            PanelBar.PrependCssClasses(new[] { UIPrimitives.Widget, "t-panelbar", UIPrimitives.ResetStyle });
            Writer.AddAttributes(PanelBar.HtmlAttributes);
            Writer.RenderBeginTag(HtmlTextWriterTag.Ul);
        }

        public void PanelBarEnd()
        {
            Writer.RenderEndTag();
        }

        public void ListGroupStart(PanelBarItem item)
        {
            if (item.Expanded && item.Enabled)
            {
                Writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:block;", true);
            }
            else
            {
                Writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:none;", true);
            }

            Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-group", true);
            Writer.RenderBeginTag(HtmlTextWriterTag.Ul);
        }

        public void ListGroupEnd()
        {
            Writer.RenderEndTag();
        }

        public void ListItemStart(PanelBarItem item)
        {
            if (!item.Enabled)
                item.PrependCssClass(UIPrimitives.DisabledState);
            else
            {
                if (item.Expanded)
                    item.PrependCssClass(UIPrimitives.ActiveState);
                
                if(!item.Expanded && !item.Selected)
                    item.PrependCssClass(UIPrimitives.DefaultState);
            }

            item.PrependCssClass(UIPrimitives.Item);

            Writer.AddAttributes(item.HtmlAttributes);
            Writer.RenderBeginTag(HtmlTextWriterTag.Li);
        }

        public void ListItemEnd()
        {
            Writer.RenderEndTag();
        }

        public void HeaderItemContent(PanelBarItem item)
        {
            Writer.AddAttribute(HtmlTextWriterAttribute.Class, UIPrimitives.Header + " " + UIPrimitives.Link, true);

            CommonItemContent(item);
        }

        public void ItemContent(PanelBarItem item)
        {
			string className = UIPrimitives.Link;

			if (item.Selected)
				className += " " + UIPrimitives.SelectedState;

			Writer.AddAttribute(HtmlTextWriterAttribute.Class, className, true);

            CommonItemContent(item);
        }

        public void WriteContent(PanelBarItem item)
        {
            bool hasContentUrl = !string.IsNullOrEmpty(item.ContentUrl);

            if (hasContentUrl || item.Content != null)
            {
                item.AppendContentCssClass(UIPrimitives.Content);
                item.ContentHtmlAttributes.Merge("id", GetItemContentId(item), true);

                if (item.Expanded && !hasContentUrl && item.Enabled)
                {
                    item.ContentHtmlAttributes.Merge(HtmlTextWriterAttribute.Style.ToString(), "display:block;", true);
                }
                else
                {
                    item.ContentHtmlAttributes.Merge("style", "display:none;", true);
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

        private void CommonItemContent(PanelBarItem item)
        {
            if (item.Enabled)
            {
                string url = PanelBar.GetItemUrl(item, PanelBar.ViewContext, PanelBar.UrlGenerator);

                Writer.AddAttribute(HtmlTextWriterAttribute.Href, url, true);
            }

            Writer.RenderBeginTag(HtmlTextWriterTag.A);

            imageRenderer.WriteSprite(item);
            imageRenderer.WriteImage(item);

            Writer.WriteEncodedText(GetText(item));

            if (item.Items.Count > 0 || item.Content != null || !string.IsNullOrEmpty(item.ContentUrl))
            {
                if (item.Enabled && item.Expanded)
                {
                    Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-arrow-up", true);
                }
                else
                {
                    Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-icon t-arrow-down", true);
                }

                Writer.RenderBeginTag(HtmlTextWriterTag.Span);
                Writer.RenderEndTag();
            }

            Writer.RenderEndTag();
        }

        private object GetItemContentId(PanelBarItem item)
        {
            return item.ContentHtmlAttributes.ContainsKey("id") ?
                   item.ContentHtmlAttributes["id"].ToString() :
                   string.Concat(PanelBar.Id, "-", (PanelBar.Items.IndexOf(item) + 1).ToString(Culture.Invariant));
        }

        private string GetText(NavigationItem<PanelBarItem> item)
        {
            string text = item.Text;

            if (string.IsNullOrEmpty(text) && ((!string.IsNullOrEmpty(item.ControllerName) && !string.IsNullOrEmpty(item.ActionName))))
            {
                foreach (MethodInfo method in actionMethodCache.GetActionMethods(PanelBar.ViewContext.RequestContext, item.ControllerName, item.ActionName))
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