using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class RentalsController : Controller
    {
        // GET: Rentals
        public ActionResult New()
        {
            return View();
        }

        public ViewResult Index()
        {
            //Ya no necesita pasar el modelo a la vista, porque la vista obtiene la data desde la API .
            
            //Perfilar vista por rol
            if (User.IsInRole(RoleName.CanManageRentals))
                return View("List");

            return View("ReadOnlyList");
        }
    }
}