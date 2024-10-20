using System.Collections.Generic;
using System.Web.Mvc;
using WOPHRMSystem.Context;
using WOPHRMSystem.Models;
using WOPHRMSystem.Services;

namespace WOPHRMSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //List<LaberUtilizationStatementWorkTypeAndGroup> data = new ReportServices().GetAll();
            return View();
        }

        public ActionResult About()
        {
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