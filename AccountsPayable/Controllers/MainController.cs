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
using System.Web.UI;
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
            ViewBag.FK_TB_LEGAL_ENTITY_ID = new SelectList(db.TB_ORACLE_LEGAL_ENTITIES, "LEGAL_ENTITY_ID", "LEGAL_ENTITY_NAME");
            ViewBag.FK_TB_ORACLE_PAY_TERMS_ID = new SelectList(db.TB_ORACLE_PAY_TERMS, "PAY_TERMS_ID", "PAY_TERMS_DESCRIPTION");
            ViewBag.FK_TB_ORACLE_SOURCE_ID = new SelectList(db.TB_ORACLE_SOURCE, "ORACLE_SOURCE_ID", "ORACLE_SOURCE_DESCRIPTION");
            ViewBag.FK_TB_ORACLE_TYPE_ID = new SelectList(db.TB_ORACLE_TYPE, "ORACLE_TYPE_ID", "ORACLE_TYPE_NAME");
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
        public ActionResult CreateWithEmail_Alias(TB_TEMPLATE templateData, TB_HIGHLIGHTS HighLightsData, TB_HISTORIC_REMIT HistoricRemitToData, List<TB_ALIAS> aliasDataList, List<TB_EMAIL_BACKUP> emailDataList)
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


                        // Add date, save to Email Table
                        foreach (var emailData in emailDataList)
                        {
                            emailData.EMAIL_BACKUP_ISDISABLED = 0;
                            emailData.EMAIL_BACKUP_DATE = DateTime.Now;
                            db.TB_EMAIL_BACKUP.Add(emailData);
                        }

                        // Save to Alias Table 

                        foreach (var aliasData in aliasDataList)
                        {
                            aliasData.ALIAS_ISDISABLED = 0;
                            db.TB_ALIAS.Add(aliasData);
                        }



                        // Save to template getting id´s from highlights, historicRemit, email, and alias and get saved template ID
                        templateData.TEMP_ISDISABLED = 0; //setting isdisabled to 0
                        db.TB_TEMPLATE.Add(templateData);
                        db.SaveChanges(); // Save changes for external tables


                        // Update highlights, historicRemit, email, and alias to add template ID
                        int newTemplateId = templateData.TEMP_ID;
                        int newHighLightsId = HighLightsData.HIGHLIGHTS_ID;
                        int newHistoricRemitId = HistoricRemitToData.HISTORIC_REMIT_ID;
                        int newAliasId = 0; //Initializing newAliasId and newEmailBackUpId
                        int newEmailBackUpId = 0;
                        HighLightsData.FK_TB_TEMPLATE_ID = newTemplateId;
                        HistoricRemitToData.FK_TB_TEMPLATE_ID = newTemplateId;

                        //Saving to alias id and template id to TEMPLATE_ALIAS table
                        List<int> savedAliasIds = aliasDataList.Select(x => x.ALIAS_ID).ToList();
                        var template = db.TB_TEMPLATE.Find(newTemplateId);
                        foreach (int aliasId in savedAliasIds)
                        {
                            var alias = db.TB_ALIAS.Find(aliasId);
                            template.TB_ALIAS.Add(alias);
                        }


                        //Saving Email backup id template id to TEMPLATE_EMAIL_BACKUP table
                        List<int> savedEmailsIds = emailDataList.Select(x => x.EMAIL_BACKUP_ID).ToList();
                        foreach (int emailId in savedEmailsIds)
                        {
                            var email = db.TB_EMAIL_BACKUP.Find(emailId);
                            template.TB_EMAIL_BACKUP.Add(email);
                        }

                        newAliasId = savedAliasIds.LastOrDefault(); //getting the last Alias and Email backup saved
                        newEmailBackUpId = savedEmailsIds.LastOrDefault();
                        templateData.FK_TB_TEMPLATE_ALIAS_ID = newAliasId;
                        templateData.FK_TB_HIGHLIGHTS_ID = newHighLightsId;
                        templateData.FK_TB_TEMPLATE_HISTORIC_REMIT_ID = newHistoricRemitId;
                        templateData.FK_TB_EMAIL_BACKUP_ID = newEmailBackUpId;
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



        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_TEMPLATE tB_TEMPLATE = db.TB_TEMPLATE.Find(id);
            if (tB_TEMPLATE == null)
            {
                return HttpNotFound();
            }

            var emailBackupList = db.TB_EMAIL_BACKUP
                .Where(a => a.TB_TEMPLATE.Any(t => t.TEMP_ID == id))
                .OrderByDescending(e => e.EMAIL_BACKUP_DATE)
                .AsEnumerable()
                .Select(e => new
                {
                    EMAIL_BACKUP_ID = e.EMAIL_BACKUP_ID,
                    EMAIL_BACKUP_DATE = e.EMAIL_BACKUP_DATE.ToString("MM/dd/yyyy hh:mm tt")
                }
                ).ToList();

            var historicRemitToList = db.TB_HISTORIC_REMIT
                .Where(x => x.FK_TB_TEMPLATE_ID == id)
                .OrderByDescending(e => e.HISTORIC_REMIT_DATE)
                .AsEnumerable()
                .Select(e => new
                {
                    HISTORIC_REMIT_ID = e.HISTORIC_REMIT_ID,
                    HISTORIC_REMIT_DATE = e.HISTORIC_REMIT_DATE.ToString("MM/dd/yyyy hh:mm tt")
                }).ToList();

            var HighLightsToList = db.TB_HIGHLIGHTS
                .Where(e => e.FK_TB_TEMPLATE_ID == id)
                .OrderByDescending(e => e.HIGHLIGHTS_DATE)
                .AsEnumerable()
                .Select(e => new
                {                    
                    HIGHLIGHTS_ID = e.HIGHLIGHTS_ID,
                    HIGHLIGHTS_DATE = e.HIGHLIGHTS_DATE.ToString("MM/dd/yyyy hh:mm tt"),
                })
                .ToList();

            var aliasesForTemplate = db.TB_ALIAS.Where(a => a.TB_TEMPLATE.Any(t => t.TEMP_ID == id)).ToList();

            ViewBag.FK_TB_APPROVER_ID = new SelectList(db.TB_APPROVER, "APPROVER_ID", "APPROVER_NAME", tB_TEMPLATE.FK_TB_APPROVER_ID);
            ViewBag.FK_TB_EMAIL_BACKUP_ID = new SelectList(emailBackupList, "EMAIL_BACKUP_ID", "EMAIL_BACKUP_DATE");
            ViewBag.FK_TB_HIGHLIGHTS = new SelectList(HighLightsToList, "HIGHLIGHTS_ID", "HIGHLIGHTS_DATE");
            ViewBag.FK_TB_LEGAL_ENTITY_ID = new SelectList(db.TB_ORACLE_LEGAL_ENTITIES, "LEGAL_ENTITY_ID", "LEGAL_ENTITY_NAME", tB_TEMPLATE.FK_TB_LEGAL_ENTITY_ID);
            ViewBag.FK_TB_ORACLE_PAY_TERMS_ID = new SelectList(db.TB_ORACLE_PAY_TERMS, "PAY_TERMS_ID", "PAY_TERMS_DESCRIPTION", tB_TEMPLATE.FK_TB_ORACLE_PAY_TERMS_ID);
            ViewBag.FK_TB_ORACLE_SOURCE_ID = new SelectList(db.TB_ORACLE_SOURCE, "ORACLE_SOURCE_ID", "ORACLE_SOURCE_DESCRIPTION", tB_TEMPLATE.FK_TB_ORACLE_SOURCE_ID);
            ViewBag.FK_TB_ORACLE_TYPE_ID = new SelectList(db.TB_ORACLE_TYPE, "ORACLE_TYPE_ID", "ORACLE_TYPE_NAME", tB_TEMPLATE.FK_TB_ORACLE_TYPE_ID);
            ViewBag.FK_TB_TEMPLATE_ALIAS_ID = new SelectList(aliasesForTemplate, "ALIAS_ID", "ALIAS_NAME");
            ViewBag.FK_TB_TEMPLATE_HISTORIC_REMIT_ID = new SelectList(historicRemitToList, "HISTORIC_REMIT_ID", "HISTORIC_REMIT_DATE");
            return View(tB_TEMPLATE);
        }

        [HttpPost]
        public ActionResult Edit(TB_TEMPLATE templateData, TB_HIGHLIGHTS HighLightsData, TB_HISTORIC_REMIT HistoricRemitToData, List<TB_ALIAS> aliasDataList, List<TB_EMAIL_BACKUP> emailDataList)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        // Retrieve existing template from the database
                        var existingTemplate = db.TB_TEMPLATE.Find(templateData.TEMP_ID);

                        if (existingTemplate != null)
                        {
                            // Update template properties


                            // Update highlights data
                            HighLightsData.FK_TB_TEMPLATE_ID = existingTemplate.TEMP_ID;
                            HighLightsData.HIGHLIGHTS_DATE = DateTime.Now;
                            db.TB_HIGHLIGHTS.Add(HighLightsData);


                            // Update historic remit data
                            HistoricRemitToData.FK_TB_TEMPLATE_ID = existingTemplate.TEMP_ID;
                            HistoricRemitToData.HISTORIC_REMIT_DATE = DateTime.Now;
                            db.TB_HISTORIC_REMIT.Add(HistoricRemitToData);

                            // Update email data
                            if (emailDataList != null)
                            {
                                foreach (var emailData in emailDataList)
                                {
                                    emailData.EMAIL_BACKUP_ISDISABLED = 0;
                                    emailData.EMAIL_BACKUP_DATE = DateTime.Now;

                                    // Check if emailData already exists
                                    var existingEmail = db.TB_EMAIL_BACKUP.Find(emailData.EMAIL_BACKUP_ID);
                                    var template = db.TB_TEMPLATE.Find(existingTemplate.TEMP_ID);
                                    if (existingEmail == null)
                                    {
                                        db.TB_EMAIL_BACKUP.Add(emailData);
                                        List<int> savedEmailsIds = emailDataList.Select(x => x.EMAIL_BACKUP_ID).ToList();
                                        foreach (int emailId in savedEmailsIds)
                                        {
                                            var email = db.TB_EMAIL_BACKUP.Find(emailId);
                                            template.TB_EMAIL_BACKUP.Add(email);
                                        }
                                    }

                                    existingTemplate.TB_EMAIL_BACKUP.Add(emailData);
                                }
                            }

                            // Update alias data
                            if (aliasDataList != null)
                            {
                                foreach (var aliasData in aliasDataList)
                                {
                                    aliasData.ALIAS_ISDISABLED = 0;

                                    // Check if aliasData already exists
                                    var existingAlias = db.TB_ALIAS.Find(aliasData.ALIAS_ID);
                                    List<int> savedAliasIds = aliasDataList.Select(x => x.ALIAS_ID).ToList();
                                    var template = db.TB_TEMPLATE.Find(existingTemplate.TEMP_ID);
                                    if (existingAlias == null)
                                    {
                                        db.TB_ALIAS.Add(aliasData);
                                        foreach (int aliasId in savedAliasIds)
                                        {
                                            var alias = db.TB_ALIAS.Find(aliasId);
                                            template.TB_ALIAS.Add(alias);
                                        }
                                    }

                                    existingTemplate.TB_ALIAS.Add(aliasData);
                                }
                            }

                            // Save changes once at the end
                            db.SaveChanges();

                            // Set foreign key properties for templateData after saving changes
                            templateData.FK_TB_TEMPLATE_HISTORIC_REMIT_ID = HistoricRemitToData.HISTORIC_REMIT_ID;
                            templateData.FK_TB_HIGHLIGHTS_ID = HighLightsData.HIGHLIGHTS_ID;
                            templateData.FK_TB_EMAIL_BACKUP_ID = emailDataList.LastOrDefault()?.EMAIL_BACKUP_ID; // Adjust this according to your needs
                            templateData.FK_TB_TEMPLATE_ALIAS_ID = aliasDataList.LastOrDefault()?.ALIAS_ID;
                            templateData.TEMP_ISDISABLED = 0;
                            db.Entry(existingTemplate).CurrentValues.SetValues(templateData);
                            db.SaveChanges();

                            transaction.Commit(); // If no issues appear, confirm save changes
                            return Json(new { success = true });
                        }
                        else
                        {
                            // The template does not exist, handle accordingly (e.g., return an error)
                            transaction.Rollback();
                            return Json(new { success = false, message = "Template not found" });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, message = "Model validation failed" });
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // If there is an issue, the data won't be saved
                    return Json(new { success = false, message = "An error occurred while saving the record: " + ex.Message });
                }
            }
        }




        //[HttpGet]
        //public ActionResult GetHistoryInfo()
        //{
        //    var historicRemit = db.TB_HISTORIC_REMIT.OrderByDescending(item => item.HISTORIC_REMIT_DATE).ToList(); /*Bring the historic creations info in descending mode */

        //    if (historicRemit != null)
        //    {
        //        var selectItems = historicRemit.Select(item => new SelectListItem
        //        {
        //            Text = item.HISTORIC_REMIT_DATE.ToString("MM/dd/yyyy"), // bring the Date
        //            Value = item.HISTORIC_REMIT_ID.ToString() // Bring the ID
        //        }).ToList();

        //        return Json(new { success = true, selectItems }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, message = "Error in obtaining historical information." });
        //    }
        //}

        //[HttpGet]
        //public ActionResult GetHighlightsInfo()
        //{
        //    var HighlightsHistoric = db.TB_HIGHLIGHTS.OrderByDescending(item => item.HIGHLIGHTS_DATE).ToList(); /*Bring the historic creations info in descending mode */

        //    if (HighlightsHistoric != null)
        //    {
        //        var selectItems = HighlightsHistoric.Select(item => new SelectListItem
        //        {
        //            Text = item.HIGHLIGHTS_DATE.ToString("MM/dd/yyyy"), // bring the Date
        //            Value = item.HIGHLIGHTS_ID.ToString() // Bring the ID
        //        }).ToList();

        //        return Json(new { success = true, selectItems }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, message = "Error in obtaining historical information." });
        //    }
        //}

        [HttpGet]
        public ActionResult GetEmailText(int emailId, int id)
        {
            // Get the text based on the ID
            var historicEmailText = db.TB_EMAIL_BACKUP
                    .Where(a => a.TB_TEMPLATE.Any(t => t.TEMP_ID == id) && a.EMAIL_BACKUP_ID == emailId)
                    .Select(item => item.EMAIL_BACKUP)
                    .FirstOrDefault();

            if (historicEmailText != null)
            {
                return Json(new { success = true, historicEmailText }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "." }, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpGet]
        public ActionResult GetHistoricRemitText(int historicId, int id)
        {
            // Get the text based on the ID
            var historicText = db.TB_HISTORIC_REMIT
                    .Where(a => a.HISTORIC_REMIT_ID == historicId)
                    .Select(item => item.HISTORIC_REMIT_INFO)
                    .FirstOrDefault();

            if (historicText != null)
            {
                return Json(new { success = true, historicText }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "." }, JsonRequestBehavior.AllowGet);

            }
        }


        [HttpGet]
        public ActionResult GetHighlights(int highlightsId, int id)
        {
            // Get the text based on the ID
            var historicText = db.TB_HIGHLIGHTS
                    .Where(a => a.HIGHLIGHTS_ID == highlightsId)
                    .Select(item =>new
                    {
                        HIGHLIGHTS = item.HIGHLIGHTS,
                        HIGHLIGHTS_COMMENTS = item.HIGHLIGHTS_COMMENTS,
                        HIGHLIGHTS_INSTRUCTIONS = item.HIGHLIGHTS_INSTRUCTIONS,
                        HIGHLIGHTS_EXCEPTIONS = item.HIGHLIGHTS_EXCEPTIONS,
                        HIGHLIGHTS_COMMON_ISSUES = item.HIGHLIGHTS_COMMON_ISSUES,
                        HIGHLIGHTS_SUPPLIER_AGENCY = item.HIGHLIGHTS_SUPPLIER_AGENCY,
                        HIGHLIGHTS_TEMPLATE_COMMENTS = item.HIGHLIGHTS_TEMPLATE_COMMENTS,
                    })
                    .FirstOrDefault();

            if (historicText != null)
            {
                return Json(new { success = true, historicText }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "." }, JsonRequestBehavior.AllowGet);

            }
        }

        //public ActionResult CreateAlias([Bind(Include = "ALIAS_NAME")] TB_ALIAS tB_ALIAS)
        //{



        //    if (ModelState.IsValid)
        //    {

        //        db.TB_ALIAS.Add(tB_ALIAS);
        //        db.SaveChanges();
        //        return Json(new { success = true });
        //    }
        //    return Json(new { success = false, message = "There was an error saving the record " });
        //}

        //public ActionResult GetAliasInfo()
        //{
        //    var AliasList = db.TB_ALIAS.ToList();

        //    if (AliasList != null)
        //    {
        //        var selectItems = AliasList.Select(item => new SelectListItem
        //        {
        //            Text = item.ALIAS_NAME.ToString(), // bring the Alias name
        //            Value = item.ALIAS_ID.ToString() // Bring the Alias ID
        //        }).ToList();

        //        return Json(new { success = true, selectItems }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, message = "Error in obtaining historical information." });
        //    }




        //}


        //public ActionResult GetAliasText(int AliasId)
        //{
        //    // Get the text based on the ID
        //    var AliasText = db.TB_ALIAS
        //        .Where(item => item.ALIAS_ID == AliasId)
        //        .Select(item => item.ALIAS_NAME)
        //        .FirstOrDefault();

        //    if (AliasText != null)
        //    {
        //        return Json(new { success = true, AliasText }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, message = "Alias name not found." });
        //    }
        //}
        //public ActionResult GetHighLightsText(int HighlightsID)
        //{
        //    // Get the text based on the ID
        //    var HighLightsText = db.TB_HIGHLIGHTS.FirstOrDefault(h => h.HIGHLIGHTS_ID == HighlightsID);

        //    if (HighLightsText != null)
        //    {
        //        return Json(new { success = true, HighLightsText.HIGHLIGHTS, HighLightsText.HIGHLIGHTS_COMMENTS, HighLightsText.HIGHLIGHTS_INSTRUCTIONS, HighLightsText.HIGHLIGHTS_EXCEPTIONS, HighLightsText.HIGHLIGHTS_COMMON_ISSUES, HighLightsText.HIGHLIGHTS_SUPPLIER_AGENCY, HighLightsText.HIGHLIGHTS_TEMPLATE_COMMENTS }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, message = "Alias name not found." });
        //    }
        //}

        //public ActionResult GetHistoricRecentData()
        //{
        //    var mostRecentData = db.TB_HISTORIC_REMIT.OrderByDescending(h => h.HISTORIC_REMIT_DATE).FirstOrDefault();

        //    if (mostRecentData != null)
        //    {

        //        string mostRecentInfo = mostRecentData.HISTORIC_REMIT_INFO;

        //        return Json(new { success = true, mostRecentData = mostRecentInfo }, JsonRequestBehavior.AllowGet);

        //    }
        //    else
        //    {
        //        return Json(new { success = false, Message = "An error ocurred while fetching recent data" }, JsonRequestBehavior.AllowGet);

        //    }
        //}
        //public ActionResult GetOracleLegalEntity()
        //{
        //    var OracleLegalEntityList = db.TB_ORACLE_LEGAL_ENTITIES.ToList();
        //    if (OracleLegalEntityList != null)
        //    {
        //        var selectItems = OracleLegalEntityList.Select(item => new SelectListItem
        //        {
        //            Value = item.LEGAL_ENTITY_ID.ToString(),
        //            Text = item.LEGAL_ENTITY_NAME
        //        }).ToList();

        //        return Json(selectItems, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, Message = "Error obtaining Oracle Legal entities" });
        //    }
        //}
        //public ActionResult GetOracleType()
        //{
        //    var OracleTypeList = db.TB_ORACLE_TYPE.ToList();
        //    if (OracleTypeList != null)
        //    {
        //        var selectItems = OracleTypeList.Select(item => new SelectListItem
        //        {
        //            Value = item.ORACLE_TYPE_ID.ToString(),
        //            Text = item.ORACLE_TYPE_NAME
        //        }).ToList();

        //        return Json(selectItems, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, Message = "Error obtaining Oracle Legal entities" });
        //    }
        //}
        //public ActionResult GetOraclePayTerms()
        //{
        //    var OraclePayTermsList = db.TB_ORACLE_PAY_TERMS.ToList();
        //    if (OraclePayTermsList != null)
        //    {
        //        var selectItems = OraclePayTermsList.Select(item => new SelectListItem
        //        {
        //            Value = item.PAY_TERMS_ID.ToString(),
        //            Text = item.PAY_TERMS_DESCRIPTION
        //        }).ToList();

        //        return Json(selectItems, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, Message = "Error obtaining Oracle Legal entities" });
        //    }
        //}
        //public ActionResult GetOracleSource()
        //{
        //    var OracleSourceList = db.TB_ORACLE_SOURCE.ToList();
        //    if (OracleSourceList != null)
        //    {
        //        var selectItems = OracleSourceList.Select(item => new SelectListItem
        //        {
        //            Value = item.ORACLE_SOURCE_ID.ToString(),
        //            Text = item.ORACLE_SOURCE_DESCRIPTION
        //        }).ToList();

        //        return Json(selectItems, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, Message = "Error obtaining Oracle Legal entities" });
        //    }
        //}

        //public ActionResult GetApprover()
        //{
        //    var ApproverList = db.TB_APPROVER.ToList();
        //    if (ApproverList != null)
        //    {
        //        var selectItems = ApproverList.Select(item => new SelectListItem
        //        {
        //            Value = item.APPROVER_ID.ToString(),
        //            Text = item.APPROVER_NAME
        //        }).ToList();

        //        return Json(selectItems, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, Message = "Error obtaining Oracle Legal entities" });
        //    }
        //}

        //public ActionResult GetBackUpEmails()
        //{
        //    var ApproverList = db.TB_EMAIL_BACKUP.ToList();
        //    if (ApproverList != null)
        //    {
        //        var selectItems = ApproverList.Select(item => new SelectListItem
        //        {
        //            Value = item.EMAIL_BACKUP_ID.ToString(),
        //            Text = item.EMAIL_BACKUP_DATE.ToString()
        //        }).ToList();

        //        return Json(selectItems, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, Message = "Error obtaining Oracle Legal entities" });
        //    }
        //}
    }
}