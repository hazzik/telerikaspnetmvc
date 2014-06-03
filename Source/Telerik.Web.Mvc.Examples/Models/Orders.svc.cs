using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Telerik.Web.Mvc.Extensions;

namespace Telerik.Web.Mvc.Examples.Models
{
    [ServiceContract(Namespace = "")]
    [ServiceBehavior(IncludeExceptionDetailInFaults=true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class OrdersWcf
    {
        [OperationContract]
        public GridModel<OrderDto> GetOrders(int page, int size, string orderBy, string filter)
        {
            NorthwindDataContext northwind = new NorthwindDataContext();
            IQueryable<OrderDto> orders = from o in northwind.Orders
                                          select new OrderDto
                                          {
                                              OrderID = o.OrderID,
                                              ContactName = o.Customer.ContactName,
                                              ShipAddress = o.ShipAddress,
                                              OrderDate = o.OrderDate
                                          };
            
            return orders.ToGridModel(page, size, orderBy, filter);
        }
    }
}
