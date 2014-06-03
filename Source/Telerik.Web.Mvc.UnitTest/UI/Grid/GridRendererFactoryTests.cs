// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.


namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System.IO;
    using System.Web.Mvc;
    using System.Web.UI;

    using Moq;
    using Xunit;

    public class GridRendererFactoryTests
    {
        [Fact]
        public void Should_be_able_to_create_renderer()
        {
            GridRendererFactory factory = new GridRendererFactory();

            IGridRenderer<Customer> renderer = factory.Create(new Grid<Customer>(new ViewContext { TempData = new TempDataDictionary() }, new Mock<IClientSideObjectWriterFactory>().Object, new Mock<IUrlGenerator>().Object, factory), new Mock<HtmlTextWriter>(new Mock<TextWriter>().Object).Object);

            Assert.IsType<GridRenderer<Customer>>(renderer);
        }
    }
}