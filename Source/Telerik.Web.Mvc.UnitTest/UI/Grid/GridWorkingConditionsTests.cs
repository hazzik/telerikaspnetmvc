namespace Telerik.Web.Mvc.UI.UnitTest.Grid
{
    using Moq;
    
    using Xunit;
    using System;

    public class GridWorkingConditionsTests
    {
        private readonly Grid<Customer> grid;
        private readonly Mock<IGridRenderer<Customer>> renderer;
        private readonly Mock<IClientSideObjectWriter> objectWriter;
        private readonly Customer customer;

        public GridWorkingConditionsTests()
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
        public void Should_throw_when_both_ajax_and_web_service_are_enabled()
        {
            grid.Ajax.Enabled = grid.WebService.Enabled = true;

            Assert.Throws<NotSupportedException>(() => grid.Render());
        }

        [Fact]
        public void Should_throw_when_using_templates_and_ajax()
        {
            grid.Ajax.Enabled = true;
            grid.Columns[0].Template = delegate { };

            Assert.Throws<NotSupportedException>(() => grid.Render());
        }

        [Fact]
        public void Should_throw_when_using_templates_and_web_service()
        {
            grid.WebService.Enabled = true;
            grid.Columns[0].Template = delegate { };

            Assert.Throws<NotSupportedException>(() => grid.Render());
        }

        [Fact]
        public void Should_throw_if_web_service_url_is_not_set()
        {
            grid.WebService.Enabled = true;
            Assert.Throws<ArgumentException>(() => grid.Render());
        }
    }
}
