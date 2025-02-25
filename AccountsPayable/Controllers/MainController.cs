using AccountsPayable.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Linq.Dynamic.Core;
namespace AccountsPayable.Controllers
{
    public class MainController : Controller
    {
        private Accounts_Payable_Entities db = new Accounts_Payable_Entities();
        // GET: Main
        public ActionResult Index()
        {
            var userPermission = Session["Permission"] as TB_VIEW_PERMISSIONS;
            // Check user permision to access Template creation only Standard user should be able to access this view
            if (userPermission != null && userPermission.FK_TB_LOGIN_ROLE_ID == 2 || userPermission.FK_TB_LOGIN_ROLE_ID == 3 || userPermission.FK_TB_LOGIN_ROLE_ID == 4 || userPermission.FK_TB_LOGIN_ROLE_ID == 5)
            {
                return View();
            }
            else
            {
                return View("Error");
            }
        }
        public JsonResult GetTemplateData()
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
                var templateData = db.TB_TEMPLATE.Where(t => t.TEMP_ISDISABLED == 0);

                // Apply search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    templateData = templateData.Where(m =>
                        m.TB_ALIAS1.ALIAS_NAME.Contains(searchValue) // Check if any alias matches the search value
                        || m.TEMP_FOLDER.Contains(searchValue)
                        || m.TEMP_TAX_ID.Contains(searchValue)
                        || m.TEMP_SUPPLIER_NAME.Contains(searchValue)
                        || m.TEMP_SUPPLIER_NUMBER.Contains(searchValue)
                        || m.TEMP_REMIT_TO.Contains(searchValue)
                        || m.TEMP_SUPPLIER_SITE.Contains(searchValue)
                        || m.TB_HISTORIC_REMIT1.HISTORIC_REMIT_INFO.Contains(searchValue)
                        || m.TEMP_VENDOR_ACCOUNT.Contains(searchValue)
                        || m.TB_ORACLE_SOURCE.ORACLE_SOURCE_DESCRIPTION.Contains(searchValue)
                        || m.TEMP_INVOICE_FORMAT.Contains(searchValue)
                        || m.TEMP_INVOICE_TYPE.Contains(searchValue)
                        || m.TEMP_INVOICE_NOTES.Contains(searchValue)
                        || m.TEMP_W9_W8.Contains(searchValue)
                        || m.TEMP_VSU.Contains(searchValue)
                        || m.TEMP_PAYMENT_METHOD.Contains(searchValue)
                        || m.TEMP_REMIT_TOACCOUNT.Contains(searchValue)
                        || m.TB_ORACLE_PAY_TERMS.PAY_TERMS_DESCRIPTION.Contains(searchValue)
                        || m.TEMP_BILLING_PERIOD.Contains(searchValue)
                        || m.TEMP_BILLING_PRERIOD_DATE.Contains(searchValue)
                        || m.TEMP_DISTRIBUTION_SET.Contains(searchValue)
                        || m.TEMP_DISTRIBUTION_COMBINATION.Contains(searchValue)
                        || m.TEMP_ACCOUNTING_DATE.Contains(searchValue)
                        || m.TB_ORACLE_LEGAL_ENTITIES.LEGAL_ENTITY_NAME.Contains(searchValue)
                        || m.TB_ORACLE_ORGANIZATION_TYPE.ORGANIZATION_TYPE_NAME.Contains(searchValue)
                        || m.TEMP_TAXPAYER_ID.Contains(searchValue)
                        || m.TEMP_INVOICE_TYPE.Contains(searchValue)
                        || m.TEMP_INVOICE_DESCRIPTION.Contains(searchValue)
                        || m.TEMP_ORACLE_NOTES.Contains(searchValue)
                        || m.TEMP_ORACLE_INSTRUCTIONS.Contains(searchValue)
                        || m.CONTACTS_CURRENT.Contains(searchValue)
                        || m.CONTACTS_PRIOR.Contains(searchValue)
                        || m.TB_APPROVER.APPROVER_NAME.Contains(searchValue)
                        || m.TEMP_APPROVER_COMMENTS.Contains(searchValue)
                        || m.TB_EMAIL_BACKUP1.EMAIL_BACKUP.Contains(searchValue));

                }

                // Apply sorting
                templateData = ApplySorting(templateData, sortColumn, sortColumnDirection);

                // Paging after sorting
                var data = templateData.Skip(skip).Take(pageSize).ToList();

                recordsTotal = templateData.Count(); // Count after filtering



                // Map entities to DTOs
                var templateDtos = data.Where(m => m.TEMP_ISDISABLED == 0)
                    .Select(m => new TemplateDto
                    {
                        Id = m.TEMP_ID,
                        Alias = m.TB_ALIAS1?.ALIAS_NAME ?? "",
                        Folder = m.TEMP_FOLDER,
                        TempTaxId = m.TEMP_TAX_ID ?? "",
                        TempSupplierName = m.TEMP_SUPPLIER_NAME ?? "",
                        TempSupplierNumber = m.TEMP_SUPPLIER_NUMBER ?? "",
                        RemitTo = m.TEMP_REMIT_TO ?? "",
                        SupplierSite = m.TEMP_SUPPLIER_SITE ?? "",
                        HistoricRemitTo = m.TB_HISTORIC_REMIT1?.HISTORIC_REMIT_INFO ?? "",
                        VendorAccount = m.TEMP_VENDOR_ACCOUNT ?? "",
                        Source = m.TB_ORACLE_SOURCE.ORACLE_SOURCE_DESCRIPTION ?? "",
                        InvoiceFormat = m.TEMP_INVOICE_FORMAT ?? "",
                        InvoiceType = m.TEMP_INVOICE_TYPE ?? "",
                        InvoiceNotes = m.TEMP_INVOICE_NOTES ?? "",
                        W9W8BENForm = m.TEMP_W9_W8 ?? "",
                        VSUForm = m.TEMP_VSU ?? "",
                        PaymentMethod = m.TEMP_PAYMENT_METHOD ?? "",
                        RemitToAccount = m.TEMP_REMIT_TOACCOUNT ?? "",
                        PayTerms = m.TB_ORACLE_PAY_TERMS.PAY_TERMS_DESCRIPTION ?? "",
                        InvoiceDescription = m.TEMP_INVOICE_DESCRIPTION ?? "",
                        BillingPeriod = m.TEMP_BILLING_PERIOD ?? "",
                        Dates = m.TEMP_BILLING_PRERIOD_DATE ?? "",
                        DistributionSet = m.TEMP_DISTRIBUTION_SET ?? "",
                        DistributionCombination = m.TEMP_DISTRIBUTION_COMBINATION ?? "",
                        AccountingDate = m.TEMP_ACCOUNTING_DATE ?? "",
                        LegalEntity = m.TB_ORACLE_LEGAL_ENTITIES.LEGAL_ENTITY_NAME ?? "",
                        OrganizationType = m.TB_ORACLE_ORGANIZATION_TYPE?.ORGANIZATION_TYPE_NAME ?? "",
                        TaxPayerID = m.TEMP_TAXPAYER_ID ?? "",
                        Type = m.TB_ORACLE_TYPE.ORACLE_TYPE_NAME ?? "",
                        Description = m.TEMP_ORACLE_DESCRIPTION ?? "",
                        OracleNotes = m.TEMP_ORACLE_NOTES ?? "",
                        OracleInstructions = m.TEMP_ORACLE_INSTRUCTIONS ?? "",
                        ARKeyContactsCurrent = m.CONTACTS_CURRENT ?? "",
                        ARKeyContactsPrior = m.CONTACTS_PRIOR ?? "",
                        Approver = m.TB_APPROVER.APPROVER_NAME ?? "",
                        ApproverComments = m.TEMP_APPROVER_COMMENTS ?? "",
                        EmailBackup = m.TB_EMAIL_BACKUP1?.EMAIL_BACKUP ?? ""
                    }).ToList();

                // Prepare JSON response
                var jsonData = new
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    UserRoleId = ((TB_VIEW_PERMISSIONS)Session["Permission"]).FK_TB_LOGIN_ROLE_ID,
                    data = templateDtos, // Use DTOs instead of entities,
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
        private IQueryable<TB_TEMPLATE> ApplySorting(IQueryable<TB_TEMPLATE> templateData, string sortColumn, string sortDirection)
        {
            // Validate sortColumn to avoid SQL Injection (create a whitelist of allowed columns)
            if (!string.IsNullOrWhiteSpace(sortColumn))
            {
                if (sortDirection.Equals("asc", StringComparison.OrdinalIgnoreCase))
                {
                    templateData = templateData.OrderBy(sortColumn); // Default sorting
                }
                else
                {
                    templateData = templateData.OrderBy($"{sortColumn} descending");

                }
            }
            else
            {
                string defaultSort = "TEMP_ID";
                templateData = templateData.OrderBy($"{defaultSort} descending");
            }
            return templateData;
        }

        public ActionResult Create()
        {

            //Check if user haves access to module
            var userPermission = Session["Permission"] as TB_VIEW_PERMISSIONS;
            // Check user permision to access Template creation only Standard user should be able to access this view
            if (userPermission != null && userPermission.FK_TB_LOGIN_ROLE_ID == 2 || userPermission.FK_TB_LOGIN_ROLE_ID == 4)
            {
                //Get data for dropdowns
                ViewBag.FK_TB_APPROVER_ID = new SelectList(db.TB_APPROVER, "APPROVER_ID", "APPROVER_NAME");
                ViewBag.FK_TB_LEGAL_ENTITY_ID = new SelectList(db.TB_ORACLE_LEGAL_ENTITIES, "LEGAL_ENTITY_ID", "LEGAL_ENTITY_NAME");
                ViewBag.FK_TB_ORACLE_PAY_TERMS_ID = new SelectList(db.TB_ORACLE_PAY_TERMS, "PAY_TERMS_ID", "PAY_TERMS_DESCRIPTION");
                ViewBag.FK_TB_ORACLE_SOURCE_ID = new SelectList(db.TB_ORACLE_SOURCE, "ORACLE_SOURCE_ID", "ORACLE_SOURCE_DESCRIPTION");
                ViewBag.FK_TB_ORACLE_TYPE_ID = new SelectList(db.TB_ORACLE_TYPE, "ORACLE_TYPE_ID", "ORACLE_TYPE_NAME");
                ViewBag.FK_TB_ORGANIZATION_TYPE_ID = new SelectList(db.TB_ORACLE_ORGANIZATION_TYPE, "ORGANIZATION_TYPE_ID", "ORGANIZATION_TYPE_NAME");
                return View();
            }
            else { return View("Error"); }

        }
        public ActionResult Edit(int? id)
        {
            //Check if user haves access to module
            var userPermission = Session["Permission"] as TB_VIEW_PERMISSIONS;
            // Check user permision to access Template update only Standard user should be able to access this view
            if (userPermission != null && userPermission.FK_TB_LOGIN_ROLE_ID == 2 || userPermission.FK_TB_LOGIN_ROLE_ID == 4)
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
                return View(tB_TEMPLATE);
            }
            else
            {
                return View("Error");
            }
        }
        public ActionResult Details(int? id)
        {
            var userPermission = Session["Permission"] as TB_VIEW_PERMISSIONS;
            // Check user permision to access Template creation only Standard user should be able to access this view
            if (userPermission != null && userPermission.FK_TB_LOGIN_ROLE_ID == 2 || userPermission.FK_TB_LOGIN_ROLE_ID == 3 || userPermission.FK_TB_LOGIN_ROLE_ID == 4 || userPermission.FK_TB_LOGIN_ROLE_ID == 5)
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
                    .Where(a => a.FK_TB_TEMPLATE_ID == id)
                    .OrderByDescending(e => e.EMAIL_BACKUP_DATE)
                    .AsEnumerable()
                    .Select(e => new
                    {
                        EMAIL_BACKUP_ID = e.EMAIL_BACKUP_ID,
                        EMAIL_BACKUP_DATE = e.EMAIL_BACKUP_DATE.ToString("MM/dd/yyyy hh:mm tt")
                    }).ToList();

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
                    }).ToList();
                //Get data for alias
                var AliasToList = db.TB_ALIAS.
                    Where(a => a.FK_TB_TEMPLATE_ID == id)
                    .OrderByDescending(a => a.ALIAS_NAME)
                    .AsEnumerable()
                    .Select(a => new
                    {
                        ALIAS_ID = a.ALIAS_ID,
                        ALIAS_NAME = a.ALIAS_NAME,
                    }).ToList();

                ViewBag.FK_TB_APPROVER_ID = new SelectList(db.TB_APPROVER, "APPROVER_ID", "APPROVER_NAME", tB_TEMPLATE.FK_TB_APPROVER_ID);
                ViewBag.FK_TB_EMAIL_BACKUP_ID = new SelectList(emailBackupList, "EMAIL_BACKUP_ID", "EMAIL_BACKUP_DATE");
                ViewBag.FK_TB_HIGHLIGHTS = new SelectList(HighLightsToList, "HIGHLIGHTS_ID", "HIGHLIGHTS_DATE");
                ViewBag.FK_TB_LEGAL_ENTITY_ID = new SelectList(db.TB_ORACLE_LEGAL_ENTITIES, "LEGAL_ENTITY_ID", "LEGAL_ENTITY_NAME", tB_TEMPLATE.FK_TB_LEGAL_ENTITY_ID);
                ViewBag.FK_TB_ORGANIZATION_TYPE_ID = new SelectList(db.TB_ORACLE_ORGANIZATION_TYPE, "ORGANIZATION_TYPE_ID", "ORGANIZATION_TYPE_NAME", tB_TEMPLATE.FK_TB_ORGANIZATION_TYPE_ID);
                ViewBag.FK_TB_ORACLE_PAY_TERMS_ID = new SelectList(db.TB_ORACLE_PAY_TERMS, "PAY_TERMS_ID", "PAY_TERMS_DESCRIPTION", tB_TEMPLATE.FK_TB_ORACLE_PAY_TERMS_ID);
                ViewBag.FK_TB_ORACLE_SOURCE_ID = new SelectList(db.TB_ORACLE_SOURCE, "ORACLE_SOURCE_ID", "ORACLE_SOURCE_DESCRIPTION", tB_TEMPLATE.FK_TB_ORACLE_SOURCE_ID);
                ViewBag.FK_TB_ORACLE_TYPE_ID = new SelectList(db.TB_ORACLE_TYPE, "ORACLE_TYPE_ID", "ORACLE_TYPE_NAME", tB_TEMPLATE.FK_TB_ORACLE_TYPE_ID);
                ViewBag.FK_TB_TEMPLATE_ALIAS_ID = new SelectList(AliasToList, "ALIAS_ID", "ALIAS_NAME");
                ViewBag.FK_TB_TEMPLATE_HISTORIC_REMIT_ID = new SelectList(historicRemitToList, "HISTORIC_REMIT_ID", "HISTORIC_REMIT_DATE");
                return View(tB_TEMPLATE);
            }

            else
            {
                return View("Error");
            }
        }
    }
}