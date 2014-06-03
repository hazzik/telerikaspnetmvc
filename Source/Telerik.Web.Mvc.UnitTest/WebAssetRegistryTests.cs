// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UnitTest
{
    using System.Collections.Generic;

    using Infrastructure;

    using Moq;
    using Xunit;

    public class WebAssetRegistryTests
    {
        private readonly Mock<ICacheManager> _cacheManager;
        private readonly Mock<IWebAssetLocator> _assetLocator;
        private readonly Mock<IPathResolver> _pathResolver;
        private readonly Mock<IFileSystem> _fileSystem;

        private readonly WebAssetRegistry _registry;

        public WebAssetRegistryTests()
        {
            _cacheManager = new Mock<ICacheManager>();
            _assetLocator = new Mock<IWebAssetLocator>();
            _pathResolver = new Mock<IPathResolver>();
            _fileSystem = new Mock<IFileSystem>();

            _registry = new WebAssetRegistry(_cacheManager.Object, _assetLocator.Object, _pathResolver.Object, _fileSystem.Object);
        }

        [Fact]
        public void Should_be_able_to_store_and_retrieve()
        {
            _cacheManager.Setup(manager => manager.GetItem(It.IsAny<string>())).Returns(null);
            _assetLocator.Setup(locator => locator.Locate(It.IsAny<string>())).Returns((string p) => p);
            _pathResolver.Setup(resolver => resolver.Resolve(It.IsAny<string>())).Returns((string p) => p.Substring(1));
            _fileSystem.Setup(fs => fs.ReadAllText(It.IsAny<string>())).Returns("function test{}");

            string id = _registry.Store("application/x-javascript", "1.0", true, 7, new List<string>{ "~/Scripts/file1.js", "~/Scripts/file2.js" });

            WebAsset asset = _registry.Retrieve(id);

            Assert.Equal("application/x-javascript", asset.ContentType);
            Assert.Equal("1.0", asset.Version);
            Assert.True(asset.Compress);
            Assert.Equal(7, asset.CacheDurationInDays);
            Assert.Contains("function test{}", asset.Content);
        }

        [Fact]
        public void Should_be_able_to_locate()
        {
            _assetLocator.Setup(locator => locator.Locate(It.IsAny<string>())).Returns((string p) => p).Verifiable();

            _registry.Locate("~/Content/site.css");

            _assetLocator.Verify();
        }
    }
}