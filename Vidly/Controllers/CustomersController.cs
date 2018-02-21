using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;
using System.Net;


namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        public ViewResult Index()
        {
            //solo se ejecuta cuando se llama a ToList, lo cual es recomendable hacerlo acá y no en la vista
            // https://stackoverflow.com/questions/17516911/applying-a-tolist-on-the-controller-versus-retrieving-the-data-from-the-view)

            //var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            //return View(customers);

            //Usando el plugin Datatable de Jquery, la acción ya no necesita enviar la lista de customer, pq se obtendrá desde la API.            
                        
            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

       
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _context.Dispose();
        }

        public ActionResult New()
        {
            //Para el dropdown
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                //inicializar para que tenga Id = 0
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }
        
        
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
                                    
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                //var message = string.Join(" | ", ModelState.Values
                //    .SelectMany(v => v.Errors)
                //    .Select(e => e.ErrorMessage));
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);


                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                //approach de Microsoft
                //TryUpdateModel(customerInDb); 

                //approach básico de Mosh
                customerInDb.Name = customer.Name;
                customerInDb.Birthday = customer.Birthday;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

                //approach avanzado de Mosh: usar automapper y en vez de parámetro Customer, pasar CustomerDTO, con solo los campos actualizables.
                //Ver ejemplo en el API Controller

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

    }
}