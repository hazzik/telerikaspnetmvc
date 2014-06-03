<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<NavigationData>>" %>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">Binding to Model</asp:Content>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

    <% Html.Telerik().PanelBar()
           .Name("PanelBar")
           .HtmlAttributes(new { style = "width: 300px;" })
           .BindTo(Model,
                   (item, navigationData) =>
                   {
                       item.Text = navigationData.Text;
                       item.ImageUrl = navigationData.ImageUrl;
                       item.Url = navigationData.NavigateUrl;
                   })
           .ClientEvents(events =>
					events
                        .OnSelect(() =>
                        {%>
							function(e) {
							    /* 
							       do not navigate
							       URLs are set only for the sake of the example
							    */
							    e.preventDefault();
							}
						<%})
				)
           .Render();
    %>

</asp:Content>
