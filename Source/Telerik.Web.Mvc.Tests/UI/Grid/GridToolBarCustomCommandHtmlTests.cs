﻿namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Moq;
    using Xunit;

    using Telerik.Web.Mvc.UI.Tests;
    using Telerik.Web.Mvc.Infrastructure;

    public class GridToolBaCustomCommandHtmlTests
    {
        private GridToolBarCustomCommand<Customer> command;
        private Grid<Customer> grid;
        private IHtmlNode parentNode;

        public GridToolBaCustomCommandHtmlTests()
        {
            var virtualPathProvider = new Mock<IVirtualPathProvider>();
            virtualPathProvider.Setup(vpp => vpp.FileExists(It.IsAny<string>())).Returns(false);

            var serviceLocator = new Mock<IServiceLocator>();
            serviceLocator.Setup(sl => sl.Resolve<IVirtualPathProvider>()).Returns(virtualPathProvider.Object);

            ServiceLocator.SetCurrent(() => serviceLocator.Object);

            grid = GridTestHelper.CreateGrid<Customer>();

            grid.Localization = new GridLocalization();

            command = new GridToolBarCustomCommand<Customer>();
            parentNode = new HtmlTag("td");
        }
#if MVC2
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

            Assert.Equal(string.Format("{0} {1} {2}",UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState), childNode.Attributes()["class"]);
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
            command.Text = "Custom";

            command.Html(grid, parentNode);
            var childNode = parentNode.Children[0];

            Assert.Equal("Custom", childNode.InnerHtml);
        }

        [Fact]
        public void Html_method_should_return_a_tag_with_span_and_select_class()
        {
            command.ButtonType = GridButtonType.Image;
            command.ImageHtmlAttributes.Add("style", "width:20px");

            command.Html(grid, parentNode);
            var childNode = parentNode.Children[0];

            Assert.Equal("<span class=\"t-icon\" style=\"width:20px\"></span>", childNode.InnerHtml);
        }

        [Fact]
        public void Html_method_should_return_a_with_span_and_text_if_button_type_is_imageAndText()
        {
            command.ButtonType = GridButtonType.ImageAndText;
            command.Text = "Custom";
            command.ImageHtmlAttributes.Add("class", "t-custom");

            command.Html(grid, parentNode);
            var childNode = parentNode.Children[0];

            Assert.Equal("<span class=\"t-icon t-custom\"></span>Custom", childNode.InnerHtml);
        }
#endif
    }
}