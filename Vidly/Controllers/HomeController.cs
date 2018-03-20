using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vidly.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));


        [OutputCache(Duration =50, Location = System.Web.UI.OutputCacheLocation.Server, VaryByParam ="genre")]
        //Permite guardar el HTML generado en Cache, cuando es HTML que no varia mucho. 
        //Si depende de todos los parámetros, usar varyByParam="*"
        //Para deshabilitar el cache: [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            logger.Info("Visita a About view");

            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}