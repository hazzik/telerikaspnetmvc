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

    public class AccordionTests
    {
        private readonly ViewContext _viewContext;
        private readonly Mock<HttpContextBase> _httpContext;
        private readonly Mock<IClientSideObjectWriterFactory> _clientSideObjectWriterFactory;

        private readonly Accordion _accordion;

        public AccordionTests()
        {
            _httpContext = TestHelper.CreateMockedHttpContext();
            _viewContext = new ViewContext { HttpContext = _httpContext.Object, ViewData = new ViewDataDictionary() };

            _clientSideObjectWriterFactory = new Mock<IClientSideObjectWriterFactory>();

            _accordion = new Accordion(_viewContext, _clientSideObjectWriterFactory.Object) { AssetKey = jQueryViewComponentFactory.DefaultAssetKey };
        }

        [Fact]
        public void Items_should_be_empty_when_new_instance_is_created()
        {
            Assert.Empty(_accordion.Items);
        }

        [Fact]
        public void AutoHeight_should_be_true_when_new_instance_is_created()
        {
            Assert.True(_accordion.AutoHeight);
        }

        [Fact]
        public void Should_be_able_to_set_auto_height()
        {
            _accordion.AutoHeight = false;

            Assert.False(_accordion.AutoHeight);
        }

        [Fact]
        public void AutoHeight_should_reset_clear_style_to_false()
        {
            _accordion.ClearStyle = true;
            _accordion.AutoHeight = false;

            Assert.False(_accordion.ClearStyle);
        }

        [Fact]
        public void Should_be_able_to_set_clear_style()
        {
            _accordion.ClearStyle = true;

            Assert.True(_accordion.ClearStyle);
        }

        [Fact]
        public void ClearStyle_should_reset_auto_height_to_false()
        {
            _accordion.AutoHeight = true;
            _accordion.ClearStyle = true;

            Assert.False(_accordion.AutoHeight);
        }

        [Fact]
        public void WriteInitializationScript_should_write_scripts_for_icon_and_selected_icon()
        {
            Mock<IClientSideObjectWriter> writer = new Mock<IClientSideObjectWriter>();
            SetupForWriteInitializationScript(writer);

            _accordion.Icon = "customIcon";
            _accordion.SelectedIcon = "customSelectedIcon";

            _accordion.WriteInitializationScript(new Mock<TextWriter>().Object);

            writer.VerifyAll();
        }

        [Fact]
        public void WriteInitializationScript_should_write_scripts_for_icon()
        {
            Mock<IClientSideObjectWriter> writer = new Mock<IClientSideObjectWriter>();
            SetupForWriteInitializationScript(writer);

            _accordion.Icon = "customIcon";

            _accordion.WriteInitializationScript(new Mock<TextWriter>().Object);
        }

        [Fact]
        public void WriteInitializationScript_should_write_scripts_for_selected_icon()
        {
            Mock<IClientSideObjectWriter> writer = new Mock<IClientSideObjectWriter>();
            SetupForWriteInitializationScript(writer);

            _accordion.SelectedIcon = "customSelectedIcon";

            _accordion.WriteInitializationScript(new Mock<TextWriter>().Object);
        }

        [Fact]
        public void Render_should_write_in_response()
        {
            _accordion.Name = "myAccordion";
            _accordion.Theme = "custom";
            _accordion.HtmlAttributes.Add("class", "myClass");

            _accordion.Items.Add(new AccordionItem { Text = "Section1", Content = delegate { }, Selected = true });
            _accordion.Items.Add(new AccordionItem { Text = "Section2", Content = delegate { } });

            _accordion.Items[0].HtmlAttributes.Add("class", "myClass");
            _accordion.Items[0].ContentHtmlAttributes.Add("class", "myContent");

            _accordion.Items[1].HtmlAttributes.Add("style", "text-align:left");
            _accordion.Items[1].ContentHtmlAttributes.Add("style", "whitepsace:nowrap");

            _httpContext.Setup(context => context.Response.Output.Write(It.IsAny<string>())).Verifiable();

            _accordion.Render();

            _httpContext.Verify();
        }

        private void SetupForWriteInitializationScript(Mock<IClientSideObjectWriter> writer)
        {
            _accordion.Name = "myAccordion";
            _accordion.AnimationName = "bounce";
            _accordion.AutoHeight = false;
            _accordion.ClearStyle = true;
            _accordion.OpenOn = "mouseover";
            _accordion.CollapsibleContent = true;
            _accordion.FillSpace = true;
            _accordion.OnChange = delegate { };
            _accordion.Items.Add(new AccordionItem { Text = "Section1", Content = delegate { } });
            _accordion.Items.Add(new AccordionItem { Text = "Section2", Content = delegate { }, Selected = true });

            writer.Setup(w => w.Start()).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<string>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<Action>())).Returns(writer.Object);
            writer.Setup(w => w.Complete());

            _clientSideObjectWriterFactory.Setup(f => f.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(writer.Object);
        }
    }
}