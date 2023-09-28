using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountsPayable.Controllers;
using AccountsPayable.Models;



namespace AccountsPayable.Filters
{
    public class VerifySession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var ouser = (TB_LOG_IN)HttpContext.Current.Session["User"];
            //Avoid redirection if the user is not signed in
            if (ouser == null)
            {

                if (filterContext.Controller is MainController == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/Access/LogIn");
                }

                

            }
            else
            {
                // if user is signed in avoid redirection to login controller
                if (filterContext.Controller is AccessController == true)
                {

                    filterContext.HttpContext.Response.Redirect("~/Main/Index");
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }



}