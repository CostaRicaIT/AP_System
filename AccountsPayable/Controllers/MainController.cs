using AccountsPayable.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.WebPages;

namespace AccountsPayable.Controllers
{

  
    public class MainController : Controller
    {
        private Accounts_Payable_Entities db = new Accounts_Payable_Entities();
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }
    

    [HttpPost]
    public ActionResult CreateHistoric([Bind(Include = "HISTORIC_REMIT_DATE,HISTORIC_REMIT_INFO")] TB_HISTORIC_REMIT tB_HISTORIC_REMIT)
    {
        
         

            if (ModelState.IsValid)
            {
                
                db.TB_HISTORIC_REMIT.Add(tB_HISTORIC_REMIT);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "There was an error saving the record " });
    }
        [HttpGet]
        public ActionResult GetHistoryInfo()
        {
            var historicRemit = db.TB_HISTORIC_REMIT.ToList();

            if (historicRemit != null)
            {
                var selectItems = historicRemit.Select(item => new SelectListItem
                {
                    Text = item.HISTORIC_REMIT_DATE.ToString("MM/dd/yyyy"), // bring the Date
                    Value = item.HISTORIC_REMIT_ID.ToString() // Bring the ID
                }).ToList();

                return Json(new { success = true, selectItems }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Error in obtaining historical information." });
            }
        }

        [HttpGet]
        public ActionResult GetHistoricText(int historicId)
        {
            // Get the text based on the ID
            var historicText = db.TB_HISTORIC_REMIT
                .Where(item => item.HISTORIC_REMIT_ID == historicId)
                .Select(item => item.HISTORIC_REMIT_INFO)
                .FirstOrDefault();

            if (historicText != null)
            {
                return Json(new { success = true, historicText }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "." });
            }
        }

        public ActionResult CreateAlias([Bind(Include = "ALIAS_NAME")] TB_ALIAS tB_ALIAS)
        {

           

            if (ModelState.IsValid)
            {

                db.TB_ALIAS.Add(tB_ALIAS);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "There was an error saving the record " });
        }
        
        public ActionResult GetAliasInfo()
        {
            var AliasList = db.TB_ALIAS.ToList();

            if (AliasList != null)
            {
                var selectItems = AliasList.Select(item => new SelectListItem
                {
                    Text = item.ALIAS_NAME.ToString(), // bring the Alias name
                    Value = item.ALIAS_ID.ToString() // Bring the Alias ID
                }).ToList();

                return Json(new { success = true, selectItems }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Error in obtaining historical information." });
            }




        }

        public ActionResult GetAliasText(int AliasId)
        {
            // Get the text based on the ID
            var AliasText = db.TB_ALIAS
                .Where(item => item.ALIAS_ID == AliasId)
                .Select(item => item.ALIAS_NAME)
                .FirstOrDefault();

            if (AliasText != null)
            {
                return Json(new { success = true, AliasText }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Alias name not found." });
            }
        }

        public ActionResult GetHistoricRecentData()
        {
            var mostRecentData = db.TB_HISTORIC_REMIT.OrderByDescending(h => h.HISTORIC_REMIT_DATE).FirstOrDefault();

            if(mostRecentData != null){

                string mostRecentInfo = mostRecentData.HISTORIC_REMIT_INFO;

                return Json(new { success = true, mostRecentData = mostRecentInfo }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = false, Message = "An error ocurred while fetching recent data" }, JsonRequestBehavior.AllowGet);

            }
        }

    }

}