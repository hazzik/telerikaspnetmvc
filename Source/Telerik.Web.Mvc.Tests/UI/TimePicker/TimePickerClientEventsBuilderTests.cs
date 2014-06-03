namespace Telerik.Web.Mvc.UI.Tests
{
    using System;
    using System.Web.Mvc;
    using Xunit;

    public class TimePickerClientEventsBuilderTests
    {
        private TimePickerClientEventsBuilder builder;
        private TimePickerClientEvents clientEvents;
        private ViewContext viewContext;


        public TimePickerClientEventsBuilderTests()
        {
            clientEvents = new TimePickerClientEvents();
            viewContext = new ViewContext();
            builder = new TimePickerClientEventsBuilder(clientEvents, viewContext);
        }

        [Fact]
        public void OnChanged_method_with_Action_param_should_set_OnChange_property()
        {
            Action param = () => { };

            builder.OnChange(param);

            Assert.NotNull(clientEvents.OnChange.InlineCode);
        }

        [Fact]
        public void OnChanged_method_with_string_param_should_set_OnChange_property()
        {
            const string param = "my_method()";

            builder.OnChange(param);

            Assert.NotNull(clientEvents.OnChange.HandlerName);
        }

        [Fact]
        public void OnDateChanged_method_with_Action_param_should_return_builder()
        {
            Action param = () => { };

            var returned = builder.OnChange(param);

            Assert.IsType(typeof(TimePickerClientEventsBuilder), returned);
        }

        [Fact]
        public void OnChanged_method_with_string_param_should_return_builder()
        {
            const string param = "my_method()";

            var returned = builder.OnChange(param);

            Assert.IsType(typeof(TimePickerClientEventsBuilder), returned);
        }

        [Fact]
        public void Loaded_with_Action_param_should_set_Loaded_property()
        {
            Action param = () => { };

            builder.OnLoad(param);

            Assert.NotNull(clientEvents.OnLoad.InlineCode);
        }

        [Fact]
        public void Loaded_with_String_param_should_set_Loaded_property()
        {
            const string param = "my_method()";

            builder.OnLoad(param);

            Assert.NotNull(clientEvents.OnLoad.HandlerName);
        }

        [Fact]
        public void Loaded_with_Action_should_return_builder()
        {
            Action param = () => { };

            var returned = builder.OnLoad(param);

            Assert.IsType(typeof(TimePickerClientEventsBuilder), returned);
        }

        [Fact]
        public void Loaded_with_String_should_return_builder()
        {
            const string param = "my_method()";

            var returned = builder.OnLoad(param);

            Assert.IsType(typeof(TimePickerClientEventsBuilder), returned);
        }

        [Fact]
        public void OnOpen_with_Action_param_should_set_OnOpen_property()
        {
            Action param = () => { };

            builder.OnOpen(param);

            Assert.NotNull(clientEvents.OnOpen.InlineCode);
        }

        [Fact]
        public void OnOpen_with_String_param_should_set_OnOpen_property()
        {
            const string param = "my_method()";

            builder.OnOpen(param);

            Assert.NotNull(clientEvents.OnOpen.HandlerName);
        }

        [Fact]
        public void OnOpen_with_Action_should_return_builder()
        {
            Action param = () => { };

            var returned = builder.OnOpen(param);

            Assert.IsType(typeof(TimePickerClientEventsBuilder), returned);
        }

        [Fact]
        public void OnOpen_with_String_should_return_builder()
        {
            const string param = "my_method()";

            var returned = builder.OnOpen(param);

            Assert.IsType(typeof(TimePickerClientEventsBuilder), returned);
        }

        [Fact]
        public void OnClose_with_Action_param_should_set_OnClose_property()
        {
            Action param = () => { };

            builder.OnClose(param);

            Assert.NotNull(clientEvents.OnClose.InlineCode);
        }

        [Fact]
        public void OnClose_with_String_param_should_set_OnClose_property()
        {
            const string param = "my_method()";

            builder.OnClose(param);

            Assert.NotNull(clientEvents.OnClose.HandlerName);
        }

        [Fact]
        public void OnClose_with_Action_should_return_builder()
        {
            Action param = () => { };

            var returned = builder.OnClose(param);

            Assert.IsType(typeof(TimePickerClientEventsBuilder), returned);
        }

        [Fact]
        public void OnPopupClose_with_String_should_return_builder()
        {
            const string param = "my_method()";

            var returned = builder.OnClose(param);

            Assert.IsType(typeof(TimePickerClientEventsBuilder), returned);
        }
    }
}
