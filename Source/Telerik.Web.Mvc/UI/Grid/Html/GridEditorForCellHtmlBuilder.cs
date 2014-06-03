// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
#if MVC2 || MVC3
namespace Telerik.Web.Mvc.UI.Html
{
    using System.Web.Mvc;
    using Extensions;
    using Infrastructure;

    public class GridEditorForCellHtmlBuilder<TModel, TValue> : GridDataCellHtmlBuilder<TModel>
        where TModel : class
    {
        public GridEditorForCellHtmlBuilder(GridCell<TModel> cell) : base (cell)
        {
        }

        protected override IHtmlNode BuildCore()
        {
            var td = new HtmlTag("td");
            var column = (GridBoundColumn<TModel, TValue>)Cell.Column;
            td.Html(GetEditorHtml(column));
            return td;
        }

        private string GetEditorHtml(GridBoundColumn<TModel, TValue> column)
        {
            var editorBuilder = new GridColumnEditorBuilder<TModel>(GetHtmlHelper(column.Grid.ViewContext));
            
            if (column.EditorTemplateName.HasValue())
            {
                return editorBuilder.GetEditor(column.Member, column.EditorTemplateName);
            }
            return editorBuilder.GetEditor(column.Expression);
        }

        private HtmlHelper<TModel> GetHtmlHelper(ViewContext viewContext)
        {
            return new HtmlHelper<TModel>(viewContext, new GridViewDataContainer<TModel>(Cell.DataItem, viewContext.ViewData));
        }
    }
}
#endif