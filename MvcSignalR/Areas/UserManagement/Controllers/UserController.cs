using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSignalR.Areas.UserManagement.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /UserManagement/User/

        public ActionResult Index()
        {
            return View();
        }

    }
}
