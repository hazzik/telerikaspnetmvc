// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure;
    using Moq;
    using Xunit;

    public class WebAssetItemMergerTests
    {
        private readonly Mock<IWebAssetRegistry> _assetRegistry;
        private readonly Mock<IUrlResolver> _urlResolver;
        private readonly Mock<IUrlEncoder> _urlEncoder;

        private readonly WebAssetItemMerger _assetItemMerger;

        public WebAssetItemMergerTests()
        {
            _assetRegistry = new Mock<IWebAssetRegistry>();
            _urlResolver = new Mock<IUrlResolver>();
            _urlEncoder = new Mock<IUrlEncoder>();

            _assetItemMerger = new WebAssetItemMerger(_assetRegistry.Object, _urlResolver.Object, _urlEncoder.Object);
            _urlEncoder.Setup(s => s.Encode(It.IsAny<string>())).Returns((string u) => u);
            _assetRegistry.Setup(r => r.Locate(It.IsAny<string>(), It.IsAny<string>())).Returns((string p, string v) => p);
            _assetRegistry.Setup(r => r.Store(It.IsAny<string>(), It.IsAny<WebAssetItemGroup>())).Returns("123");

            _urlResolver.Setup(resolver => resolver.Resolve(It.IsAny<string>())).Returns((string p) => p.Replace("~", ""));
        }

        [Fact]
        public void Merge_should_return_url_of_standalone_files()
        {
            var result = Merge(items =>
            {
                items.Add(new WebAssetItem("~/Scripts/script.js"));
            });

            Assert.Equal(1, result.Count());
            Assert.Equal("/Scripts/script.js", result.First());
        }

        [Fact]
        public void Merge_should_return_the_urls_of_registerd_files_when_combining_is_disabled()
        {
            var result = Merge(items =>
            {
                var group = new WebAssetItemGroup("group", false);
                group.Items.Add(new WebAssetItem("~/Scripts/script1.js"));
                group.Items.Add(new WebAssetItem("~/Scripts/script2.js"));

                items.Add(group);
            });

            Assert.Equal(2, result.Count());
            Assert.Equal("/Scripts/script1.js", result.ElementAt(0));
            Assert.Equal("/Scripts/script2.js", result.ElementAt(1));
        }

        [Fact]
        public void Merge_should_return_content_delivery_url()
        {
            var result = Merge(items =>
            {
                var group = new WebAssetItemGroup("group", false);
                group.Items.Add(new WebAssetItem("~/Scripts/script1.js"));
                group.Items.Add(new WebAssetItem("~/Scripts/script2.js"));
                group.ContentDeliveryNetworkUrl = "http://www.example.com";
                
                items.Add(group);
            });

            Assert.Equal(1, result.Count());
            Assert.Equal("http://www.example.com", result.ElementAt(0));
        }
        
        [Fact]
        public void Merge_should_return_combined_url()
        {
            var result = Merge(items =>
            {
                var group = new WebAssetItemGroup("group", false);
                group.Items.Add(new WebAssetItem("~/Scripts/script1.js"));
                group.Items.Add(new WebAssetItem("~/Scripts/script2.js"));
                group.Combined = true;

                items.Add(group);
            });

            Assert.Equal(1, result.Count());
            Assert.Equal("/asset.axd?id=123", result.ElementAt(0));
        }

        [Fact]
        public void Merge_should_output_native_files_first_when_combining()
        {
            var result = Merge(items =>
            {
                var group = new WebAssetItemGroup("group", false);
                group.Items.Add(new WebAssetItem("~/Scripts/script1.js"));
                group.Items.Add(new WebAssetItem("~/Scripts/" + ScriptRegistrar.jQuery));
                group.Items.Add(new WebAssetItem("~/Scripts/script2.js"));
                group.DefaultPath = WebAssetDefaultSettings.ScriptFilesPath;
                group.Combined = true;
                group.UseTelerikContentDeliveryNetwork = true;
                items.Add(group);
            });

            Assert.Equal(2, result.Count());
            Assert.Contains(WebAssetDefaultSettings.TelerikContentDeliveryNetworkScriptUrl + 
                "/mvcz/" + WebAssetDefaultSettings.Version, result.ElementAt(0));
            Assert.Equal("/asset.axd?id=123", result.ElementAt(1));
        }

        [Fact]
        public void Merge_should_output_full_urls_when_combination_is_disabled()
        {
            var result = Merge(items =>
            {
                var group = new WebAssetItemGroup("group", false);
                group.Items.Add(new WebAssetItem("~/Scripts/script1.js"));
                group.Items.Add(new WebAssetItem("http://www.example.com"));
                
                items.Add(group);
            });

            Assert.Equal(2, result.Count());

            Assert.Equal("/Scripts/script1.js", result.ElementAt(0));
            Assert.Equal("http://www.example.com", result.ElementAt(1));
        }


        [Fact]
        public void Merge_should_output_full_urls_first_when_combination_is_enabled()
        {
            var result = Merge(items =>
            {
                var group = new WebAssetItemGroup("group", false);
                
                group.Items.Add(new WebAssetItem("http://www.example.com"));
                group.Items.Add(new WebAssetItem("~/Scripts/script1.js"));
                group.Combined = true;
                items.Add(group);
            });

            Assert.Equal(2, result.Count());

            Assert.Equal("http://www.example.com", result.ElementAt(0));
            Assert.Equal("/asset.axd?id=123", result.ElementAt(1));
        }

        private IEnumerable<string> Merge(Action<WebAssetItemCollection> configure)
        {
            WebAssetItemCollection assets = new WebAssetItemCollection(WebAssetDefaultSettings.ScriptFilesPath);
            configure(assets);

            return _assetItemMerger.Merge("application/x-javascript", WebAssetHttpHandler.DefaultPath, false, true, assets);
        }
    }
}