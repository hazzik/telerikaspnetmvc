namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI.Tests;
    using Xunit;

    public class GridToolBarInsertCommandHtmlTests
    {
        private GridToolBarInsertCommand<Customer> command;
        private Grid<Customer> grid;
        private IHtmlNode parentNode;

        public GridToolBarInsertCommandHtmlTests()
        {
            grid = GridTestHelper.CreateGrid<Customer>();

            command = new GridToolBarInsertCommand<Customer>();
            parentNode = new HtmlTag("td");
        }
#if MVC2 || MVC3
        [Fact]
        public void Html_method_should_return_a_tag_append_to_parent()
        {
            command.Html(grid, parentNode);
            var childNode = parentNode.Children[0];

            Assert.Equal("a", childNode.TagName);
        }

        [Fact]
        public void Html_method_should_return_a_tag_with_css_class()
        {
            command.Html(grid, parentNode);
            var childNode = parentNode.Children[0];

            Assert.Equal(string.Format("{0} {1} {2} {3}",UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Add), childNode.Attributes()["class"]);
        }

        [Fact]
        public void Html_method_should_return_a_tag_with_applied_html_attributes()
        {
            command.HtmlAttributes.Add("test", "test");

            command.Html(grid, parentNode);
            var childNode = parentNode.Children[0];

            Assert.Equal("test", childNode.Attributes()["test"]);
        }

        [Fact]
        public void Html_method_should_return_only_text_if_buttonType_is_text()
        {
            command.ButtonType = GridButtonType.Text;

            command.Html(grid, parentNode);
            var childNode = parentNode.Children[0];

            Assert.Equal("Add new record", childNode.InnerHtml);
        }

        [Fact]
        public void Html_method_should_return_a_tag_with_span_and_select_class()
        {
            command.ButtonType = GridButtonType.Image;
            command.ImageHtmlAttributes.Add("style", "width:20px");

            command.Html(grid, parentNode);
            var childNode = parentNode.Children[0];

            Assert.Equal("<span class=\"t-add t-icon\" style=\"width:20px\"></span>", childNode.InnerHtml);
        }

        [Fact]
        public void Html_method_should_return_a_with_span_and_text_if_button_type_is_imageAndText()
        {
            command.ButtonType = GridButtonType.ImageAndText;

            command.Html(grid, parentNode);
            var childNode = parentNode.Children[0];

            Assert.Equal("<span class=\"t-add t-icon\"></span>Add new record", childNode.InnerHtml);
        }
#endif
    }
}
