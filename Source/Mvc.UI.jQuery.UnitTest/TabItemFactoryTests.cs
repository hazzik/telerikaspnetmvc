// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System.Collections.Generic;

    using Moq;
    using Xunit;

    public class TabItemFactoryTests
    {
        private readonly Mock<ITabItemContainer> _container;
        private readonly IList<Mvc.UI.jQuery.TabItem> _items;
        private readonly Mvc.UI.jQuery.TabItemFactory _factory;

        public TabItemFactoryTests()
        {
            _items = new List<Mvc.UI.jQuery.TabItem>();

            _container = new Mock<ITabItemContainer>();
            _container.SetupGet(container => container.Items).Returns(_items);

            _factory = new Mvc.UI.jQuery.TabItemFactory(_container.Object);
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