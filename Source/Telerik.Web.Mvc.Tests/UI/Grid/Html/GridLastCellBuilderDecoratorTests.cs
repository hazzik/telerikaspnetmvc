// (c) Copyright 2002-2009 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Html.Tests
{
    using Infrastructure;
    using Xunit;

    public class GridLastCellBuilderDecoratorTests
    {
        [Fact]
        public void Should_add_css_class_to_given_cell()
        {
            const string expectedCssClass = "foo";
            var cellToDecorate = new HtmlElement("bar");

            var decorator = new GridLastCellBuilderDecorator(expectedCssClass);
            decorator.Decorate(cellToDecorate);

            cellToDecorate.Attribute("class").ShouldEqual(expectedCssClass);
        }
    }
}