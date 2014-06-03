<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.Telerik().Menu()
	.Name("product-lines")
	.Theme("demos")
	.Items(menu =>
	{
		menu.Add()
			.Text("RadControls for ASP.NET AJAX")
            .Url("http://demos.telerik.com/aspnet-ajax")
			.HtmlAttributes(new { id = "productAjaxSuite" });
		
		menu.Add()
			.Text("RadControls for WinForms")
			.Url("http://www.telerik.com/products/winforms.aspx")
			.HtmlAttributes(new { id = "productWfSuite" });
		
		menu.Add()
			.Text("RadControls for WPF")
			.Url("http://demos.telerik.com/wpf")
			.HtmlAttributes(new { id = "productWpfSuite" });
		
		menu.Add()
			.Text("RadControls for Silverlight")
			.Url("http://demos.telerik.com/silverlight")
			.HtmlAttributes(new { id = "productSlSuite" });
		
		menu.Add()
			.Text("Telerik Reporting")
			.Url("http://demos.telerik.com/reporting")
			.HtmlAttributes(new { id = "productReporting" });
		
		menu.Add()
			.Text("Telerik OpenAccess ORM")
			.Url("http://demos.telerik.com/orm")
			.HtmlAttributes(new { id = "productORM" });
	}).Render(); %>