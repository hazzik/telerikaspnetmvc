// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UnitTest
{
    using System.IO;

    using Infrastructure;

    using Moq;
    using Xunit;

    public class WebAssetLocatorTests
    {
        private readonly Mock<IPathResolver> _pathResolver;
        private readonly Mock<IFileSystem> _fileSystem;

        private WebAssetLocator _locator;

        public WebAssetLocatorTests()
        {
            WebAssetLocator.ClearCache();

            _pathResolver = new Mock<IPathResolver>();
            _fileSystem = new Mock<IFileSystem>();
        }

        [Fact]
        public void Locate_should_return_correct_path_in_debug_mode()
        {
            _locator = new WebAssetLocator(true, _pathResolver.Object, _fileSystem.Object);

            _pathResolver.Setup(resolver => resolver.Resolve(It.IsAny<string>())).Returns((string p) => p.Substring(1));
            _fileSystem.Setup(fs => fs.FileExists("/scripts/jquery-1.3.2.debug.js")).Returns(true);

            string path = _locator.Locate("~/scripts/jquery-1.3.2.js");

            Assert.Equal("~/scripts/jquery-1.3.2.debug.js", path);
        }

        [Fact]
        public void Locate_should_return_correct_path_in_release_mode()
        {
            _locator = new WebAssetLocator(false, _pathResolver.Object, _fileSystem.Object);

            _pathResolver.Setup(resolver => resolver.Resolve(It.IsAny<string>())).Returns((string p) => p.Substring(1));
            _fileSystem.Setup(fs => fs.FileExists("/content/site.min.css")).Returns(true);

            string path = _locator.Locate("~/content/site.css");

            Assert.Equal("~/content/site.min.css", path);
        }

        [Fact]
        public void Locate_should_return_same_path_when_file_does_not_exists()
        {
            _locator = new WebAssetLocator(false, _pathResolver.Object, _fileSystem.Object);

            _pathResolver.Setup(resolver => resolver.Resolve(It.IsAny<string>())).Returns((string p) => p.Substring(1));
            _fileSystem.Setup(fs => fs.FileExists("/content/site.css")).Returns(true);

            string path = _locator.Locate("~/content/site.css");

            Assert.Equal("~/content/site.css", path);
        }

        [Fact]
        public void Locate_should_throw_exception_when_file_does_not_exist()
        {
            _locator = new WebAssetLocator(false, _pathResolver.Object, _fileSystem.Object);

            _pathResolver.Setup(resolver => resolver.Resolve(It.IsAny<string>())).Returns((string p) => p.Substring(1));

            Assert.Throws<FileNotFoundException>(() => _locator.Locate("~/content/site.css"));
        }
    }
}