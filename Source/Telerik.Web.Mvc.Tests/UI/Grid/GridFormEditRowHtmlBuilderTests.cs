#if MVC2 || MVC3
namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Telerik.Web.Mvc.UI;
    using Telerik.Web.Mvc.UI.Tests;
    using Xunit;

    public class GridFormEditRowHtmlBuilderTests
    {
        public class Test
        {
            public bool Active
            {
                get;
                set;
            }
        }
        
        private Grid<Test> grid;
        private GridRow<Test> row;
        
        public GridFormEditRowHtmlBuilderTests()
        {
            grid = GridTestHelper.CreateGrid<Test>();
            grid.Columns.Add(new GridBoundColumn<Test, bool>(grid, c => c.Active));
            row = new GridRow<Test>(grid, new Test(), 0);
        }

        [Fact]
        public void Should_use_editor_for()
        {
            using (new MockViewEngine())
            {
                var form = Act();

                Assert.Equal("form", form.TagName);

                Assert.Contains(@"<div class=""t-edit-form-container""><div class=""editor-label""><label for=""Active"">Active</label></div>", form.InnerHtml.ToString());
            }
        }

        [Fact]
        public void Should_use_editor_for_when_editor_template_is_declared()
        {
            using (var viewEngine = new MockViewEngine())
            {
                grid.Editing.TemplateName = "someEditorTemplate";

                var form = Act();
                Assert.Equal("form", form.TagName);

                viewEngine.Engine.VerifyAll();
            }
        }

        [Fact]
        public void Should_create_insert_and_cancel_buttons_without_edit_command()
        {
            using (new MockViewEngine())
            {
                row.InInsertMode = true;

                IHtmlNode form = Act();
                
                var editor = form.Children[0];
                var insert = editor.Children[1];
                var cancel = editor.Children[2];

                Assert.Contains("t-grid-insert", insert.Attribute("class"));
                Assert.Contains("t-grid-cancel", cancel.Attribute("class"));
            }
        }
        
        [Fact]
        public void Should_create_insert_and_cancel_buttons_with_edit_command()
        {
            using (new MockViewEngine())
            {
                grid.Columns.Add(new GridActionColumn<Test>(grid)
                    {
                        Commands = 
                        {
                            new GridEditActionCommand
                            {
                                HtmlAttributes = {{"class","foo"}}
                            }
                        }
                    });

                row.InInsertMode = true;

                var form = Act();
                var editor = form.Children[0];
                var insert = editor.Children[1];

                Assert.Contains("foo", insert.Attribute("class"));
            }
        }

        [Fact]
        public void Should_create_update_and_cancel_buttons_without_edit_command()
        {
            using (new MockViewEngine())
            {
                row.InEditMode = true;

                IHtmlNode form = Act();

                var editor = form.Children[0];
                var insert = editor.Children[1];
                var cancel = editor.Children[2];

                Assert.Contains("t-grid-update", insert.Attribute("class"));
                Assert.Contains("t-grid-cancel", cancel.Attribute("class"));
            }
        }

        [Fact]
        public void Should_create_update_and_cancel_buttons_with_edit_command()
        {
            using (new MockViewEngine())
            {
                grid.Columns.Add(new GridActionColumn<Test>(grid)
                {
                    Commands = 
                        {
                            new GridEditActionCommand
                            {
                                HtmlAttributes = {{"class","foo"}}
                            }
                        }
                });

                row.InEditMode = true;

                var form = Act();
                var editor = form.Children[0];
                var insert = editor.Children[1];

                Assert.Contains("foo", insert.Attribute("class"));
            }
        }
        private IHtmlNode Act()
        {
            var builder = new GridFormEditRowHtmlBuilder<Test>(row);
            var tr = builder.Build();
            var td = tr.Children[0];
            var form = td.Children[0];
            return form;
        }
    }
}
#endif