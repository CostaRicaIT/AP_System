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
            var historicRemit = db.TB_HISTORIC_REMIT.OrderByDescending(item => item.HISTORIC_REMIT_DATE).ToList(); /*Bring the historic creations info in descending mode */

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
        public ActionResult GetHighlightsInfo()
        {
            var HighlightsHistoric = db.TB_HIGLIGHTS.OrderByDescending(item => item.HIGHLIGTS_DATE).ToList(); /*Bring the historic creations info in descending mode */

            if (HighlightsHistoric != null)
            {
                var selectItems = HighlightsHistoric.Select(item => new SelectListItem
                {
                    Text = item.HIGHLIGTS_DATE.ToString("MM/dd/yyyy"), // bring the Date
                    Value = item.HIGLIGHTS_ID.ToString() // Bring the ID
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
        public ActionResult GetHighLightsText(int HighlightsID)
        {
            // Get the text based on the ID
            var HighLightsText = db.TB_HIGLIGHTS.FirstOrDefault(h => h.HIGLIGHTS_ID == HighlightsID);

            if (HighLightsText != null)
            {
                return Json(new { success = true, HighLightsText.HIGLIGTHS, HighLightsText.HIGLIGTHS_COMMENTS, HighLightsText.HIGLIGTHS_INSTRUCTIONS, HighLightsText.HIGLIGTHS_EXCEPTIONS, HighLightsText.HIGLIGTHS_COMMON_ISSUES, HighLightsText.HIGLIGTHS_SUPPLIER_AGENCY, HighLightsText.HIGLIGTHS_TEMPLATE_COMMENTS }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Alias name not found." });
            }
        }

        public ActionResult GetHistoricRecentData()
        {
            var mostRecentData = db.TB_HISTORIC_REMIT.OrderByDescending(h => h.HISTORIC_REMIT_DATE).FirstOrDefault();

            if (mostRecentData != null)
            {

                string mostRecentInfo = mostRecentData.HISTORIC_REMIT_INFO;

                return Json(new { success = true, mostRecentData = mostRecentInfo }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = false, Message = "An error ocurred while fetching recent data" }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult GetOracleLegalEntity()
        {
            var OracleLegalEntityList = db.TB_ORACLE_LEGAL_ENTITIES.ToList();
            if (OracleLegalEntityList != null)
            {
                var selectItems = OracleLegalEntityList.Select(item => new SelectListItem
                {
                    Value = item.LEGAL_ENTITY_ID.ToString(),
                    Text = item.LEGAL_ENTITY_NAME
                }).ToList();

                return Json(selectItems, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Message = "Error obtaining Oracle Legal entities" });
            }
        }
        public ActionResult GetOracleType()
        {
            var OracleTypeList = db.TB_ORACLE_TYPE.ToList();
            if (OracleTypeList != null)
            {
                var selectItems = OracleTypeList.Select(item => new SelectListItem
                {
                    Value = item.ORACLE_TYPE_ID.ToString(),
                    Text = item.ORACLE_TYPE_NAME
                }).ToList();

                return Json(selectItems, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Message = "Error obtaining Oracle Legal entities" });
            }
        }
        public ActionResult GetOraclePayTerms()
        {
            var OraclePayTermsList = db.TB_ORACLE_PAY_TERMS.ToList();
            if (OraclePayTermsList != null)
            {
                var selectItems = OraclePayTermsList.Select(item => new SelectListItem
                {
                    Value = item.PAY_TERMS_ID.ToString(),
                    Text = item.PAY_TERMS_DESCRIPTION
                }).ToList();

                return Json(selectItems, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Message = "Error obtaining Oracle Legal entities" });
            }
        }
        public ActionResult GetOracleSource()
        {
            var OracleSourceList = db.TB_ORACLE_SOURCE.ToList();
            if (OracleSourceList != null)
            {
                var selectItems = OracleSourceList.Select(item => new SelectListItem
                {
                    Value = item.ORACLE_SOURCE_ID.ToString(),
                    Text = item.ORACLE_SOURCE_DESCRIPTION
                }).ToList();

                return Json(selectItems, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Message = "Error obtaining Oracle Legal entities" });
            }
        }

        public ActionResult GetApprover()
        {
            var ApproverList = db.TB_APPROVER.ToList();
            if (ApproverList != null)
            {
                var selectItems = ApproverList.Select(item => new SelectListItem
                {
                    Value = item.APPROVER_ID.ToString(),
                    Text = item.APPROVER_NAME
                }).ToList();

                return Json(selectItems, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Message = "Error obtaining Oracle Legal entities" });
            }
        }

        public ActionResult GetBackUpEmails()
        {
            var ApproverList = db.TB_EMAIL_BACKUP.ToList();
            if (ApproverList != null)
            {
                var selectItems = ApproverList.Select(item => new SelectListItem
                {
                    Value = item.EMAIL_BACKUP_ID.ToString(),
                    Text = item.EMAIL_BACKUP_DATE.ToString()
                }).ToList();

                return Json(selectItems, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, Message = "Error obtaining Oracle Legal entities" });
            }
        }
    }
}