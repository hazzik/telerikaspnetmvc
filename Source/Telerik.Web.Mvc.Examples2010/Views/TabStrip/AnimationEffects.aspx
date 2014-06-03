<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content contentPlaceHolderID="ExampleTitle" runat="server">Animation Effects</asp:Content>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

<% Html.Telerik().TabStrip()
        .Name("TabStrip")
        .HtmlAttributes(new { style = "margin-top: 200px; height: 163px;" })
		.Effects(fx =>
		{
            if ((string)ViewData["enabledAnimation"] == "height")
            {
                fx.Expand(properties =>
                        properties
                               .OpenDuration((int)ViewData["heightOpenDuration"])
                               .CloseDuration((int)ViewData["heightCloseDuration"]));
            }
            else
            {
                fx.Toggle().Opacity(properties =>
                    properties
                        .OpenDuration((int)ViewData["opacityOpenDuration"])
                        .CloseDuration((int)ViewData["opacityCloseDuration"]));
            }
		})
        .Items(tabstrip =>
        {
            tabstrip.Add()
                    .Text("Based on jQuery")
                    .Content(() =>
                    {%>
                        <p>
                            The client-side code of the Telerik Extensions for ASP.NET MVC is based on
                            the open source and Microsoft-supported jQuery JavaScript library.
                            By using jQuery, the Telerik Extensions minimize their client-side footprint
                            and draw on the power of jQuery for advanced visual effects as well as for an easy
                            and reliable way to work with HTML elements.
                        </p>
                    <%})
                    .Selected(true);

            tabstrip.Add()
                    .Text("Cross-browser Support")
                    .Content(() =>
                    {%>
                        <p>
                            Telerik is renowned for making its product consistent across all major browsers:
                            Internet Explorer, Firefox, Safari, Opera and Google Chrome.
                            If an update or a new browser is coming,
                            the Telerik web components will be the first to support it!
                        </p>
                    <%});
            
            tabstrip.Add()
                    .Text("Exceptional Performance")
                    .Content(() =>
                    {%>
                        <p>
                            You can achieve unprecedented performance for your web application
                            with the lightweight, semantically rendered Extensions
                            that completely leverage the ASP.NET MVC model of no postbacks,
                            no ViewState, and no page life cycle.
                            Additional performance gains are delivered through the Extensions’
                            Web Asset Managers, which enable you to optimize the delivery of
                            CSS and JavaScript to your pages. You combine, cache, and compress
                            resources resulting in fewer requests that your page must make to load,
                            improving page load time performance.
                        </p>
                    <%});

			tabstrip.Add()
                    .Text("Completely Open-Source")
                    .Content(() =>
                    {%>
                        <p>
                            The Telerik Extensions for ASP.NET MVC are licensed under the widely
                            adopted GPLv2. In fact, the complete source for the Extensions is
                            available on CodePlex. For those that need dedicated support or need 
                            to use the Extensions in an environment where open source software is 
                            hard to get approved, Telerik provides a commercial license with support included.
                        </p>
                    <%});
        })
        .Render(); %>

<% using (Html.Configurator("Animate with...")
              .PostTo("AnimationEffects", "TabStrip")
              .Begin())
   { %>
    <ul>
        <li style="float: left; width: 200px;">
            <%= Html.RadioButton("enabledAnimation", "height", true, new { id = "height" })
            %><label for="height"><strong>height animation</strong>, which will...</label>
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
        <li style="float: left; width: 200px;">
            <%= Html.RadioButton("enabledAnimation", "opacity", new { id = "opacity" })
            %><label for="opacity"><strong>opacity animation</strong>, which will...</label>
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
	        width: 500px;
	        position: absolute;
	        top: 0;
	        right: 40px;
	    }
	    
	    .configurator li { padding: 3px 0; margin: 0; }
	    .configurator input[type=text] { width: 50px; }
		.configurator ul ul { padding-left: 24px; margin: 0; }
		.configurator ul ul label { width: 48px; margin: 0; }
	    .example .configurator .t-button { clear: both; margin: 0; }
	    
	    #TabStrip p
	    {
	        margin: 0;
	        padding: 1em 0;
	        height: 100px;
	    }
	</style>
</asp:Content>