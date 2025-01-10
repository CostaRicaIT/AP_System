using AccountsPayable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace AccountsPayable.Controllers
{
    public class WSCrudController : Controller
    {
        private Accounts_Payable_Entities db = new Accounts_Payable_Entities();

        // Helper method to strip <p> tags
        public string StripHtmlTags(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null; // This will return null for empty fields
            }

            // Replace <p> tags with an empty string
            return Regex.Replace(input, @"<p>|<\/p>", string.Empty);
        }
        // GET: WSCrud
        [HttpPost]
        public ActionResult Create(TB_WORKSPACE workspaceData, WS_COMMENTS WorkspaceCommentsData, WS_LAST_ACTIONS WorkspaceLastActionsData, TB_TEMPLATE templateData)
        {
            // Get UTC timezone and convert it to UTC-6 Costa Rica local time
            var dateTimeUTC = DateTime.UtcNow;
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            DateTime targetTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUTC, targetTimeZone);

            // Start transaction for workspace creation
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        // Retrieve the template based on TEMP_ID from the user input
                        var existingTemplate = db.TB_TEMPLATE.Find(templateData.TEMP_ID);

                        if (existingTemplate != null)
                        {
                            // Manually set user-entry fields for the workspace
                            var newWorkspace = new TB_WORKSPACE
                            {
                                // Fields entered manually by the user
                                WS_STATUS = workspaceData.WS_STATUS,
                                WS_REASON = workspaceData.WS_REASON,
                                WS_EMAIL_RECEIVED = workspaceData.WS_EMAIL_RECEIVED,
                                WS_CREATED_DATE = workspaceData.WS_CREATED_DATE,
                                WS_SOURCE = workspaceData.WS_SOURCE,
                                WS_HANDLED_BY = workspaceData.WS_HANDLED_BY,
                                WS_INVOICE_DATE = workspaceData.WS_INVOICE_DATE,
                                WS_DUE_DATE = workspaceData.WS_DUE_DATE,
                                WS_AMOUNT = workspaceData.WS_AMOUNT,
                                WS_INVOICE_NUMBER = workspaceData.WS_INVOICE_NUMBER,
                                WS_ISDISABLED = workspaceData.WS_ISDISABLED,

                                // Copy fields from TB_TEMPLATE
                                TEMP_ID = existingTemplate.TEMP_ID,
                                WS_TEMP_TAX_ID = existingTemplate.TEMP_TAX_ID,
                                WS_TEMP_REMIT_TO = existingTemplate.TEMP_REMIT_TO,
                                WS_TEMP_SUPPLIER_NAME = existingTemplate.TEMP_SUPPLIER_NAME,
                                WS_TEMP_SUPPLIER_NUMBER = existingTemplate.TEMP_SUPPLIER_NUMBER,
                                WS_FK_TB_TEMPLATE_HISTORIC_REMIT_ID = existingTemplate.FK_TB_TEMPLATE_HISTORIC_REMIT_ID,
                                WS_TEMP_VENDOR_ACCOUNT = existingTemplate.TEMP_VENDOR_ACCOUNT,
                                WS_FK_TB_TEMPLATE_ALIAS_ID = existingTemplate.FK_TB_TEMPLATE_ALIAS_ID,
                                WS_TEMP_SUPPLIER_SITE = existingTemplate.TEMP_SUPPLIER_SITE,
                                //WS_TEMP_ADDRESS = existingTemplate.TEMP_ADDRESS, //no existe en TEMPLATE REVISAR!!!
                                WS_FK_TB_LEGAL_ENTITY_ID = existingTemplate.FK_TB_LEGAL_ENTITY_ID,
                                WS_TEMP_TAXPAYER_ID = existingTemplate.TEMP_TAXPAYER_ID,
                                WS_FK_TB_ORACLE_TYPE_ID = existingTemplate.FK_TB_ORACLE_TYPE_ID,
                                WS_TEMP_ORACLE_DESCRIPTION = existingTemplate.TEMP_ORACLE_DESCRIPTION,
                                WS_FK_TB_ORACLE_PAY_TERMS_ID = existingTemplate.FK_TB_ORACLE_PAY_TERMS_ID,
                                WS_TEMP_DISTRIBUTION_SET = existingTemplate.TEMP_DISTRIBUTION_SET,
                                WS_FK_TB_ORACLE_SOURCE_ID = existingTemplate.FK_TB_ORACLE_SOURCE_ID,
                                WS_TEMP_ORACLE_NOTES = existingTemplate.TEMP_ORACLE_NOTES,
                                WS_TEMP_ORACLE_INSTRUCTIONS = existingTemplate.TEMP_ORACLE_INSTRUCTIONS,
                                WS_FK_TB_HIGHLIGHTS_ID = existingTemplate.FK_TB_HIGHLIGHTS_ID,
                                WS_FK_TB_EMAIL_BACKUP_ID = existingTemplate.FK_TB_EMAIL_BACKUP_ID,
                                WS_FK_TB_APPROVER_ID = existingTemplate.FK_TB_APPROVER_ID,
                                WS_TEMP_APPROVER_COMMENTS = existingTemplate.TEMP_APPROVER_COMMENTS,
                                WS_TEMP_INVOICE_FORMAT = existingTemplate.TEMP_INVOICE_FORMAT,
                                WS_TEMP_INVOICE_TYPE = existingTemplate.TEMP_INVOICE_TYPE,
                                WS_TEMP_ISDISABLED = existingTemplate.TEMP_ISDISABLED,
                                WS_TEMP_FOLDER = existingTemplate.TEMP_FOLDER,
                                WS_TEMP_PAYMENT_METHOD = existingTemplate.TEMP_PAYMENT_METHOD,
                                WS_TEMP_REMIT_TOACCOUNT = existingTemplate.TEMP_REMIT_TOACCOUNT,
                                WS_TEMP_BILLING_PERIOD = existingTemplate.TEMP_BILLING_PERIOD,
                                WS_TEMP_DISTRIBUTION_COMBINATION = existingTemplate.TEMP_DISTRIBUTION_COMBINATION,
                                WS_TEMP_ACCOUNTING_DATE = existingTemplate.TEMP_ACCOUNTING_DATE,
                                WS_TEMP_VSU = existingTemplate.TEMP_VSU,
                                WS_TEMP_W9_W8 = existingTemplate.TEMP_W9_W8,
                                WS_TEMP_INVOICE_NOTES = existingTemplate.TEMP_INVOICE_NOTES,
                                WS_TEMP_INVOICE_DESCRIPTION = existingTemplate.TEMP_INVOICE_DESCRIPTION,
                                WS_TEMP_BILLING_PERIOD_DATE = existingTemplate.TEMP_BILLING_PRERIOD_DATE,
                                WS_CONTACTS_CURRENT = existingTemplate.CONTACTS_CURRENT,
                                WS_CONTACTS_PRIOR = existingTemplate.CONTACTS_PRIOR,
                                WS_FK_TB_ORGANIZATION_TYPE_ID = existingTemplate.FK_TB_ORGANIZATION_TYPE_ID
                            };

                            if (WorkspaceCommentsData.WORKSPACE_INFO != null) { }
                            {
                                WorkspaceCommentsData.WORKSPACE_DATE = targetTime;
                                
                            }
                            WorkspaceCommentsData.WORKSPACE_INFO = StripHtmlTags(WorkspaceCommentsData.WORKSPACE_INFO);
                            db.WS_COMMENTS.Add(WorkspaceCommentsData);

                            if (WorkspaceLastActionsData.LAST_ACTIONS_INFO != null)
                            {
                                WorkspaceLastActionsData.LAST_ACTIONS_DATE = targetTime;
                                
                            }
                            WorkspaceLastActionsData.LAST_ACTIONS_INFO = StripHtmlTags(WorkspaceLastActionsData.LAST_ACTIONS_INFO);
                            db.WS_LAST_ACTIONS.Add(WorkspaceLastActionsData);

                            // Add the new workspace entry to the database
                            db.TB_WORKSPACE.Add(newWorkspace);

                            // Save changes to the new workspace
                            db.SaveChanges();
                            int newComments_Id = WorkspaceCommentsData.COMMENTS_ID;
                            int newLastActions_Id = WorkspaceLastActionsData.LAST_ACTIONS_ID;
                            int newWorkspaceid = newWorkspace.WS_ID;



                            if (newComments_Id != 0)
                            {
                                newWorkspace.FK_WS_COMMENTS_ID = newComments_Id;
                                WorkspaceCommentsData.FK_WS_WORKSPACE_ID = newWorkspaceid;
                            }

                            if(newLastActions_Id != 0)
                            {
                                newWorkspace.FK_WS_LAST_ACTIONS_ID = newLastActions_Id;
                                WorkspaceLastActionsData.FK_WS_WORKSPACE_ID = newWorkspaceid;
                            }
                            // Commit the transaction
                            db.SaveChanges();
                            transaction.Commit();
                            return Json(new { success = true });
                        }
                        else
                        {
                            // Handle template not found
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
                    transaction.Rollback(); // Rollback the transaction if there's an error
                    return Json(new { success = false, message = "An error occurred while saving the record: " + ex.Message });
                }
            }
        }
    }
}