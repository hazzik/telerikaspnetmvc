namespace Telerik.Web.Mvc.UnitTest.Menu
{
	using System;
	using System.IO;
	using System.Web.UI;
	using System.Collections.Generic;

	using Xunit;
	using Moq;

	using Telerik.Web.Mvc.UI;

	public class MenuSerializationTests
	{
		private readonly Menu menu;
		private Mock<TextWriter> textWriter;
		private string output;

		public MenuSerializationTests()
		{
			Mock<HtmlTextWriter> htmlWriter = new Mock<HtmlTextWriter>(TextWriter.Null);

			textWriter = new Mock<TextWriter>();
			textWriter.Setup(tw => tw.Write(It.IsAny<string>())).Callback<string>(s => output += s);

			menu = MenuTestHelper.CreteMenu(htmlWriter.Object, null);
			menu.Name = "myMenu";
		}

		[Fact]
		public void Default_configuration_outputs_only_tMenu_init()
		{
			menu.WriteInitializationScript(textWriter.Object);

			Assert.Equal("jQuery('#myMenu').tMenu();", output);
		}

		[Fact]
		public void Vertical_menu_should_serialize_orientation()
		{
			menu.Orientation = MenuOrientation.Vertical;

			menu.WriteInitializationScript(textWriter.Object);

			Assert.Equal("jQuery('#myMenu').tMenu({orientation:'vertical'});", output);
		}

		[Fact]
		public void Menu_with_non_default_effects_should_serialize_them()
		{
            menu.Effects.Clear();
		    menu.Effects.Add(
		        new PropertyAnimation(PropertyAnimationType.Opacity)
		            {
		                OpenDuration = 100,
		                CloseDuration = 200
		            });

			menu.WriteInitializationScript(textWriter.Object);

			Assert.Equal("jQuery('#myMenu').tMenu({effects:[{name:'property',properties:['opacity'],openDuration:100,closeDuration:200}]});", output);
		}

		[Fact]
		public void Menu_serializes_multiple_effects_correctly()
		{
            menu.Effects.Clear();
		    menu.Effects.Add(new SlideAnimation { OpenDuration = 100, CloseDuration = 200 });
		    menu.Effects.Add(new PropertyAnimation(PropertyAnimationType.Opacity) { OpenDuration = 100, CloseDuration = 200 });
            menu.Effects.Add(new ToggleEffect());

			menu.WriteInitializationScript(textWriter.Object);

			Assert.Equal("jQuery('#myMenu').tMenu(" + 
							"{effects:[" +
								"{name:'slide',openDuration:100,closeDuration:200}," +
								"{name:'property',properties:['opacity'],openDuration:100,closeDuration:200}," +
								"{name:'toggle'}" +
							"]});", output);
		}
	}
}
