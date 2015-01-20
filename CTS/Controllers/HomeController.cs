using CTS.Context;
using CTS.Models;
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
            using (CTSContext context = new CTSContext())
            {
                var model = new CourierCompany()
                {
                    CourierName = "1",
                    CourierCode = "2",
                    ContactPhone = "3",
                    ContactName = "a",
                    ContactMobilePhone = "2",
                };
                context.CourierCompanys.Add(model);
                context.SaveChanges();
            }
            return View();
        }

    }
}
