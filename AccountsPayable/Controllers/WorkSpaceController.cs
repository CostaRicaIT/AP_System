using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccountsPayable.Models;
using System.Linq.Dynamic.Core;
using System.Data.Entity.Core.Metadata.Edm;

namespace AccountsPayable.Controllers
{
    public class WorkSpaceController : Controller
    {
        private Accounts_Payable_Entities db = new Accounts_Payable_Entities();

        // GET: WorkSpace
        public ActionResult Index()
        {
            var userPermission = Session["Permission"] as TB_VIEW_PERMISSIONS;
            // Check user permission to access Template creation only Standard user should be able to access this view
            if (userPermission == null || (userPermission.FK_TB_LOGIN_ROLE_ID != 2 && userPermission.FK_TB_LOGIN_ROLE_ID != 3))
            {
                // Close session and redirect to login page
                Session.Abandon();
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }
        public JsonResult GetWorkspaceData()
        {
            try
            {
                var draw = Request.Form["draw"];
                var start = Request.Form["start"];
                var length = Request.Form["length"];
                var sortColumnIndex = Request.Form["order[0][column]"];
                var sortColumn = Request.Form["columns[" + sortColumnIndex + "][name]"];
                var sortColumnDirection = Request.Form["order[0][dir]"];
                var searchValue = Request.Form["search[value]"];

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Fetch data from database
                var workspaceData = db.TB_WORKSPACE.Where(t => t.WS_ISDISABLED == 0);

                // Apply search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    workspaceData = workspaceData.Where(m =>
                        m.TB_ALIAS.ALIAS_NAME.Contains(searchValue) // Check if any alias matches the search value
                        || m.WS_TEMP_FOLDER.Contains(searchValue)
                        || m.WS_TEMP_TAX_ID.Contains(searchValue)
                        || m.WS_TEMP_SUPPLIER_NAME.Contains(searchValue)
                        || m.WS_TEMP_SUPPLIER_NUMBER.Contains(searchValue)
                        || m.WS_TEMP_REMIT_TO.Contains(searchValue)
                        || m.WS_TEMP_SUPPLIER_SITE.Contains(searchValue)
                        || m.TB_HISTORIC_REMIT.HISTORIC_REMIT_INFO.Contains(searchValue)
                        || m.WS_TEMP_VENDOR_ACCOUNT.Contains(searchValue)
                        || m.TB_ORACLE_SOURCE.ORACLE_SOURCE_DESCRIPTION.Contains(searchValue)
                        || m.WS_TEMP_INVOICE_FORMAT.Contains(searchValue)
                        || m.WS_TEMP_INVOICE_TYPE.Contains(searchValue)
                        || m.WS_TEMP_INVOICE_NOTES.Contains(searchValue)
                        || m.WS_TEMP_W9_W8.Contains(searchValue)
                        || m.WS_TEMP_VSU.Contains(searchValue)
                        || m.WS_TEMP_PAYMENT_METHOD.Contains(searchValue)
                        || m.WS_TEMP_REMIT_TOACCOUNT.Contains(searchValue)
                        || m.TB_ORACLE_PAY_TERMS.PAY_TERMS_DESCRIPTION.Contains(searchValue)
                        || m.WS_TEMP_BILLING_PERIOD.Contains(searchValue)
                        || m.WS_TEMP_BILLING_PERIOD_DATE.Contains(searchValue)
                        || m.WS_TEMP_DISTRIBUTION_SET.Contains(searchValue)
                        || m.WS_TEMP_DISTRIBUTION_COMBINATION.Contains(searchValue)
                        || m.WS_TEMP_ACCOUNTING_DATE.Contains(searchValue)
                        || m.TB_ORACLE_LEGAL_ENTITIES.LEGAL_ENTITY_NAME.Contains(searchValue)
                        || m.TB_ORACLE_ORGANIZATION_TYPE.ORGANIZATION_TYPE_NAME.Contains(searchValue)
                        || m.WS_TEMP_TAXPAYER_ID.Contains(searchValue)
                        || m.WS_TEMP_INVOICE_TYPE.Contains(searchValue)
                        || m.WS_TEMP_INVOICE_DESCRIPTION.Contains(searchValue)
                        || m.WS_TEMP_ORACLE_NOTES.Contains(searchValue)
                        || m.WS_TEMP_ORACLE_INSTRUCTIONS.Contains(searchValue)
                        || m.WS_CONTACTS_CURRENT.Contains(searchValue)
                        || m.WS_CONTACTS_PRIOR.Contains(searchValue)
                        || m.TB_APPROVER.APPROVER_NAME.Contains(searchValue)
                        || m.WS_TEMP_APPROVER_COMMENTS.Contains(searchValue)
                        || m.TB_EMAIL_BACKUP.EMAIL_BACKUP.Contains(searchValue));

                }

                // Apply sorting
                workspaceData = ApplySorting(workspaceData, sortColumn, sortColumnDirection);

                // Paging after sorting
                var data = workspaceData.Skip(skip).Take(pageSize).ToList();

                recordsTotal = workspaceData.Count(); // Count after filtering



                // Map entities to DTOs
                var workspaceDtos = data.Where(m => m.WS_ISDISABLED == 0)
                    .Select(m => new WorkspaceDto
                    {
                        WS_Id = m.WS_ID,
                        Ws_duedate = m.WS_DUE_DATE ?? "",
                        Ws_status = m.WS_STATUS ?? "",
                        Ws_reason = m.WS_REASON ?? "",
                        Ws_email_received = m.WS_EMAIL_RECEIVED ?? "",
                        Ws_created_date = m.WS_CREATED_DATE ?? "",
                        Ws_source = m.WS_SOURCE ?? "",
                        Ws_handled_by = m.WS_HANDLED_BY ?? "",
                        Ws_invoice_date = m.WS_INVOICE_DATE ?? "",
                        Ws_amount = m.WS_AMOUNT ?? "",
                        Ws_invoice_number = m.WS_INVOICE_NUMBER ?? "",
                        Ws_comments = m.WS_COMMENTS?.WORKSPACE_INFO ?? "",
                        Ws_last_actions = m.WS_LAST_ACTIONS?.LAST_ACTIONS_INFO ?? "",
                        Id = m.TEMP_ID,
                        Alias = m.TB_ALIAS?.ALIAS_NAME ?? "",
                        Folder = m.WS_TEMP_FOLDER,
                        TempTaxId = m.WS_TEMP_TAX_ID ?? "",
                        TempSupplierName = m.WS_TEMP_SUPPLIER_NAME ?? "",
                        TempSupplierNumber = m.WS_TEMP_SUPPLIER_NUMBER ?? "",
                        RemitTo = m.WS_TEMP_REMIT_TO ?? "",
                        SupplierSite = m.WS_TEMP_SUPPLIER_SITE ?? "",
                        HistoricRemitTo = m.TB_HISTORIC_REMIT?.HISTORIC_REMIT_INFO ?? "",
                        VendorAccount = m.WS_TEMP_VENDOR_ACCOUNT ?? "",
                        Source = m.TB_ORACLE_SOURCE?.ORACLE_SOURCE_DESCRIPTION ?? "",
                        InvoiceFormat = m.WS_TEMP_INVOICE_FORMAT ?? "",
                        InvoiceType = m.WS_TEMP_INVOICE_TYPE ?? "",
                        InvoiceNotes = m.WS_TEMP_INVOICE_NOTES ?? "",
                        W9W8BENForm = m.WS_TEMP_W9_W8 ?? "",
                        VSUForm = m.WS_TEMP_VSU ?? "",
                        PaymentMethod = m.WS_TEMP_PAYMENT_METHOD ?? "",
                        RemitToAccount = m.WS_TEMP_REMIT_TOACCOUNT ?? "",
                        PayTerms = m.TB_ORACLE_PAY_TERMS?.PAY_TERMS_DESCRIPTION ?? "",
                        InvoiceDescription = m.WS_TEMP_INVOICE_DESCRIPTION ?? "",
                        BillingPeriod = m.WS_TEMP_BILLING_PERIOD ?? "",
                        Dates = m.WS_TEMP_BILLING_PERIOD_DATE ?? "",
                        DistributionSet = m.WS_TEMP_DISTRIBUTION_SET ?? "",
                        DistributionCombination = m.WS_TEMP_DISTRIBUTION_COMBINATION ?? "",
                        AccountingDate = m.WS_TEMP_ACCOUNTING_DATE ?? "",
                        LegalEntity = m.TB_ORACLE_LEGAL_ENTITIES?.LEGAL_ENTITY_NAME ?? "",
                        OrganizationType = m.TB_ORACLE_ORGANIZATION_TYPE?.ORGANIZATION_TYPE_NAME ?? "",
                        TaxPayerID = m.WS_TEMP_TAXPAYER_ID ?? "",
                        Type = m.TB_ORACLE_TYPE.ORACLE_TYPE_NAME ?? "",
                        Description = m.WS_TEMP_ORACLE_DESCRIPTION ?? "",
                        OracleNotes = m.WS_TEMP_ORACLE_NOTES ?? "",
                        OracleInstructions = m.WS_TEMP_ORACLE_INSTRUCTIONS ?? "",
                        ARKeyContactsCurrent = m.WS_CONTACTS_CURRENT ?? "",
                        ARKeyContactsPrior = m.WS_CONTACTS_PRIOR ?? "",
                        Approver = m.TB_APPROVER?.APPROVER_NAME ?? "",
                        ApproverComments = m.WS_TEMP_APPROVER_COMMENTS ?? "",
                        EmailBackup = m.TB_EMAIL_BACKUP?.EMAIL_BACKUP ?? ""
                    }).ToList();

                // Prepare JSON response
                var jsonData = new
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    UserRoleId = ((TB_VIEW_PERMISSIONS)Session["Permission"]).FK_TB_LOGIN_ROLE_ID,
                    data = workspaceDtos, // Use DTOs instead of entities,
                };

                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log exception here
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        // Helper function for dynamic sorting
        private IQueryable<TB_WORKSPACE> ApplySorting(IQueryable<TB_WORKSPACE> workspaceData, string sortColumn, string sortDirection)
        {
            // Validate sortColumn to avoid SQL Injection (create a whitelist of allowed columns)
            if (!string.IsNullOrWhiteSpace(sortColumn))
            {
                if (sortDirection.Equals("asc", StringComparison.OrdinalIgnoreCase))
                {
                    workspaceData = workspaceData.OrderBy(sortColumn); // Default sorting
                }
                else
                {
                    workspaceData = workspaceData.OrderBy($"{sortColumn} descending");

                }
            }
            else
            {
                string defaultSort = "WS_ID";
                workspaceData = workspaceData.OrderBy($"{defaultSort} descending");
            }
            return workspaceData;
        }
        // GET: WorkSpace/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TB_WORKSPACE tB_WORKSPACE = db.TB_WORKSPACE.Find(id);
            if (tB_WORKSPACE == null)
            {
                return HttpNotFound();
            }
            var TempID = tB_WORKSPACE.TEMP_ID;
            // Retrieve the associated TB_TEMPLATE object
            TB_TEMPLATE tB_TEMPLATE = db.TB_TEMPLATE.FirstOrDefault(t => t.TEMP_ID == tB_WORKSPACE.TEMP_ID);
            if (tB_TEMPLATE == null)
            {
                return HttpNotFound();
            }

