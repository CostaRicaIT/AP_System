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
            var tB_TEMPLATE = db.TB_TEMPLATE.Include(t => t.TB_APPROVER).Include(t => t.TB_EMAIL_BACKUP).Include(t => t.TB_HIGHLIGHTS).Include(t => t.TB_ORACLE_LEGAL_ENTITIES).Include(t => t.TB_ORACLE_PAY_TERMS).Include(t => t.TB_ORACLE_SOURCE).Include(t => t.TB_ORACLE_TYPE);
            return View(tB_TEMPLATE.ToList());

        }

        public ActionResult Create()
        {
            ViewBag.FK_TB_APPROVER_ID = new SelectList(db.TB_APPROVER, "APPROVER_ID", "APPROVER_NAME");
            ViewBag.FK_TB_EMAIL_BACKUP_ID = new SelectList(db.TB_EMAIL_BACKUP, "EMAIL_BACKUP_ID", "EMAIL_BACKUP");
            ViewBag.FK_TB_HIGHLIGHTS_ID = new SelectList(db.TB_HIGHLIGHTS, "HIGLIGHTS_ID", "HIGHLIGHTS");
            ViewBag.FK_TB_LEGAL_ENTITY_ID = new SelectList(db.TB_ORACLE_LEGAL_ENTITIES, "LEGAL_ENTITY_ID", "LEGAL_ENTITY_NAME");
            ViewBag.FK_TB_ORACLE_PAY_TERMS_ID = new SelectList(db.TB_ORACLE_PAY_TERMS, "PAY_TERMS_ID", "PAY_TERMS_DESCRIPTION");
            ViewBag.FK_TB_ORACLE_SOURCE_ID = new SelectList(db.TB_ORACLE_SOURCE, "ORACLE_SOURCE_ID", "ORACLE_SOURCE_DESCRIPTION");
            ViewBag.FK_TB_ORACLE_TYPE_ID = new SelectList(db.TB_ORACLE_TYPE, "ORACLE_TYPE_ID", "ORACLE_TYPE_NAME");
            ViewBag.FK_TB_TEMPLATE_ALIAS_ID = new SelectList(db.TB_ALIAS, "ALIAS_ID", "ALIAS_NAME");
            return View();
        }

        [HttpPost]
        public ActionResult CreateNoEmail_Alias(TB_TEMPLATE templateData, TB_HIGHLIGHTS HighLightsData, TB_HISTORIC_REMIT HistoricRemitToData) //Create when Alias and email backup are not filled
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        // Add date, save to Highlights Table and get ID created
                        HighLightsData.HIGHLIGHTS_DATE = DateTime.Now;
                        db.TB_HIGHLIGHTS.Add(HighLightsData);

                        // Add date, save to Historic remit Table and get ID created
                        HistoricRemitToData.HISTORIC_REMIT_DATE = DateTime.Now;
                        db.TB_HISTORIC_REMIT.Add(HistoricRemitToData);

                        // Save to template
                        templateData.TEMP_ISDISABLED = 0; //setting isdisabled to 0
                        db.TB_TEMPLATE.Add(templateData);
                        db.SaveChanges(); // Save changes for external tables


                        // Get external tables ID, adding it to template and saving
                        int newTemplateId = templateData.TEMP_ID;
                        int newHighLightsId = HighLightsData.HIGHLIGHTS_ID;
                        int newHistoricRemitId = HistoricRemitToData.HISTORIC_REMIT_ID;
                        templateData.FK_TB_HIGHLIGHTS_ID = newHighLightsId;
                        templateData.FK_TB_TEMPLATE_HISTORIC_REMIT_ID = newHistoricRemitId;
                        HighLightsData.FK_TB_TEMPLATE_ID = newTemplateId;
                        HistoricRemitToData.FK_TB_TEMPLATE_ID = newTemplateId;
                        // Save changes once at the end
                        db.SaveChanges();

                        transaction.Commit(); //If no issues appear confirm save changes
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Model validation failed" });
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // If there is a issue the data wont be saved
                    return Json(new { success = false, message = "An error occurred while saving the record: " + ex.Message });
                }
            }
        }

        [HttpPost]
        public ActionResult CreateWithEmail_Alias(TB_TEMPLATE templateData, TB_HIGHLIGHTS HighLightsData, TB_HISTORIC_REMIT HistoricRemitToData, TB_ALIAS aliasData, TB_EMAIL_BACKUP emailData)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        // Add date, save to Highlights Table and get ID created
                        HighLightsData.HIGHLIGHTS_DATE = DateTime.Now;
                        db.TB_HIGHLIGHTS.Add(HighLightsData);


                        // Add date, save to Historic remit Table and get ID created
                        HistoricRemitToData.HISTORIC_REMIT_DATE = DateTime.Now;
                        db.TB_HISTORIC_REMIT.Add(HistoricRemitToData);


                        // Add date, save to Email Table and get ID created
                        emailData.EMAIL_BACKUP_DATE = DateTime.Now;
                        emailData.EMAIL_BACKUP_ISDISABLED = 0;//setting isdisabled to 0
                        db.TB_EMAIL_BACKUP.Add(emailData);


                        // Save to Alias Table and get ID created
                        aliasData.ALIAS_ISDISABLED = 0;//setting isdisabled to 0
                        db.TB_ALIAS.Add(aliasData);


                        // Save to template getting id´s from highlights, historicRemit, email, and alias and get saved template ID
                        templateData.TEMP_ISDISABLED = 0; //setting isdisabled to 0
                        db.TB_TEMPLATE.Add(templateData);
                        db.SaveChanges(); // Save changes for external tables


                        // Update highlights, historicRemit, email, and alias to add template ID
                        int newTemplateId = templateData.TEMP_ID;
                        int newHighLightsId = HighLightsData.HIGHLIGHTS_ID;
                        int newHistoricRemitId = HistoricRemitToData.HISTORIC_REMIT_ID;
                        int newEmailBackUpId = emailData.EMAIL_BACKUP_ID;
                        int newAliasId = aliasData.ALIAS_ID;
                        templateData.FK_TB_HIGHLIGHTS_ID = newHighLightsId;
                        templateData.FK_TB_TEMPLATE_HISTORIC_REMIT_ID = newHistoricRemitId;
                        templateData.FK_TB_EMAIL_BACKUP_ID = newEmailBackUpId;
                        templateData.FK_TB_TEMPLATE_ALIAS_ID = newAliasId;
                        HighLightsData.FK_TB_TEMPLATE_ID = newTemplateId;
                        HistoricRemitToData.FK_TB_TEMPLATE_ID = newTemplateId;
                        
                        //Saving to alias id and template id to TEMPLATE_ALIAS table
                        var template = db.TB_TEMPLATE.Find(newTemplateId);
                        var alias = db.TB_ALIAS.Find(newAliasId);
                        template.TB_ALIAS.Add(alias);
                        //Saving Email backup id template id to TEMPLATE_EMAIL_BACKUP table
                        var email_backup = db.TB_EMAIL_BACKUP.Find(newEmailBackUpId);
                        template.TB_EMAIL_BACKUP.Add(email_backup);

                        // Save changes once at the end
                        db.SaveChanges();

                        transaction.Commit(); //If no issues appear confirm save changes
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Model validation failed" });
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // If there is a issue the data wont be saved
                    return Json(new { success = false, message = "An error occurred while saving the record: " + ex.Message });
                }
            }
        }



        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TB_TEMPLATE tB_TEMPLATE = db.TB_TEMPLATE.Find(id);
        //    if (tB_TEMPLATE == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var emailBackupList = db.TB_EMAIL_BACKUP
        //                    .OrderByDescending(e => e.EMAIL_BACKUP_DATE) // Sort in descending order
        //                    .AsEnumerable() // Switch to LINQ to Objects
        //                    .Select(e => new
        //                    {
        //                        EMAIL_BACKUP_ID = e.EMAIL_BACKUP_ID,
        //                        EMAIL_BACKUP_DATE = e.EMAIL_BACKUP_DATE.ToString("MM/dd/yyyy hh:mm tt")
        //                    }).ToList();

        //    var historicRemitToList = db.TB_HISTORIC_REMIT
        //        .OrderByDescending(e => e.HISTORIC_REMIT_DATE)
        //        .AsEnumerable()
        //        .Select(e => new
        //        {
        //            HISTORIC_REMIT_ID = e.HISTORIC_REMIT_ID,
        //            HISTORIC_REMIT_DATE = e.HISTORIC_REMIT_DATE.ToString("MM/dd/yyyy hh:mm tt")
        //        }).ToList();

        //    ViewBag.FK_TB_APPROVER_ID = new SelectList(db.TB_APPROVER, "APPROVER_ID", "APPROVER_NAME");
        //    ViewBag.FK_TB_EMAIL_BACKUP_ID = new SelectList(emailBackupList, "EMAIL_BACKUP_ID", "EMAIL_BACKUP_DATE");
        //    ViewBag.FK_TB_HIGHLIGHTS_ID = new SelectList(db.TB_HIGHLIGHTS, "HIGHLIGHTS_ID", "HIGLHIGTHS");
        //    ViewBag.FK_TB_LEGAL_ENTITY_ID = new SelectList(db.TB_ORACLE_LEGAL_ENTITIES, "LEGAL_ENTITY_ID", "LEGAL_ENTITY_NAME");
        //    ViewBag.FK_TB_ORACLE_PAY_TERMS_ID = new SelectList(db.TB_ORACLE_PAY_TERMS, "PAY_TERMS_ID", "PAY_TERMS_DESCRIPTION");
        //    ViewBag.FK_TB_ORACLE_SOURCE_ID = new SelectList(db.TB_ORACLE_SOURCE, "ORACLE_SOURCE_ID", "ORACLE_SOURCE_DESCRIPTION");
        //    ViewBag.FK_TB_ORACLE_TYPE_ID = new SelectList(db.TB_ORACLE_TYPE, "ORACLE_TYPE_ID", "ORACLE_TYPE_NAME");
        //    ViewBag.FK_TB_TEMPLATE_ALIAS_ID = new SelectList(db.TB_ALIAS, "ALIAS_ID", "ALIAS_NAME");
        //    ViewBag.FK_TB_TEMPLATE_HISTORIC_REMIT_ID = new SelectList(historicRemitToList, "HISTORIC_REMIT_ID", "HISTORIC_REMIT_DATE");
        //    return View(tB_TEMPLATE);
        //}

        //[HttpPost]
        //public ActionResult Edit([Bind(Include = "TEMP_ID,TEMP_TAX_ID,TEMP_REMIT_TO,TEMP_SUPPLIER_NAME,TEMP_SUPPLIER_NUMBER,FK_TB_TEMPLATE_HISTORIC_REMIT_ID,TEMP_VENDOR_ACCOUNT,FK_TB_TEMPLATE_ALIAS_ID,TEMP_SUPPLIER_SITE,TEMP_ADDRESS,FK_TB_LEGAL_ENTITY_ID,TEMP_TAXPAYER_ID,FK_TB_ORACLE_TYPE_ID,TEMP_ORACLE_DESCRIPTION,FK_TB_ORACLE_PAY_TERMS_ID,TEMP_ACCOUNT_CODING,FK_TB_ORACLE_SOURCE_ID,TEMP_ORACLE_NOTES,TEMP_ORACLE_INSTRUCTIONS,FK_TB_HIGHLIGHTS_ID,FK_TB_EMAIL_BACKUP_ID,FK_TB_APPROVER_ID,TEMP_APPROVER_COMMENTS,TEMP_INVOICE_FORMAT,TEMP_INVOICE_TYPE")] TB_TEMPLATE tB_TEMPLATE)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tB_TEMPLATE).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.FK_TB_APPROVER_ID = new SelectList(db.TB_APPROVER, "APPROVER_ID", "APPROVER_NAME");
        //    ViewBag.FK_TB_HIGHLIGHTS_ID = new SelectList(db.TB_HIGHLIGHTS, "HIGHLIGHTS_ID", "HIGLHIGTHS");
        //    ViewBag.FK_TB_LEGAL_ENTITY_ID = new SelectList(db.TB_ORACLE_LEGAL_ENTITIES, "LEGAL_ENTITY_ID", "LEGAL_ENTITY_NAME");
        //    ViewBag.FK_TB_ORACLE_PAY_TERMS_ID = new SelectList(db.TB_ORACLE_PAY_TERMS, "PAY_TERMS_ID", "PAY_TERMS_DESCRIPTION");
        //    ViewBag.FK_TB_ORACLE_SOURCE_ID = new SelectList(db.TB_ORACLE_SOURCE, "ORACLE_SOURCE_ID", "ORACLE_SOURCE_DESCRIPTION");
        //    ViewBag.FK_TB_ORACLE_TYPE_ID = new SelectList(db.TB_ORACLE_TYPE, "ORACLE_TYPE_ID", "ORACLE_TYPE_NAME");
        //    ViewBag.FK_TB_TEMPLATE_ALIAS_ID = new SelectList(db.TB_ALIAS, "ALIAS_ID", "ALIAS_NAME");
        //    return View(tB_TEMPLATE);
        //}

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
            var HighlightsHistoric = db.TB_HIGHLIGHTS.OrderByDescending(item => item.HIGHLIGHTS_DATE).ToList(); /*Bring the historic creations info in descending mode */

            if (HighlightsHistoric != null)
            {
                var selectItems = HighlightsHistoric.Select(item => new SelectListItem
                {
                    Text = item.HIGHLIGHTS_DATE.ToString("MM/dd/yyyy"), // bring the Date
                    Value = item.HIGHLIGHTS_ID.ToString() // Bring the ID
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
            var HighLightsText = db.TB_HIGHLIGHTS.FirstOrDefault(h => h.HIGHLIGHTS_ID == HighlightsID);

            if (HighLightsText != null)
            {
                return Json(new { success = true, HighLightsText.HIGHLIGHTS, HighLightsText.HIGHLIGHTS_COMMENTS, HighLightsText.HIGHLIGHTS_INSTRUCTIONS, HighLightsText.HIGHLIGHTS_EXCEPTIONS, HighLightsText.HIGHLIGHTS_COMMON_ISSUES, HighLightsText.HIGHLIGHTS_SUPPLIER_AGENCY, HighLightsText.HIGHLIGHTS_TEMPLATE_COMMENTS }, JsonRequestBehavior.AllowGet);
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