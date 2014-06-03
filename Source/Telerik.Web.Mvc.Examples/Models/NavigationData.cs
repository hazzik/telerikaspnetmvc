using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telerik.Web.Mvc.Examples.Models
{
	public class NavigationData
	{
		public string Text { get; set; }
		public string ImageUrl { get; set; }
		public string NavigateUrl { get; set; }
	}

    public static class NavigationDataBuilder
    {
        public static List<NavigationData> GetCollection() 
        {
            return new List<NavigationData> {
                new NavigationData {
					Text="ASP.NET MVC", 
					ImageUrl="~/Content/Images/Icons/Suites/mvc.png",
					NavigateUrl="http://www.telerik.com/products/aspnet-mvc.aspx"
				},
                new NavigationData {
					Text = "Silverlight", 
                    ImageUrl = "~/Content/Images/Icons/Suites/sl.png",
                    NavigateUrl = "http://www.telerik.com/products/silverlight.aspx"
				},
                new NavigationData {
					Text = "ASP.NET AJAX", 
                    ImageUrl = "~/Content/Images/Icons/Suites/ajax.png",
                    NavigateUrl = "http://www.telerik.com/products/aspnet-ajax.aspx"
				},
                new NavigationData {
					Text = "OpenAccess ORM",
                    ImageUrl = "~/Content/Images/Icons/Suites/orm.png",
                    NavigateUrl = "http://www.telerik.com/products/orm.aspx"
				},
                new NavigationData {
					Text = "Reporting", 
                    ImageUrl = "~/Content/Images/Icons/Suites/rep.png",
                    NavigateUrl = "http://www.telerik.com/products/reporting.aspx"
				},
                new NavigationData {
					Text = "Sitefinity ASP.NET CMS", 
                    ImageUrl = "~/Content/Images/Icons/Suites/sitefinity.png",
                    NavigateUrl = "http://www.telerik.com/products/sitefinity.aspx"
				},
                new NavigationData {
					Text = "Web Testing Tools", 
                    ImageUrl = "~/Content/Images/Icons/Suites/test.png",
                    NavigateUrl = "http://www.telerik.com/products/web-testing-tools.aspx"
				}
            };
        }
    }
}