            // Pass the data as a tuple to the view
            var model = new Tuple<TB_WORKSPACE, TB_TEMPLATE>(tB_WORKSPACE, tB_TEMPLATE);

            // Prepare viewbags as before
            var emailBackupList = db.TB_EMAIL_BACKUP
                .Where(a => a.FK_TB_TEMPLATE_ID == TempID)
                .OrderByDescending(e => e.EMAIL_BACKUP_DATE)
                .AsEnumerable()
                .Select(e => new
                {
                    EMAIL_BACKUP_ID = e.EMAIL_BACKUP_ID,
                    EMAIL_BACKUP_DATE = e.EMAIL_BACKUP_DATE.ToString("MM/dd/yyyy hh:mm tt")
                }).ToList();

            var historicRemitToList = db.TB_HISTORIC_REMIT
                .Where(x => x.FK_TB_TEMPLATE_ID == TempID)
                .OrderByDescending(e => e.HISTORIC_REMIT_DATE)
                .AsEnumerable()
                .Select(e => new
                {
                    HISTORIC_REMIT_ID = e.HISTORIC_REMIT_ID,
                    HISTORIC_REMIT_DATE = e.HISTORIC_REMIT_DATE.ToString("MM/dd/yyyy hh:mm tt")
                }).ToList();

