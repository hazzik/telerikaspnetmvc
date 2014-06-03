namespace Telerik.Web.Mvc.Examples.Models
{
	using System;

    public class OrderDto
    {
        public int OrderID
        {
            get;
            set;
        }

        public string ContactName
        {
            get;
            set;
        }

        public string ShipAddress
        {
            get;
            set;
        }

        public DateTime? OrderDate
        {
            get;
            set;
        }
    }
}
