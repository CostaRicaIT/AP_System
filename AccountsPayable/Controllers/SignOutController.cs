using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountsPayable.Controllers
{
    public class SignOutController : Controller
    {
        // GET: LogOut
        public ActionResult EndSession()
        {
            Session["User"] = null;
            return RedirectToAction("Login", "Access");
        }
    }
}