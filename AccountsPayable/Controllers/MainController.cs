using AccountsPayable.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
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
            if (userPermission != null && userPermission.FK_TB_LOGIN_ROLE_ID == 2 || userPermission.FK_TB_LOGIN_ROLE_ID == 3)
            {
                //Get data for dashboard
                var tB_TEMPLATE = db.TB_TEMPLATE
                .Include(t => t.TB_APPROVER)
                .Include(t => t.TB_HIGHLIGHTS)
                .Include(t => t.TB_ORACLE_LEGAL_ENTITIES)
                .Include(t => t.TB_ORACLE_ORGANIZATION_TYPE)
                .Include(t => t.TB_ORACLE_PAY_TERMS)
                .Include(t => t.TB_ORACLE_SOURCE)
                .Include(t => t.TB_ORACLE_TYPE)
                .Where(t => t.TEMP_ISDISABLED == 0);
                var templates = tB_TEMPLATE.ToList();
                //get id of each template
                var templateIds = templates.Select(t => t.TEMP_ID).ToList();
                //Query to get the data
                var templateData = db.TB_TEMPLATE
                       .Where(t => templateIds.Contains(t.TEMP_ID))
                       .Select(t => new
                       {
                           Template = t,
                           Alias = t.TB_ALIAS.FirstOrDefault(a => a.ALIAS_ID == t.FK_TB_TEMPLATE_ALIAS_ID),
                           EmailBackup = t.TB_EMAIL_BACKUP.FirstOrDefault(e => e.EMAIL_BACKUP_ID == t.FK_TB_EMAIL_BACKUP_ID),
                           HistoricRemit = t.TB_HISTORIC_REMIT.FirstOrDefault(e => e.HISTORIC_REMIT_ID == t.FK_TB_TEMPLATE_HISTORIC_REMIT_ID)
                       })
                       .ToList();
                //Join data of email and alias to template
                //If model is recreated due to db change ALIAS_NAME and EMAIL_BACKCUP HISTORIC_REMIT properties need to be recreated using Generate property option on VS
                foreach (var template in templates)
                {
                    var data = templateData.FirstOrDefault(t => t.Template.TEMP_ID == template.TEMP_ID);
                    template.ALIAS_NAME = data?.Alias?.ALIAS_NAME;
                    template.EMAIL_BACKUP = data?.EmailBackup?.EMAIL_BACKUP;
                    template.HISTORIC_REMIT = data?.HistoricRemit?.HISTORIC_REMIT_INFO;
                }
                return View(templates);
            }
            else
            {
                return View("Error");
            }
        }
        public ActionResult Create()
        {

            //Check if user haves access to module
            var userPermission = Session["Permission"] as TB_VIEW_PERMISSIONS;
            // Check user permision to access Template creation only Standard user should be able to access this view
            if (userPermission != null && userPermission.FK_TB_LOGIN_ROLE_ID == 2)
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
            if (userPermission != null && userPermission.FK_TB_LOGIN_ROLE_ID == 2 || userPermission.FK_TB_LOGIN_ROLE_ID == 3)
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
                var aliasesForTemplate = db.TB_ALIAS.Where(a => a.TB_TEMPLATE.Any(t => t.TEMP_ID == id)).ToList();
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
    }
}