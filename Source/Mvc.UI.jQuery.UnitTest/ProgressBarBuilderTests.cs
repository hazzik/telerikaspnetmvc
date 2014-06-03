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

    public class ProgressBarBuilderTests
    {
        private readonly ProgressBar _progressBar;
        private readonly ProgressBarBuilder _builder;

        public ProgressBarBuilderTests()
        {
            _progressBar = new ProgressBar(new ViewContext(), new Mock<IClientSideObjectWriterFactory>().Object);
            _builder = new ProgressBarBuilder(_progressBar);
        }

        [Fact]
        public void Should_be_able_to_set_value()
        {
            _builder.Value(50);

            Assert.Equal(50, _progressBar.Value);
        }

        [Fact]
        public void Should_be_able_to_set_updated_elements_for_value()
        {
            _builder.UpdateElements("#foobar");

            Assert.Contains("#foobar", _progressBar.UpdateElements);
        }

        [Fact]
        public void Should_be_able_to_set_on_change()
        {
            Action onChange = delegate { };

            _builder.OnChange(onChange);

            Assert.Same(onChange, _progressBar.OnChange);
        }

        [Fact]
        public void Should_be_able_to_set_theme()
        {
            _builder.Theme("custom");

            Assert.Equal("custom", _progressBar.Theme);
        }
    }
}