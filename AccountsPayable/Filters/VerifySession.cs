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

                if (filterContext.Controller is UserController == true)
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

            // Redirect admin to User administration view
            if (filterContext.Controller is MainController == true && ouser.FK_TB_LOGIN_ROLES_ID == 1)
            {
                filterContext.HttpContext.Response.Redirect("~/User/Index");
            }
            base.OnActionExecuting(filterContext);
        }
    }



}