﻿namespace Telerik.Web.Mvc.UI.UnitTest
{
    using Xunit;
    using System;
    using Moq;
    using System.Web.Mvc;

    public class PanelBarItemBuilderTests
    {
        private readonly PanelBarItem item;
        private readonly PanelBarItemBuilder builder;

        public PanelBarItemBuilderTests()
        {

            Mock<ViewContext> viewContext = new Mock<ViewContext>();

            item = new PanelBarItem();
            builder = new PanelBarItemBuilder(item, viewContext.Object);
        }

        [Fact]
        public void Builder_should_set_expanded_property()
        {
            builder.Expanded(true);
            Assert.Equal(true, item.Expanded);
        }

        [Fact]
        public void Expanded_should_return_PanelBatItemBuilder_object()
        {
            var returnedBuilder = builder.Expanded(true);
            Assert.IsType(typeof(PanelBarItemBuilder), returnedBuilder);
        }

        [Fact]
        public void ChildItems_should_return_PanelBatItemBuilder_object()
        {
            var returnedBuilder = builder.Items(item =>
                    {
                        item.Add().Text("Child 1");
                    });

            Assert.IsType(typeof(PanelBarItemBuilder), returnedBuilder);
        }

        [Fact]
        public void ChildItems_action_should_add_one_item()
        {
            var returnedBuilder = builder.Items(item =>
            {
                item.Add().Text("Child 1");
            });

            var result = ((LinkedObjectCollection<PanelBarItem>)(((PanelBarItem)(returnedBuilder.ToItem())).Items)).Count;
            Assert.Equal(1, result);
        }

        [Fact]
        public void LoadContentFrom_should_set_contentUrl_of_item()
        {
            const string value = "test";

            builder.LoadContentFrom(value);

            Assert.Equal(value, item.ContentUrl);
        }

        [Fact]
        public void LoadContentFrom_should_return_TBuilder_object()
        {
            const string value = "test";

            var returnedBuilder = builder.LoadContentFrom(value);

            Assert.IsType(typeof(PanelBarItemBuilder), returnedBuilder);
        }

        [Fact]
        public void Setting_Url_when_contentUrl_is_set_should_throw_exception()
        {
            const string url = "test";
            item.ContentUrl = url;

            Assert.Throws(typeof(NotSupportedException), () => { builder.Url(url); });
        }


        [Fact]
        public void Setting_ContentUrl_when_Url_is_set_should_throw_exception()
        {
            const string url = "test";
            item.Url = url;

            Assert.Throws(typeof(NotSupportedException), () => { builder.LoadContentFrom(url); });
        }
    }
}