            var HighLightsToList = db.TB_HIGHLIGHTS
                .Where(e => e.FK_TB_TEMPLATE_ID == TempID)
                .OrderByDescending(e => e.HIGHLIGHTS_DATE)
                .AsEnumerable()
                .Select(e => new
                {
                    HIGHLIGHTS_ID = e.HIGHLIGHTS_ID,
                    HIGHLIGHTS_DATE = e.HIGHLIGHTS_DATE.ToString("MM/dd/yyyy hh:mm tt")
                }).ToList();

            var aliasesForTemplate = db.TB_ALIAS
                .Where(a => a.FK_TB_TEMPLATE_ID == TempID)
                .OrderByDescending(a => a.ALIAS_NAME)
                .AsEnumerable()
                .Select(a => new
                {
                    ALIAS_ID = a.ALIAS_ID,
                    ALIAS_NAME = a.ALIAS_NAME
                }).ToList();
            //Get data for Comments
            var commentsToList = db.WS_COMMENTS
                .Where(x => x.FK_WS_WORKSPACE_ID == id)
                .OrderByDescending(e => e.WORKSPACE_DATE)
                .AsEnumerable()
                .Select(e => new
                {
                    COMMENTS_ID = e.COMMENTS_ID,
                    WORKSPACE_DATE = e.WORKSPACE_DATE.ToString("MM/dd/yyyy hh:mm tt")
                }).ToList();
            //Get data for LastActions
            var lastActionsToList = db.WS_LAST_ACTIONS
                .Where(x => x.FK_WS_WORKSPACE_ID == id)
                .OrderByDescending(e => e.LAST_ACTIONS_DATE)
                .AsEnumerable()
                .Select(e => new
                {
                    LAST_ACTIONS_ID = e.LAST_ACTIONS_ID,
                    LAST_ACTIONS_DATE = e.LAST_ACTIONS_DATE.ToString("MM/dd/yyyy hh:mm tt")
                }).ToList();
            ViewBag.WS_FK_TB_APPROVER_ID = new SelectList(db.TB_APPROVER, "APPROVER_ID", "APPROVER_NAME", tB_WORKSPACE.WS_FK_TB_APPROVER_ID);
            ViewBag.WS_FK_TB_TEMPLATE_ALIAS_ID = new SelectList(aliasesForTemplate, "ALIAS_ID", "ALIAS_NAME");
            ViewBag.FK_TB_TEMPLATE_HISTORIC_REMIT_ID = new SelectList(historicRemitToList, "HISTORIC_REMIT_ID", "HISTORIC_REMIT_DATE", tB_WORKSPACE.WS_FK_TB_TEMPLATE_HISTORIC_REMIT_ID);
            ViewBag.WS_FK_TB_EMAIL_BACKUP_ID = new SelectList(emailBackupList, "EMAIL_BACKUP_ID", "EMAIL_BACKUP_DATE");
            ViewBag.FK_TB_HIGHLIGHTS = new SelectList(HighLightsToList, "HIGHLIGHTS_ID", "HIGHLIGHTS_DATE", tB_WORKSPACE.WS_FK_TB_HIGHLIGHTS_ID);
            //ViewBag.WS_FK_TB_HIGHLIGHTS = new SelectList(HighLightsToList, "HIGHLIGHTS_ID", "HIGHLIGHTS_DATE");
            ViewBag.WS_FK_TB_ORACLE_SOURCE_ID = new SelectList(db.TB_ORACLE_SOURCE, "ORACLE_SOURCE_ID", "ORACLE_SOURCE_DESCRIPTION", tB_WORKSPACE.WS_FK_TB_ORACLE_SOURCE_ID);
            ViewBag.WS_FK_TB_ORACLE_PAY_TERMS_ID = new SelectList(db.TB_ORACLE_PAY_TERMS, "PAY_TERMS_ID", "PAY_TERMS_DESCRIPTION", tB_WORKSPACE.WS_FK_TB_ORACLE_PAY_TERMS_ID);
            ViewBag.WS_FK_TB_LEGAL_ENTITY_ID = new SelectList(db.TB_ORACLE_LEGAL_ENTITIES, "LEGAL_ENTITY_ID", "LEGAL_ENTITY_NAME", tB_WORKSPACE.WS_FK_TB_LEGAL_ENTITY_ID);
            ViewBag.WS_FK_TB_ORGANIZATION_TYPE_ID = new SelectList(db.TB_ORACLE_ORGANIZATION_TYPE, "ORGANIZATION_TYPE_ID", "ORGANIZATION_TYPE_NAME", tB_WORKSPACE.WS_FK_TB_ORGANIZATION_TYPE_ID);
            ViewBag.WS_FK_TB_ORACLE_TYPE_ID = new SelectList(db.TB_ORACLE_TYPE, "ORACLE_TYPE_ID", "ORACLE_TYPE_NAME");
            ViewBag.FK_WS_COMMENTS_ID = new SelectList(commentsToList, "COMMENTS_ID", "WORKSPACE_DATE");
            ViewBag.FK_WS_LAST_ACTIONS_ID = new SelectList(lastActionsToList, "LAST_ACTIONS_ID", "LAST_ACTIONS_DATE");
            return View(model);
        }


