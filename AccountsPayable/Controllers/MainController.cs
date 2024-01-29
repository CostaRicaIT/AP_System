using AccountsPayable.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AccountsPayable.Controllers
{


    public class MainController : Controller
    {

        //In case of merge from DevEnv you must change the entity to AccountsPayableTestProdEntities //
        private AccountsPayableTestProdEntities db = new AccountsPayableTestProdEntities();
        // GET: Main
        public ActionResult Index()
        {
            //Get data for dashboard
            var tB_TEMPLATE = db.TB_TEMPLATE
                .Include(t => t.TB_APPROVER)
                .Include(t => t.TB_HIGHLIGHTS)
                .Include(t => t.TB_ORACLE_LEGAL_ENTITIES)
                .Include(t => t.TB_ORACLE_PAY_TERMS)
                .Include(t => t.TB_ORACLE_SOURCE)
                .Include(t => t.TB_ORACLE_TYPE)
                .Where(t => t.TEMP_ISDISABLED == 0);


            var templates = tB_TEMPLATE.ToList();

            //Get data for email backup and alias

            //get id of each template
            var templateIds = templates.Select(t => t.TEMP_ID).ToList();
            //Query to get the data
            var templateData = db.TB_TEMPLATE
                   .Where(t => templateIds.Contains(t.TEMP_ID))
                   .Select(t => new
                   {
                       Template = t,
                       Alias = t.TB_ALIAS.FirstOrDefault(a => a.ALIAS_ID == t.FK_TB_TEMPLATE_ALIAS_ID),
                       EmailBackup = t.TB_EMAIL_BACKUP.FirstOrDefault(e => e.EMAIL_BACKUP_ID == t.FK_TB_EMAIL_BACKUP_ID)
                   })
                   .ToList();

            //Join data of email and alias to template

            //Is model is recreated due to db change ALIAS_NAME and EMAIL_BAKCUP properties need to be recreated using Generate property option on VS
            foreach (var template in templates)
            {
                var data = templateData.FirstOrDefault(t => t.Template.TEMP_ID == template.TEMP_ID);

                template.ALIAS_NAME = data?.Alias?.ALIAS_NAME;
                template.EMAIL_BACKUP = data?.EmailBackup?.EMAIL_BACKUP;
            }

            
            return View(templates);
        }


        public ActionResult Create()
        {
            //Get data for dropdowns
            ViewBag.FK_TB_APPROVER_ID = new SelectList(db.TB_APPROVER, "APPROVER_ID", "APPROVER_NAME");
            ViewBag.FK_TB_LEGAL_ENTITY_ID = new SelectList(db.TB_ORACLE_LEGAL_ENTITIES, "LEGAL_ENTITY_ID", "LEGAL_ENTITY_NAME");
            ViewBag.FK_TB_ORACLE_PAY_TERMS_ID = new SelectList(db.TB_ORACLE_PAY_TERMS, "PAY_TERMS_ID", "PAY_TERMS_DESCRIPTION");
            ViewBag.FK_TB_ORACLE_SOURCE_ID = new SelectList(db.TB_ORACLE_SOURCE, "ORACLE_SOURCE_ID", "ORACLE_SOURCE_DESCRIPTION");
            ViewBag.FK_TB_ORACLE_TYPE_ID = new SelectList(db.TB_ORACLE_TYPE, "ORACLE_TYPE_ID", "ORACLE_TYPE_NAME");
            return View();
        }


        [HttpPost]
        public ActionResult Create(TB_TEMPLATE templateData, TB_HIGHLIGHTS HighLightsData, TB_HISTORIC_REMIT HistoricRemitToData, List<TB_ALIAS> aliasDataList, List<TB_EMAIL_BACKUP> emailDataList)
        {
            //Get UTC timezone and convert it to UTC-6 Costa Rica local time
            var dateTimeUTC = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            DateTime targetTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUTC, targetTimeZone);
            
            //Start transaction for creation of template
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        // Add date to highlights and add data to be saved later
                        HighLightsData.HIGHLIGHTS_DATE = targetTime;
                        db.TB_HIGHLIGHTS.Add(HighLightsData);


                        // Add date to Historic Remit and add data to be saved later
                        HistoricRemitToData.HISTORIC_REMIT_DATE = targetTime;
                        db.TB_HISTORIC_REMIT.Add(HistoricRemitToData);


                        // Get all emails created add date and add data to be saved later
                        if (emailDataList != null && emailDataList.Any())
                        {
                            foreach (var emailData in emailDataList)
                            {
                                emailData.EMAIL_BACKUP_ISDISABLED = 0;
                                emailData.EMAIL_BACKUP_DATE = targetTime;
                                db.TB_EMAIL_BACKUP.Add(emailData);
                            }
                        }

                        // Get all alias created and add data to be saved later
                        if (aliasDataList != null && aliasDataList.Any())
                        {
                            foreach (var aliasData in aliasDataList)
                            {
                                aliasData.ALIAS_ISDISABLED = 0;
                                db.TB_ALIAS.Add(aliasData);
                            }
                        }



                        /*templateData.TEMP_ISDISABLED = 0;*/ //setting isdisabled to 0

                        //Save template data
                        db.TB_TEMPLATE.Add(templateData);
                        //Save all changes to DB
                        db.SaveChanges(); // Save changes for external tables


                        // Update highlights, historicRemit, email, and alias to add template ID
                        int newAliasId;
                        int newEmailBackUpId;
                        int newTemplateId = templateData.TEMP_ID;
                        int newHighLightsId = HighLightsData.HIGHLIGHTS_ID;
                        int newHistoricRemitId = HistoricRemitToData.HISTORIC_REMIT_ID;
                        HighLightsData.FK_TB_TEMPLATE_ID = newTemplateId;
                        HistoricRemitToData.FK_TB_TEMPLATE_ID = newTemplateId;

                        //Saving to alias id and template id to TEMPLATE_ALIAS table

                        var template = db.TB_TEMPLATE.Find(newTemplateId);
                        if (aliasDataList != null && aliasDataList.Any())
                        {
                            List<int> savedAliasIds = aliasDataList.Select(x => x.ALIAS_ID).ToList();
                            foreach (int aliasId in savedAliasIds)
                            {
                                var alias = db.TB_ALIAS.Find(aliasId);
                                template.TB_ALIAS.Add(alias);
                            }
                            newAliasId = savedAliasIds.LastOrDefault(); //getting the last Alias and Email backup saved
                            templateData.FK_TB_TEMPLATE_ALIAS_ID = newAliasId;
                        }

                        //Saving Email backup id template id to TEMPLATE_EMAIL_BACKUP table

                        if (emailDataList != null && emailDataList.Any())
                        {
                            List<int> savedEmailsIds = emailDataList.Select(x => x.EMAIL_BACKUP_ID).ToList();
                            foreach (int emailId in savedEmailsIds)
                            {
                                var email = db.TB_EMAIL_BACKUP.Find(emailId);
                                template.TB_EMAIL_BACKUP.Add(email);

                            }
                            newEmailBackUpId = savedEmailsIds.LastOrDefault(); //getting the last Alias and Email backup saved
                            templateData.FK_TB_EMAIL_BACKUP_ID = newEmailBackUpId;
                        }
                        templateData.FK_TB_HIGHLIGHTS_ID = newHighLightsId;
                        templateData.FK_TB_TEMPLATE_HISTORIC_REMIT_ID = newHistoricRemitId;

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

            //Get data for email backup
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
            //Get data for historic Remit
            var historicRemitToList = db.TB_HISTORIC_REMIT
                .Where(x => x.FK_TB_TEMPLATE_ID == id)
                .OrderByDescending(e => e.HISTORIC_REMIT_DATE)
                .AsEnumerable()
                .Select(e => new
                {
                    HISTORIC_REMIT_ID = e.HISTORIC_REMIT_ID,
                    HISTORIC_REMIT_DATE = e.HISTORIC_REMIT_DATE.ToString("MM/dd/yyyy hh:mm tt")
                }).ToList();
            //Get data for highlights
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

            //Get data for alias
            var aliasesForTemplate = db.TB_ALIAS.Where(a => a.TB_TEMPLATE.Any(t => t.TEMP_ID == id)).ToList();

            //Send data to view with external tables data
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
            //Get UTC timezone and convert it to UTC-6 Costa Rica local time
            var dateTimeUTC = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            DateTime targetTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUTC, targetTimeZone);
            //Start transaction for template update
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

                            // Update highlights data if there are changes
                            if (HighLightsData.HIGHLIGHTS != existingTemplate.TB_HIGHLIGHTS1.HIGHLIGHTS
                                || HighLightsData.HIGHLIGHTS_COMMENTS != existingTemplate.TB_HIGHLIGHTS1.HIGHLIGHTS_COMMENTS
                                || HighLightsData.HIGHLIGHTS_INSTRUCTIONS != existingTemplate.TB_HIGHLIGHTS1.HIGHLIGHTS_INSTRUCTIONS
                                || HighLightsData.HIGHLIGHTS_EXCEPTIONS != existingTemplate.TB_HIGHLIGHTS1.HIGHLIGHTS_EXCEPTIONS
                                || HighLightsData.HIGHLIGHTS_COMMON_ISSUES != existingTemplate.TB_HIGHLIGHTS1.HIGHLIGHTS_COMMON_ISSUES
                                || HighLightsData.HIGHLIGHTS_SUPPLIER_AGENCY != existingTemplate.TB_HIGHLIGHTS1.HIGHLIGHTS_SUPPLIER_AGENCY
                                || HighLightsData.HIGHLIGHTS_INSTRUCTIONS != existingTemplate.TB_HIGHLIGHTS1.HIGHLIGHTS_INSTRUCTIONS
                                || HighLightsData.HIGHLIGHTS_COMMENTS != existingTemplate.TB_HIGHLIGHTS1.HIGHLIGHTS_COMMENTS)
                            {
                                HighLightsData.FK_TB_TEMPLATE_ID = existingTemplate.TEMP_ID;
                                HighLightsData.HIGHLIGHTS_DATE = targetTime;
                                db.TB_HIGHLIGHTS.Add(HighLightsData);
                            }

                            // Update historic remit data if there are changes
                            if (HistoricRemitToData.HISTORIC_REMIT_INFO != existingTemplate.TB_HISTORIC_REMIT1.HISTORIC_REMIT_INFO)
                            {
                                HistoricRemitToData.FK_TB_TEMPLATE_ID = existingTemplate.TEMP_ID;
                                HistoricRemitToData.HISTORIC_REMIT_DATE = targetTime;
                                db.TB_HISTORIC_REMIT.Add(HistoricRemitToData);
                            }


                            // Update email data
                            if (emailDataList != null && emailDataList.Any())
                            {
                                foreach (var emailData in emailDataList)
                                {
                                    emailData.EMAIL_BACKUP_ISDISABLED = 0;
                                    emailData.EMAIL_BACKUP_DATE = targetTime;

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
                            if (aliasDataList != null && aliasDataList.Any())
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
                            int newHistoricRemit = (int)(HistoricRemitToData?.HISTORIC_REMIT_ID);
                            int newHighlightsId = (int)(HighLightsData?.HIGHLIGHTS_ID);
                            if (newHistoricRemit != 0)
                            {
                                templateData.FK_TB_TEMPLATE_HISTORIC_REMIT_ID = HistoricRemitToData?.HISTORIC_REMIT_ID;
                            }
                            else
                            {
                                templateData.FK_TB_TEMPLATE_HISTORIC_REMIT_ID = existingTemplate.FK_TB_TEMPLATE_HISTORIC_REMIT_ID;
                            }
                            if (newHighlightsId != 0)
                            {
                                templateData.FK_TB_HIGHLIGHTS_ID = HighLightsData?.HIGHLIGHTS_ID;
                            }
                            else
                            {
                                templateData.FK_TB_HIGHLIGHTS_ID = existingTemplate.FK_TB_HIGHLIGHTS_ID;
                            }



                            if (emailDataList != null && emailDataList.Any())
                            {
                                templateData.FK_TB_EMAIL_BACKUP_ID = emailDataList.LastOrDefault()?.EMAIL_BACKUP_ID;
                            }
                            else
                            {

                                templateData.FK_TB_EMAIL_BACKUP_ID = existingTemplate.FK_TB_EMAIL_BACKUP_ID;
                            }

                            if (aliasDataList != null && aliasDataList.Any())
                            {
                                templateData.FK_TB_TEMPLATE_ALIAS_ID = aliasDataList.LastOrDefault()?.ALIAS_ID;
                            }
                            else
                            {

                                templateData.FK_TB_TEMPLATE_ALIAS_ID = existingTemplate.FK_TB_TEMPLATE_ALIAS_ID;
                            }
                            //templateData.TEMP_ISDISABLED = 0;
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

        [HttpPost]
        public ActionResult AddLegalEntity(string legalEntityName)
        {
            try
            {

                bool entityExists = db.TB_ORACLE_LEGAL_ENTITIES.Any(entity => entity.LEGAL_ENTITY_NAME == legalEntityName);

                if (!entityExists)
                {
                    var newEntity = new TB_ORACLE_LEGAL_ENTITIES { LEGAL_ENTITY_NAME = legalEntityName };
                    db.TB_ORACLE_LEGAL_ENTITIES.Add(newEntity);
                    db.SaveChanges();

                    return Json(new { id = newEntity.LEGAL_ENTITY_ID, name = newEntity.LEGAL_ENTITY_NAME });

                }

                return Json(new { message = "Entity already exists" });

            }

            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        [HttpPost]
        public ActionResult AddApprover(string approverName)
        {
            try
            {

                bool entiExists = db.TB_APPROVER.Any(enti => enti.APPROVER_NAME == approverName);

                if (!entiExists)
                {
                    var newEnti = new TB_APPROVER { APPROVER_NAME = approverName };
                    db.TB_APPROVER.Add(newEnti);
                    db.SaveChanges();

                    return Json(new { id = newEnti.APPROVER_ID, name = newEnti.APPROVER_NAME });

                }

                return Json(new { message = "Entity already exists" });

            }

            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        public ActionResult Details(int? id)
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


        [HttpDelete]
        public ActionResult DELETE(int? id, int disabled = 1)
        {
            TB_TEMPLATE tB_TEMPLATE = db.TB_TEMPLATE.Find(id);
            if (tB_TEMPLATE.TEMP_ISDISABLED != 1)
            {
                tB_TEMPLATE.TEMP_ISDISABLED = disabled;
                db.SaveChanges();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }


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
                    .Select(item => new
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
    }
}