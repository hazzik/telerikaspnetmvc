#if MVC2
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

        [Fact]
        public void Should_use_editor_for()
        {
            using (new MockViewEngine())
            {
                var grid = GridTestHelper.CreateGrid<Test>();
                grid.Columns.Add(new GridBoundColumn<Test, bool>(grid, c => c.Active));

                var row = new GridRow<Test>(grid, new Test(), 0);
                var builder = new GridFormEditRowHtmlBuilder<Test>(row);
                var tr = builder.Build();
                var td = tr.Children[0];
                var form = td.Children[0];

                Assert.Equal("form", form.TagName);

                Assert.Contains(@"<div class=""t-edit-form-container""><div class=""editor-label""><label for=""Active"">Active</label></div>", form.InnerHtml.ToString());
            }
        }
    }
}
#endif