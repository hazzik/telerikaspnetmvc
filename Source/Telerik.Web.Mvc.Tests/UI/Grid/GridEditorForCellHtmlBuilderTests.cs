#if MVC2 || MVC3
namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using UI.Tests;
    using Xunit;
    
    public class GridEditorForCellHtmlBuilderTests
    {
        [Fact]
        public void Should_return_td()
        {
            using (new MockViewEngine())
            {
                var builder = new GridEditorForCellHtmlBuilder<Customer, bool>(ArrangeCell());

                var td = builder.Build();

                Assert.Equal("td", td.TagName);
            }
        }

        [Fact]
        public void Should_use_editor_for()
        {
            using (new MockViewEngine())
            {
                var builder = new GridEditorForCellHtmlBuilder<Customer, bool>(ArrangeCell());

                var td = builder.Build();

                Assert.Equal("<input class=\"check-box\" id=\"IsActive\" name=\"IsActive\" type=\"checkbox\" value=\"true\" /><input name=\"IsActive\" type=\"hidden\" value=\"false\" />", td.InnerHtml.ToString());
            }
        }

        [Fact]
        public void Should_use_editor_by_name_if_such_is_declared()
        {
            using (var viewEngine = new MockViewEngine())
            {
                const string modelName = "myEditor";
                var column = new GridBoundColumn<Customer, bool>(GridTestHelper.CreateGrid<Customer>(), c => c.IsActive)
                                 {
                                     EditorTemplateName = modelName
                                 };

                var cell = new GridCell<Customer>(column, new Customer());

                var builder = new GridEditorForCellHtmlBuilder<Customer, bool>(cell);
                builder.Build();

                viewEngine.Engine.VerifyAll();
            }
        }

        private GridCell<Customer> ArrangeCell()
        {
            var column = new GridBoundColumn<Customer, bool>(GridTestHelper.CreateGrid<Customer>(), c => c.IsActive);
            return new GridCell<Customer>(column, new Customer());
        }
    }
}
#endif