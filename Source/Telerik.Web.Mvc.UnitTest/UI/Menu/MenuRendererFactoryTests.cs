// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.


namespace Telerik.Web.Mvc.UnitTest.Menu
{
    using System.IO;
    using System.Web.Mvc;
    using System.Web.UI;

    using Moq;
    using Xunit;

	using Telerik.Web.Mvc.UI;
	using Telerik.Web.Mvc.Infrastructure;

	public class MenuRendererFactoryTests
    {
        [Fact]
        public void Should_be_able_to_create_renderer()
        {
			MenuRendererFactory factory = new MenuRendererFactory(new Mock<IActionMethodCache>().Object);

			IMenuRenderer renderer = factory.Create(new Menu(new ViewContext(), new Mock<IClientSideObjectWriterFactory>().Object, new Mock<IUrlGenerator>().Object, new Mock<INavigationItemAuthorization>().Object, factory), new Mock<HtmlTextWriter>(new Mock<TextWriter>().Object).Object);

			Assert.IsType<MenuRenderer>(renderer);
        }
    }
}