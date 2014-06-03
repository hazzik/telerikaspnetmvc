namespace Telerik.Web.Mvc.Examples
{
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;
    using System.Web.Mvc;
    
    using Models;

    [AutoPopulateSourceCode]
    [PopulateSiteMap(SiteMapName = "examples", ViewDataKey = "telerik.mvc.examples")]
    public partial class GridController : Controller
    {

        private IList<CustomerDto> GetEditableCustomers()
        {
            IList<CustomerDto> customers = (IList<CustomerDto>)Session["Customers"];
            
            if (customers == null)
            {
                Session["Customers"] = customers = (from c in GetCustomers()
                select new CustomerDto
                {
                    ContactName = c.ContactName,
                    Address = c.Address,
                    Country = c.Country,
                    CustomerID = c.CustomerID
                }).ToList();
            }

            return customers;
		}

        private static IEnumerable<Order> GetOrders()
        {
            NorthwindDataContext northwind = new NorthwindDataContext();

            DataLoadOptions loadOptions = new DataLoadOptions();

            loadOptions.LoadWith<Order>(o => o.Customer);
            northwind.LoadOptions = loadOptions;

            return northwind.Orders;
        }

        private static IEnumerable<Order> GetOrdersForCustomer(string customerId)
        {
            NorthwindDataContext northwind = new NorthwindDataContext();
            
            return from order in northwind.Orders 
                   where order.CustomerID == customerId 
                   select order;
        }

        private static IQueryable<Customer> GetCustomers()
        {
            NorthwindDataContext northwind = new NorthwindDataContext();
            return northwind.Customers;
        }
    }
}