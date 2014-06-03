#if MVC2 || MVC3
namespace Telerik.Web.Mvc.UI.Html
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure;
    using UI;
    using Extensions;

    public class GridFormEditRowHtmlBuilder<T> : GridEditRowHtmlBuilder<T>
        where T : class
    {
        public GridFormEditRowHtmlBuilder(GridRow<T> row) : base(row)
        {
        }

        protected override IHtmlNode BuildCore()
        {
            var tr = CreateRow();

            var td = CreateCell();

            td.AppendTo(tr);

            var form = BuildForm();
            
            form.AppendTo(td);
            
            return tr;
        }
        
        public string CreateEditorHtml()
        {
            var editorBuilder = new GridColumnEditorBuilder<T>(GetHelper());
            var templateName = Row.Grid.Editing.TemplateName;
            if (templateName.HasValue())
            {
                return editorBuilder.GetEditorForModel(templateName);
            }
            return editorBuilder.GetEditorForModel();
        }

        private HtmlHelper<T> GetHelper()
        {
            ViewContext viewContext = Row.Grid.ViewContext;
            return new HtmlHelper<T>(viewContext, new GridViewDataContainer<T>(Row.DataItem, viewContext.ViewData));
        }

        public IHtmlNode BuildForm()
        {
            var form = CreateForm();

            var editor = new HtmlTag("div")
                .AddClass("t-edit-form-container");

            editor.AppendTo(form);

            editor.Html(CreateEditorHtml());
            var command = Row.Grid
                             .Columns
                             .OfType<GridActionColumn<T>>()
                             .SelectMany(c => c.Commands, (c, cmd) => cmd)
                             .OfType<GridEditActionCommand>()
                             .FirstOrDefault() ?? new GridEditActionCommand();
            
            if (Row.InEditMode)
            {
                command.EditModeHtml<T>(editor, Row);
            }
            else if (Row.InInsertMode)
            {
                command.InsertModeHtml<T>(editor, Row);
            }
            
            return form;
        }
    }
}
#endif