using System.ComponentModel;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using Telerik.Web.Mvc.Extensions;
using System.Collections.Generic;

namespace Telerik.Web.Mvc.Examples.Models
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class OrdersAsmx : System.Web.Services.WebService
    {
        [WebMethod]
        public GridModel<Order> GetOrders(int page, int size, string orderBy, string filter)
        {
            NorthwindDataContext northwind = new NorthwindDataContext();

            return northwind.Orders.ToGridModel(page, size, orderBy, filter);
        }
    }
}