        // GET: WorkSpace/Create
        public ActionResult Create(int? id)
        {
            var userPermission = Session["Permission"] as TB_VIEW_PERMISSIONS;

            if (userPermission != null && userPermission.FK_TB_LOGIN_ROLE_ID == 2)
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

                // Example: Fetching workspace
                TB_WORKSPACE tB_WORKSPACE = db.TB_WORKSPACE.FirstOrDefault(x => x.TEMP_ID == id);

                if (tB_WORKSPACE == null)
                {
                    tB_WORKSPACE = new TB_WORKSPACE(); // Default or placeholder instance
                }

                // Example: Creating Tuple
                var model = new Tuple<TB_WORKSPACE, TB_TEMPLATE>(tB_WORKSPACE, tB_TEMPLATE);
                //Get data for email backup
                var emailBackupList = db.TB_EMAIL_BACKUP
                    .Where(a => a.FK_TB_TEMPLATE_ID == id)
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
                var aliasesForTemplate = db.TB_ALIAS.
                    Where(a => a.FK_TB_TEMPLATE_ID == id)
                    .OrderByDescending(a => a.ALIAS_NAME)
                    .AsEnumerable()
                    .Select(a => new
                    {
                        ALIAS_ID = a.ALIAS_ID,
                        ALIAS_NAME = a.ALIAS_NAME,
                    }).ToList();

                //Send data to view with external tables data
                ViewBag.FK_TB_APPROVER_ID = new SelectList(db.TB_APPROVER, "APPROVER_ID", "APPROVER_NAME", tB_TEMPLATE.FK_TB_APPROVER_ID);
                ViewBag.FK_TB_EMAIL_BACKUP_ID = new SelectList(emailBackupList, "EMAIL_BACKUP_ID", "EMAIL_BACKUP_DATE");
                ViewBag.FK_TB_HIGHLIGHTS = new SelectList(HighLightsToList, "HIGHLIGHTS_ID", "HIGHLIGHTS_DATE");
                ViewBag.FK_TB_LEGAL_ENTITY_ID = new SelectList(db.TB_ORACLE_LEGAL_ENTITIES, "LEGAL_ENTITY_ID", "LEGAL_ENTITY_NAME", tB_TEMPLATE.FK_TB_LEGAL_ENTITY_ID);
                ViewBag.FK_TB_ORGANIZATION_TYPE_ID = new SelectList(db.TB_ORACLE_ORGANIZATION_TYPE, "ORGANIZATION_TYPE_ID", "ORGANIZATION_TYPE_NAME", tB_TEMPLATE.FK_TB_ORGANIZATION_TYPE_ID);
                ViewBag.FK_TB_ORACLE_PAY_TERMS_ID = new SelectList(db.TB_ORACLE_PAY_TERMS, "PAY_TERMS_ID", "PAY_TERMS_DESCRIPTION", tB_TEMPLATE.FK_TB_ORACLE_PAY_TERMS_ID);
                ViewBag.FK_TB_ORACLE_SOURCE_ID = new SelectList(db.TB_ORACLE_SOURCE, "ORACLE_SOURCE_ID", "ORACLE_SOURCE_DESCRIPTION", tB_TEMPLATE.FK_TB_ORACLE_SOURCE_ID);
                ViewBag.FK_TB_ORACLE_TYPE_ID = new SelectList(db.TB_ORACLE_TYPE, "ORACLE_TYPE_ID", "ORACLE_TYPE_NAME", tB_TEMPLATE.FK_TB_ORACLE_TYPE_ID);
                ViewBag.FK_TB_TEMPLATE_ALIAS_ID = new SelectList(aliasesForTemplate, "ALIAS_ID", "ALIAS_NAME");
                ViewBag.FK_TB_TEMPLATE_HISTORIC_REMIT_ID = new SelectList(historicRemitToList, "HISTORIC_REMIT_ID", "HISTORIC_REMIT_DATE");

                return View(model);
            }
            else
            {
                return View("Error");
            }


        }

        // GET: WorkSpace/Edit/5
        public ActionResult Edit(int? id)
        {
            //Check if user haves access to module
            var userPermission = Session["Permission"] as TB_VIEW_PERMISSIONS;
            // Check user permision to access Template update only Standard user should be able to access this view
            if (userPermission != null && userPermission.FK_TB_LOGIN_ROLE_ID == 2)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TB_WORKSPACE tB_WORKSPACE = db.TB_WORKSPACE.Find(id);
                if (tB_WORKSPACE == null)
                {
                    return HttpNotFound();
                }
                var TempID = tB_WORKSPACE.TEMP_ID;
                //Get data for email backup
                var emailBackupList = db.TB_EMAIL_BACKUP
                    .Where(a => a.FK_TB_TEMPLATE_ID == TempID)
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
                    .Where(x => x.FK_TB_TEMPLATE_ID == TempID)
                    .OrderByDescending(e => e.HISTORIC_REMIT_DATE)
                    .AsEnumerable()
                    .Select(e => new
                    {
                        HISTORIC_REMIT_ID = e.HISTORIC_REMIT_ID,
                        HISTORIC_REMIT_DATE = e.HISTORIC_REMIT_DATE.ToString("MM/dd/yyyy hh:mm tt")
                    }).ToList();
                //Get data for highlights
                var HighLightsToList = db.TB_HIGHLIGHTS
                    .Where(e => e.FK_TB_TEMPLATE_ID == TempID)
                    .OrderByDescending(e => e.HIGHLIGHTS_DATE)
                    .AsEnumerable()
                    .Select(e => new
                    {
                        HIGHLIGHTS_ID = e.HIGHLIGHTS_ID,
                        HIGHLIGHTS_DATE = e.HIGHLIGHTS_DATE.ToString("MM/dd/yyyy hh:mm tt"),
                    })
                    .ToList();

                //Get data for alias
                var aliasesForTemplate = db.TB_ALIAS.
                    Where(a => a.FK_TB_TEMPLATE_ID == TempID)
                    .OrderByDescending(a => a.ALIAS_NAME)
                    .AsEnumerable()
                    .Select(a => new
                    {
                        ALIAS_ID = a.ALIAS_ID,
                        ALIAS_NAME = a.ALIAS_NAME,
                    }).ToList();
                //Get data for Comments
                var commentsToList = db.WS_COMMENTS
                    .Where(x => x.FK_WS_WORKSPACE_ID == id)
                    .OrderByDescending(e => e.WORKSPACE_DATE)
                    .AsEnumerable()
                    .Select(e => new
                    {
                        COMMENTS_ID = e.COMMENTS_ID,
                        WORKSPACE_DATE = e.WORKSPACE_DATE.ToString("MM/dd/yyyy hh:mm tt")
                    }).ToList();
                //Get data for LastActions
                var lastActionsToList = db.WS_LAST_ACTIONS
                    .Where(x => x.FK_WS_WORKSPACE_ID == id)
                    .OrderByDescending(e => e.LAST_ACTIONS_DATE)
                    .AsEnumerable()
                    .Select(e => new
                    {
                        LAST_ACTIONS_ID = e.LAST_ACTIONS_ID,
                        LAST_ACTIONS_DATE = e.LAST_ACTIONS_DATE.ToString("MM/dd/yyyy hh:mm tt")
                    }).ToList();

                ViewBag.WS_FK_TB_APPROVER_ID = new SelectList(db.TB_APPROVER, "APPROVER_ID", "APPROVER_NAME", tB_WORKSPACE.WS_FK_TB_APPROVER_ID);
                ViewBag.FK_TB_HIGHLIGHTS = new SelectList(HighLightsToList, "HIGHLIGHTS_ID", "HIGHLIGHTS_DATE", tB_WORKSPACE.WS_FK_TB_HIGHLIGHTS_ID);
                ViewBag.WS_FK_TB_LEGAL_ENTITY_ID = new SelectList(db.TB_ORACLE_LEGAL_ENTITIES, "LEGAL_ENTITY_ID", "LEGAL_ENTITY_NAME", tB_WORKSPACE.WS_FK_TB_LEGAL_ENTITY_ID);
                ViewBag.WS_FK_TB_ORACLE_PAY_TERMS_ID = new SelectList(db.TB_ORACLE_PAY_TERMS, "PAY_TERMS_ID", "PAY_TERMS_DESCRIPTION", tB_WORKSPACE.WS_FK_TB_ORACLE_PAY_TERMS_ID);
                ViewBag.WS_FK_TB_ORACLE_SOURCE_ID = new SelectList(db.TB_ORACLE_SOURCE, "ORACLE_SOURCE_ID", "ORACLE_SOURCE_DESCRIPTION", tB_WORKSPACE.WS_FK_TB_ORACLE_SOURCE_ID);
                ViewBag.WS_FK_TB_ORACLE_TYPE_ID = new SelectList(db.TB_ORACLE_TYPE, "ORACLE_TYPE_ID", "ORACLE_TYPE_NAME", tB_WORKSPACE.WS_FK_TB_ORACLE_TYPE_ID);
                ViewBag.WS_FK_TB_TEMPLATE_ALIAS_ID = new SelectList(aliasesForTemplate, "ALIAS_ID", "ALIAS_NAME");
                ViewBag.FK_TB_TEMPLATE_HISTORIC_REMIT_ID = new SelectList(historicRemitToList, "HISTORIC_REMIT_ID", "HISTORIC_REMIT_DATE", tB_WORKSPACE.WS_FK_TB_TEMPLATE_HISTORIC_REMIT_ID);
                ViewBag.WS_FK_TB_EMAIL_BACKUP_ID = new SelectList(emailBackupList, "EMAIL_BACKUP_ID", "EMAIL_BACKUP_DATE");
                ViewBag.WS_FK_TB_ORGANIZATION_TYPE_ID = new SelectList(db.TB_ORACLE_ORGANIZATION_TYPE, "ORGANIZATION_TYPE_ID", "ORGANIZATION_TYPE_NAME");
                ViewBag.FK_WS_COMMENTS_ID = new SelectList(commentsToList, "COMMENTS_ID", "WORKSPACE_DATE");
                ViewBag.FK_WS_LAST_ACTIONS_ID = new SelectList(lastActionsToList, "LAST_ACTIONS_ID", "LAST_ACTIONS_DATE");
                return View(tB_WORKSPACE);
            }
            else
            {
                return View("Error");
            }
        }

    }
}