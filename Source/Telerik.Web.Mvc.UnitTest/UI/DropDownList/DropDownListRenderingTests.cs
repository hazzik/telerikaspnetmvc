namespace Telerik.Web.Mvc.UI.UnitTest
{
    using Moq;
    using System.IO;
    using System.Collections.Generic;
    using Xunit;
    using System.Web.Mvc;

    public class DropDownListRenderingTests
    {
        


        private readonly DropDownList dropdownlist;
        private readonly Mock<IDropDownListHtmlBuilder> builder;
        private readonly Mock<IHtmlNode> rootTag;
        private readonly Mock<IHtmlNode> headerTag;

        public DropDownListRenderingTests()
        {
            builder = new Mock<IDropDownListHtmlBuilder>();
            rootTag = new Mock<IHtmlNode>();
            rootTag.SetupGet(t => t.Children).Returns(() => new List<IHtmlNode>());

            headerTag = new Mock<IHtmlNode>();
            headerTag.SetupGet(t => t.Children).Returns(() => new List<IHtmlNode>());
            headerTag.Setup(t => t.AppendTo(It.IsAny<IHtmlNode>())).Returns(headerTag.Object);

            builder.Setup(t => t.DropDownListTag()).Returns(rootTag.Object);
            builder.Setup(t => t.DropDownListInnerContentTag()).Returns(headerTag.Object);

            dropdownlist = DropDownListTestHelper.CreateDropDownList(builder.Object);
            dropdownlist.Name = "dropdownlist";
        }

        [Fact]
        public void Render_should_output_DropDownList_start() 
        {
            builder.Setup(b => b.DropDownListTag()).Returns(rootTag.Object).Verifiable();

            dropdownlist.Render();

            builder.Verify();
        }

        [Fact]
        public void Render_should_output_DropDownList_inner_content_tag() 
        {
            builder.Setup(b => b.DropDownListInnerContentTag()).Returns(headerTag.Object).Verifiable();

            dropdownlist.Render();

            builder.Verify();
        }

        [Fact]
        public void Render_should_call_rootTag_WriteTo_method() 
        {
            rootTag.Setup(t => t.WriteTo(It.IsAny<TextWriter>())).Verifiable();

            dropdownlist.Render();

            rootTag.Verify();
        }


        [Fact]
        public void ObjectWriter_should_call_objectWriter_Start_method()
        {
            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.Start());

            dropdownlist.WriteInitializationScript(new Mock<TextWriter>().Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.Start());
        }

        [Fact]
        public void ObjectWriter_should_call_objectWriter_complete_method()
        {
            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.Complete());

            dropdownlist.WriteInitializationScript(new Mock<TextWriter>().Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.Complete());
        }

        [Fact]
        public void ObjectWriter_should_call_appendObject_when_ajax_settings_are_provided() 
        {
            dropdownlist.DataBinding.Ajax.Enabled = true;

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.AppendObject("ajax", It.IsAny<object>()));

            dropdownlist.WriteInitializationScript(new Mock<TextWriter>().Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.AppendObject("ajax", It.IsAny<object>()));
        }

        [Fact]
        public void ObjectWriter_should_call_appendObject_when_ws_settings_are_provided()
        {
            dropdownlist.DataBinding.WebService.Enabled = true;

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.AppendObject("ws", It.IsAny<object>()));

            dropdownlist.WriteInitializationScript(new Mock<TextWriter>().Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.AppendObject("ws", It.IsAny<object>()));
        }

        [Fact]
        public void ObjectWriter_should_never_call_appendObject_for_items_when_collection_is_empty()
        {
            dropdownlist.Items.Clear();

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.AppendObject("items", It.IsAny<object>()));

            dropdownlist.WriteInitializationScript(new Mock<TextWriter>().Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.AppendObject("items", It.IsAny<object>()), Times.Never());
        }

        [Fact]
        public void ObjectWriter_should_call_appendObject_for_items_when_collection_is_NOT_empty()
        {
            dropdownlist.Items.Clear();
            dropdownlist.Items.Add(new SelectListItem { Text = "item1", Value = "item1", Selected = true });
            dropdownlist.Items.Add(new SelectListItem { Text = "item2", Value = "item2" });
            dropdownlist.Items.Add(new SelectListItem { Text = "item3", Value = "item3" });
            dropdownlist.Items.Add(new SelectListItem { Text = "item4", Value = "item4" });

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.AppendCollection("items", It.IsAny<IEnumerable<SelectListItem>>()));

            dropdownlist.WriteInitializationScript(new Mock<TextWriter>().Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.AppendCollection("items", It.IsAny<IEnumerable<SelectListItem>>()));
        }


        [Fact]
        public void ObjectWriter_should_call_append_if_selectedIndex_is_not_negative()
        {
            dropdownlist.SelectedIndex = 1;

            DropDownListTestHelper.clientSideObjectWriter.Setup(w => w.Append("index", It.IsAny<int>(), 0));

            dropdownlist.WriteInitializationScript(new Mock<TextWriter>().Object);

            DropDownListTestHelper.clientSideObjectWriter.Verify(w => w.Append("index", It.IsAny<int>(), 0));
        }
    }
}