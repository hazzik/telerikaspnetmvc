namespace Northwind
{
    using System;
    using System.Web.Mvc;

    [HandleError]
    public class CustomerController : Controller
    {
        private readonly IRepository<Customers, string> repository;

        public CustomerController() : this(new Repository<Customers, string>())
        {
        }

        public CustomerController(IRepository<Customers, string> repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            return View(repository.All());
        }

        public ActionResult Details(string id)
        {
            return View(repository.Get(id));
        }

        public ActionResult Create()
        {
            return View();
        } 

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "CustomerId")]Customers customer)
        {
            customer.CustomerID = CreateNewId();
            repository.Add(customer);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            return View(repository.Get(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var customer = repository.Get(id);

            UpdateModel(customer, collection.ToValueProvider());

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            return View(repository.Get(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(string id, string confirm)
        {
            repository.Delete(id);

            return RedirectToAction("Index");
        }

        // Not a bullet proof method, but it should work for the demo
        private string CreateNewId()
        {
            Func<int, string> generateId = length =>
                                           {
                                               string generatingId = string.Empty;
                                               Random rnd = new Random();

                                               for (int i = 1; i <= length; i++)
                                               {
                                                   int characterCode = rnd.Next(65, 90); // Only uppercase;
                                                   generatingId += Convert.ToChar(characterCode).ToString();
                                               }

                                               return generatingId;
                                           };

            string id = generateId(5);

            while (repository.Get(id) != null)
            {
                id = generateId(5);
            }

            return id;
        }
    }
}