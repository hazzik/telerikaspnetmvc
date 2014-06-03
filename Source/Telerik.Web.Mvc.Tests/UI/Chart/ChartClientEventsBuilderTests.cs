﻿namespace Telerik.Web.Mvc.UI.Tests.Chart
{
    using System;
    using Telerik.Web.Mvc.UI;
    using Telerik.Web.Mvc.UI.Fluent;
    using Xunit;

    public class ChartClientEventsBuilderTests
    {
        private readonly ChartClientEventsBuilder builder;
        private readonly ChartClientEvents clientEvents;
        private readonly Action emptyAction;
        private readonly Func<object, object> nullFunc;
        private readonly string handlerName;

        public ChartClientEventsBuilderTests()
        {
            clientEvents = new ChartClientEvents();
            builder = new ChartClientEventsBuilder(clientEvents);

            emptyAction = () => { };
            nullFunc = (o) => null;
            handlerName = "myHandler";
        }

        [Fact]
        public void OnLoad_with_Action_should_set_CodeBlock()
        {
            builder.OnLoad(emptyAction);
            clientEvents.OnLoad.CodeBlock.ShouldNotBeNull();
        }

        [Fact]
        public void OnLoad_with_Action_should_return_builder()
        {
            builder.OnLoad(emptyAction).ShouldBeType<ChartClientEventsBuilder>();
        }

        [Fact]
        public void OnLoad_with_Func_should_set_InlineCodeBlock()
        {
            builder.OnLoad(nullFunc);
            clientEvents.OnLoad.InlineCodeBlock.ShouldNotBeNull();
        }

        [Fact]
        public void OnLoad_with_Func_should_return_builder()
        {
            builder.OnLoad(nullFunc).ShouldBeType<ChartClientEventsBuilder>();
        }

        [Fact]
        public void OnLoad_with_string_should_set_HandlerName()
        {
            builder.OnLoad(handlerName);
            clientEvents.OnLoad.HandlerName.ShouldEqual(handlerName);
        }

        [Fact]
        public void OnLoad_with_string_should_return_builder()
        {
            builder.OnLoad(handlerName).ShouldBeType<ChartClientEventsBuilder>();
        }
    }
}
