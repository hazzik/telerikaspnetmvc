namespace Telerik.Web.Mvc.Examples
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;

    using Models;

	public partial class GridController : Controller
	{
        public ActionResult Editing(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(GetEditableCustomers());
            }

            CustomerDto customer = (from c in GetEditableCustomers()
                                    where c.CustomerID == id
                                    select c).FirstOrDefault();

            return View("Edit", customer);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(CustomerDto customer)
        {
            CustomerDto existingCustomer = (from c in GetEditableCustomers()
                                            where c.CustomerID == customer.CustomerID
                                            select c).FirstOrDefault();

            existingCustomer.Country = customer.Country;
            existingCustomer.Address = customer.Address;
            existingCustomer.ContactName = customer.ContactName;

            return RedirectToAction("Editing");
        }

		public ActionResult Delete(string id)
		{
			IList<CustomerDto> customers = GetEditableCustomers();
			CustomerDto customer = (from c in GetEditableCustomers()
									where c.CustomerID == id
									select c).FirstOrDefault();
			if (customer != null)
			{
				customers.Remove(customer);
			}

			return RedirectToAction("Editing");
		}
    }
}