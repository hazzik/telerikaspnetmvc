namespace Telerik.Web.Mvc.UI.UnitTest.Grid
{
    using System.IO;
    using System.Web.UI;
	
    using UI;
    
    using Moq;
    using Xunit;

    public class GridRendererTests
    {
        private readonly Grid<Customer> grid;
        private readonly Mock<HtmlTextWriter> writer;
        private readonly GridRenderer<Customer> renderer;

        public GridRendererTests()
        {
            writer = new Mock<HtmlTextWriter>(TextWriter.Null);
            
            grid = GridTestHelper.CreateGrid<Customer>(writer.Object, null);
            grid.DataSource = new[] { new Customer { Id = 1, Name = "John Doe" } };

            grid.Columns.Add(new GridColumn<Customer>(c => c.Id));
            grid.Columns.Add(new GridColumn<Customer>(c => c.Name));

            renderer = new GridRenderer<Customer>(grid, writer.Object);
        }
        
        [Fact]
        public void GridStart_outputs_div()
        {
            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Id, grid.Name)).Verifiable();
            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-widget t-grid")).Verifiable();
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Div)).Verifiable();

            renderer.GridStart();

            writer.VerifyAll();
        }

        [Fact]
        public void GridEnd_calls_render_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.GridEnd();

            writer.Verify();
        }

        [Fact]
        public void TableStart_otputs_table()
        {
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Table)).Verifiable();

            renderer.TableStart();

            writer.Verify();
        }

        [Fact]
        public void TableEnd_calls_render_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.TableEnd();

            writer.Verify();
        }

        [Fact]
        public void HeaderStart_outputs_thead()
        {
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Thead)).Verifiable();

            renderer.HeaderStart();

            writer.Verify();
        }

        [Fact]
        public void HeaderEnd_calls_render_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.HeaderEnd();

            writer.Verify();
        }
        
        [Fact]
        public void HeaderRowStart_outputs_tr()
        {
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Tr)).Verifiable();

            renderer.HeaderRowStart();

            writer.Verify();
        }

        [Fact]
        public void HeaderRowEnd_calls_render_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.HeaderRowEnd();

            writer.Verify();
        }

        [Fact]
        public void HeaderCellStart_outputs_th()
        {
            writer.Setup(w => w.AddAttribute("scope", "col", true)).Verifiable();
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Th)).Verifiable();

            renderer.HeaderCellStart(grid.Columns[0]);

            writer.Verify();
        }

        [Fact]
        public void HeaderCellEnd_calls_render_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.HeaderCellEnd();

            writer.Verify();
        }

        [Fact]
        public void HeaderCellContent_should_output_header_text_from_column_value()
        {
            writer.Setup(w => w.Write(grid.Columns[0].Title)).Verifiable();

            renderer.HeaderCellContent(grid.Columns[0]);

            writer.Verify();
        }

        [Fact]
        public void Pager_should_output_paging_tags()
        {
            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-pager t-reset")).Verifiable();
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Div)).Verifiable();
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.Pager();

            writer.VerifyAll();
        }

        [Fact]
        public void FooterStart_outputs_tfoot()
        {
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Tfoot)).Verifiable();

            renderer.FooterStart();

            writer.Verify();
        }

        [Fact]
        public void FooterEnd_calls_render_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.FooterEnd();

            writer.Verify();
        }

        [Fact]
        public void FooterRowStart_outputs_tr()
        {
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Tr)).Verifiable();

            renderer.FooterRowStart();

            writer.Verify();
        }

        [Fact]
        public void FooterRowEnd_calls_render_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.FooterRowEnd();

            writer.Verify();
        }

        [Fact]
        public void FooterCellSTart_renders_td()
        {
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Td)).Verifiable();
            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Class, "t-footer")).Verifiable();
            writer.Setup(w => w.AddAttribute(HtmlTextWriterAttribute.Colspan, grid.Columns.Count.ToString())).Verifiable();

            renderer.FooterCellStart();

            writer.VerifyAll();
        }

        [Fact]
        public void FooterCellEnd_calls_render_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.FooterCellEnd();

            writer.Verify();
        }

        [Fact]
        public void BodyStart_renders_tbody()
        {
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Tbody)).Verifiable();

            renderer.BodyStart();

            writer.VerifyAll();
        }

        [Fact]
        public void BodyEnd_calls_render_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.BodyEnd();

            writer.Verify();
        }

        [Fact]
        public void RowStart_renders_tr()
        {
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Tr)).Verifiable();

            renderer.RowStart(new GridRowRenderingContext<Customer>(new Customer(), 1));

            writer.VerifyAll();
        }

        [Fact]
        public void RowEnd_calls_render_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.RowEnd();

            writer.Verify();
        }
        
        [Fact]
        public void CellStart_renders_td()
        {
            writer.Setup(w => w.RenderBeginTag(HtmlTextWriterTag.Td)).Verifiable();

            renderer.RowCellStart(new GridCellRenderingContext<Customer>(grid.Columns[0], new Customer(), 1));

            writer.VerifyAll();
        }

        [Fact]
        public void CellEnd_calls_render_end_tag()
        {
            writer.Setup(w => w.RenderEndTag()).Verifiable();

            renderer.RowCellEnd();

            writer.Verify();
        }
        
        [Fact]
        public void CellContent_outputs_the_supplied_text()
        {
            writer.Setup(w => w.Write("content")).Verifiable();

            renderer.RowCellContent(new GridCellRenderingContext<Customer>(grid.Columns[1], new Customer { Name = "content" }, 1));

            writer.Verify();
        }
    }
}