<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">Vertical</asp:Content>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

<%= Html.Telerik().Menu()
		.Name("myMenu")
		.Orientation(MenuOrientation.Vertical)
		.HtmlAttributes(new { style = "width: 150px" })
		.Items(menu =>
		{
			menu.Add().Text("Books")
				.Items(item =>
				{
					item.Add().Text("Books");
					item.Add().Text("Textbooks");
					item.Add().Text("Magazines");
				});
			
			menu.Add().Text("Movies, Music & Games")
				.Items(item =>
				{
					item.Add().Text("Movies & TV");
					item.Add().Text("Blu-ray");
					item.Add().Text("Video On Demand");
					item.Add().Text("Music");
					item.Add().Text("Video Games");
				});

			menu.Add().Text("Computers & Office")
				.Items(item =>
				{
					item.Add().Text("Computers & Accessories");
					item.Add().Text("Computer Components");
					item.Add().Text("Software");
					item.Add().Text("PC Games");
					item.Add().Text("Office Products & Supplies");
				});

			menu.Add().Text("Electronics")
				.Items(item =>
				{
					item.Add().Text("TV & Video");
					item.Add().Text("Home Audio & Theater");
					item.Add().Text("Camera & Photo");
					item.Add().Text("Video Games");
					item.Add().Text("Car Electronics & GPS");
				});
		})
%>
</asp:Content>
