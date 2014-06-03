// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Web.UI;

    public class GridScrollableRenderer<T> : GridRenderer<T> where T : class
    {
        public GridScrollableRenderer(Grid<T> grid, HtmlTextWriter htmlWriter): base(grid, htmlWriter)
        {
        }

        public override void HeaderStart()
        {
            Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-grid-header");
            Writer.RenderBeginTag(HtmlTextWriterTag.Div);
            Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-grid-header-wrap");
            Writer.RenderBeginTag(HtmlTextWriterTag.Div);

            TableStart();
        }

        public override void HeaderEnd()
        {
            TableEnd();

            Writer.RenderEndTag(); //t-grid-header-wrap
            Writer.RenderEndTag(); //t-grid-header
        }

        public override void BodyStart()
        {
            Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-grid-content");
            Writer.AddStyleAttribute(HtmlTextWriterStyle.Height, Grid.Scrolling.Height);
            Writer.RenderBeginTag(HtmlTextWriterTag.Div);

            TableStart();

            base.BodyStart();
        }

        public override void BodyEnd()
        {
            base.BodyEnd();

            TableEnd();
            
            Writer.RenderEndTag(); //t-grid-content
        }

        public override void FooterStart()
        {
            Writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-grid-footer");
            Writer.RenderBeginTag(HtmlTextWriterTag.Div);

            TableStart();
        }

        public override void FooterEnd()
        {
            TableEnd();

            Writer.RenderEndTag(); //t-grid-footer
        }
    }
}