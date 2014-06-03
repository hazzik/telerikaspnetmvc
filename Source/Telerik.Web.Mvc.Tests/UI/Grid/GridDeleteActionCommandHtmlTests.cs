﻿namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Moq;
    using Xunit;

    using Telerik.Web.Mvc.UI.Tests;
    using Telerik.Web.Mvc.Infrastructure;

    public class GridDeleteActionCommandHtmlTests
    {
        private GridDeleteActionCommand command;
        private Mock<IGridRenderingContext<Customer>> context;
        private IHtmlNode parentNode;

        public GridDeleteActionCommandHtmlTests()
        {
            var virtualPathProvider = new Mock<IVirtualPathProvider>();
            virtualPathProvider.Setup(vpp => vpp.FileExists(It.IsAny<string>())).Returns(false);

            var serviceLocator = new Mock<IServiceLocator>();
            serviceLocator.Setup(sl => sl.Resolve<IVirtualPathProvider>()).Returns(virtualPathProvider.Object);

            ServiceLocator.SetCurrent(() => serviceLocator.Object);

            var grid = GridTestHelper.CreateGrid<Customer>();

            grid.Localization = new GridLocalization();

            command = new GridDeleteActionCommand();
            context = new Mock<IGridRenderingContext<Customer>>();
            context.Setup(c => c.Grid).Returns(grid);
            parentNode = new HtmlTag("td");
        }
#if MVC2
        [Fact]
        public void BoundModeHtml_method_should_return_form_tag_append_to_parent()
        {
            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("form", childNode.TagName);
        }

        [Fact]
        public void BoundModeHtml_method_should_return_form_tag_with_css_class()
        {
            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal(UIPrimitives.Grid.ActionForm, childNode.Attributes()["class"].ToString());
        }

        [Fact]
        public void BoundModeHtml_method_should_return_form_tag_with_method_attribute()
        {
            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("post", childNode.Attributes()["method"].ToString());
        }

        [Fact]
        public void BoundModeHtml_method_should_return_div_tag_append_to_form()
        {
            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0].Children[0];

            Assert.Equal("div", childNode.TagName);
        }

        [Fact]
        public void BoundModeHtml_method_should_return_button_tag_append_to_div()
        {
            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0].Children[0].Children[0];

            Assert.Equal("button", childNode.TagName);
        }

        [Fact]
        public void BoundModeHtml_method_should_return_a_tag_with_css_class()
        {
            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0].Children[0].Children[0];

            Assert.Equal(string.Format("{0} {1} {2} {3}",UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Delete), childNode.Attributes()["class"]);
        }

        [Fact]
        public void BoundModeHtml_method_should_return_a_tag_with_applied_html_attributes()
        {
            command.HtmlAttributes.Add("test", "test");

            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0].Children[0].Children[0];

            Assert.Equal("test", childNode.Attributes()["test"]);
        }

        [Fact]
        public void BoundModeHtml_method_should_return_only_text_if_buttonType_is_text()
        {
            command.ButtonType = GridButtonType.Text;

            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0].Children[0].Children[0];

            Assert.Equal("Delete", childNode.InnerHtml);
        }

        [Fact]
        public void BoundModeHtml_method_should_return_a_tag_with_span_and_select_class()
        {
            command.ButtonType = GridButtonType.Image;
            command.ImageHtmlAttributes.Add("style", "width:20px");

            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0].Children[0].Children[0];

            Assert.Equal("<span class=\"t-icon t-delete\" style=\"width:20px\"></span>", childNode.InnerHtml);
        }

        [Fact]
        public void BoundModeHtml_method_should_return_a_with_span_and_text_if_button_type_is_imageAndText()
        {
            command.ButtonType = GridButtonType.ImageAndText;
            command.ImageHtmlAttributes.Add("style", "width:20px");

            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0].Children[0].Children[0];

            Assert.Equal("<span class=\"t-icon t-delete\" style=\"width:20px\"></span>Delete", childNode.InnerHtml);
        }
#endif
    }
}
