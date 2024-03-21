using AccountsPayable.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AccountsPayable.Controllers
{
    public class CRUDController : Controller
    {
        private Accounts_Payable_Entities db = new Accounts_Payable_Entities();

        [HttpPost]
        public ActionResult Create(TB_TEMPLATE templateData, TB_HIGHLIGHTS HighLightsData, TB_HISTORIC_REMIT HistoricRemitToData, List<TB_ALIAS> aliasDataList, List<TB_EMAIL_BACKUP> emailDataList)
        {
            //Get UTC timezone and convert it to UTC-6 Costa Rica local time
            var dateTimeUTC = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            DateTime targetTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUTC, targetTimeZone);

            //Start transaction for creation of template
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        // Add date to highlights and add data to be saved later, verifies if all the HIGHLIGHTS data is different from null can be inserted
                        if (HighLightsData.HIGHLIGHTS != null
                                || HighLightsData.HIGHLIGHTS_COMMENTS != null
                                || HighLightsData.HIGHLIGHTS_INSTRUCTIONS != null
                                || HighLightsData.HIGHLIGHTS_EXCEPTIONS != null
                                || HighLightsData.HIGHLIGHTS_COMMON_ISSUES != null
                                || HighLightsData.HIGHLIGHTS_SUPPLIER_AGENCY != null
                                || HighLightsData.HIGHLIGHTS_TEMPLATE_COMMENTS != null)
                        {
                            HighLightsData.HIGHLIGHTS_DATE = targetTime;
                            db.TB_HIGHLIGHTS.Add(HighLightsData);
                        }
                        // Add date to Historic Remit and add data to be saved later, verifies if the HISTORIC_REMIT data is different from null can be inserted
                        if (HistoricRemitToData.HISTORIC_REMIT_INFO != null)
                        {
                            HistoricRemitToData.HISTORIC_REMIT_DATE = targetTime;
                            db.TB_HISTORIC_REMIT.Add(HistoricRemitToData);
                        }
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
                        //Validates if data in historic remit is different from NULL, creates the needed table in TB_HIGHLIGHTS else omits creating it
                        if (newHighLightsId != 0)
                        {
                            templateData.FK_TB_HIGHLIGHTS_ID = newHighLightsId;
                        }
                        //Validates if data in historic remit is different from NULL, creates the needed table in TB_HISTORIC_REMIT else omits creating it
                        if (newHistoricRemitId != 0)
                        {
                            templateData.FK_TB_TEMPLATE_HISTORIC_REMIT_ID = newHistoricRemitId;
                        }
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
        public ActionResult Edit(TB_TEMPLATE templateData, TB_HIGHLIGHTS HighLightsData, TB_HISTORIC_REMIT HistoricRemitToData, List<TB_ALIAS> aliasDataList, List<TB_EMAIL_BACKUP> emailDataList)
        {
            //Get UTC timezone and convert it to UTC-6 Costa Rica local time
            var dateTimeUTC = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
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

                            // Check if template contains a Highlight

                            if (existingTemplate.FK_TB_HIGHLIGHTS_ID != null)
                            {
                                //If highlights exists validate if there is changed and create a new one
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

                            }
                            else
                            {
                                //If there is not a highlight and there is data create a new one
                                if (HighLightsData.HIGHLIGHTS != null
                                || HighLightsData.HIGHLIGHTS_COMMENTS != null
                                || HighLightsData.HIGHLIGHTS_INSTRUCTIONS != null
                                || HighLightsData.HIGHLIGHTS_EXCEPTIONS != null
                                || HighLightsData.HIGHLIGHTS_COMMON_ISSUES != null
                                || HighLightsData.HIGHLIGHTS_SUPPLIER_AGENCY != null
                                || HighLightsData.HIGHLIGHTS_TEMPLATE_COMMENTS != null)
                                {
                                    HighLightsData.FK_TB_TEMPLATE_ID = existingTemplate.TEMP_ID;
                                    HighLightsData.HIGHLIGHTS_DATE = targetTime;
                                    db.TB_HIGHLIGHTS.Add(HighLightsData);
                                }
                            }

                            // Update historic remit data if there are changes

                            if (existingTemplate.FK_TB_TEMPLATE_HISTORIC_REMIT_ID != null)
                            {
                                //If there is there is a historic remit create a new one
                                if (HistoricRemitToData.HISTORIC_REMIT_INFO != existingTemplate.TB_HISTORIC_REMIT1.HISTORIC_REMIT_INFO)
                                {
                                    HistoricRemitToData.FK_TB_TEMPLATE_ID = existingTemplate.TEMP_ID;
                                    HistoricRemitToData.HISTORIC_REMIT_DATE = targetTime;
                                    db.TB_HISTORIC_REMIT.Add(HistoricRemitToData);
                                }

                            }
                            else
                            {
                                // If there is not a historic remit create a new one
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
    }
}