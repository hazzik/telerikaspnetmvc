namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Moq;
    using Xunit;

    using Telerik.Web.Mvc.UI.Tests;
    using Telerik.Web.Mvc.Infrastructure;

    public class GridEditActionCommandHtmlTests
    {
        private GridEditActionCommand command;
        private Mock<IGridRenderingContext<Customer>> context;
        private IHtmlNode parentNode;

        public GridEditActionCommandHtmlTests()
        {
            var grid = GridTestHelper.CreateGrid<Customer>();

            command = new GridEditActionCommand();
            context = new Mock<IGridRenderingContext<Customer>>();
            context.Setup(c => c.Grid).Returns(grid);
            parentNode = new HtmlTag("td");
        }

#if MVC2 || MVC3
        [Fact]
        public void EditModeHtml_method_should_return_button_tag_append_to_parent()
        {
            command.EditModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("button", childNode.TagName);
        }

        [Fact]
        public void EditModeHtml_method_should_return_button_tag_with_css_class()
        {
            command.EditModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal(string.Format("{0} {1} {2} {3}", UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Update), childNode.Attributes()["class"]);
        }

        [Fact]
        public void EditModeHtml_method_should_return_a_tag_with_applied_html_attributes()
        {
            command.HtmlAttributes.Add("test", "test");

            command.EditModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("test", childNode.Attributes()["test"]);
        }

        [Fact]
        public void EditModeHtml_method_should_return_type_attribute()
        {
            command.EditModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("submit", childNode.Attributes()["type"]);
        }

        [Fact]
        public void EditModeHtml_method_should_return_only_text_if_buttonType_is_text()
        {
            command.ButtonType = GridButtonType.Text;

            command.EditModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("Update", childNode.InnerHtml);
        }

        [Fact]
        public void EditModeHtml_method_should_return_a_tag_with_span_and_select_class()
        {
            command.ButtonType = GridButtonType.Image;
            command.ImageHtmlAttributes.Add("style", "width:20px");

            command.EditModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("<span class=\"t-icon t-update\" style=\"width:20px\"></span>", childNode.InnerHtml);
        }

        [Fact]
        public void EditModeHtml_method_should_return_a_with_span_and_text_if_button_type_is_imageAndText()
        {
            command.ButtonType = GridButtonType.ImageAndText;
            command.ImageHtmlAttributes.Add("style", "width:20px");

            command.EditModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("<span class=\"t-icon t-update\" style=\"width:20px\"></span>Update", childNode.InnerHtml);
        }

        [Fact]
        public void EditModeHtml_method_should_return_button_tag_append_as_second_child_to_parent_() //cancel button
        {
            command.EditModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[1];

            Assert.Equal("a", childNode.TagName);
        }

        [Fact]
        public void EditModeHtml_method_should_return_a_tag_with_css_classas_second_child() //cancel button
        {
            command.EditModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[1];

            Assert.Equal(string.Format("{0} {1} {2} {3}", UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Cancel), childNode.Attributes()["class"]);
        }

        [Fact]
        public void EditModeHtml_method_should_return_a_tag_with_applied_html_attributes_second_child() //cancel button
        {
            command.HtmlAttributes.Add("test", "test");

            command.EditModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[1];

            Assert.Equal("test", childNode.Attributes()["test"]);
        }

        [Fact]
        public void EditModeHtml_method_should_return_only_text_for_second_child_if_buttonType_is_text() //cancel button
        {
            command.ButtonType = GridButtonType.Text;

            command.EditModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[1];

            Assert.Equal("Cancel", childNode.InnerHtml);
        }

        [Fact]
        public void EditModeHtml_method_should_return_a_tag_with_span_and_select_class_as_second_child() //cancel button
        {
            command.ButtonType = GridButtonType.Image;
            command.ImageHtmlAttributes.Add("style", "width:20px");

            command.EditModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[1];

            Assert.Equal("<span class=\"t-icon t-cancel\" style=\"width:20px\"></span>", childNode.InnerHtml);
        }

        [Fact]
        public void EditModeHtml_method_should_return_a_with_span_and_text_as_second_child_if_button_type_is_imageAndText()
        {
            command.ButtonType = GridButtonType.ImageAndText;
            command.ImageHtmlAttributes.Add("style", "width:20px");

            command.EditModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[1];

            Assert.Equal("<span class=\"t-icon t-cancel\" style=\"width:20px\"></span>Cancel", childNode.InnerHtml);
        }

        [Fact]
        public void InsertModeHtml_method_should_return_button_tag_append_to_parent()
        {
            command.InsertModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("button", childNode.TagName);
        }

        [Fact]
        public void InsertModeHtml_method_should_return_button_tag_with_css_class()
        {
            command.InsertModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal(string.Format("{0} {1} {2} {3}", UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Insert), childNode.Attributes()["class"]);
        }

        [Fact]
        public void InsertModeHtml_method_should_return_a_tag_with_applied_html_attributes()
        {
            command.HtmlAttributes.Add("test", "test");

            command.InsertModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("test", childNode.Attributes()["test"]);
        }

        [Fact]
        public void InsertModeHtml_method_should_return_type_attribute()
        {
            command.InsertModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("submit", childNode.Attributes()["type"]);
        }

        [Fact]
        public void InsertModeHtml_method_should_return_only_text_if_buttonType_is_text()
        {
            command.ButtonType = GridButtonType.Text;

            command.InsertModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("Insert", childNode.InnerHtml);
        }

        [Fact]
        public void InsertModeHtml_method_should_return_a_tag_with_span_and_select_class()
        {
            command.ButtonType = GridButtonType.Image;
            command.ImageHtmlAttributes.Add("style", "width:20px");

            command.InsertModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("<span class=\"t-icon t-insert\" style=\"width:20px\"></span>", childNode.InnerHtml);
        }

        [Fact]
        public void InsertModeHtml_method_should_return_a_with_span_and_text_if_button_type_is_imageAndText()
        {
            command.ButtonType = GridButtonType.ImageAndText;
            command.ImageHtmlAttributes.Add("style", "width:20px");

            command.InsertModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("<span class=\"t-icon t-insert\" style=\"width:20px\"></span>Insert", childNode.InnerHtml);
        }

        [Fact]
        public void BoundModeHtml_method_should_return_a_tag_append_to_parent()
        {
            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("a", childNode.TagName);
        }

        [Fact]
        public void BoundModeHtml_method_should_return_a_tag_with_css_class()
        {
            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal(string.Format("{0} {1} {2} {3}", UIPrimitives.Grid.Action, UIPrimitives.Button, UIPrimitives.DefaultState, UIPrimitives.Grid.Edit), childNode.Attributes()["class"]);
        }

        [Fact]
        public void BoundModeHtml_method_should_return_a_tag_with_applied_html_attributes()
        {
            command.HtmlAttributes.Add("test", "test");

            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("test", childNode.Attributes()["test"]);
        }

        [Fact]
        public void BoundModeHtml_method_should_return_only_text_if_buttonType_is_text()
        {
            command.ButtonType = GridButtonType.Text;

            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("Edit", childNode.InnerHtml);
        }

        [Fact]
        public void BoundModeHtml_method_should_return_a_tag_with_span_and_select_class()
        {
            command.ButtonType = GridButtonType.Image;
            command.ImageHtmlAttributes.Add("style", "width:20px");

            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("<span class=\"t-icon t-edit\" style=\"width:20px\"></span>", childNode.InnerHtml);
        }

        [Fact]
        public void BoundModeHtml_method_should_return_a_with_span_and_text_if_button_type_is_imageAndText()
        {
            command.ButtonType = GridButtonType.ImageAndText;
            command.ImageHtmlAttributes.Add("style", "width:20px");

            command.BoundModeHtml<Customer>(parentNode, context.Object);
            var childNode = parentNode.Children[0];

            Assert.Equal("<span class=\"t-icon t-edit\" style=\"width:20px\"></span>Edit", childNode.InnerHtml);
        }
#endif
    }
}
