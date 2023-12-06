using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AccountsPayable.Models;

namespace AccountsPayable.Controllers
{
    public class AccessController : Controller
    {
        //In case of merge from DevEnv you must change the entity to AccountsPayableTestProdEntities //
        private AccountsPayableTestProdEntities db = new AccountsPayableTestProdEntities();

        [HttpGet]
        public ActionResult LogIn()
        {

            return View();
        }

        [HttpPost]
        public ActionResult LoginAuthorize(string username, string password)
        {
            using (AccountsPayableTestProdEntities db = new AccountsPayableTestProdEntities())
            {
                // Hash the password using SHA256 algorithm
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = new SHA256Managed().ComputeHash(passwordBytes);
                string hashedPassword = Convert.ToBase64String(hashedBytes);

                // Verify the input matches with the DB
                var user = (from d in db.TB_LOG_IN
                            where d.LOG_IN_USER_NAME == username.Trim()
                            && d.LOG_IN_PASSWORD == hashedPassword
                            && d.LOG_IN_ISDISABLED == 0 // Check if log in is active
                            select d).FirstOrDefault();


                if (user != null)
                {
                    // Check the user's role
                    var userRole = (from r in db.TB_LOGIN_ROLES
                                    where r.LOGIN_ROLE_ID == user.FK_TB_LOGIN_ROLES_ID
                                    select r).FirstOrDefault();

                    if (userRole != null)
                    {
                        // Set the user's permission based on their role
                        var userPermission = (from p in db.TB_VIEW_PERMISSIONS
                                              where p.FK_TB_LOGIN_ROLE_ID == userRole.LOGIN_ROLE_ID
                                              select p).FirstOrDefault();

                        if (userPermission != null)
                        {
                            Session["User"] = user;
                            Session["CurrentUserName"] = username;

                            //Set the user's permission in the session
                            Session["Permission"] = userPermission;
                            return Content("1");
                        }
                    }
                }

                return Content("Incorrect username or password, please try again");
            }

        }
    }
}
