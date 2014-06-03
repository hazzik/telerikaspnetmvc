namespace Telerik.Web.Mvc.UI.Tests
{
    using System;
    using Moq;
    using Xunit;

    public class GridToolBarSettingsTests
    {
        private GridToolBarSettings<object> toolBarSettings;

        public GridToolBarSettingsTests()
        {
            toolBarSettings = new GridToolBarSettings<object>(GridTestHelper.CreateGrid<object>());
        }

        [Fact]
        public void Should_AppendTo_calls_html_for_all_commands()
        {
            var toolBarCommand1 = new Mock<GridToolBarCommandBase<object>> { CallBase = true };
            var toolBarCommand2 = new Mock<GridToolBarCommandBase<object>> { CallBase = true };
            var htmlNode = new Mock<IHtmlNode>();

            toolBarSettings.Commands.Add(toolBarCommand1.Object);
            toolBarSettings.Commands.Add(toolBarCommand2.Object);

            toolBarSettings.AppendTo(htmlNode.Object);

            toolBarCommand1.Verify(c => c.Html(It.IsAny<Grid<object>>(), htmlNode.Object), Times.Once());
            toolBarCommand1.Verify(c => c.Html(It.IsAny<Grid<object>>(), htmlNode.Object), Times.Once());
        }

        [Fact]
        public void Should_append_template_if_such_exists()
        {
            bool isCalled = false;
            var htmlNode = new Mock<IHtmlNode>();

            htmlNode.Setup(node => node.Template(It.IsAny<Action>())).Callback<Action>(a => a());

            Action contentAction = () => { isCalled = true; };

            toolBarSettings.Template.Content = contentAction;
            toolBarSettings.AppendTo(htmlNode.Object);

            isCalled.ShouldBeTrue();
        }

        [Fact]
        public void Should_append_template_if_both_commands_and_template_is_defined()
        {
            bool isCalled = false;
            var htmlNode = new Mock<IHtmlNode>();
            htmlNode.Setup(node => node.Template(It.IsAny<Action>())).Callback<Action>(a => a());
            var toolBarCommand1 = new Mock<GridToolBarCommandBase<object>> { CallBase = true };
            Action contentAction = () => { isCalled = true; };

            toolBarSettings.Commands.Add(toolBarCommand1.Object);
            toolBarSettings.Template.Content = contentAction;
            toolBarSettings.AppendTo(htmlNode.Object);

            toolBarCommand1.Verify(c => c.Html(It.IsAny<Grid<object>>(), htmlNode.Object), Times.Never());
            isCalled.ShouldBeTrue();
        }

        [Fact]
        public void Enabled_should_return_true_if_commands_are_not_empty()
        {
            toolBarSettings.Commands.Add(new Mock<GridToolBarCommandBase<object>>().Object);
            toolBarSettings.Enabled.ShouldBeTrue();
        }

        [Fact]
        public void Enabled_should_return_true_if_template_is_not_empty()
        {
            toolBarSettings.Template.Content = () => { };
            toolBarSettings.Enabled.ShouldBeTrue();
        }
    }
}