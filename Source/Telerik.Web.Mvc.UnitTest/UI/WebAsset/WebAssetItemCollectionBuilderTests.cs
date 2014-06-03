// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System;

    using Xunit;

    public class WebAssetItemCollectionBuilderTests
    {
        private readonly WebAssetItemCollection _collection;
        private readonly WebAssetItemCollectionBuilder _builder;

        public WebAssetItemCollectionBuilderTests()
        {
            _collection = new WebAssetItemCollection(WebAssetDefaultSettings.ScriptFilesPath);
            _builder = new WebAssetItemCollectionBuilder(_collection);
        }

        [Fact]
        public void ToCollection_should_return_internal_collection()
        {
            Assert.Same(_collection, _builder.ToCollection());
        }

        [Fact]
        public void WebAssetItemCollection_operator_should_return_internal_collection()
        {
            WebAssetItemCollection collection = _builder;

            Assert.Same(_collection, collection);
        }

        [Fact]
        public void Should_be_able_to_add()
        {
            _builder.Add("~/Scripts/script.js");

            Assert.IsType<WebAssetItem>(_collection[0]);
        }

        [Fact]
        public void Should_be_able_to_add_group()
        {
            _builder.AddGroup("group", assets => assets.Compress(false));

            Assert.IsType<WebAssetItemGroup>(_collection[0]);
        }
        
        [Fact]
        public void Add_should_throw_exception_when_a_group_withsame_name_already_exists()
        {
            _builder.AddGroup("group", assets => { });

            Assert.Throws<ArgumentException>(() => _builder.AddGroup("group", assets => { }));
        }

        [Fact]
        public void Should_be_able_to_get_existing_group()
        {
            _builder.AddGroup("group", assets => { });
            _builder.GetGroup("group", assets => Assert.Equal("group", assets.ToGroup().Name));
        }

        [Fact]
        public void Get_should_throw_exception_when_specified_group_does_not_exsist()
        {
            Assert.Throws<ArgumentException>(() => _builder.GetGroup("group", assets => { }));
        }
    }
}