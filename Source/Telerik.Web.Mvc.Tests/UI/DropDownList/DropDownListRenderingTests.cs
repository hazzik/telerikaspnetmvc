namespace Telerik.Web.Mvc.UI.Tests
{
    using Moq;
    using System.IO;
    using System.Collections.Generic;
    using Xunit;

    public class DropDownListRenderingTests
    {
        private readonly DropDownList dropdownlist;
        private readonly Mock<IDropDownHtmlBuilder> builder;
        private readonly Mock<IHtmlNode> rootTag;
        private readonly Mock<IHtmlNode> headerTag;
        Mock<TextWriter> textWriter;

        public DropDownListRenderingTests()
        {
            builder = new Mock<IDropDownHtmlBuilder>();
            rootTag = new Mock<IHtmlNode>();
            rootTag.SetupGet(t => t.Children).Returns(() => new List<IHtmlNode>());

            headerTag = new Mock<IHtmlNode>();
            headerTag.SetupGet(t => t.Children).Returns(() => new List<IHtmlNode>());
            headerTag.Setup(t => t.AppendTo(It.IsAny<IHtmlNode>())).Returns(headerTag.Object);

            builder.Setup(t => t.Build()).Returns(rootTag.Object);
            builder.Setup(t => t.InnerContentTag()).Returns(headerTag.Object);

            dropdownlist = DropDownListTestHelper.CreateDropDownList();
            dropdownlist.Name = "dropdownlist";

            textWriter = new Mock<TextWriter>();
        }

        [Fact]
        public void ObjectWriter_should_call_objectWriter_Start_method()
        {
            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.Start());

            dropdownlist.WriteInitializationScript(textWriter.Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.Start());
        }

        [Fact]
        public void ObjectWriter_should_call_objectWriter_complete_method()
        {
            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.Complete());

            dropdownlist.WriteInitializationScript(textWriter.Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.Complete());
        }

        [Fact]
        public void ObjectWriter_should_call_appendObject_when_ajax_settings_are_provided() 
        {
            dropdownlist.DataBinding.Ajax.Enabled = true;

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.AppendObject("ajax", It.IsAny<object>()));

            dropdownlist.WriteInitializationScript(textWriter.Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.AppendObject("ajax", It.IsAny<object>()));
        }

        [Fact]
        public void ObjectWriter_should_call_appendObject_when_ws_settings_are_provided()
        {
            dropdownlist.DataBinding.WebService.Enabled = true;

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.AppendObject("ws", It.IsAny<object>()));

            dropdownlist.WriteInitializationScript(textWriter.Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.AppendObject("ws", It.IsAny<object>()));
        }

        [Fact]
        public void ObjectWriter_should_never_call_appendObject_for_items_when_collection_is_empty()
        {
            dropdownlist.Items.Clear();

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.AppendObject("data", It.IsAny<object>()));

            dropdownlist.WriteInitializationScript(textWriter.Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.AppendObject("data", It.IsAny<object>()), Times.Never());
        }

        [Fact]
        public void ObjectWriter_should_call_appendObject_for_items_when_collection_is_NOT_empty()
        {
            dropdownlist.Items.Clear();
            dropdownlist.Items.Add(new DropDownItem { Text = "item1", Value = "item1", Selected = true });
            dropdownlist.Items.Add(new DropDownItem { Text = "item2", Value = "item2" });
            dropdownlist.Items.Add(new DropDownItem { Text = "item3", Value = "item3" });
            dropdownlist.Items.Add(new DropDownItem { Text = "item4", Value = "item4" });

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.AppendCollection("data", It.IsAny<IEnumerable<DropDownItem>>()));

            dropdownlist.WriteInitializationScript(textWriter.Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.AppendCollection("data", It.IsAny<IEnumerable<DropDownItem>>()));
        }

        [Fact]
        public void ObjectWriter_should_call_append_if_selectedIndex_is_not_negative()
        {
            dropdownlist.SelectedIndex = 1;

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.Append("index", It.IsAny<int>(), 0));

            dropdownlist.WriteInitializationScript(textWriter.Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.Append("index", It.IsAny<int>(), 0));
        }

        [Fact]
        public void ObjectWriter_should_append_Load_property_of_clientEvents()
        {
            dropdownlist.ClientEvents.OnLoad.InlineCode = () => { };

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.AppendClientEvent("onLoad", dropdownlist.ClientEvents.OnLoad)).Verifiable();

            dropdownlist.WriteInitializationScript(textWriter.Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.AppendClientEvent("onLoad", dropdownlist.ClientEvents.OnLoad));
        }

        [Fact]
        public void ObjectWriter_should_append_Select_property_of_clientEvents()
        {
            dropdownlist.ClientEvents.OnChange.InlineCode = () => { };

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.AppendClientEvent("onChange", dropdownlist.ClientEvents.OnChange)).Verifiable();

            dropdownlist.WriteInitializationScript(textWriter.Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.AppendClientEvent("onChange", dropdownlist.ClientEvents.OnChange));
        }

        [Fact]
        public void ObjectWriter_should_append_PopUpOpen_property_of_clientEvents()
        {
            dropdownlist.ClientEvents.OnOpen.InlineCode = () => { };

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.AppendClientEvent("onOpen", dropdownlist.ClientEvents.OnOpen)).Verifiable();

            dropdownlist.WriteInitializationScript(textWriter.Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.AppendClientEvent("onOpen", dropdownlist.ClientEvents.OnOpen));
        }

        [Fact]
        public void ObjectWriter_should_append_PopUpClose_property_of_clientEvents()
        {
            dropdownlist.ClientEvents.OnClose.InlineCode = () => { };

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.AppendClientEvent("onClose", dropdownlist.ClientEvents.OnClose)).Verifiable();

            dropdownlist.WriteInitializationScript(textWriter.Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.AppendClientEvent("onClose", dropdownlist.ClientEvents.OnClose));
        }

        [Fact]
        public void ObjectWriter_should_append_databinding_property_of_clientEvents()
        {
            dropdownlist.ClientEvents.OnDataBinding.InlineCode = () => { };

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.AppendClientEvent("onDataBinding", dropdownlist.ClientEvents.OnDataBinding)).Verifiable();

            dropdownlist.WriteInitializationScript(textWriter.Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.AppendClientEvent("onDataBinding", dropdownlist.ClientEvents.OnDataBinding));
        }

        [Fact]
        public void ObjectWriter_should_append_databound_property_of_clientEvents()
        {
            dropdownlist.ClientEvents.OnDataBound.InlineCode = () => { };

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.AppendClientEvent("onDataBound", dropdownlist.ClientEvents.OnDataBound)).Verifiable();

            dropdownlist.WriteInitializationScript(textWriter.Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.AppendClientEvent("onDataBound", dropdownlist.ClientEvents.OnDataBound));
        }

        [Fact]
        public void ObjectWriter_should_append_error_property_of_clientEvents()
        {
            dropdownlist.ClientEvents.OnError.InlineCode = () => { };

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.AppendClientEvent("onError", dropdownlist.ClientEvents.OnError)).Verifiable();

            dropdownlist.WriteInitializationScript(textWriter.Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.AppendClientEvent("onError", dropdownlist.ClientEvents.OnError));
        }

        [Fact]
        public void ObjectWriter_should_call_append_if_DropDownHtmlAttributes_are_set()
        {
            dropdownlist.DropDownHtmlAttributes.Add("height", "100px");

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.Append("dropDownAttr", It.IsAny<string>()));

            dropdownlist.WriteInitializationScript(textWriter.Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.Append("dropDownAttr", It.IsAny<string>()));
        }
    }
}