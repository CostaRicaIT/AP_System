using AccountsPayable.Models;
using System.Linq;
using System.Web.Mvc;

namespace AccountsPayable.Controllers
{
    public class HistoricController : Controller
    {
        private AccountsPayableTestProdEntities db = new AccountsPayableTestProdEntities();
        [HttpGet]
        public ActionResult GetEmailText(int emailId, int id)
        {
            // Get the text based on the ID
            var historicEmailText = db.TB_EMAIL_BACKUP
                    .Where(a => a.EMAIL_BACKUP_ID == emailId)
                    .Select(item => item.EMAIL_BACKUP)
                    .FirstOrDefault();            
            //var historicEmailText = db.TB_EMAIL_BACKUP
            //        .Where(a => a.TB_TEMPLATE.Any(t => t.TEMP_ID == id) && a.EMAIL_BACKUP_ID == emailId)
            //        .Select(item => item.EMAIL_BACKUP)
            //        .FirstOrDefault();
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
        public ActionResult GetCommentsText(int commentsId, int id)
        {
            // Get the text based on the ID
            var commentsText = db.WS_COMMENTS
                    .Where(a => a.COMMENTS_ID == commentsId)
                    .Select(item => item.WORKSPACE_INFO)
                    .FirstOrDefault();
            if (commentsText != null)
            {
                return Json(new { success = true, commentsText }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "." }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult GetLastActionsText(int lastActionsId, int id)
        {
            // Get the text based on the ID
            var lastActionsText = db.WS_LAST_ACTIONS
                    .Where(a => a.LAST_ACTIONS_ID == lastActionsId)
                    .Select(item => item.LAST_ACTIONS_INFO)
                    .FirstOrDefault();
            if (lastActionsText != null)
            {
                return Json(new { success = true, lastActionsText }, JsonRequestBehavior.AllowGet);
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