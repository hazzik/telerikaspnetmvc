<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% 
	Html.Telerik().PanelBar()
		.Name("demos-navigation")
		.BindTo("telerik.mvc.examples")
		.HighlightPath(true)
		.ItemAction(item =>
		{
			if (!string.IsNullOrEmpty(Request.QueryString["theme"]))
				item.RouteValues.Add("theme", Request.QueryString["theme"]);
			
			if (item.Selected)
                item.HtmlAttributes["class"] = "active-page";
            
            if (item.Parent == null)
                item.SpriteCssClasses = "t" + item.Text;
		})
        .ExpandMode(PanelBarExpandMode.Single)
		.Render();
%>