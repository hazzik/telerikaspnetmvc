// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System;
    using System.Web.Mvc;

    using Telerik.Web.Mvc.UI;

    using Moq;
    using Xunit;

    public class MessageBoxBuilderTests
    {
        private readonly MessageBox _messageBox;
        private readonly MessageBoxBuilder _builder;

        public MessageBoxBuilderTests()
        {
            _messageBox = new MessageBox(new ViewContext(), new Mock<IClientSideObjectWriterFactory>().Object);
            _builder = new MessageBoxBuilder(_messageBox);
        }

        [Fact]
        public void Should_be_able_to_set_message_type()
        {
            _builder.MessageType(MessageBoxType.Error);

            Assert.Equal(MessageBoxType.Error, _messageBox.MessageType);
        }

        [Fact]
        public void Should_be_able_to_set_content()
        {
            Action content = () => { };

            _builder.Content(content);

            Assert.Same(content, _messageBox.Content);
        }

        [Fact]
        public void Should_be_able_to_set_theme()
        {
            _builder.Theme("custom");

            Assert.Equal("custom", _messageBox.Theme);
        }
    }
}