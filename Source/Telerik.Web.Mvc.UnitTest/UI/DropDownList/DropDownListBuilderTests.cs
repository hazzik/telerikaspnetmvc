namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.UI;
    using Moq;
    using Telerik.Web.Mvc.UI.Fluent;
    using Xunit;
    using System.Web.Mvc;

    public class DropDownListBuilderTests
    {
        private readonly DropDownList dropDownList;
        private readonly DropDownListBuilder builder;

        public DropDownListBuilderTests()
        {
            dropDownList = DropDownListTestHelper.CreateDropDownList(null);
            builder = new DropDownListBuilder(dropDownList);
        }

        [Fact]
        public void Items_call_SelectListItemFactory_to_add_item()
        {
            builder.Items(c => c.Add());

            Assert.Equal(1, dropDownList.Items.Count);
        }

        [Fact]
        public void Items_should_return_builder()
        {
            var returnedBuilder = builder.Items(c => c.Add());

            Assert.IsType(typeof(DropDownListBuilder), returnedBuilder);
        }

        [Fact]
        public void BindTo_for_IEnumerable_should_create_two_items()
        {
            List<SelectListItem> list = new List<SelectListItem>
                                    {
                                        new SelectListItem{Text="", Value=""},
                                        new SelectListItem{Text="", Value=""}
                                    };

            builder.BindTo(list);

            Assert.Equal(2, dropDownList.Items.Count);
        }

        [Fact]
        public void BindTo_for_IEnumerable_should_return_builder()
        {
            var returnedBuilder = builder.BindTo(new List<SelectListItem>());

            Assert.IsType(typeof(DropDownListBuilder), returnedBuilder);
        }

        [Fact]
        public void DataBinding_should_enable_Ajax() 
        {
            builder.DataBinding(c => c.Ajax());

            Assert.True(dropDownList.DataBinding.Ajax.Enabled);
        }

        [Fact]
        public void ClientEvents_should_set_events()
        {
            Action<DropDownListClientEventsBuilder> clientEventsAction = eventBuilder => { eventBuilder.OnLoad("Load"); };

            builder.ClientEvents(clientEventsAction);

            Assert.NotNull(dropDownList.ClientEvents.OnLoadHtml);
        }

        [Fact]
        public void ClientEvents_should_return_builder()
        {
            Action<DropDownListClientEventsBuilder> clientEventsAction = eventBuilder => { eventBuilder.OnLoad("Load"); };

            var returnedBuilder = builder.ClientEvents(clientEventsAction);

            Assert.IsType(typeof(DropDownListBuilder), returnedBuilder);
        }

        [Fact]
        public void SelectedIndex_should_set_selected_item_index()
        {
            builder.SelectedIndex(1);

            Assert.Equal(1, dropDownList.SelectedIndex);
        }

        [Fact]
        public void SelectedIndex_should_return_builder()
        {
            var returnedBuilder = builder.SelectedIndex(0);

            Assert.IsType(typeof(DropDownListBuilder), returnedBuilder);
        }

        //[Fact]
        //public void Effects_creates_fx_factory()
        //{
        //    var fxFacCreated = false;

        //    builder.Effects(fx =>
        //    {
        //        fxFacCreated = fx != null;
        //    });

        //    Assert.True(fxFacCreated);
        //}
    }
}