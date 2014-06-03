// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.UI;

    using Moq;
    using Xunit;

    public class ProgressBarTests
    {
        private readonly ViewContext _viewContext;
        private readonly Mock<HttpContextBase> _httpContext;
        private readonly Mock<IClientSideObjectWriterFactory> _clientSideObjectWriterFactory;

        private readonly ProgressBar _progressBar;

        public ProgressBarTests()
        {
            _httpContext = TestHelper.CreateMockedHttpContext();
            _viewContext = new ViewContext { HttpContext = _httpContext.Object, ViewData = new ViewDataDictionary() };

            _clientSideObjectWriterFactory = new Mock<IClientSideObjectWriterFactory>();

            _progressBar = new ProgressBar(_viewContext, _clientSideObjectWriterFactory.Object) { AssetKey = jQueryViewComponentFactory.DefaultAssetKey };
        }

        [Fact]
        public void UpdateElements_should_be_empty_when_new_instance_is_created()
        {
            Assert.Empty(_progressBar.UpdateElements);
        }

        [Fact]
        public void Should_be_able_to_set_value()
        {
            _progressBar.Value = 20;

            Assert.Equal(20, _progressBar.Value);
        }

        [Fact]
        public void Value_should_throw_exception_when_trying_to_set_out_of_range_value()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _progressBar.Value = 101);
        }

        [Fact]
        public void GetValue_should_return_correct_value_from_view_date()
        {
            _progressBar.Name = "myProgressBar";
            _viewContext.ViewData["myProgressBar"] = 30;

            Assert.Equal(30, _progressBar.GetValue());
        }

        [Fact]
        public void WriteInitializationScript_should_write_scripts()
        {
            _progressBar.Name = "myProgressBar";
            _progressBar.Value = 10;
            _progressBar.UpdateElements.Add("#foo");
            _progressBar.OnChange = delegate { };

            Mock<IClientSideObjectWriter> writer = new Mock<IClientSideObjectWriter>();

            writer.Setup(w => w.Start()).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<int?>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<Action>())).Returns(writer.Object);
            writer.Setup(w => w.Complete());

            _clientSideObjectWriterFactory.Setup(f => f.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(writer.Object);

            _progressBar.WriteInitializationScript(new Mock<TextWriter>().Object);

            writer.VerifyAll();
        }

        [Fact]
        public void Render_should_write_in_response()
        {
            _progressBar.Name = "myProgressBar";
            _progressBar.HtmlAttributes.Add("style", "border:1px solid #222");
            _progressBar.Theme = "custom";

            _httpContext.Setup(context => context.Response.Output.Write(It.IsAny<string>())).Verifiable();

            _progressBar.Render();

            _httpContext.Verify();
        }
    }
}