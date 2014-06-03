// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System.Collections.Generic;

    using Moq;
    using Xunit;

    public class AccordionItemFactoryTests
    {
        private readonly Mock<IAccordionItemContainer> _container;
        private readonly IList<AccordionItem> _items;
        private readonly AccordionItemFactory _factory;

        public AccordionItemFactoryTests()
        {
            _items = new List<AccordionItem>();

            _container = new Mock<IAccordionItemContainer>();
            _container.SetupGet(container => container.Items).Returns(_items);

            _factory = new AccordionItemFactory(_container.Object);
        }

        [Fact]
        public void Add_should_return_new_instance()
        {
            Assert.NotNull(_factory.Add());
        }

        [Fact]
        public void Add_should_register_new_instance_in_container()
        {
            _factory.Add();

            Assert.NotEmpty(_items);
        }
    }
}