using CTS.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CTS.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            using (CTSContext context = new Context.CTSContext())
            {
                context.CourierCompanys.ToList();
            }
            return View();
        }

    }
}
