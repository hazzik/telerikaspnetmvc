// (c) Copyright Telerik Corp. 
// This source is subject to the Microsoft Public License. 
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
// All other rights reserved.

namespace Mvc.UI.jQuery.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.UI;

    using Moq;
    using Xunit;

    public class TabTests
    {
        private readonly ViewContext _viewContext;
        private readonly Mock<HttpContextBase> _httpContext;
        private readonly Mock<IClientSideObjectWriterFactory> _clientSideObjectWriterFactory;

        private readonly Tab _tab;

        public TabTests()
        {
            _httpContext = TestHelper.CreateMockedHttpContext();
            _viewContext = new ViewContext { HttpContext = _httpContext.Object, ViewData = new ViewDataDictionary() };

            _clientSideObjectWriterFactory = new Mock<IClientSideObjectWriterFactory>();

            _tab = new Tab(_viewContext, _clientSideObjectWriterFactory.Object) { AssetKey = jQueryViewComponentFactory.DefaultAssetKey };
        }

        [Fact]
        public void Items_should_be_empty_when_new_instance_is_created()
        {
            Assert.Empty(_tab.Items);
        }

        [Fact]
        public void AnimationOpacity_should_throw_exception_when_open_animation_is_already_set()
        {
            _tab.OpenAnimationOpacity = "fadeOut";

            Assert.Throws<InvalidOperationException>(() => _tab.AnimationOpacity = "toggle");
        }

        [Fact]
        public void AnimationOpacity_should_throw_exception_when_close_animation_is_already_set()
        {
            _tab.CloseAnimationOpacity = "fadeIn";

            Assert.Throws<InvalidOperationException>(() => _tab.AnimationOpacity = "toggle");
        }

        [Fact]
        public void AnimationDuration_should_throw_exception_when_open_animation_is_already_set()
        {
            _tab.OpenAnimationOpacity = "fadeOut";

            Assert.Throws<InvalidOperationException>(() => _tab.AnimationDuration = 300);
        }

        [Fact]
        public void AnimationDuration_should_throw_exception_when_close_animation_is_already_set()
        {
            _tab.CloseAnimationOpacity = "fadeIn";

            Assert.Throws<InvalidOperationException>(() => _tab.AnimationDuration = 300);
        }

        [Fact]
        public void OpenAnimationOpacity_should_throw_exception_when_animation_is_already_set()
        {
            _tab.AnimationOpacity = "toggle";

            Assert.Throws<InvalidOperationException>(() => _tab.OpenAnimationOpacity = "fadeOut");
        }

        [Fact]
        public void OpenAnimationDuration_should_throw_exception_when_animation_is_already_set()
        {
            _tab.AnimationOpacity = "toggle";

            Assert.Throws<InvalidOperationException>(() => _tab.OpenAnimationDuration = 300);
        }

        [Fact]
        public void CloseAnimationOpacity_should_throw_exception_when_animation_is_already_set()
        {
            _tab.AnimationOpacity = "toggle";

            Assert.Throws<InvalidOperationException>(() => _tab.CloseAnimationOpacity = "fadeIn");
        }

        [Fact]
        public void CloseAnimationDuration_should_throw_exception_when_animation_is_already_set()
        {
            _tab.AnimationOpacity = "toggle";

            Assert.Throws<InvalidOperationException>(() => _tab.CloseAnimationDuration = 300);
        }

        [Fact]
        public void BuildAnimationSettings_should_return_null_when_animation_is_not_set()
        {
            Assert.Null(_tab.BuildAnimationSettings());
        }

        [Fact]
        public void BuildAnimationSettings_should_return_correct_settings_for_both_animation()
        {
            _tab.OpenAnimationOpacity = "fadeOut";
            _tab.OpenAnimationDuration = (int)AnimationDuration.Slow;

            _tab.CloseAnimationOpacity = "fadeIn";
            _tab.CloseAnimationDuration = (int)AnimationDuration.Fast;

            string settings = _tab.BuildAnimationSettings();

            Assert.Equal("[{opacity:'fadeOut', duration:'slow'}, {opacity:'fadeIn', duration:'fast'}]", settings);
        }

        [Fact]
        public void BuildAnimationSettings_should_return_correct_settings_for_open_animation()
        {
            _tab.OpenAnimationOpacity = "fadeOut";
            _tab.OpenAnimationDuration = (int) AnimationDuration.Fast;

            string settings = _tab.BuildAnimationSettings();

            Assert.Equal("[{opacity:'fadeOut', duration:'fast'}, null]", settings);
        }

        [Fact]
        public void BuildAnimationSettings_should_return_correct_settings_for_close_animation()
        {
            _tab.CloseAnimationOpacity = "fadeIn";
            _tab.CloseAnimationDuration = (int)AnimationDuration.Fast;

            string settings = _tab.BuildAnimationSettings();

            Assert.Equal("[null, {opacity:'fadeIn', duration:'fast'}]", settings);
        }

        [Fact]
        public void WriteInitializationScript_should_write_scripts()
        {
            _tab.Name = "myTab";
            _tab.Items.Add(new TabItem { Text = "Tab1", LoadContentFromUrl = "/content/1" });
            _tab.Items.Add(new TabItem { Text = "Tab2", LoadContentFromUrl = "/content/2", Disabled = true });
            _tab.Items.Add(new TabItem { Text = "Tab3", LoadContentFromUrl = "/content/3", Selected = true });
            _tab.OpenOn = "mouseover";
            _tab.AnimationDuration = (int) AnimationDuration.Slow;
            _tab.AnimationOpacity = "toggle";
            _tab.CollapsibleContent = true;
            _tab.CacheAjaxResponse = true;
            _tab.SpinnerText = "Please wait..";
            _tab.RotationDurationInMilliseconds = 1000;
            _tab.RotationContinue = true;
            _tab.OnSelect = delegate { };
            _tab.OnShow = delegate { };
            _tab.OnAdd = delegate { };
            _tab.OnRemove = delegate { };
            _tab.OnEnable = delegate { };
            _tab.OnDisable = delegate { };
            _tab.OnLoad = delegate { };

            Mock<IClientSideObjectWriter> writer = new Mock<IClientSideObjectWriter>();

            writer.Setup(w => w.Start()).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<string>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<Action>())).Returns(writer.Object);
            writer.Setup(w => w.Append(It.IsAny<string>(), It.IsAny<IList<int>>())).Returns(writer.Object);
            writer.Setup(w => w.Complete());

            _clientSideObjectWriterFactory.Setup(f => f.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TextWriter>())).Returns(writer.Object);

            Mock<TextWriter> textWriter = new Mock<TextWriter>();
            textWriter.Setup(w => w.WriteLine(It.IsAny<string>()));

            _tab.WriteInitializationScript(textWriter.Object);

            writer.VerifyAll();
            textWriter.VerifyAll();
        }

        [Fact]
        public void Render_should_write_in_response()
        {
            _tab.Name = "myTab";
            _tab.Theme = "custom";
            _tab.HtmlAttributes.Add("style", "border:#000 1px solid");
            _tab.HtmlAttributes.Add("class", "myClass");
            _tab.Items.Add(new TabItem { Text = "Tab1", Content = delegate { } });
            _tab.Items.Add(new TabItem { Text = "Tab2", Content = delegate { }, Selected = true });
            _tab.Items.Add(new TabItem { Text = "Tab3", Content = delegate { }, Disabled = true });

            _tab.Items[0].HtmlAttributes.Add("class", "myClass");
            _tab.Items[1].HtmlAttributes.Add("style", "text-align:left");

            _tab.Items[0].ContentHtmlAttributes.Add("class", "myContent");
            _tab.Items[1].ContentHtmlAttributes.Add("style", "whitepsace:nowrap");

            _httpContext.Setup(context => context.Response.Output.Write(It.IsAny<string>())).Verifiable();

            _tab.Render();

            _httpContext.Verify();
        }
    }
}