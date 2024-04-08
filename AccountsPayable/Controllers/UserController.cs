using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AccountsPayable.Models;

namespace AccountsPayable.Controllers
{
    public class UserController : Controller
    {
        private Accounts_Payable_Entities db = new Accounts_Payable_Entities();

        // GET: User
        public ActionResult Index()
        {
            var userPermission = Session["Permission"] as TB_VIEW_PERMISSIONS;
            if (userPermission == null || userPermission.FK_TB_LOGIN_ROLE_ID != 1)
            {

                return View("Error");
            }
            var login = db.TB_LOG_IN.Include(L => L.TB_LOGIN_ROLES);
            ViewBag.Roles = db.TB_LOGIN_ROLES.ToList();
            var loginview = from l in db.TB_LOG_IN
                            where l.LOG_IN_ISDISABLED != 1
                            orderby l.LOG_IN_ID
                            select l;
            return View(loginview);
        }


       
        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "LOG_IN_ID,FK_TB_LOGIN_ROLES_ID,LOG_IN_PASSWORD,LOG_IN_FULL_NAME,LOG_IN_USER_NAME")] TB_LOG_IN tB_LOG_IN)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Hash the password using SHA256 algorithm
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(tB_LOG_IN.LOG_IN_PASSWORD);
                    byte[] hashedBytes = new SHA256Managed().ComputeHash(passwordBytes);
                    string hashedPassword = Convert.ToBase64String(hashedBytes);
                    tB_LOG_IN.LOG_IN_PASSWORD = hashedPassword;

                    db.TB_LOG_IN.Add(tB_LOG_IN);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "There was an error saving the record " });
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
           .SelectMany(x => x.ValidationErrors)
           .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                return Json(new { success = false, message = exceptionMessage });
            }
        }
        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(int LOG_IN_ID, int FK_TB_LOGIN_ROLES_ID, string LOG_IN_PASSWORD, string LOG_IN_FULL_NAME, string LOG_IN_USER_NAME)
        {
            if (ModelState.IsValid)
            {
                var tB_LOG_IN = db.TB_LOG_IN.Find(LOG_IN_ID);
                tB_LOG_IN.LOG_IN_ID = LOG_IN_ID;
                tB_LOG_IN.FK_TB_LOGIN_ROLES_ID = FK_TB_LOGIN_ROLES_ID;
                tB_LOG_IN.LOG_IN_FULL_NAME = LOG_IN_FULL_NAME;
                tB_LOG_IN.LOG_IN_USER_NAME = LOG_IN_USER_NAME;

                if (!string.IsNullOrEmpty(LOG_IN_PASSWORD))
                {
                    // Encrypt new password
                    var sha256 = new SHA256Managed();
                    var bytes = Encoding.UTF8.GetBytes(LOG_IN_PASSWORD);
                    var hash = sha256.ComputeHash(bytes);
                    var hashedPassword = Convert.ToBase64String(hash);
                    tB_LOG_IN.LOG_IN_PASSWORD = hashedPassword;
                }

                db.SaveChanges();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id, int log = 1)
        {
            var login = db.TB_LOG_IN.Find(id);
            if (login.LOG_IN_ISDISABLED != 1)
            {

                login.LOG_IN_ISDISABLED = log;

                db.SaveChanges();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }

        }
    }
}
