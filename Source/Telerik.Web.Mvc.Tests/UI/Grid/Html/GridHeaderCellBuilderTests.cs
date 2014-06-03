// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using System;
    using System.Collections.Generic;
    using Infrastructure;
    using Moq;
    using Xunit;

    public class GridHeaderCellBuilderTests
    {
        private readonly GridHeaderCellBuilder builder;

        public GridHeaderCellBuilderTests()
        {
            builder = new GridHeaderCellBuilder(new Dictionary<string, object>(), delegate { });
        }

        [Fact]
        public void Should_apply_scope()
        {
            builder.CreateCell().Attribute("scope").ShouldEqual("col");
        }

        [Fact]
        public void Should_return_th()
        {
            builder.CreateCell().TagName.ShouldEqual("th");
        }

        [Fact]
        public void Should_apply_css_classes()
        {
            builder.CreateCell().Attribute("class").ShouldEqual("t-header");
        }

        [Fact]
        public void Should_apply_header_html_attributes()
        {
            const string key = "foo";
            const string value = "bar";

            var headerAttributes = new Dictionary<string, object> {{key, value}};

            var cell = new GridHeaderCellBuilder(headerAttributes, delegate { }).CreateCell();
            cell.Attribute(key).ShouldEqual(value);
        }

        [Fact]
        public void Should_call_append_content()
        {
            var headerAttributes = new Dictionary<string, object>();

            bool isCalled = false;
            Action<IHtmlNode> appendContent = node => isCalled = true;
            new GridHeaderCellBuilder(headerAttributes, appendContent).CreateCell();

            isCalled.ShouldBeTrue();
        }

        [Fact]
        public void Should_append_content()
        {
            var content = new HtmlElement("foo");

            Action<IHtmlNode> appendContent = node => content.AppendTo(node);
            var headerAttributes = new Dictionary<string, object>();
            var cell = new GridHeaderCellBuilder(headerAttributes, appendContent).CreateCell();

            cell.Children.ShouldContain(content);
        }

        [Fact]
        public void Should_apply_decorators()
        {
            var decorator = new Mock<IGridCellBuilderDecorator>();
            builder.Decorators.Add(decorator.Object);

            builder.CreateCell();
            decorator.Verify(d => d.Decorate(It.IsAny<IHtmlNode>()), Times.Once());
        }
    }
}