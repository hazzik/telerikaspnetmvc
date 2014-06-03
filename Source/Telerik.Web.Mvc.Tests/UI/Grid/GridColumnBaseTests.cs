namespace Telerik.Web.Mvc.UI.Tests
{
    using Moq;
    using Moq.Protected;
    using Telerik.Web.Mvc.UI.Html;
    using Xunit;
    
    public class GridColumnBaseTests
    {
        [Fact]
        public void Should_create_hidden_column_adorner_when_hidden()
        {
            var grid = GridTestHelper.CreateGrid<Customer>();
            
            var column = new Mock<GridColumnBase<Customer>>(grid)
                {
                    CallBase = true
                };
            
            column.SetupGet(c => c.Hidden)
                  .Returns(true);

            var builder = new Mock<IHtmlBuilder>();

            builder.Setup(b => b.Adorners.Add(It.IsAny<GridHiddenColumnAdorner>()));
            
            var cell = new GridCell<Customer>(column.Object, new Customer());
            
            column.Protected()
                  .Setup<IHtmlBuilder>("CreateDisplayHtmlBuilderCore", cell)
                  .Returns(builder.Object);

            
            column.Object.CreateDisplayHtmlBuilder(cell);
            builder.VerifyAll();
        }

        [Fact]
        public void Should_create_class_adorner_for_last_column()
        {
            var grid = GridTestHelper.CreateGrid<Customer>();
            var column = new Mock<GridColumnBase<Customer>>(grid)
            {
                CallBase = true
            };

            grid.Columns.Add(column.Object);

            var builder = new Mock<IHtmlBuilder>();

            builder.Setup(b => b.Adorners.Add(It.IsAny<GridCssClassAdorner>()));

            var cell = new GridCell<Customer>(column.Object, new Customer());

            column.Protected()
                  .Setup<IHtmlBuilder>("CreateDisplayHtmlBuilderCore", cell)
                  .Returns(builder.Object);


            column.Object.CreateDisplayHtmlBuilder(cell);
            builder.VerifyAll();
        }
    }
}
