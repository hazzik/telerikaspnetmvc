// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System;

    using Xunit;

    public class AccordionItemTests
    {
        private readonly AccordionItem _accordionItem;

        public AccordionItemTests()
        {
            _accordionItem = new AccordionItem();
        }

        [Fact]
        public void HtmlAttributes_should_be_empty_when_new_instance_is_created()
        {
            Assert.Empty(_accordionItem.HtmlAttributes);
        }

        [Fact]
        public void ContentHtmlAttributes_should_be_empty_when_new_instance_is_created()
        {
            Assert.Empty(_accordionItem.ContentHtmlAttributes);
        }

        [Fact]
        public void Should_be_able_to_text()
        {
            _accordionItem.Text = "My header";

            Assert.Equal("My header", _accordionItem.Text);
        }

        [Fact]
        public void Should_be_able_to_set_content()
        {
            Action content = () => { };

            _accordionItem.Content = content;

            Assert.Same(content, _accordionItem.Content);
        }
    }
}