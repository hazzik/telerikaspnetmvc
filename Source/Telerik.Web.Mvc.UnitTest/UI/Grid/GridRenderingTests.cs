namespace Telerik.Web.Mvc.UI.UnitTest.Grid
{
    using System.Linq;

    using Moq;
    using Xunit;

    using UI;
    using System.IO;
    using System.Collections.Generic;

    public class GridRenderingTests
    {
        private readonly Grid<Customer> grid;
        private readonly Mock<IGridRenderer<Customer>> renderer;
        private readonly Mock<IClientSideObjectWriter> objectWriter;
        private readonly Customer customer;

        public GridRenderingTests()
        {
            renderer = new Mock<IGridRenderer<Customer>>();
            objectWriter = new Mock<IClientSideObjectWriter>();
            grid = GridTestHelper.CreateGrid(null, renderer.Object, objectWriter.Object);

            customer = new Customer { Id = 1, Name = "John Doe" };
            grid.DataSource = new[] { customer };

            grid.Columns.Add(new GridColumn<Customer>(c => c.Id));
            grid.Columns.Add(new GridColumn<Customer>(c => c.Name));
        }

        [Fact]
        public void Render_should_output_as_many_rows_as_items_in_datasource()
        {
            renderer.Setup(r => r.RowStart(It.IsAny<GridRowRenderingContext<Customer>>()));

            grid.Render();

            renderer.Verify(r => r.RowStart(It.IsAny<GridRowRenderingContext<Customer>>()), Times.Exactly(grid.DataSource.Count()));
        }

        [Fact]
        public void Render_should_output_the_value_of_the_data_item()
        {
            renderer.Setup(r => r.RowCellContent(It.IsAny<GridCellRenderingContext<Customer>>())).Verifiable();

            grid.Render();

            renderer.Verify();
        }

        [Fact]
        public void Render_calls_pager()
        {
            renderer.Setup(r => r.Pager()).Verifiable();
            grid.Paging.Enabled = true;

            grid.Render();

            renderer.Verify();
        }

        [Fact]
        public void Render_calls_page_twice_if_pager_position_set_to_both()
        {
            renderer.Setup(r => r.Pager());
            grid.Paging.Enabled = true;
            grid.Paging.Position = GridPagerPosition.Both;

            grid.Render();

            renderer.Verify(r=>r.Pager(), Times.Exactly(2));
        }

        [Fact]
        public void Render_calls_the_start_and_end_grid()
        {
            renderer.Setup(r => r.GridStart()).Verifiable();
            renderer.Setup(r => r.GridEnd()).Verifiable();

            grid.Render();

            renderer.VerifyAll();
        }

        [Fact]
        public void Render_calls_start_end_table()
        {
            renderer.Setup(r => r.TableStart()).Verifiable();
            renderer.Setup(r => r.TableEnd()).Verifiable();

            grid.Render();

            renderer.VerifyAll();
        }

        [Fact]
        public void Render_calls_start_end_tablehead()
        {
            renderer.Setup(r => r.HeaderStart()).Verifiable();
            renderer.Setup(r => r.HeaderEnd()).Verifiable();

            grid.Render();

            renderer.VerifyAll();
        }

        [Fact]
        public void Render_calls_start_end_tablefooter()
        {
            renderer.Setup(r => r.FooterStart()).Verifiable();
            renderer.Setup(r => r.FooterEnd()).Verifiable();

            grid.Paging.Enabled = true;
            grid.Render();

            renderer.VerifyAll();
        }

        [Fact]
        public void Render_calls_start_end_tablehead_row()
        {
            renderer.Setup(r => r.HeaderRowStart()).Verifiable();
            renderer.Setup(r => r.HeaderRowEnd()).Verifiable();

            grid.Render();

            renderer.VerifyAll();
        }

        [Fact]
        public void Render_calls_start_end_head_cell()
        {
            renderer.Setup(r => r.HeaderCellStart(It.IsAny<GridColumn<Customer>>())).Verifiable();
            renderer.Setup(r => r.HeaderCellEnd()).Verifiable();

            grid.Render();

            renderer.VerifyAll();
        }

        [Fact]
        public void Render_calls_head_contents()
        {
            renderer.Setup(r => r.HeaderCellContent(It.IsAny<GridColumn<Customer>>())).Verifiable();
            
            grid.Render();
            
            renderer.Verify();
        }

        [Fact]
        public void Render_calls_start_end_tablefooter_row()
        {
            renderer.Setup(r => r.FooterRowStart()).Verifiable();
            renderer.Setup(r => r.FooterRowEnd()).Verifiable();

            grid.Paging.Enabled = true;
            grid.Render();

            renderer.VerifyAll();
        }

        [Fact]
        public void Render_calls_start_end_footer_cell()
        {
            renderer.Setup(r => r.FooterCellStart()).Verifiable();
            renderer.Setup(r => r.FooterCellEnd()).Verifiable();

            grid.Paging.Enabled = true;
            grid.Render();

            renderer.VerifyAll();
        }

        [Fact]
        public void Render_calls_body_start_end()
        {
            renderer.Setup(r => r.BodyStart()).Verifiable();
            renderer.Setup(r => r.BodyEnd()).Verifiable();

            grid.Render();

            renderer.VerifyAll();
        }

        [Fact]
        public void Render_calls_row_start_end()
        {
            renderer.Setup(r => r.RowStart(It.IsAny<GridRowRenderingContext<Customer>>())).Verifiable();
            renderer.Setup(r => r.RowEnd()).Verifiable();

            grid.Render();

            renderer.VerifyAll();
        }

        [Fact]
        public void Render_calls_cell_start_end_contents()
        {
            renderer.Setup(r => r.RowCellStart(It.IsAny<GridCellRenderingContext<Customer>>())).Verifiable();
            renderer.Setup(r => r.RowCellContent(It.IsAny<GridCellRenderingContext<Customer>>())).Verifiable();
            renderer.Setup(r => r.RowCellEnd()).Verifiable();

            grid.Render();

            renderer.VerifyAll();
		}

		[Fact]
		public void Render_calls_loadingindicator_if_pager_is_enabled()
		{
			grid.Paging.Enabled = true;

			renderer.Setup(r => r.LoadingIndicator()).Verifiable();

			grid.Render();

			renderer.VerifyAll();
		}

        [Fact]
        public void Write_initialization_script_outputs_grid_component()
        {
            objectWriter.Setup(s => s.Start()).Verifiable();
            objectWriter.Setup(s => s.Complete()).Verifiable();

            grid.WriteInitializationScript(new Mock<TextWriter>().Object);

            objectWriter.VerifyAll();
        }

        [Fact]
        public void Write_initialization_script_output_column_format()
        {
            grid.Columns[0].Format="test";
            
            IEnumerable<IDictionary<string, object>> result = null;

            objectWriter.Setup(s => s.AppendCollection("columns", It.IsAny<IEnumerable<IDictionary<string, object>>>()))
                .Callback((string key, IEnumerable<IDictionary<string, object>> data) => result = data);

            grid.WriteInitializationScript(new Mock<TextWriter>().Object);

            Assert.Equal("test", result.First()["format"]);
        }

        [Fact]
        public void Write_initialization_script_outputs_paging_info()
        {
            grid.Paging.Enabled = true;

            objectWriter.Setup(w => w.Append("pageSize", 10, 10)).Verifiable();
            
            grid.WriteInitializationScript(new Mock<TextWriter>().Object);

            objectWriter.VerifyAll();
        }

        [Fact]
        public void Write_initialization_script_does_not_output_paging_info_if_pager_is_disabled()
        {
            grid.Paging.Enabled = false;

            objectWriter.Setup(w => w.Append("pageSize", It.IsAny<int>(), It.IsAny<int>()));

            grid.WriteInitializationScript(new Mock<TextWriter>().Object);

            objectWriter.Verify(w => w.Append("pageSize", It.IsAny<int>(), It.IsAny<int>()), Times.Never());

        }

        [Fact]
        public void Write_initialization_script_does_not_output_query_string_parameter_names_if_prefixing_is_disabled()
        {
            grid.PrefixUrlParameters = false;

            objectWriter.Setup(w => w.AppendObject("queryString", It.IsAny<object>()));

            grid.WriteInitializationScript(new Mock<TextWriter>().Object);

            objectWriter.Verify(w => w.AppendObject("queryString", It.IsAny<object>()), Times.Never());
        }
    
    }
}