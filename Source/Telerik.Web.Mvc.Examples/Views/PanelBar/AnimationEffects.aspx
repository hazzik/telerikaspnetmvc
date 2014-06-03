<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">Animation Effects</asp:Content>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

<% Html.Telerik().PanelBar()
		.Name("PanelBar")
        .HtmlAttributes(new { style = "width: 300px; float: left; margin-bottom: 30px;" })
		.Effects(fx =>
		{
			if ((bool)ViewData["enableHeightAnimation"])
			{
				fx.Expand(properties =>
						properties
                               .OpenDuration((int)ViewData["heightOpenDuration"])
                               .CloseDuration((int)ViewData["heightCloseDuration"]));
			}
			else
			{
				/* activate only toggle, so that the items show */
				fx.Toggle();
			}

			if ((bool)ViewData["enableOpacityAnimation"])
				fx.Opacity(properties =>
					properties
						.OpenDuration((int)ViewData["opacityOpenDuration"])
						.CloseDuration((int)ViewData["opacityCloseDuration"]));
		})
        .Items(panelBarItem =>
        {
            panelBarItem.Add().Text("Item 1")
                .Items(item =>
                {
                    item.Add().Text("Child Item 1.1");
                    item.Add().Text("Child Item 1.2");
                    item.Add().Text("Child Item 1.3");
                    item.Add().Text("Child Item 1.4");
                })
                .Expanded(true);
            
            panelBarItem.Add().Text("Item 2")
                .Items(item =>
                {
                    item.Add().Text("Child Item 2.1");
                    item.Add().Text("Child Item 2.2");
                    item.Add().Text("Child Item 2.3");
                    item.Add().Text("Child Item 2.4");
                });

			panelBarItem.Add().Text("Item 3")
				.Items(item =>
				{
					item.Add().Text("Child Item 3.1");
					item.Add().Text("Child Item 3.2");
					item.Add().Text("Child Item 3.3");
					item.Add().Text("Child Item 3.4");
				});
            
            panelBarItem.Add().Text("Item 4")
                .Items(item =>
                {
                    item.Add().Text("Child Item 4.1");
                    item.Add().Text("Child Item 4.2");
                    item.Add().Text("Child Item 4.3");
                    item.Add().Text("Child Item 4.4");
                });
        })
        .Render(); %>

<% using (Html.Configurator("Animate with...")
              .PostTo("AnimationEffects", "PanelBar")
              .Begin())
   { %>
    <ul>
        <li>
			<%= Html.CheckBox(
                    "enableHeightAnimation",
                    (bool)ViewData["enableHeightAnimation"],
                    "<strong>height animation</strong>, which will...")%>
            <ul>
                <li>
                    <label for="heightOpenDuration">open for</label>
                    <%= Html.TextBox("heightOpenDuration", ViewData["heightOpenDuration"])%> ms
                </li>
                <li>
                    <label for="heightCloseDuration">close for</label>
                    <%= Html.TextBox("heightCloseDuration", ViewData["heightCloseDuration"])%> ms
                </li>
            </ul>
        </li>
        <li>
			<%= Html.CheckBox(
                    "enableOpacityAnimation",
                    (bool)ViewData["enableOpacityAnimation"],
                    "<strong>opacity animation</strong>, which will...")%>
            <ul>
                <li>
                    <label for="opacityOpenDuration">open for</label>
                    <%= Html.TextBox("opacityOpenDuration", ViewData["opacityOpenDuration"])%> ms
                </li>
                <li>
                    <label for="opacityCloseDuration">close for</label>
                    <%= Html.TextBox("opacityCloseDuration", ViewData["opacityCloseDuration"])%> ms
                </li>
            </ul>
        </li>
    </ul>
    
    <button class="t-button t-state-default" type="submit">Apply Changes</button>
<% } %>

<% Html.Telerik().ScriptRegistrar().OnDocumentReady(() => {%>
	/* client-side validation */
    $('.configurator button').click(function(e) {
        $('.configurator :text').each(function () {
            if (!/^\d+$/.test(this.value)) {
                alert("TextBox `" + this.name + "` has an invalid param!");
                e.preventDefault();
            }
        });
    });
<%}); %>
	
</asp:Content>


<asp:Content contentPlaceHolderID="HeadContent" runat="server">
	<style type="text/css">
	    .example .configurator
	    {
	        width: 300px;
	        float: left;
	        margin: 0 0 0 10em;
	        display: inline;
	    }
	    
	    .configurator li
		{
		    padding: 3px 0;
		}
	    
		.configurator input[type=text]
		{
			width: 50px;
		}
		
		.configurator ul ul
		{
		    padding-left: 24px;
		    margin: 0;
		}
		
		.configurator ul ul label
		{
		    width: 48px;
		    margin: 0;
		}
	</style>
</asp:Content>